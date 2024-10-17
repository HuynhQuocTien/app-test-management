using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NhomQuyenBLL
    {
        private NhomQuyenDAL nhomQuyenDAL;
        public NhomQuyenBLL()
        {
            nhomQuyenDAL = NhomQuyenDAL.getInstance();
        }
        public NhomQuyenDTO getNhomQuyenById(int id)
        {
            NhomQuyenDTO nq = new NhomQuyenDTO();
            nq.MaNhomQuyen = id;
            return nhomQuyenDAL.GetById(nq);
        }

    }
}
