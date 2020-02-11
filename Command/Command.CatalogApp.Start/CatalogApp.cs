using System;
using System.Collections.Generic;
using System.Text;

namespace Command.CatalogApp.Start
{
    // CatalogApp 类负责根据不同的情况进行调度，执行动作，并返回响应
    // CatalogApp 使用一个庞大的条件语句来执行这些任务
    // 当需要按照众多不同的条件去做某些操作的时候，就可以重构到 Command 模式
    public class CatalogApp
    {
        private static string NEW_WORKSHOP = "NEW_WORKSHOP";
        private static string ALL_WORKSHOPS = "ALL_WORKSHOPS";
        private static string ALL_WORKSHOPS_STYLESHEET = "ALL_WORKSHOPS_STYLESHEET";

        public HandlerResponse ExecuteActionAndGetResponse(string actionName, Dictionary<string, string> parameters)
        {
            // 当处理一个新的研讨班时
            if (actionName.ToUpper() == NEW_WORKSHOP)
            {
                string nextWorkshopID = WorkshopManager.GetNextWorkshopId();

                StringBuilder newWorkshopContents =
                    WorkshopManager.CreateNewFileFromTemplate(
                        nextWorkshopID,
                        parameters["Name"]
                    );
                WorkshopManager.AddWorkshop(newWorkshopContents);

                Dictionary<string, string> allWorksParameters = new Dictionary<string, string>();
                return ExecuteActionAndGetResponse(ALL_WORKSHOPS, allWorksParameters);
            }
            // 当处理所有研讨班时
            else if (actionName.ToUpper() == ALL_WORKSHOPS)
            {
                XmlBuilder allWorkshopsXml = new XmlBuilder("workshops");

                WorkshopRepository repository = WorkshopManager.GetWorkshopRepository();
                using (IEnumerator<string> ids = repository.GetEnumerator())
                {
                    while (ids.MoveNext())
                    {
                        string id = ids.Current;
                        Workshop workshop = repository.GetWorkshop(id);
                        TagNode workshopNode = allWorkshopsXml.AddBelowParent("workshop");
                        workshopNode.AddAttribute("id", workshop.Id);
                        workshopNode.AddAttribute("name", workshop.Name);
                        workshopNode.AddAttribute("status", workshop.Status);
                        workshopNode.AddAttribute("duration", workshop.Duration);
                    }
                }

                string formattedXml = GetFormattedData(allWorkshopsXml.ToString());
                return new HandlerResponse(new StringBuilder(formattedXml), ALL_WORKSHOPS_STYLESHEET);
            }
            // 还有很多 else if 语句，这里略去
            else
            {
                return null;
            }
        }

        private string GetFormattedData(string xml)
        {
            // 格式化操作略去，直接返回字符串
            return xml;
        }
    }
}
