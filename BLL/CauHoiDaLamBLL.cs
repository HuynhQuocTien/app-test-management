using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CauHoiDaLamBLL
    {
        private readonly CauHoiDaLamDAL cauHoiDaLamDAL;

        public CauHoiDaLamBLL()
        {
            cauHoiDaLamDAL = CauHoiDaLamDAL.getInstance();
        }

        // Add a question
        public bool Add(CauHoiDaLamDTO cauHoi)
        {
            // Add business logic checks if necessary (e.g., validate fields)
            return cauHoiDaLamDAL.Add(cauHoi);
        }

        // Delete a question (soft delete)
        public bool Delete(int maCauHoiDaLam)
        {
            // Fetch the question to delete
            CauHoiDaLamDTO cauHoi = cauHoiDaLamDAL.GetById(new CauHoiDaLamDTO { MaCauHoiDaLam = maCauHoiDaLam });
            if (cauHoi == null)
                return false;

            return cauHoiDaLamDAL.Delete(cauHoi);
        }

        // Update a question
        public bool Update(CauHoiDaLamDTO cauHoi)
        {
            // Fetch the existing question to ensure it exists before updating
            var existingCauHoi = cauHoiDaLamDAL.GetById(new CauHoiDaLamDTO { MaCauHoiDaLam = cauHoi.MaCauHoiDaLam });
            if (existingCauHoi == null)
                return false;

            return cauHoiDaLamDAL.Update(cauHoi);
        }

        // Retrieve all active questions
        public List<CauHoiDaLamDTO> GetAll()
        {
            return cauHoiDaLamDAL.GetAll();
        }

        // Get a specific question by ID
        public CauHoiDaLamDTO GetById(int maCauHoiDaLam)
        {
            return cauHoiDaLamDAL.GetById(new CauHoiDaLamDTO { MaCauHoiDaLam = maCauHoiDaLam });
        }
        public int getAutoIncrement()
        {
            return cauHoiDaLamDAL.GetAutoIncrement();
        }
        public CauHoiDaLamDTO GetCauHoiDaLamByMaCauHoi(int MaCauHoi)
        {
            try
            {
                return cauHoiDaLamDAL.GetByMaCauHoi(MaCauHoi);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetChiTietDeDaLamById: " + ex.Message);
                return null;
            }
        }

    }
}
