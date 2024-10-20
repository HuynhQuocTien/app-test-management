using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public enum EnumDoKho
    {
        [Description("Hiểu")]
        De = 1,

        [Description("Trung bình")]
        TrungBinh = 2,

        [Description("Khó")]
        Kho = 3
    }
}
