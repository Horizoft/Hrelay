using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Horizoft.Relay.DTO;

namespace Horizoft.Relay.DAL
{
    public class TemperatureDataRepository
    {
		private const string DATETIME_FORMAT = @"(\d{2}\/\d{2}\/\d{4}\s\d{2}:\d{2}:\d{2})";

		private HorizoftRelayEntities entities = new HorizoftRelayEntities();
        private int placeId = -1;
        private int areaId = -1;

        public TemperatureDataRepository() { }

        public TemperatureDataRepository(int _placeId, int _areaId)
        {
            placeId = _placeId;
            areaId = _areaId;
        }

	    public TemperatureData GetLast()
	    {
			TemperatureData data = new TemperatureData();

            //TemperatureData2 data2 = entities.TemperatureData2.Where(t => (t.PlaceId == placeId && t.AreaId == areaId)).OrderByDescending(t => t.Id).FirstOrDefault();
            //data.MonitoredDateTime = data2.MonitoredDateTime;
            //data.MonitoredDate = DateTime.Now;
            //data.MonitoredTime = DateTime.Now.TimeOfDay;
            //data.PlaceId = data2.PlaceId;
            //data.AreaId = data2.AreaId;
            //data.R1 = data2.R1;
            //data.R2 = data2.R2;
            //data.R3 = data2.R3;
            //data.R4 = data2.R4;
            //data.R5 = data2.R5;
            //data.R6 = data2.R6;
            //data.R7 = data2.R7;
            //data.R8 = data2.R8;
            //data.R9 = data2.R9;
            //data.R10 = data2.R10;

            data = entities.TemperatureDatas.Where(t => (t.PlaceId == placeId && t.AreaId == areaId)).OrderByDescending(t => t.MonitoredDateTime).FirstOrDefault();

			return data;
		}

	    public DateTime GetLastDateTime()
	    {
		    DateTime datetime = new DateTime();

		    TemperatureData tempData = GetLast();
		    if (tempData != null && tempData.MonitoredDateTime != null)
			    datetime = (DateTime)GetLast().MonitoredDateTime;

		    return datetime;
	    }

        public void Add(TemperatureData temperatureData)
        {
            if (temperatureData == null) return;

            entities.TemperatureDatas.Add(temperatureData);
            entities.SaveChanges();
        }

        public void Add(List<TemperatureData> temperatureDataList)
        {
            if (temperatureDataList == null) return;

            foreach(TemperatureData temparatureData in temperatureDataList)
            {
                entities.TemperatureDatas.Add(temparatureData);
            }

            entities.SaveChanges();
        }
        
    }

}
