using ConsoleApplication1.Attributes;
using Crow.Library.Aspects.Attributes;
using System;
using Crow.Library.Foundation.Common.Aspects;

namespace ConsoleApplication1.Businesses
{
    //[Url(Name = "Data", ActionType=Http.Post)]
    public interface IDataBusiness
    {
        [Async]
        //[Url(ActionName="GetData", ActionType=Http.Get)]
        Data GetDataById(int id);
    }
}