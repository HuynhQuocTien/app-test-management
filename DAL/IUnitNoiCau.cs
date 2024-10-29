using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUnitNoiCau<T> where T : class
    {
        T GetById(T t);
        List<T> GetAll();
        KeyValuePair<int, string> Add(T t);
        bool Update(T t);
        bool Delete(int maCauHoi);
    }
}
