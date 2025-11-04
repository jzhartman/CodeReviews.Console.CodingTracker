using CodingTracker.Models.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Data.TypeHandlers;
public class GoalTypeHandler : SqlMapper.TypeHandler<GoalType>
{
    public override GoalType Parse(object value)
    {
        if (value == null || value is DBNull)
        {
            return default(GoalType);
        }

        return (GoalType)Enum.Parse(typeof(GoalType), value.ToString());
    }

    public override void SetValue(IDbDataParameter parameter, GoalType value)
    {
        parameter.DbType = DbType.String;
        parameter.Value = value.ToString();
    }
}
