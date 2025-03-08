

// ControllersProject, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// ControllersProject.Controller.Plan_Controller
using System;
using System.Collections.Generic;
using System.Linq;
using ControllersProject.Modal;
namespace ControllersProject.Controller
{ 
public class Plan_Controller
{
    private Modal_Plan m_plan = new Modal_Plan();

    private LinkedList<Workout> plan = new LinkedList<Workout>();

    private int daysAWEEK;

    private int typeOfTraining;

    private int experience;

    private int duration;

    private Random rnd = new Random();

    private Exercise_Controller controller_Exercise = new Exercise_Controller();

    private Vector userVector = new Vector(0.0, 0.0, 0.0);

    private int userAge = 0;

    private Users_Controller cu = new Users_Controller();

    public bool UserFiledARequest(int user_id)
    {
        return m_plan.UserFiledARequest(user_id);
    }

    public bool DeletePlan(int user_id)
    {
        return m_plan.DeletePlan(user_id);
    }

    public int GetUserAge(int user_id)
    {
        return cu.GetAge(cu.GetUsernameFromId(user_id));
    }

    public int GetPlanIdByUser(int user_id)
    {
        return m_plan.GetPlanIdByUser(user_id);
    }

    public int GetUserIdByPlan(int planId)
    {
        return m_plan.GetUserIdByPlan(planId);
    }

    public bool UserHasLogged(int planId, int exerciseId)
    {
        return m_plan.UserHasLogged(planId, exerciseId);
    }

    public string GetPlanCreationDate(int planId)
    {
        return m_plan.GetPlanCreationDate(planId);
    }

    public int GetAmoutOflogged(int planId)
    {
        return m_plan.GetAmoutOflogged(planId);
    }

    public LinkedList<Workout> CreateAPlan(int userId)
    {
        SetData(userId);
        return CreatePPLPlan();
    }

    public void SetData(int userId)
    {
        daysAWEEK = m_plan.GetDaysAweek(userId);
        experience = m_plan.GetExeprience(userId);
        typeOfTraining = m_plan.GetTypeOfTraining(userId);
        duration = m_plan.GetDuration(userId);
        userAge = cu.GetAge(cu.GetUsernameFromId(userId));
        userVector = m_plan.GetUserVector(userId, userAge);
    }

    public Workout GenerateWorkoutByName(string workoutName)
    {
        if (workoutName.Contains("Push"))
        {
            return CreatePushWorkout(duration);
        }
        if (workoutName.Contains("Legs"))
        {
            return CreateLegWorkout(duration);
        }
        return CreatePullWorkout(duration);
    }

    public Workout CreateLegWorkout(int duration)
    {
        return duration switch
        {
            1 => VectorLegWorkout(1, rnd.Next(1, 2)),
            2 => VectorLegWorkout(rnd.Next(1, 3), rnd.Next(2, 5)),
            _ => VectorLegWorkout(rnd.Next(2, 3), rnd.Next(2, 5)),
        };
    }

    public Workout CreatePushWorkout(int duration)
    {
        return duration switch
        {
            1 => VectorExercisePlanPush(1, rnd.Next(1, 3)),
            2 => VectorExercisePlanPush(1, rnd.Next(2, 5)),
            _ => VectorExercisePlanPush(rnd.Next(2, 4), rnd.Next(2, 5)),
        };
    }

    public Workout CreatePullWorkout(int duration)
    {
        return duration switch
        {
            1 => VectorExercisePlanPull(1, rnd.Next(1, 3)),
            2 => VectorExercisePlanPull(rnd.Next(1, 3), rnd.Next(2, 5)),
            _ => VectorExercisePlanPull(rnd.Next(2, 4), rnd.Next(2, 5)),
        };
    }

    public Workout CreateUpperWorkout(int duration)
    {
        return duration switch
        {
            1 => CreateUpperWorkout(1, rnd.Next(1, 3)),
            2 => CreateUpperWorkout(rnd.Next(1, 3), rnd.Next(2, 5)),
            _ => CreateUpperWorkout(rnd.Next(2, 4), rnd.Next(2, 5)),
        };
    }

    public LinkedList<Workout> CreateUpperLowerPlan()
    {
        LinkedList<Workout> linkedList = new LinkedList<Workout>();
        Workout workout = new Workout("");
        Workout workout2 = new Workout("");
        workout = CreateLegWorkout(duration);
        workout2 = CreateUpperWorkout(duration);
        linkedList.AddLast(workout);
        linkedList.AddLast(workout2);
        return linkedList;
    }

    public LinkedList<Workout> CreatePPLPlan()
    {
        LinkedList<Workout> linkedList = new LinkedList<Workout>();
        Workout workout = new Workout("");
        Workout workout2 = new Workout("");
        Workout workout3 = new Workout("");
        workout = CreateLegWorkout(duration);
        workout2 = CreatePushWorkout(duration);
        workout3 = CreatePullWorkout(duration);
        linkedList.AddLast(workout);
        linkedList.AddLast(workout2);
        linkedList.AddLast(workout3);
        return linkedList;
    }

    public Workout VectorLegWorkout(int heavy, int light)
    {
        Workout workout = new Workout("Legs - Quadriceps, Hamstrings and Calves");
        LinkedList<Exercise> exerciseList = workout.ExerciseList;
        LinkedList<Exercise> linkedList = controller_Exercise.AllExercises("quadriceps", userVector);
        LinkedList<Exercise> linkedList2 = controller_Exercise.AllExercises("hamstrings", userVector);
        LinkedList<Exercise> linkedList3 = controller_Exercise.AllExercises("calves", userVector);
        exerciseList.AddLast(linkedList.First());
        linkedList.Remove(linkedList.First());
        heavy--;
        if (heavy == 1)
        {
            exerciseList.AddLast(linkedList2.First());
            linkedList2.Remove(linkedList2.First());
        }
        for (int i = 0; i < light && i < 2; i++)
        {
            LinkedList<Exercise> linkedList4 = ((i % 2 == 0) ? linkedList : linkedList2);
            exerciseList.AddLast(linkedList4.First());
            linkedList4.Remove(linkedList4.First());
            light--;
        }
        while (light != 0)
        {
            LinkedList<Exercise> linkedList4 = linkedList3;
            exerciseList.AddLast(linkedList4.First());
            linkedList4.Remove(linkedList4.First());
            light--;
        }
        workout.ExerciseList = exerciseList;
        return workout;
    }

    public Workout VectorExercisePlanPush(int heavy, int light)
    {
        Workout workout = new Workout("Push - Chest, Shoulders and Triceps");
        LinkedList<Exercise> exerciseList = workout.ExerciseList;
        LinkedList<Exercise> linkedList = controller_Exercise.AllExercises("chest", userVector);
        LinkedList<Exercise> linkedList2 = controller_Exercise.AllExercises("delts", userVector);
        LinkedList<Exercise> linkedList3 = controller_Exercise.AllExercises("triceps", userVector);
        exerciseList.AddLast(linkedList.First());
        linkedList.Remove(linkedList.First());
        heavy--;
        if (heavy == 1)
        {
            exerciseList.AddLast(linkedList2.First());
            linkedList2.Remove(linkedList2.First());
        }
        for (int i = 0; i < light && i < 2; i++)
        {
            LinkedList<Exercise> linkedList4 = ((i % 2 != 0) ? linkedList2 : linkedList);
            exerciseList.AddLast(linkedList4.First());
            linkedList4.Remove(linkedList4.First());
            light--;
        }
        while (light != 0)
        {
            LinkedList<Exercise> linkedList4 = linkedList3;
            exerciseList.AddLast(linkedList4.First());
            linkedList4.Remove(linkedList4.First());
            light--;
        }
        workout.ExerciseList = exerciseList;
        return workout;
    }

    public Workout VectorExercisePlanPull(int heavy, int light)
    {
        Workout workout = new Workout("Pull - Lats and Middle back");
        LinkedList<Exercise> exerciseList = workout.ExerciseList;
        LinkedList<Exercise> linkedList = controller_Exercise.AllExercises("lats", userVector);
        LinkedList<Exercise> linkedList2 = controller_Exercise.AllExercises("middle_back", userVector);
        LinkedList<Exercise> linkedList3 = controller_Exercise.AllExercises("biceps", userVector);
        exerciseList.AddLast(linkedList.First());
        linkedList.Remove(linkedList.First());
        exerciseList.AddLast(linkedList2.First());
        linkedList2.Remove(linkedList2.First());
        for (int i = 0; i < light && i < 2; i++)
        {
            LinkedList<Exercise> linkedList4 = ((i % 2 != 0) ? linkedList : linkedList2);
            exerciseList.AddLast(linkedList4.First());
            linkedList4.Remove(linkedList4.First());
            light--;
        }
        while (light != 0)
        {
            LinkedList<Exercise> linkedList4 = linkedList3;
            exerciseList.AddLast(linkedList4.First());
            linkedList4.Remove(linkedList4.First());
            light--;
        }
        workout.ExerciseList = exerciseList;
        return workout;
    }

    public Workout CreateUpperWorkout(int heavy, int ligt)
    {
        return new Workout("Upper - Chest, Back and Arms");
    }

    public void InsertPlan(LinkedList<Workout> plan, int userId)
    {
        m_plan.InsertPlan(plan, userId);
    }

    public bool HasAPlan(int userId)
    {
        return m_plan.HasAPlan(userId);
    }

    public LinkedList<Workout> GetPlan(int userId)
    {
        return m_plan.GetPlan(userId);
    }

    public void UpdatePlan(int planId, int exerciseId, int newExerciseId)
    {
        m_plan.UpdatePlan(planId, exerciseId, newExerciseId);
    }
}

}
