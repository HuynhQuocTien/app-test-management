using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GiaoDeThiBLL
    {
        private GiaoDeThiDAL giaoDeThiDAL;
        public GiaoDeThiBLL()
        {
            giaoDeThiDAL = GiaoDeThiDAL.getInstance();
        }
        public bool Add(GiaoDeThiDTO giaoDeThi)
        {
            return giaoDeThiDAL.Add(giaoDeThi);
        }
        public GiaoDeThiDTO GetByID(GiaoDeThiDTO giaoDeThi)
        {
            return giaoDeThiDAL.GetById(giaoDeThi);
        }
        public List<DeThiDTO> GetDeThiChuaThem(DeThiDTO dethi)
        {
            return giaoDeThiDAL.GetDeThiChuaThem(dethi);
        }
        public List<MonHocDTO> GetMonHoc(long maNguoiDung)
        {
            List<MonHocDTO> dsMonHocPhanCong = new List<MonHocDTO>();
            List<PhanCongDTO> dsPhanCong = giaoDeThiDAL.GetPhanCong(maNguoiDung);
            foreach (PhanCongDTO monhoc in dsPhanCong)
            {
                int maMonHoc = monhoc.MaMonHoc;
                List<MonHocDTO> monHocDTO1 = giaoDeThiDAL.LayTenMonHoc(maMonHoc);
                foreach (MonHocDTO monHocDTO2 in monHocDTO1)
                {
                    dsMonHocPhanCong.Add(monHocDTO2);
                }
            }
            return dsMonHocPhanCong;
        }
    }
}
