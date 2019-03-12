using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exercises.Attributes
{
    public class ValidGPAAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is decimal)
            {
                decimal checkNum = (decimal)value;
                if (checkNum > 4 || checkNum < 0)
                    return false;
                else
                    return true;
            }

            return false;
        }
    }
}