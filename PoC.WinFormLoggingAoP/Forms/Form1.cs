using KingAOP;
using log4net;
using PoC.WinFormLoggingAoP.Classes;
using PoC.WinFormLoggingAoP.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoC.WinFormLoggingAoP.Forms
{
    public partial class Form1 : Form, IDynamicMetaObjectProvider
    {
        private static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Form1()
        {
            InitializeComponent();
        }
        [LoggingAspect(typeof(Form1))]
        public void btnWriteDebugLog_Click(object sender, EventArgs e)
        {
            TestEntity testEntity = new TestEntity()
            {
                TestEntityId=1,
                Name="Test name",
                Description="Test description"
            };
            dynamic testController = new TestController();
            testController.DoSomething(testEntity);
        }
        public DynamicMetaObject GetMetaObject(Expression parameter)
        {
            return new AspectWeaver(parameter, this); // need for AOP weaving
        }
    }
}
