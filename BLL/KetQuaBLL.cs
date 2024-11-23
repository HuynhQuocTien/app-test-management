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
            ketQuaDAL = KetQuaDAL.getInstance();
        }

        public KetQuaDTO Get(int maDe, long maSV)
        {
            return ketQuaDAL.Get(maDe, maSV);
        }
        public List<KetQuaDTO> GetAll()
        {
            return ketQuaDAL.GetAll();
        }
        public bool Update(KetQuaDTO t)
        {
            if (ketQuaDAL.Update(t))
            {
                return true;
            }
            return false;
        }

        public bool UpdateByMaDe(KetQuaDTO t)
        {
            if (ketQuaDAL.UpdateByMaDe(t))
            {
                return true;
            }
            return false;
        }

        public bool Add(KetQuaDTO kq)
        {
            if (ketQuaDAL.Add(kq))
            {
                return true;
            }
            return false;
        }

        public bool UpdateTrangThai(int maLop, int maDe, int trangThai)
        {
       
            return ketQuaDAL.UpdateTrangThaiMoDapAn(maLop, maDe, trangThai);
        }

        public int? GetTrangThai(int maLop, int maDe)
        {
            return ketQuaDAL.GetTrangThaiByMaLopAndMaDe(maLop, maDe);
        }
    }

}
