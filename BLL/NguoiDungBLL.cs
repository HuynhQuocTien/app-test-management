using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NguoiDungBLL
    {
        private NguoiDungDAL nguoiDungDAL;
        public NguoiDungBLL()
        {
            nguoiDungDAL = NguoiDungDAL.getInstance();
        }
        public string Add(NguoiDungDTO n)
        {
            if (nguoiDungDAL.Add(n))
                return "Thêm người dùng thành công!";
            return "Thêm người dùng thất bại!";
        }


        public List<NguoiDungDTO> GetAllNguoiDung()
        {
            return nguoiDungDAL.GetAll();
        }

        public bool Delete(NguoiDungDTO nguoiDung)
        {
            return nguoiDungDAL.DeleteByMaNguoiDung(nguoiDung);
        }


        //public string Import(NguoiDungDTO nguoiDung)
        //{
        //    if (NguoiDungDAL.Import(nguoiDung))
        //    {
        //        return "Thêm thành công";
        //    }
        //    return "Thêm thất bại";
        //}

        public NguoiDungDTO getUserLoginById(long id)
        {
            NguoiDungDTO nd = new NguoiDungDTO();
            nd.MaNguoiDung = id;
            return nguoiDungDAL.GetById(nd);
        }
    }
}
