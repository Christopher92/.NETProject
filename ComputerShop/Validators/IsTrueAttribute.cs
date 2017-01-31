using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Validators
{
    public class IsTrueAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            if (value.GetType() != typeof(bool)) throw new InvalidOperationException("Attribute can be used only on bool types.");

            return (bool)value == true;
        }
    }
}