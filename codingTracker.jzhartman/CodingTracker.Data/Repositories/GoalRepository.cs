using CodingTracker.Data.Interfaces;
using CodingTracker.Data.Parameters;
using CodingTracker.Models.Entities;

namespace CodingTracker.Data.Repositories;
public class GoalRepository : RepositoryGenerics, IGoalRepository
{
    public GoalRepository(ISqliteConnectionFactory connectionFactory) : base(connectionFactory) { }

    public List<GoalDTO> GetAllGoalsByStatus(GoalStatus status)
    {
        var goalStatus = new GoalStatusQuery {Status = status };
        string sql = $"select * from Goals where Status = @Status";
        return LoadData<GoalDTO, GoalStatusQuery>(sql, goalStatus);
    }

    public List<GoalDTO> GetAllGoals()
    {
        string sql = $"select * from Goals";
        return LoadData<GoalDTO>(sql);
    }

    public void AddGoal(GoalModel goal)
    {
        string sql = "insert into Goals (StartTime, EndTime, Type, Status, GoalValue, CurrentValue, Progress) values (@StartTime, @EndTime, @Type, @Status, @GoalValue, @CurrentValue, @Progress)";
        SaveData(sql, goal);
    }

    public void UpdateGoal(GoalDTO goal)
    {
        string sql = "update Goals set StartTime = @StartTime, EndTime = @EndTime, Type = @Type, Status = @Status where Id = @Id";
        SaveData(sql, goal);
    }
    public void DeleteById(int id)
    {
        string sql = $"delete from Goals where Id = {id}";
        SaveData(sql);
    }

    public void EvaluateGoal(GoalDTO goal)
    {
        string sql = "update Goals set Status = @Status, GoalValue = @GoalValue, CurrentValue = @CurrentValue, Progress = @Progress where Id = @Id";
        SaveData(sql, goal);
    }

    public int GetGoalCount()
    {
        string sql = "select count(*) from Goals";
        return LoadData<int>(sql).First();
    }

}
