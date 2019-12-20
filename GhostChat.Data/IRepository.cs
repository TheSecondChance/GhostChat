using System;
using System.Collections.Generic;
using System.Text;

namespace GhostChat.Data
{
    public interface IRepository<Type> where Type : class
    {
        List<Type> GetAll();
        Type GetById(object id);
        void Add(Type entity);
        void Update(Type entity);
        void Put(Type entity);
        void Remove(Type entity);
    }
}
