using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Security.Model;

namespace Security.Tests.SecurityFake
{
    public class ApplicationCollection : BaseCollection<Application>
    {
        private List<Application> _collection;
        protected override List<Application> Collection => _collection ?? (_collection =  new List<Application>());

        public override void Add(Application item)
        {
            var id = Database.Applications.Identity();
            item.IdApplication = id;

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