using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CauHoiBLL
    {
        private CauHoiDAL cauHoiDAL;
        public CauHoiBLL()
        {
            cauHoiDAL = new CauHoiDAL();
        }
        public List<CauHoiDTO> GetAll()
        {
            return cauHoiDAL.GetAll();
        }
        public List<CauHoiDTO> GetTimKiem(string timkiem)
        {
            return cauHoiDAL.GetTimKiem(timkiem);
        }

        public DataTable GetDataForPage(int startRecord, int recordsPerPage)
        {
            return cauHoiDAL.GetDataForPage(startRecord, recordsPerPage);
        }
        public int Add(CauHoiDTO cauhoi)
        {
            int MaCauHoi = cauHoiDAL.Add(cauhoi);
            if (MaCauHoi > 0)
            {
                return MaCauHoi;
            }
            return 0;
        }

        public CauHoiDTO GetMonHocById(int maCauHoi)
        {
            CauHoiDTO cauHoi = new CauHoiDTO();
            cauHoi.MaCauHoi = maCauHoi;
            return cauHoiDAL.GetById(cauHoi);
        }

        public string Delete(CauHoiDTO cauhoi)
        {
            if (cauHoiDAL.Delete(cauhoi))
            {
                return "Xoá thành công";
            }
            return "Xóa thất bại";
        }

        public bool Update(CauHoiDTO cauhoi)
        {
            if (cauHoiDAL.Update(cauhoi))
            {
                return true;
            }
            return false;
        }

        public string Import(CauHoiDTO cauhoi)
        {
            if (cauHoiDAL.Import(cauhoi))
            {
                return "Thêm thành công";
            }
            return "Thêm thất bại";
        }

        public int ImportDT(CauHoiDTO cauhoi)
        {
            int MaCauHoi = cauHoiDAL.Add(cauhoi);
            if (MaCauHoi>0)
            {
                return MaCauHoi;
            }
            return 0;
        }
    }
}
