using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Models.Validation
{
    public class ValidationResult<T>
    {
        public bool IsValid { get;}
        public T? Value { get;}
        public string? Parameter { get;}
        public string? Message { get;}

        private ValidationResult(bool isValid, T? value,string parameter, string message)
        {
            IsValid = isValid;
            Value = value;
            Parameter = parameter;
            Message = message;
        }

        public static ValidationResult<T> Success(T value)
        {
            return new ValidationResult<T>(true, value, default, default);
        }

        public static ValidationResult<T> Fail(string parameter, string message)
        {
            return new ValidationResult<T>(false, default, parameter, message);
        }

    }
}
