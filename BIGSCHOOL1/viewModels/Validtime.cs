using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BIGSCHOOL1.viewModels
{
    public class Validtime: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            var isValid = DateTime.TryParseExact(Convert.ToString(value), "H:mm",
               CultureInfo.CurrentCulture,
               DateTimeStyles.None,
               out dateTime);
            return isValid;
        }
    }
}