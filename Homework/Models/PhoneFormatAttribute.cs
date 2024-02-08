using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Homework.Models
{
    public class PhoneFormatAttribute : DataTypeAttribute
    {
        public PhoneFormatAttribute() : base(DataType.Text)
        {
            ErrorMessage = "The phone format should same as xxxx-xxxxxx";
        }
        public override bool IsValid(object value)
        {
            var regex = @"\d{4}-\d{6}";
            if(value == null)
            {
                return false;
            }
            var match = Regex.Match(value.ToString(), regex, RegexOptions.IgnoreCase);
            
            if (!match.Success)
            {
                return false;
            }
            return base.IsValid(value);
        }
    }
}