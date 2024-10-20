using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class KetQuaBLL
    {
        private KetQuaDAL ketQuaDAL;
        public KetQuaBLL()
        {
        }

        public KetQuaDTO Get(int maDe, long maSV)
        {
            return ketQuaDAL.Get(maDe, maSV);
        }
    }
}
