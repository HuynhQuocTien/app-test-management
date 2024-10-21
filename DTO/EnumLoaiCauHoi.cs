using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public enum EnumLoaiCauHoi
    {
        [Description("Trắc nghiệm")]
        TracNghiem = 1,

        [Description("Điền từ")]
        DienTu = 2,

        [Description("Nối câu")]
        NoiCau = 3
    }
}
