using System.Collections.Generic;
using System.Linq;

namespace Command.CatalogApp.Start
{
    public class WorkshopRepository
    {
        private List<Workshop> _workshops;

        public WorkshopRepository()
        {
            _workshops = new List<Workshop>()
            {
                new Workshop()
                {
                    Id = "efcv4d",
                    Name = "C++",
                    Status = "Open",
                    Duration = "22 Jan. 2019 ~ 15 Feb. 2019"
                },
                new Workshop()
                {
                    Id = "74d5jh",
                    Name = "C#",
                    Status = "Closed",
                    Duration = "2 May. 2018 ~ 19 May. 2018"
                },
                new Workshop()
                {
                    Id = "r982jk",
                    Name = "DDD",
                    Status = "Open",
                    Duration = "15 Dec. 2019 ~ 15 Mar. 2020"
                }
            };
        }

        public void AddWorkshop(Workshop workshop)
        {
            _workshops.Add(workshop);
        }

        public IEnumerator<string> GetEnumerator()
        {
            IEnumerable<string> keys = _workshops.Select(w => w.Id);
            return keys.GetEnumerator();
        }

        public Workshop GetWorkshop(string id)
        {
            Workshop workshop = _workshops.SingleOrDefault(w => w.Id == id);
            return workshop;
        }
    }
}