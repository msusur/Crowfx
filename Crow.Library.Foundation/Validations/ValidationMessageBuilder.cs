using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Crow.Library.Foundation.Validations
{
    internal static class ValidationMessageBuilder
    {
        internal static string BuildErrorMessages(List<ValidationResult> results)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var result in results)
            {
                builder.AppendLine(result.ErrorMessage);
            }
            return builder.ToString();
        }
    }
}