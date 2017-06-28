using Horizoft.Relay.DTO;
using Horizoft.Relay.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Horizoft.Relay.API
{
    public class TemperatureReportController : ApiController
    {
        private TemperatureReportRepository temperatureReportRepository = new TemperatureReportRepository();
        public List<TemperatureReport> GetReports(string date)
        {
            string[] dateList = date.Split(',');

            List<TemperatureReport> dateReportList = new List<TemperatureReport>();

            return temperatureReportRepository.GetReports(dateList);
        }
    }
}