using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUnitCTL<T> where T : class
    {
        T GetById(T t);
        List<T> GetAll();
        int Add(T t);
        bool Update(T t);
        bool Delete(T t);
        List<T> GetTimKiem(string timkiem);
    }
}
