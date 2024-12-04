using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LopBLL
    {
        public LopDAL lopDAL;
        public LopBLL()
        {
            lopDAL = LopDAL.getInstance();
        }
        public string Add(LopDTO t)
        {
            if (lopDAL.Add(t)) 
                return "Thêm lớp học thành công!";
            return "Thêm lớp học thất bại!";
        }
        public bool Delete(LopDTO t)
        {
            return lopDAL.Delete(t);
        }
        public bool Update(LopDTO t)
        {
            return lopDAL.Update(t);
        }
        public List<LopDTO> GetAll()
        {
            return lopDAL.GetAll();
        }
        public LopDTO GetById(LopDTO t)
        {
            return lopDAL.GetById(t);
        }
        public List<LopDTO> getListLopByMaGV(long maGV)
        {
            return lopDAL.GetAllByMaGV(maGV);
        }
        public List<LopDTO> getListLopByMaSV(long maSV)
        {
            return lopDAL.GetAllByMaSV(maSV);
        }
        public int GetAutoIncrement()
        {
            return lopDAL.GetAutoIncrement();
        }
        public bool checkMaMoi(string maMoi)
        {
            List<LopDTO> l = lopDAL.GetAll();
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].MaMoi.Equals(maMoi)) 
                    return true;

            }
            return false;
        }

        public int GetMaLopByMaMoi(string text)
        {
            return lopDAL.GetMaLopByMaMoi(text);
        }

        public List<LopDTO> getAll()
        {
            return lopDAL.GetAll();
        }

        public bool UpdateMaMoi(LopDTO lopDTO)
        {
            return lopDAL.UpdateMaMoi(lopDTO);
        }
    }
}
