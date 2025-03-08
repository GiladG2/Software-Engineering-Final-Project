
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ControllersProject.Controller;
using ControllersProject.Modal;
using Newtonsoft.Json;

namespace ControllersProject.Controller
{
public class FeedbackAI_Controller
{
    private const string ApiUrl = "https://api.cohere.ai/v1/generate";

    private const string ApiKey = "HP23zMHlUQ1DCnrd78BwivgZCVoA8q1GYOajSqUO";

    private Plan_Controller _planController = new Plan_Controller();

    public async Task<string> GenerateFeedbackForPlanAsync(int planId)
    {
        try
        {
            int userId = _planController.GetUserIdByPlan(planId);
            bool hasLoggedEnough = _planController.GetAmoutOflogged(planId) >= 30;
            (double, int) planProgressionScores = GetPlanProgressionScores(planId, userId);
            double totalScore = planProgressionScores.Item1;
            int exerciseCount = planProgressionScores.Item2;
            double averageScore = ((exerciseCount > 0) ? (totalScore / (double)exerciseCount) : 0.0);
            string prompt = GeneratePrompt(averageScore, hasLoggedEnough);
            return await CallCohereApiAsync(prompt);
        }
        catch (Exception ex)
        {
            Exception ex2 = ex;
            return "<div><p>Error generating feedback: " + ex2.Message + "</p></div>";
        }
    }

    private (double totalScore, int exerciseCount) GetPlanProgressionScores(int planId, int userId)
    {
        double num = 0.0;
        int num2 = 0;
        LinkedList<Workout> plan = _planController.GetPlan(userId);
        foreach (Workout item in plan)
        {
            foreach (Exercise exercise in item.ExerciseList)
            {
                Progression_Controller progression_Controller = new Progression_Controller();
                num += progression_Controller.GetGradeOfProgression(planId, 1, exercise.Id);
                num2++;
            }
        }
        return (totalScore: num, exerciseCount: num2);
    }

    private string GeneratePrompt(double averageScore, bool hasLoggedEnough)
    {
        if (!hasLoggedEnough)
        {
            return "Write a long and positive, motivational feedback encouraging the user to log at least 30 workouts. Suggest they track their workouts to see improvement. Include an HTML button With a bit of style in it linking to 'https://localhost:44345/View/Training_Plans.aspx'.";
        }
        if (averageScore == -1.0)
        {
            return "Write a long feedback suggesting the current plan is not suitable for the user. Encourage them to try a different plan. Include a button linking to 'Training_Plans.aspx'.";
        }
        if (averageScore == 0.0)
        {
            return "Write a long and encouraging feedback for someone just starting. Motivate the user to keep up the consistency and track their progress. Include a link to Training_Log.aspx for logging workouts.";
        }
        if (averageScore > 0.6)
        {
            return "Write a long and congratulatory feedback for the user showing excellent progress. Encourage them to keep up the great work and continue to challenge themselves! Include positive reinforcement and a link to Training_Log.aspx.";
        }
        if (averageScore > 0.01)
        {
            return "Write a long and encouraging feedback for steady, but improvable progress. Suggest some small adjustments, like increasing reps or improving form, and motivate the user to stay on track. Include a link to Training_Log.aspx.";
        }
        return "Write a long and motivational feedback for below-average progress. Encourage the user to stick with it and try a new plan if necessary. Include a button linking to 'Training_Plans.aspx'.";
    }

    private async Task<string> CallCohereApiAsync(string prompt)
    {
        using HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", "Bearer HP23zMHlUQ1DCnrd78BwivgZCVoA8q1GYOajSqUO");
        var requestBody = new
        {
            prompt = prompt,
            max_tokens = 1000,
            temperature = 0.7
        };
        StringContent content = new StringContent(JsonConvert.SerializeObject((object)requestBody), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("https://api.cohere.ai/v1/generate", content);
        if (response.IsSuccessStatusCode)
        {
            dynamic jsonResponse = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
            return jsonResponse.generations[0].text;
        }
        return "<div><p>Unable to generate feedback at the moment. Please try again later.</p></div>";
    }
}

}
