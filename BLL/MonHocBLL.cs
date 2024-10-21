using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MonHocBLL
    {
        private MonHocDAL monHocDAL;
        public MonHocBLL() 
        { 
            monHocDAL = new MonHocDAL();
        }
        public List<MonHocDTO> GetAll()
        {
            return monHocDAL.GetAll();
        }
        public string Add(MonHocDTO monHoc)
        {
            if (monHocDAL.Add(monHoc))
            {
                return "Thêm thành công";
            }
            return "Thêm thất bại";
        }
        public string Import(MonHocDTO monHoc)
        {
            if (monHocDAL.Import(monHoc))
            {
                return "Thêm thành công";
            }
            return "Thêm thất bại";
        }
        public string Update(MonHocDTO monHoc) 
        {
            if (monHocDAL.Update(monHoc))
            {
                return "Sửa thành công";
            }
            return "Sửa thất bại";
        }
        public string Delete(MonHocDTO monHoc)
        {
            if (monHocDAL.Delete(monHoc))
            {
                return "Xoá thành công";
            }
            return "Xóa thất bại";
        }
        public MonHocDTO GetMonHocById(int maMonHoc)
        {
            MonHocDTO monHoc = new MonHocDTO();
            monHoc.MaMonHoc = maMonHoc;
            return monHocDAL.GetById(monHoc);
        }
    }
}
