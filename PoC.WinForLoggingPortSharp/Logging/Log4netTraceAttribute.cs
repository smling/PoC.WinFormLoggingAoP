using log4net;
using log4net.Repository.Hierarchy;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.WinForLoggingPortSharp.Logging
{
    [Serializable]
    public class Log4netTraceAttribute: OnMethodBoundaryAspect
    {
        public static ILog Logger = LogManager.GetLogger("PoC.Log4net");

        /// <summary>  
        /// On Method Entry  
        /// </summary>  
        /// <param name="args"></param>  
        public override void OnEntry(MethodExecutionArgs args)
        {
            Logger.Info($"OnEntry : {(args.Method != null ? args.Method.Name : "")}");

            string className=String.Empty, methodName=String.Empty, arguments=String.Empty;
            if (args.Method != null)
            {
                methodName = args.Method.Name;
                if (args.Method.DeclaringType != null)
                    className = $"{args.Method.DeclaringType.Namespace}.{args.Method.DeclaringType.Name}";
            }
            arguments = args.Arguments.ToString();

            Logger.Info($"className: {className}; methodName:{methodName};arguments:{arguments}");
        }

        /// <summary>  
        /// On Method success  
        /// </summary>  
        /// <param name="args"></param>  
        public override void OnSuccess(MethodExecutionArgs args)
        {
            Logger.Info($"OnSuccess : {(args.Method != null ? args.Method.Name : "")}");
            var returnValue = args.ReturnValue;
            Logger.Info($"ReturnValue : {returnValue}");
        }


        /// <summary>  
        /// On Method Exception  
        /// </summary>  
        /// <param name="args"></param>  
        public override void OnException(MethodExecutionArgs args)
        {
            if (args.Exception != null)
                Logger.Error($"OnException : {(!string.IsNullOrEmpty(args.Exception.Message) ? args.Exception.Message : "")}");


            string message = args.Exception.Message;
            string stackTrace = args.Exception.StackTrace;

            Logger.Error($"Application has got exception in method-{args.Method.Name} and message is {message}");

            // or you can send email notification         
        }

        /// <summary>  
        /// On Method Exit  
        /// </summary>  
        /// <param name="args"></param>  
        public override void OnExit(MethodExecutionArgs args)
        {
        }
    }
}
