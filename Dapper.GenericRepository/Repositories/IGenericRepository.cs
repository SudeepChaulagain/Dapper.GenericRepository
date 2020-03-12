using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.GenericRepository.Repositories
{
    public interface IGenericRepository<T> where T:class
    {
        List<T> GetAll();
        T GetById(int id);
        bool Insert(T o);
        bool Update(T o, int id);
        bool Delete(int id);





    }
}
