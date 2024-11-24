using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NoiCauBLL
    {
        private NoiCauDAL NoiCauDAL;
        public NoiCauBLL()
        {
            NoiCauDAL = new NoiCauDAL();
        }
        public List<NoiCauDTO> GetAll(int maCauHoi)
        {
            return NoiCauDAL.GetAll();
        }
        public List<NoiCauDTO> GetAllByMaCauHoi(int maCauHoi)
        {
            return NoiCauDAL.GetAllByMaCauHoi(maCauHoi);
        }
        public List<int> GetAllMaNoiCau(int maCauHoi)
        {
            return NoiCauDAL.GetAllMaNoiCau(maCauHoi);
        }
        public KeyValuePair<int,string> Add(NoiCauDTO noicau)
        {
            var result = NoiCauDAL.Add(noicau); // Gọi hàm Add một lần và lưu kết quả

            if (!result.Equals(default(KeyValuePair<int, string>))) // Kiểm tra nếu không bị lỗi
            {
                return result;
            }

            return default; // Trả về giá trị mặc định nếu có lỗi (KeyValuePair<int, string> mặc định là 0 và null)
        }

        public NoiCauDTO GetMonHocById(int maNoiCau)
        {
            NoiCauDTO noiCau = new NoiCauDTO();
            noiCau.MaNoiCau = maNoiCau;
            return NoiCauDAL.GetById(noiCau);
        }

        public bool Delete(int MaCauHoi)
        {
            if (NoiCauDAL.Delete(MaCauHoi))
            {
                return true;
            }
            return false;
        }

        public bool Update(NoiCauDTO noicau)
        {
            if (NoiCauDAL.Update(noicau))
            {
                return true;
            }
            return false;
        }
    }
}
