using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public static class EnumHelper
    {

        public static string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                 .FirstOrDefault() as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

    }
    //var descriptionLoaiCauHoi = EnumHelper.GetEnumDescription(EnumLoaiCauHoi.TracNghiem);
    //var descriptionDoKhoCauHoi = EnumHelper.GetEnumDescription(EnumDoKhoCauHoi.De);

    //Console.WriteLine(descriptionLoaiCauHoi); // Output: Trắc nghiệm
    //Console.WriteLine(descriptionDoKhoCauHoi); // Output: Dễ
}
