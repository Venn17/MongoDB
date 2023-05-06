using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongo.Models.BusinessModels
{
    public interface IGenericRepository<T,K>
    {
        List<T> getAll();
        T getById(K key);
        bool insert(T entity);
        bool update(T entity);
        bool delete(K key);
    }
}
