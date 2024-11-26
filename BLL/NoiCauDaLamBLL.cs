using DTO;
using DAL;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace BLL
{
    public class NoiCauDaLamBLL
    {
        private readonly NoiCauDaLamDAL _noiCauDaLamDAL;

        public NoiCauDaLamBLL()
        {
            _noiCauDaLamDAL = NoiCauDaLamDAL.getInstance();
        }

        public bool AddNoiCauDaLam(NoiCauDaLamDTO noiCauDaLam)
        {
            // Validate input data
            if (noiCauDaLam == null || string.IsNullOrWhiteSpace(noiCauDaLam.NoiDung) || noiCauDaLam.MaCauHoi <= 0)
            {
                throw new ArgumentException("Invalid input data for NoiCauDaLam.");
            }

            return _noiCauDaLamDAL.Add(noiCauDaLam);
        }

        public bool DeleteNoiCauDaLam(int maNoiCauDaLam)
        {
            // Ensure ID is valid
            if (maNoiCauDaLam <= 0)
            {
                throw new ArgumentException("Invalid MaNoiCauDaLam.");
            }

            NoiCauDaLamDTO noiCauDaLam = new NoiCauDaLamDTO { MaNoiCauDaLam = maNoiCauDaLam };
            return _noiCauDaLamDAL.Delete(noiCauDaLam);
        }

        public List<NoiCauDaLamDTO> GetAllNoiCauDaLam()
        {
            return _noiCauDaLamDAL.GetAll();
        }

        public NoiCauDaLamDTO GetNoiCauDaLamById(int maNoiCauDaLam)
        {
            // Ensure ID is valid
            if (maNoiCauDaLam <= 0)
            {
                throw new ArgumentException("Invalid MaNoiCauDaLam.");
            }

            NoiCauDaLamDTO noiCauDaLam = new NoiCauDaLamDTO { MaNoiCauDaLam = maNoiCauDaLam };
            return _noiCauDaLamDAL.GetById(noiCauDaLam);
        }

        public bool UpdateNoiCauDaLam(NoiCauDaLamDTO noiCauDaLam)
        {
            // Validate input data
            if (noiCauDaLam == null || string.IsNullOrWhiteSpace(noiCauDaLam.NoiDung) || noiCauDaLam.MaCauHoi <= 0 || noiCauDaLam.MaNoiCauDaLam <= 0)
            {
                throw new ArgumentException("Invalid input data for updating NoiCauDaLam.");
            }

            return _noiCauDaLamDAL.Update(noiCauDaLam);
        }
        public bool AddNoiCauDaLamByMaCauHoi(int MaCauHoi)
        {
            return _noiCauDaLamDAL.AddNoiCauDaLamByMaCauHoi(MaCauHoi);
        }
        //Write Method GetAutoIncrement
        public int GetAutoIncrement()
        {
            return _noiCauDaLamDAL.GetAutoIncrement();
        }
    }
}
