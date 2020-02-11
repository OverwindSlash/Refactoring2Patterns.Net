using System;
using System.Collections.Generic;
using System.Text;

namespace Command.CatalogApp.Start
{
    public class WorkshopManager
    {
        private static readonly WorkshopRepository WorkshopRepository = new WorkshopRepository();

        public static string GetNextWorkshopId()
        {
            //string guid = Guid.NewGuid().ToString();
            //return guid.Substring(0,6);

            return "57adf0";
        }

        public static void AddWorkshop(StringBuilder newWorkshopContents)
        {
            string[] contents = newWorkshopContents.ToString().Split(":");

            Workshop newWorkshop = new Workshop()
            {
                Id = contents[0],
                Name = contents[1],
                Status = "Open",
                Duration = "Unknown"
            };

            WorkshopRepository.AddWorkshop(newWorkshop);
        }

        public static StringBuilder CreateNewFileFromTemplate(string workshopId, string workshopName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(workshopId);
            sb.Append(":");
            sb.Append(workshopName);
            return sb;
        }

        public static WorkshopRepository GetWorkshopRepository()
        {
            return WorkshopRepository;
        }
    }
}