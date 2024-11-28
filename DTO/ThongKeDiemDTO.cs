using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ThongKeDiemDTO
    {
        public MonHocDTO MonHoc { get; set; }
        public LopDTO Lop { get; set; }
        public string Ten { get; set; }
        public decimal diem { get; set; }
        public string tenDe {  get; set; }
    }
}
