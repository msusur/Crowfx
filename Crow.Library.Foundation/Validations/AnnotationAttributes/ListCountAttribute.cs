using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace System.ComponentModel.DataAnnotations
{
    public class ListCountAttribute : ValidationAttribute
    {
        public int ListMinimumCount { get; set; }

        public ListCountAttribute(int listMinimumCount, string message)
            : base(message)
        {
            this.ListMinimumCount = listMinimumCount;
        }

        public override bool IsValid(object value)
        {
            if (value is IList)
            {
                return (value as IList).Count >= ListMinimumCount;
            }
            return false;
        }
    }
}