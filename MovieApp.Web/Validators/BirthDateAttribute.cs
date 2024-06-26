using System;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Web.Validators
{
    public class BirthDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime datetime = Convert.ToDateTime(value);
            return datetime < DateTime.Now;
        }
    }
}
