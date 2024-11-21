﻿using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PhanCongBLL
    {
        private PhanCongDAL phanCongDAL;


        public bool Add(PhanCongDTO phancong)
        {
            PhanCongDAL phanCongDAL = new PhanCongDAL();
            return phanCongDAL.Add(phancong);
        }
        public List<PhanCongDTO> GetAll()
        {
            return phanCongDAL.GetAll();
        }
        public PhanCongBLL()
        {
            phanCongDAL = PhanCongDAL.getInstance();
        }

        public List<PhanCongDTO> GetTimKiem(string timkiem)
        {
            return phanCongDAL.GetTimKiem(timkiem);
        }

        public DataTable GetDataForPage(int startRecord, int recordsPerPage)
        {
            return phanCongDAL.GetDataForPage(startRecord, recordsPerPage);
        }

        //form add
        public DataTable loadComboBox()
        {
            return phanCongDAL.loadComboBox();
        }

        public DataTable loadListboxPC(string maGV)
        {
            return phanCongDAL.loadListboxPC(maGV);
        }

        public DataTable loadListboxCPC(string maGV)
        {
            return phanCongDAL.loadListboxCPC(maGV);
        }

        public bool CheckPCExists(string maGV)
        {
            return phanCongDAL.CheckPCExists(maGV);
        }

        public bool DeleteMAGV(string maGV)
        {

            PhanCongDAL phanCongDAL = new PhanCongDAL();
            return phanCongDAL.DeleteMAGV(maGV);
        }
    }
}
