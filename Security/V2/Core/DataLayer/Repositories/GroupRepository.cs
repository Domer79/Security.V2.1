﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.CommonContracts;
using Security.V2.Contracts.Repository;

namespace Security.V2.Core.DataLayer.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ICommonDb _commonDb;

        public GroupRepository(ICommonDb commonDb)
        {
            _commonDb = commonDb;
        }

        public Group Create(Group entity)
        {
            entity.Id = Guid.NewGuid();
            var id = _commonDb.ExecuteScalar<int>("execute AddGroup @id, @name, @description", entity);
            entity.IdMember = id;
            return entity;
        }

        public async Task<Group> CreateAsync(Group entity)
        {
            entity.Id = Guid.NewGuid();
            var id = _commonDb.ExecuteScalarAsync<int>("execute AddGroup @id, @name, @description", entity);
            entity.IdMember = await id;
            return entity;
        }

        public Group Get(object id)
        {
            return _commonDb.QuerySingle<Group>("select * from GroupsView where idMember = @id", new {id});
        }

        public IEnumerable<Group> Get()
        {
            return _commonDb.Query<Group>("select * from GroupsView");
        }

        public Task<Group> GetAsync(object id)
        {
            return _commonDb.QuerySingleAsync<Group>("select * from GroupsView where idMember = @id", new { id });
        }

        public Task<IEnumerable<Group>> GetAsync()
        {
            return _commonDb.QueryAsync<Group>("select * from GroupsView");
        }

        public Group GetByName(string name)
        {
            return _commonDb.QuerySingle<Group>("select * from GroupsView where name = @name", new { name });
        }

        public Task<Group> GetByNameAsync(string name)
        {
            return _commonDb.QuerySingleAsync<Group>("select * from GroupsView where name = @name", new { name });
        }

        public void Remove(object id)
        {
            _commonDb.ExecuteNonQuery("execute DeleteGroup @idMember", new {idMember = id});
        }

        public Task RemoveAsync(object id)
        {
            return _commonDb.ExecuteNonQueryAsync("execute DeleteGroup @idMember", new { idMember = id });
        }

        public void Update(Group entity)
        {
            _commonDb.ExecuteNonQuery("execute UpdateGroup @id, @idMember, @name, @description", entity);
        }

        public Task UpdateAsync(Group entity)
        {
            return _commonDb.ExecuteNonQueryAsync("execute UpdateGroup @id, @idMember, @name, @description", entity);
        }
    }
}
