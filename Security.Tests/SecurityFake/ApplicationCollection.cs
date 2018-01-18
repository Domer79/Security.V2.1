using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Security.Model;

namespace Security.Tests.SecurityFake
{
    public class ApplicationCollection : BaseCollection<Application>
    {
        private List<Application> _applications = new List<Application>();

        protected override List<Application> Collection => new List<Application>();

        public override void Add(Application item)
        {
            Collection.Add(item);
        }

        public override void Remove(Application item)
        {
            var app = Collection.FirstOrDefault(_ => _.IdApplication == item.IdApplication);
            if (app == null)
                return;

            Collection.Remove(app);
        }

        public override void Update(Application item)
        {
            var app = Collection.FirstOrDefault(_ => _.IdApplication == item.IdApplication);
            if (app == null)
                return;

            app.AppName = item.AppName;
            app.Description = item.Description;
        }
    }
}