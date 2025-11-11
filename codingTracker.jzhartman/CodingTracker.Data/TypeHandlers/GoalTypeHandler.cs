using CodingTracker.Models.Entities;
using Dapper;
using System.Data;

namespace CodingTracker.Data.TypeHandlers;
public class GoalTypeHandler : SqlMapper.TypeHandler<GoalType>
{
    public override GoalType Parse(object value)
    {
        if (value == null || value is DBNull)
        {
            return default(GoalType);
        }

        return (GoalType)Enum.Parse(typeof(GoalType), value.ToString(), true);
    }

    public override void SetValue(IDbDataParameter parameter, GoalType value)
    {
        parameter.Value = value.ToString();
        parameter.DbType = DbType.String;
    }
}
