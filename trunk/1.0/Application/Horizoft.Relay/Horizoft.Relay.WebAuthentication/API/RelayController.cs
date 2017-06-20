using Horizoft.Relay.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;
using Horizoft.Relay.DAL;

namespace Horizoft.Relay.Web.API
{
    [Authorize]
    public class RelayController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetReference()
        {
            string url = ConfigurationManager.AppSettings["URL"];

            return GetReference(url);

        }
        
        [HttpGet]
        public IHttpActionResult GetReference(string hotsUrl)
        {
            try
            {
                IoTReferenceParser iotRefParser = new IoTReferenceParser();
                IoTReference iotRef = iotRefParser.Get(hotsUrl);

                return Ok(iotRef);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpGet]
        public IHttpActionResult GetCurrentState()
        {
            IoTTransactionParser iotTransactionParser = new IoTTransactionParser();

            try
            {
                IoTTransaction iotTrs = iotTransactionParser.GetCurrentState();

                return Ok(iotTrs);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        public IHttpActionResult GetCurrentState(string hostUrl)
        {
            IoTTransactionParser iotTransactionParser = new IoTTransactionParser(hostUrl);

            try
            {
                IoTTransaction iotTrs = iotTransactionParser.GetCurrentState();

                return Ok(iotTrs);

            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
