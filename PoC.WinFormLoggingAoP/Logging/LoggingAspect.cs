using KingAOP.Aspects;
using log4net;
using System;
using System.Text;

namespace PoC.WinFormLoggingAoP.Logging
{
    public class LoggingAspect : OnMethodBoundaryAspect
    {
        private static ILog log;
        private Type type;
        public LoggingAspect() { }
        public LoggingAspect(Type type)
        {
            this.type = type;
            InitialLogger();
        }
        public override void OnEntry(MethodExecutionArgs args)
        {
            InitialLogger();
            string logData = CreateLogData("Entering", args);
            log.Debug(logData);
            Console.WriteLine(logData);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            InitialLogger();
            string logData = CreateLogData("Leaving", args);
            log.Debug(logData);
            Console.WriteLine(logData);
        }
        private void InitialLogger()
        {
            if (log == null)
            {
                log = LogManager.GetLogger(type);
            }
        }
        private string CreateLogData(string methodStage, MethodExecutionArgs args)
        {
            var str = new StringBuilder();
            str.Append(string.Format(methodStage + " {0} ", args.Method));
            foreach (var argument in args.Arguments)
            {
                var argType = argument.GetType();
                str.Append(argType.Name + ": ");
                if (argType == typeof(string) || argType.IsPrimitive)
                {
                    str.Append(argument);
                }
                else
                {
                    foreach (var property in argType.GetProperties())
                    {
                        str.AppendFormat("{0} = {1}; ",
                        property.Name, property.GetValue(argument, null));
                    }
                }
            }
            return str.ToString();
        }
    }
}
