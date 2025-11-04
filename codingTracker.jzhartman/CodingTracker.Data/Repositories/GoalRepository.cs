using CodingTracker.Data.Interfaces;
using CodingTracker.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Data.Repositories;
public class GoalRepository : RepositoryGenerics, IGoalRepository
{
    public GoalRepository(ISqliteConnectionFactory connectionFactory) : base(connectionFactory) { }

    // TODO: Setup the Goals repository
    // TODO: Create Goal object with relevant members
    // TODO: Create Goal input
    // TODO: Create Goal output to console
    // TODO: Determine how goal status will be determined

    public List<GoalDTO> GetAllGoalsByStatus(GoalStatus status)
    {
        string sql = $"select * from Goals where Status = {status}";
        return LoadData<GoalDTO>(sql);
    }

    public void AddGoal(GoalModel goal)
    {
        string sql = "insert into Goals (StartTime, EndTime, Type, Status) values (@StartTime, @EndTime, @Type, @Status)";
        SaveData(sql, goal);
    }

    public void UpdateGoal(GoalDTO goal)
    {
        string sql = "update Goals set StartTime = @StartTime, EndTime = @EndTime, Type = @Type, Status = @Status where Id = @Id";
        SaveData(sql, goal);
    }

}
