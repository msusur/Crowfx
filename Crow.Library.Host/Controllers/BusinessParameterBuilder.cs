using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using Crow.Library.Host.Helpers;
using Newtonsoft.Json;

namespace Crow.Library.Host.Controllers
{
    internal static class BusinessParameterBuilder
    {
        internal static RequestParameters CreateParameters(BusinessControllerBase controller, HttpRequestMessage request)
        {
            controller.ThrowIfNull("controller");
            request.ThrowIfNull("request");

            Dictionary<string, object> postParameters = HttpMessageReader.ReadResponseAs<Dictionary<string, object>>(request);

            RequestParameters parameters = new RequestParameters();
            MethodInfo method = controller.GetControllerMethod(controller.Url.BusinessAction);
            ParameterInfo[] methodParams = method.GetParameters();
            for (int i = 0; i < methodParams.Length; i++)
            {
                object parameterValue;
                if (!controller.Url.Parameters.ContainsKey(methodParams[i].Name))
                {
                    if (postParameters.ContainsKey(methodParams[i].Name))
                    {
                        parameterValue = JsonConvert.DeserializeObject(postParameters[methodParams[i].Name].ToString(), methodParams[i].ParameterType);
                        //parameterValue = Convert.ChangeType(, );
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {

                    parameterValue = Convert.ChangeType(controller.Url.Parameters[methodParams[i].Name], methodParams[i].ParameterType);
                }
                parameters.AddParameter(methodParams[i].Name, parameterValue);
            }
            return parameters;
        }
    }
}
