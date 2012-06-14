using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Validation;

namespace BizConnect.Services
{
    public class ValidateException : Exception
    {
        public IEnumerable<DbEntityValidationResult> ValidationErrors { get; private set; }

        public ValidateException(IEnumerable<DbEntityValidationResult> validatationErrors)
        {
            ValidationErrors = validatationErrors;
        }
    }
}
