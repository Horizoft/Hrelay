using Horizoft.Relay.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizoft.Relay.DAL
{
    public class TemperatureReportRepository
    {
        private HorizoftRelayEntities entities = new HorizoftRelayEntities();
        private int placeId = 1;
        private int areaId = 1;

        public TemperatureReportRepository() { }

        public TemperatureReportRepository(int _placeId, int _areaId)
        {
            placeId = _placeId;
            areaId = _areaId;
        }

        public List<SPTemperatureReport_Result> GetReportByDate(DateTime datetime)
        {
            List<SPTemperatureReport_Result> list = new List<SPTemperatureReport_Result>();

            list = entities.SPTemperatureReport(placeId, areaId, datetime).ToList();

            return list;
        }

        public TemperatureReport GetReport(DateTime datetime)
        {
            TemperatureReport dateReport = new TemperatureReport();

            if (datetime != null)
            {
                dateReport.DateTime = datetime.ToString("yyyy-MM-dd");
                dateReport.TemperatureReports = GetReportByDate(datetime);
            }

            return dateReport;
        }

        public List<TemperatureReport> GetReports(string[] dateList)
        {
            List<TemperatureReport> dateReports = new List<TemperatureReport>();

            for(int n=0; n<dateList.Length;n++)
            {
                TemperatureReport dateReport = GetReport(Convert.ToDateTime(dateList[n]));
                dateReports.Add(dateReport);
            }

            return dateReports;
        }

    }

}
