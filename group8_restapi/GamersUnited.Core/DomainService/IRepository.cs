using GamersUnited.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamersUnited.Core.DomainService
{
    public interface IRepository<T>
    {
        int Count();
        T Add(T obj);
        T GetById(int id);
        IList<T> GetAll();
        IList<T> GetPage(PageProperty pageProperty);
        T Update(int id, T obj);
        T Remove(T obj);
    }
}
