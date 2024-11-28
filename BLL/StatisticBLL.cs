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
    }
}
