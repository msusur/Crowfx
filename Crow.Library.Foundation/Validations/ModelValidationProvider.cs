using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Crow.Library.Foundation.Validations
{
    public static class DataErrorProvider
    {
        /// <summary>
        /// Validates object for the given property.
        /// </summary>
        /// <returns></returns>
        public static string GetError(IDataErrorInfo model, string columnName)
        {
            ValidationContext context = new ValidationContext(model, null, null) { MemberName = columnName };
            object val = model.GetType().GetProperty(columnName).GetValue(model, null);
            List<ValidationResult> results = new List<ValidationResult>();
            if (!Validator.TryValidateProperty(val, context, results))
            {
                string v = string.Empty;
                results.ForEach((r) => v += r.ErrorMessage);
                return v;
            }
            return string.Empty;
        }
        
        /// <summary>
        /// Validates object for all properties.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetError(IDataErrorInfo model)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(model, null, null);
            if (!Validator.TryValidateObject(model, context, results))
            {
                string v = string.Empty;
                results.ForEach((r) => v += r.ErrorMessage);
                return v;
            }
            return string.Empty;
        }
    }
}
