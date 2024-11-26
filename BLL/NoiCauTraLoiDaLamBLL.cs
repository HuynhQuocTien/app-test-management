using DTO;
using DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class NoiCauTraLoiDaLamBLL
    {
        private readonly NoiCauTraLoiDaLamDAL _noiCauTraLoiDaLamDAL;

        public NoiCauTraLoiDaLamBLL()
        {
            _noiCauTraLoiDaLamDAL = NoiCauTraLoiDaLamDAL.getInstance();
        }

        public bool AddNoiCauTraLoiDaLam(NoiCauTraLoiDaLamDTO noiCauTraLoiDaLam)
        {
            return _noiCauTraLoiDaLamDAL.Add(noiCauTraLoiDaLam);
        }

        public bool UpdateNoiCauTraLoiDaLam(NoiCauTraLoiDaLamDTO noiCauTraLoiDaLam)
        {
            if (noiCauTraLoiDaLam == null || noiCauTraLoiDaLam.MaCauTLDaLam <= 0)
            {
                throw new ArgumentException("Invalid ID or data for NoiCauTraLoiDaLam");
            }
            return _noiCauTraLoiDaLamDAL.Update(noiCauTraLoiDaLam);
        }

        public bool DeleteNoiCauTraLoiDaLam(int maCauTLDaLam)
        {
            if (maCauTLDaLam <= 0)
            {
                throw new ArgumentException("Invalid MaCauTLDaLam");
            }

            NoiCauTraLoiDaLamDTO noiCauTraLoiDaLam = new NoiCauTraLoiDaLamDTO
            {
                MaCauTLDaLam = maCauTLDaLam
            };
            return _noiCauTraLoiDaLamDAL.Delete(noiCauTraLoiDaLam);
        }

        public List<NoiCauTraLoiDaLamDTO> GetAllNoiCauTraLoiDaLam()
        {
            return _noiCauTraLoiDaLamDAL.GetAll();
        }

        public NoiCauTraLoiDaLamDTO GetNoiCauTraLoiDaLamById(int maCauTLDaLam)
        {
            if (maCauTLDaLam <= 0)
            {
                throw new ArgumentException("Invalid MaCauTLDaLam");
            }

            NoiCauTraLoiDaLamDTO noiCauTraLoiDaLam = new NoiCauTraLoiDaLamDTO
            {
                MaCauTLDaLam = maCauTLDaLam
            };
            return _noiCauTraLoiDaLamDAL.GetById(noiCauTraLoiDaLam);
        }
    }
}
