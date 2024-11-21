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
        public NguoiDungDTO getUserLoginById(long id)
        {
            NguoiDungDTO nd = new NguoiDungDTO();
            nd.MaNguoiDung = id;
            return nguoiDungDAL.GetById(nd);
        }
        public string updateInfo(string Ten, string SDT, string Avatar, string MaNguoiDung)
        {
            if(nguoiDungDAL.UpdateInfo(Ten, SDT, Avatar, MaNguoiDung))
            {
                return "1";
            }
            else
            {
                return "2";
            }    
        }
    }
}
