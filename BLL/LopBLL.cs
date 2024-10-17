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
    }
}
