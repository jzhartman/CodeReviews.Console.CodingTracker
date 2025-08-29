using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Data.TypeHandlers
{
    public class DateTimeHandler : SqlMapper.TypeHandler<DateTime>
    {
        private readonly string _format;

        public DateTimeHandler(string format)
        {
            _format = format;
        }

        public override DateTime Parse(object value)
        {
            if (value is string s
                && DateTime.TryParseExact
                    (
                        s,
                        _format,
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.AssumeLocal,
                        out var dt))
            {
                return dt;
            }

            return Convert.ToDateTime(value);
        }

        public override void SetValue(IDbDataParameter parameter, DateTime value)
        {
            parameter.DbType = DbType.String;
            parameter.Value = value.ToString(_format);
        }
    }
}
