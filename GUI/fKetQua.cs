using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class fKetQua : Form
    {
        private DeThiDTO deThi;
        private LopDTO lop;
        private KetQuaDTO ketQua;
        public fKetQua(DeThiDTO deThi, LopDTO lop,KetQuaDTO ketQua)
        {
            InitializeComponent();
            this.deThi = deThi;
            this.lop = lop;
            this.ketQua = ketQua;
        }
    }
}
