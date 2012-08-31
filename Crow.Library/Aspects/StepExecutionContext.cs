using System.Collections.Generic;
using Castle.DynamicProxy;

namespace Crow.Library.CodeExecutionFlow
{
    public class StepExecutionContext
    {
        private SortedList<string, object> m_InnerList;

        internal IInvocation Invocation { get; set; }
        internal SortedList<string, object> InnerList
        {
            get
            {
                m_InnerList = m_InnerList ?? new SortedList<string, object>();
                return m_InnerList;
            }
        }

        internal StepExecutionContext()
        {
        }

        public TArgumentType GetByName<TArgumentType>(string argumentName)
        {
            return GetByName<TArgumentType>(argumentName, default(TArgumentType));
        }
        public TArgumentType GetByName<TArgumentType>(string argumentName, TArgumentType defaultValue)
        {
            if (this.InnerList.ContainsKey(argumentName))
            {
                return (TArgumentType)this.InnerList[argumentName];
                //ConversionHelper.ConvertTo<TArgumentType>(this.InnerList[argumentName]);
            }
            return defaultValue;
        }

        public void AddArgument(string key, object argumentValue)
        {
            if (this.InnerList.ContainsKey(key))
            {
                this.InnerList[key] = argumentValue;
            }
            else
            {
                this.InnerList.Add(key, argumentValue);
            }
        }
    }
}