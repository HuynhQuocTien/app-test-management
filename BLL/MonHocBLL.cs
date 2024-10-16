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
    }
}
