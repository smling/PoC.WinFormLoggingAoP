using KingAOP;
using log4net;
using PoC.WinFormLoggingAoP.Classes;
using PoC.WinFormLoggingAoP.Logging;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PoC.WinFormLoggingAoP
{
    public class TestController : IDynamicMetaObjectProvider
    {
        private static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [LoggingAspect(typeof(TestController))]
        public bool DoSomething(TestEntity testEntity) {
            testEntity.TestEntityId++;
            testEntity.Name += " - add something";
            testEntity.Description += " - add something";
            return true;
        }

        public DynamicMetaObject GetMetaObject(Expression parameter)
        {
            return new AspectWeaver(parameter, this); // need for AOP weaving
        }
    }
}
