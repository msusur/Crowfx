using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.CodeExecutionFlow.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Crow.Library.Common;
using Crow.Library.Foundation.Validations;
using Castle.DynamicProxy;

namespace Crow.Library.CodeExecutionFlow.Steps
{
    internal sealed class ValidationStep : MachineWorkStep
    {
        public override Type RequiresAttributeType
        {
            get { return typeof(ValidateAttribute); }
        }

        public ValidationStep(string stepName = "Validation Step")
            : base(stepName)
        {

        }

        protected override void OnStepExecuted(IInvocation invocation, IEnumerable<AspectAttributeBase> attribute)
        {
            //List<ValidationResult> results = new List<ValidationResult>();

            //for (int i = 0; i < invocation.Arguments.Length; i++)
            //{
            //    ValidationContext context = new ValidationContext(invocation.Arguments[0], null, null);
            //    bool validated = Validator.TryValidateObject(invocation.Arguments[i], context, results);
            //    if (!validated)
            //    {
            //        string errorMessages = ValidationMessageBuilder.BuildErrorMessages(results);
            //        throw new ValidationException(errorMessages);
            //    }
            //}
        }
    }
}