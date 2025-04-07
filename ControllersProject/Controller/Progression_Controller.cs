using ControllersProject.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersProject.Controller
{
    public class Progression_Controller
    {
        Modal_Progression progression_Model = new Modal_Progression();
        Plan_Controller plan_controller = new Plan_Controller();
        User_Requests_Controller user_request_controller = new User_Requests_Controller();
        FeedbackAI_Controller feedbackAI_Controller = new FeedbackAI_Controller();
        public double GetProgression(int planId, int period, int exerciseId)
        {
            return progression_Model.GetProgression(planId, period, exerciseId);
        }
        public double GetGradeOfProgression(int planId, int period, int exerciseId)
        {
            double m = GetProgression(planId, period, exerciseId);
            if (m == 0)
                if (!plan_controller.UserHasLogged(planId, exerciseId))
                    return -1;
                else
                    return 0;
            int userId = plan_controller.GetUserIdByPlan(planId);
            int experience = user_request_controller.GetExperience(userId);

            double x = period != 0 ? ((double)period / experience) * m : (1 / (double)experience) * m;
            if (x < 0)
                return -1;
            const double t0 = 6;
            int L = 10;
            double growthFunction = L / (1 + Math.Pow(Math.E, -0.5 * (x - t0)));
            return growthFunction;
            // f(x) = L / (1+e^(-k(x-t0)))
            // Where
            // L is the limit of f(x)
            // t0 - halfway point to the limit of f(x)
            // x - user growth
            // k - function rate of growth
        }
        public string TestGradeOfProgression(int planId, int period, int exerciseId)
        {
            double score = GetGradeOfProgression(planId, period, exerciseId);
            if (score == -1)
                return score + " Bad progression";
            if (score == 0)
                return score + " start logging your workouts!";
            if (score > 0.6)
                return score + " really good progression";
            if (score < 0.01)
                return score + " Ok progression";
            return score + "fine progression";
        }

        public string GetFeedbackForExercise(int userId, int planId, int period, int exerciseId)
        {
            // Get the progression score for the exercise
            double score = GetGradeOfProgression(planId, period, exerciseId);
            string feedback = "";

            // Case 1: Bad progression (-1)
            if (score == -1)
            {
                // Get the next best exercise
                Exercise_Controller ce = new Exercise_Controller();
                Exercise nextExercise = ce.GetNextBestExercise(userId, exerciseId);

                if (nextExercise != null)
                {
                    feedback = $@"
                <div>
                    <p>Your progression is not suitable for this exercise. Consider switching to a different exercise.</p>
                    <p>Would you like to switch to <b>{nextExercise.Name}</b>?</p>
                    <button onclick='switchExercise({planId},{exerciseId},{nextExercise.Id})'>Yes</button>
                            <button onclick ='closeFeedbackModal()'>No</button>
                        </div> ";
                }
                else
                {
                    feedback = "<p>Your progression is not suitable for this exercise, but no alternative exercise was found.</p>";
                }
            }
            // Case 2: Start logging workouts (0)
            else if (score == 0)
            {
                feedback = "<p>Your progression is not measurable. Start logging your workouts to track your progress effectively!</p>";
            }
            // Case 3: Really good progression (> 0.6)
            else if (score > 0.6)
            {
                feedback = "Write something along those lines inside a <p> tag. You are doing really well! Keep up the great work and continue with this exercise! ";
                feedback =  feedbackAI_Controller.CallCohereApiAsync(feedback).GetAwaiter().GetResult();
            }
            // Case 4: Fine progression (other cases)
            else if (score > 0.01)
            {
                feedback = "<p>Your progress is fine, but you could improve by adjusting the number of reps per session.</p>";
            }
            // Case 5: Ok progression (< 0.01)
            else
            {
                feedback = "<p>Don't give up! Keep going; progression might be just around the corner!</p>";
            }

            return feedback;
        }
        public string GetFeedbackForEntirePlan(int planId)
        {
            string feedback = "";
            Plan_Controller pc = new Plan_Controller();

            // Get the user ID associated with the plan
            int userId = pc.GetUserIdByPlan(planId);
            if (plan_controller.GetAmoutOflogged(planId) < 30)
            {
                feedback = $@"
<div>
    <p>We noticed that you haven't logged enough workout sessions for us to provide a detailed review of your progress. 
    Consistency is key when it comes to achieving fitness goals, and maintaining a regular workout routine will not only help you see better results 
    but also give us more data to analyze and tailor feedback to your needs.</p>
    <p>Right now, with less than 30 logged sessions in your current plan, it's challenging for us to assess your performance trends, 
    volume progression, and other metrics that contribute to effective training. To make the most out of your fitness journey, 
    we encourage you to engage with your plan more frequently and log each session consistently.</p>
    <p>Remember, progress doesn’t have to be perfect—it just needs to be consistent. 
    Whether you're hitting the gym, working out at home, or following a custom routine, every logged session counts towards your goals!</p>
    <p>Keep pushing forward, and as you log more workouts, we’ll be able to provide you with deeper insights, tailored recommendations, and actionable feedback to enhance your training experience.</p>
    <a href='Training_Log.aspx' class='btn btn-primary'>Log a Workout</a>
</div>";
            }

            // Initialize score variables
            double totalScore = 0;
            int exerciseCount = 0;

            // Retrieve the user's current plan details
            LinkedList<Workout> plan = pc.GetPlan(userId);

            // Calculate the total and average progression score for all exercises in the plan
            foreach (Workout workout in plan)
            {
                foreach (Exercise exc in workout.ExerciseList)
                {
                    totalScore += GetGradeOfProgression(planId, 1, exc.Id);
                    exerciseCount++;
                }
            }

            double averageScore = exerciseCount > 0 ? totalScore / exerciseCount : 0;

            // Generate feedback based on the average score
            if (averageScore == -1)
            {
                feedback = $@"
        <div>
            <p>We’ve analyzed your progression across all exercises in your current workout plan, 
            and it seems that the plan might not be the most suitable fit for your current fitness level or goals. 
            This could be due to a mismatch in exercise difficulty, lack of variety, or insufficient focus on your target areas.</p>
            <p>It's completely normal for a workout plan to require adjustments as you progress. 
            Sometimes, trying a new plan can help you stay motivated and ensure you're working out in a way that supports your long-term growth. 
            We recommend exploring different options tailored to your preferences and goals.</p>
            <p>If you'd like to create a new plan, visit the <b>Training Plans</b> page where you can select a fresh set of exercises, 
            adjust your preferences, and build a plan that aligns better with your needs.</p>
            <a href='https://localhost:44345/View/Training_Plans.aspx' class='btn btn-primary'>Create a New Plan</a>
        </div>";
            }
            else if (averageScore == 0)
            {
                feedback = $@"
        <div>
            <p>Your progression for this plan is currently not measurable. This often happens when workouts aren’t logged regularly 
            or the plan is too new for patterns to emerge. Tracking your workouts is a crucial step in understanding your progress, 
            identifying areas of improvement, and celebrating milestones along the way.</p>
            <p>We strongly encourage you to start logging your workouts consistently. By doing this, you’ll gain valuable insights 
            into how you’re performing over time, which exercises are most effective for you, and where adjustments might be needed.</p>
            <p>Remember, progress is about small, consistent steps forward. Once you start tracking your workouts, 
            you’ll have a clearer picture of your fitness journey and be able to make informed decisions about your training.</p>
        </div>";
            }
            else if (averageScore > 0.6)
            {
                feedback = $@"
        <div>
            <p>Congratulations! Your overall progression across the current plan is outstanding. 
            It’s clear that you’ve been putting in the effort and following the plan consistently, 
            and your results are a testament to your hard work and dedication.</p>
            <p>Keep up the fantastic work and continue with this plan as long as it challenges you and aligns with your goals. 
            If you ever feel that the exercises are becoming too easy or repetitive, it might be time to increase the intensity, 
            add more variety, or adjust the difficulty to ensure you’re constantly improving.</p>
            <p>Remember to celebrate your achievements along the way and stay motivated. 
            Progress isn’t just about reaching the destination; it’s about enjoying the journey.</p>
        </div>";
            }
            else if (averageScore > 0.01)
            {
                feedback = $@"
        <div>
            <p>Your progress is moving in the right direction, and it’s great to see you staying consistent with your workouts. 
            While you’re making steady improvements, there’s still room to fine-tune your routine to achieve even better results.</p>
            <p>Consider focusing on aspects like:
            <ul>
                <li>Increasing the number of reps or sets for key exercises.</li>
                <li>Adding a slight increase in weight or resistance for strength-building exercises.</li>
                <li>Improving your form to maximize the effectiveness of each movement.</li>
            </ul></p>
            <p>Small adjustments can make a big difference in the long run. Keep going and stay committed to your plan; 
            you’re doing well and are on the right path!</p>
        </div>";
            }
            else
            {
                feedback = $@"
        <div>
            <p>Your progression is slightly below average for this plan, but don’t let that discourage you. 
            Fitness journeys are not linear, and everyone experiences ups and downs along the way.</p>
            <p>This could be a sign that your current plan might not be the best fit for your goals or needs at this time. 
            Take a moment to reflect on whether the plan feels challenging and motivating enough for you.</p>
            <p>If you’re feeling stuck, consider exploring a different approach by creating a new plan that incorporates 
            exercises you enjoy and aligns with your current fitness level. Visit the <b>Training Plans</b> page to get started:</p>
            <a href='Training_Plans.aspx' class='btn btn-primary'>Explore New Plans</a>
        </div>";
            }

            return feedback;
        }

        public async Task<string> GetFeedbackForPlanAsync(int planId)
        {
            FeedbackAI_Controller feedbackController = new FeedbackAI_Controller();
            return await feedbackController.GenerateFeedbackForPlanAsync(planId);
        }

    }
}
