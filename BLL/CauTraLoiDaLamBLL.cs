using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CauTraLoiDaLamBLL
    {
        private readonly CauTraLoiDaLamDAL cauTraLoiDaLamDAL;

        public CauTraLoiDaLamBLL()
        {
            cauTraLoiDaLamDAL = CauTraLoiDaLamDAL.getInstance();
        }

        // Add a new answer
        public bool AddCauTraLoi(CauTraLoiDaLamDTO cauTraLoi)
        {
            return cauTraLoiDaLamDAL.Add(cauTraLoi);
        }

        // Delete an answer by its ID
        public bool DeleteCauTraLoi(int maCauTraLoiDaLam)
        {
            var cauTraLoi = cauTraLoiDaLamDAL.GetById(new CauTraLoiDaLamDTO { MaCauTraLoiDaLam = maCauTraLoiDaLam });
            if (cauTraLoi == null)
                return false;

            return cauTraLoiDaLamDAL.Delete(cauTraLoi);
        }

        // Update an answer
        public bool UpdateCauTraLoi(CauTraLoiDaLamDTO cauTraLoi)
        {
            var existingCauTraLoi = cauTraLoiDaLamDAL.GetById(new CauTraLoiDaLamDTO { MaCauTraLoiDaLam = cauTraLoi.MaCauTraLoiDaLam });
            if (existingCauTraLoi == null)
                return false;

            return cauTraLoiDaLamDAL.Update(cauTraLoi);
        }

        // Get all answers
        public List<CauTraLoiDaLamDTO> GetAllCauTraLoi()
        {
            return cauTraLoiDaLamDAL.GetAll();
        }

        // Get a specific answer by ID
        public CauTraLoiDaLamDTO GetCauTraLoiById(int maCauTraLoiDaLam)
        {
            return cauTraLoiDaLamDAL.GetById(new CauTraLoiDaLamDTO { MaCauTraLoiDaLam = maCauTraLoiDaLam });
        }

        // Additional business logic: check if a selected answer is correct
        public bool IsCorrectAnswer(int maCauTraLoiDaLam)
        {
            var cauTraLoi = GetCauTraLoiById(maCauTraLoiDaLam);
            return cauTraLoi != null && cauTraLoi.IsDapAn == 1;
        }
        public int getAutoIncrement()
        {
            return cauTraLoiDaLamDAL.GetAutoIncrement();
        }
        public List<CauTraLoiDaLamDTO> GetCauTraLoiDaLamOfDeThi(int maDe)
        {
            return cauTraLoiDaLamDAL.GetCauTraLoiDaLamOfDeThi(maDe);
        }
        public List<CauTraLoiDaLamDTO> GetCauTraLoiByMaCauHoi(int MaCauHoi)
        {
            try
            {
                return cauTraLoiDaLamDAL.GetAllByMaCauHoi(MaCauHoi);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetChiTietDeDaLamById: " + ex.Message);
                return null;
            }
        }
    }
}
