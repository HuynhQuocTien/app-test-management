using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class StatisticBLL
    {
        private StatisticDAL _statistic;
        public StatisticBLL()
        {
            _statistic = new StatisticDAL();
        }

        public List<ThongKeDiemTheoMonDTO> ThongKeDiemTheoMon()
        {
            return _statistic.ThongKeDiemTheoMon();
        }

        public List<ThongKeDiemTheoLopDTO> ThongKeDiemTheoLop()
        {
            return _statistic.ThongKeDiemTheoLop();
        }

        public List<ThongKeSVThamGiaThiTheoLopDTO> ThongKeSVThamGiaThiTheoLop()
        {
            return _statistic.ThongKeSVThamGiaThiTheoLop();
        }

        public List<ThongKeSVThamGiaThiTheoMonDTO> ThongKeSVThamGiaThiTheoMon()
        {
            return _statistic.ThongKeSVThamGiaThiTheoMon();
        }

        public int SoLuongGiangVien()
        {
            return _statistic.TongTaiKhoan(2);
        }

        public int SoLuongSinhVien()
        {
            return _statistic.TongTaiKhoan(3);
        }

        public int SoLuongCauHoi()
        {
            return _statistic.SoLuongCauHoi();
        }
    }
}
