using System.Diagnostics;

using Unity.Interception.PolicyInjection.Pipeline;

namespace PrismMetroSample.Infrastructure.Interceptor.Handlers
{
    public class LogHandler : ICallHandler
    {
        public int Order { get; set; }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            Debug.WriteLine("-------------Method Excute Befored-------------");
            Debug.WriteLine($"Method Name:{input.MethodBase.Name}");
            if (input.Arguments.Count > 0)
            {
                Debug.WriteLine("Arguments:");
                for (int i = 0; i < input.Arguments.Count; i++)
                {
                    Debug.WriteLine($"parameterName:{input.Arguments.ParameterName(i)},parameterValue:{input.Arguments[i]}");
                }
            }
            IMethodReturn methodReturn = getNext()(input, getNext);
            Debug.WriteLine("-------------Method Excute After-------------");
            if (methodReturn.Exception != null)
            {
                Debug.WriteLine($"Exception:{methodReturn.Exception.Message} \n");
            }
            else
            {
                Debug.WriteLine($"Excuted Successed \n");
            }
            return methodReturn;
        }
    }
}
