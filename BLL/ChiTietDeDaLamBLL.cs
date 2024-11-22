using DAL;
using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class ChiTietDeDaLamBLL
    {
        private ChiTietDeDaLamDAL _chiTietDeDaLamDAL;

        public ChiTietDeDaLamBLL()
        {
            _chiTietDeDaLamDAL = ChiTietDeDaLamDAL.getInstance();
        }

        public bool AddChiTietDeDaLam(ChiTietDeDaLamDTO chiTietDeDaLam)
        {
            try
            {
                return _chiTietDeDaLamDAL.Add(chiTietDeDaLam);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in AddChiTietDeDaLam: " + ex.Message);
                return false;
            }
        }

        public bool DeleteChiTietDeDaLam(ChiTietDeDaLamDTO chiTietDeDaLam)
        {
            try
            {
                return _chiTietDeDaLamDAL.Delete(chiTietDeDaLam);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DeleteChiTietDeDaLam: " + ex.Message);
                return false;
            }
        }

        public bool UpdateChiTietDeDaLam(ChiTietDeDaLamDTO chiTietDeDaLam)
        {
            try
            {
                return _chiTietDeDaLamDAL.Update(chiTietDeDaLam);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in UpdateChiTietDeDaLam: " + ex.Message);
                return false;
            }
        }

        public List<ChiTietDeDaLamDTO> GetAllChiTietDeDaLam()
        {
            try
            {
                return _chiTietDeDaLamDAL.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetAllChiTietDeDaLam: " + ex.Message);
                return new List<ChiTietDeDaLamDTO>();
            }
        }

        public ChiTietDeDaLamDTO GetChiTietDeDaLamById(ChiTietDeDaLamDTO chiTietDeDaLam)
        {
            try
            {
                return _chiTietDeDaLamDAL.GetById(chiTietDeDaLam);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetChiTietDeDaLamById: " + ex.Message);
                return null;
            }
        }
    }
}
