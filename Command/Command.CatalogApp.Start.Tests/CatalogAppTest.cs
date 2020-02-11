using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Command.CatalogApp.Start.Tests
{
    [TestClass]
    public class CatalogAppTest
    {
        [TestMethod]
        public void TestExecuteActionAndGetResponse()
        {
            CatalogApp app = new CatalogApp();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Name", "Python");
            string action = "NEW_WORKSHOP";

            HandlerResponse response = app.ExecuteActionAndGetResponse(action, parameters);

            string result =
                "<workshops>" +
                    "<workshop id='efcv4d' name='C++' status='Open' duration='22 Jan. 2019 ~ 15 Feb. 2019'></workshop>" +
                    "<workshop id='74d5jh' name='C#' status='Closed' duration='2 May. 2018 ~ 19 May. 2018'></workshop>" +
                    "<workshop id='r982jk' name='DDD' status='Open' duration='15 Dec. 2019 ~ 15 Mar. 2020'></workshop>" +
                    "<workshop id='57adf0' name='Python' status='Open' duration='Unknown'></workshop>" +
                "</workshops>";

            Assert.AreEqual(result, response.GetXmlString());
        }
    }
}
