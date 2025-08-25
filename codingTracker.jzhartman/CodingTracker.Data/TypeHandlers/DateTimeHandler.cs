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
            return DateTime.ParseExact(value.ToString(), _format, null);
        }

        public override void SetValue(IDbDataParameter parameter, DateTime value)
        {
            parameter.Value = value.ToString(_format);
        }
    }
}
