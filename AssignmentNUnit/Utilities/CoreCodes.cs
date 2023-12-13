using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentNUnit.Utilities
{
    public class CoreCodes
    {

        protected RestClient client;

        protected ExtentReports extent;
        protected ExtentTest test;
        ExtentSparkReporter sparkReporter;

        private string? baseUrl = "https://jsonplaceholder.typicode.com/";

        [OneTimeSetUp]
        public void Initialization()
        {
            string currDir = Directory.GetParent(@"../../../").FullName;

            extent = new ExtentReports();
            sparkReporter = new ExtentSparkReporter(currDir + "/ExtentReports/extent-report"
                + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".html");

            extent.AttachReporter(sparkReporter);

            string? curDir = Directory.GetParent(@"../../../").FullName;
            string? fileName = curDir + "/Logs/log_" +
                DateTime.Now.ToString("ddMMyyyy-HHmmss") + ".txt";

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(fileName, rollingInterval: RollingInterval.Day)
                .CreateLogger();

        }

        [SetUp]
        public void Setup()
        {
            client = new RestClient(baseUrl);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            extent.Flush();
        }
    }
}
