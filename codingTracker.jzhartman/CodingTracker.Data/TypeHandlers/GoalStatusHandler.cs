using CodingTracker.Models.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Data.TypeHandlers;
public class GoalStatusHandler : SqlMapper.TypeHandler<GoalStatus>
{
    public override GoalStatus Parse(object value)
    {
        if (value == null || value is DBNull)
        {
            return default(GoalStatus);
        }

        return (GoalStatus)Enum.Parse(typeof(GoalStatus), value.ToString());
    }

    public override void SetValue(IDbDataParameter parameter, GoalStatus value)
    {
        parameter.DbType = DbType.String;
        parameter.Value = value.ToString();
    }
}
