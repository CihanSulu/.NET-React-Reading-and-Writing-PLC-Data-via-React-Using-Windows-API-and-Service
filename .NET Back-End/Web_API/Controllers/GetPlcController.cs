using Microsoft.AspNetCore.Mvc;
using S7;
using S7.Net;
using Web_API.Dto.GetPlc;
using Web_API.Entities;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("api/getplcs")]
    public class GetPlcController : ControllerBase
    {
        [HttpGet]
        public List<GetPlcResponse> GetPlc()
        {
            using (OmgContext db = new OmgContext())
            {
                var oplcsList = db.OPlcs.ToList();
                List<GetPlcResponse> getPlcDatas = new List<GetPlcResponse>();
                foreach (var plc in oplcsList)
                {
                    var newPlcRequest = new GetPlcResponse()
                    {
                        Plc_Id = plc.Id,
                        Plc_Name = plc.PlcName,
                        Plc_Ip = plc.PlcIp

                    };
                    getPlcDatas.Add(newPlcRequest);
                }
                return getPlcDatas;
            };
        }

        public class PlcRequest
        {
            public int Plc_ID { get; set; }
        }

        [HttpPost]
        public GetPlcPostResponse GetPlc([FromBody] PlcRequest request)
        {
            using (OmgContext db = new OmgContext())
            {

                var getPlc = db.OPlcs.Where(x => x.Id == request.Plc_ID).FirstOrDefault();

                if (getPlc != null)
                {

                    Plc plcObj = null;
                    bool connected = false;

                    try
                    {
                        plcObj = new Plc(CpuType.S71500, getPlc.PlcIp, Convert.ToByte(getPlc.PlcRack), Convert.ToByte(getPlc.PlcSlot));
                        plcObj.Open();
                        connected = plcObj.IsConnected;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"PLC bağlantısı kurulurken hata oluştu: {ex.Message}");
                        connected = false;
                    }

                    var newPlcRequest = new GetPlcPostResponse()
                    {
                        Plc_Name = getPlc.PlcName,
                        Plc_Ip = getPlc.PlcIp,
                        Plc_Status = connected
                    };

                    plcObj?.Close();
                    return newPlcRequest;
                }
                else
                {
                    return new GetPlcPostResponse
                    {
                        Plc_Name = "Not Found",
                        Plc_Ip = "Not Found",
                        Plc_Status = false
                    };
                }

            }
        }





    }
}
