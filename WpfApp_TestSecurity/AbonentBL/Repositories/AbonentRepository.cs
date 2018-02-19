using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonContracts;
using WpfApp_TestSecurity.AbonentBL.Interfaces;
using WpfApp_TestSecurity.AbonentBL.Models;

namespace WpfApp_TestSecurity.AbonentBL.Repositories
{
    public class AbonentRepository : IAbonentRepository
    {
        private readonly ICommonDb _commonDb;

        public AbonentRepository(ICommonDb commonDb)
        {
            _commonDb = commonDb;
        }

        public Abonent Create(Abonent entity)
        {
            var id = _commonDb.ExecuteScalar<int>(@"
insert into Abonents(inn, shortName, fullName, address, phones, managerFullName, agent, agentPhones) values(@inn, @shortName, @fullName, @address, @phones, @managerFullName, @agent, @agentPhones)
select SCOPE_IDENTITY()
", entity);

            return Get(id);
        }

        public Abonent Get(object id)
        {
            return _commonDb.Query<Abonent>("select * from Abonents where id = @id", new {id}).SingleOrDefault();
        }

        public IEnumerable<Abonent> Get()
        {
            return _commonDb.Query<Abonent>("select * from Abonents");
        }

        public IEntityCollectionInfo<Abonent> GetEntityCollectionInfo(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public void Remove(object id)
        {
            _commonDb.ExecuteNonQuery("delete from Abonents where id = @id", new {id});
        }

        public void Update(Abonent entity)
        {
            _commonDb.ExecuteNonQuery(
                "update Abonents set inn = @inn, shortName = @shortName, fullname = @fullName, address = @address, phones = @phones, managerFullName = @managerFullName, agent = @agent, agentPhones = @agentPhones where id = @id",
                entity);
        }
    }
}
