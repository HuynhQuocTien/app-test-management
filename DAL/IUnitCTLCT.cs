using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUnitCTLCT<T> where T : class
    {
        T GetById(T t);
        List<T> GetAll(int MaCauHoi);
        bool Add(T t);
        bool Update(T t);
        bool Delete(T t);
    }
}
