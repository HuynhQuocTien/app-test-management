﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CauHoiDTO
    {
        public int MaCauHoi { get; set; }
        public string NoiDung { get; set; }
        public int MaMonHoc { get; set; }
        public int MaNguoiTao { get; set; }
        public string DoKho { get; set; }
        public int TrangThai { get; set; }
        private int is_delete { get; set; }
        public CauHoiDTO()
        {
        }
        public CauHoiDTO(int maCauHoi, string noiDung, int maMonHoc, int maNguoiTao, string doKho, int trangThai, int is_delete)
        {
            MaCauHoi = maCauHoi;
            NoiDung = noiDung;
            MaMonHoc = maMonHoc;
            MaNguoiTao = maNguoiTao;
            DoKho = doKho;
            TrangThai = trangThai;
            this.is_delete = is_delete;
        }
    }
}