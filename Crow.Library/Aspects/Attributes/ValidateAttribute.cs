using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.Aspects.Attributes
{
    public sealed class ValidateAttribute : AspectAttributeBase
    {
        //bu kısım ileride düzenlenebilir.
        //public Type CustomValidator { get; private set; }
        //public ValidatorAttribute(Type customValidator)
        //{
        //    this.CustomValidator = customValidator;
        //}
        //public ValidatorAttribute()
        //    : this(typeof(GrailModelValidator))
        //{
        //}

        public ValidateAttribute()
            : base()
        {

        }
    }
}