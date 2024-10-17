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
    public static class EnumHelper //Get Description of Enum
    {
        public static string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                 .FirstOrDefault() as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
