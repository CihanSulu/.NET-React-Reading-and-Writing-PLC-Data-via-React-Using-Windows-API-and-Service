using Microsoft.AspNetCore.Mvc;
using S7;
using S7.Net;
using Web_API.Dto.GetData;
using Web_API.Entities;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("api/getdatas")]
    public class GetPlcDataController : ControllerBase
    {
        [HttpPost]
        public List<GetDataResponse> GetDatas([FromBody] GetDataRequest req)
        {
            List<GetDataResponse> ResultResponse = new List<GetDataResponse>();
            using (OmgContext db = new OmgContext())
            {
                var getDataTypes = db.ODataTypes.Where(x => x.DataPage == req.page).ToList();

                foreach (var dataT in getDataTypes)
                {
                    Plc plc;
                    var getPlc = db.OPlcs.FirstOrDefault(x => x.Id == dataT.DataPlc);

                    if (getPlc != null)
                    {
                        plc = new Plc(CpuType.S71500, getPlc.PlcIp, Convert.ToByte(getPlc.PlcRack), Convert.ToByte(getPlc.PlcSlot));

                        try
                        {
                            plc.Open();
                            object value = null;

                            switch (dataT.DataType.ToLower())
                            {
                                case "bool":
                                    value = Convert.ToBoolean(plc.Read(dataT.DataAdress));
                                    break;
                                case "byte":
                                    value = Convert.ToByte(plc.Read(dataT.DataAdress));
                                    break;
                                case "word":
                                    value = Convert.ToUInt16(plc.Read(dataT.DataAdress));
                                    break;
                                case "dword":
                                    value = Convert.ToUInt32(plc.Read(dataT.DataAdress));
                                    break;
                                case "lword":
                                    value = Convert.ToUInt64(plc.Read(dataT.DataAdress));
                                    break;
                                case "sint":
                                    value = Convert.ToSByte(plc.Read(dataT.DataAdress));
                                    break;
                                case "int":
                                case "ınt":
                                    value = Convert.ToInt16(plc.Read(dataT.DataAdress));
                                    break;
                                case "dint":
                                    value = Convert.ToInt32(plc.Read(dataT.DataAdress));
                                    break;
                                case "usint":
                                    value = Convert.ToByte(plc.Read(dataT.DataAdress));
                                    break;
                                case "uint":
                                    value = Convert.ToUInt32(plc.Read(dataT.DataAdress));
                                    break;
                                case "udint":
                                    value = Convert.ToUInt32(plc.Read(dataT.DataAdress));
                                    break;
                                case "lint":
                                    value = Convert.ToInt64(plc.Read(dataT.DataAdress));
                                    break;
                                case "ulint":
                                    value = Convert.ToUInt64(plc.Read(dataT.DataAdress));
                                    break;
                                case "real":
                                    value = ((uint)plc.Read(dataT.DataAdress)).ConvertToFloat();
                                    break;
                                case "lreal":
                                    value = Convert.ToDouble(plc.Read(dataT.DataAdress));
                                    break;
                                case "s5time":
                                case "time":
                                    string _date = "";
                                    string[] result = dataT.DataAdress.Split(new string[] { ". " }, StringSplitOptions.RemoveEmptyEntries);
                                    for (int i = 0; i < result.Length; i++)
                                    {
                                        result[i] = result[i].Trim();
                                        var gecici = Convert.ToInt16(plc.Read(result[i])).ToString();
                                        if (gecici.Length == 1)
                                        {
                                            gecici = "0" + gecici;
                                        }
                                        _date += gecici;
                                        if (i < result.Length - 1)
                                        {
                                            _date += ":";
                                        }
                                    }
                                    value = _date;
                                    break;
                                case "datetime":
                                    string _date2 = "";
                                    string[] result2 = dataT.DataAdress.Split(new string[] { ". " }, StringSplitOptions.RemoveEmptyEntries);
                                    for (int i = 0; i < result2.Length; i++)
                                    {
                                        result2[i] = result2[i].Trim();
                                        var gecici = Convert.ToInt16(plc.Read(result2[i])).ToString();

                                        if (gecici.Length == 1)
                                        {
                                            gecici = "0" + gecici;
                                        }
                                        _date2 += gecici;
                                        if (i == 0 || i == 1)
                                        {
                                            _date2 += ".";
                                        }
                                        else if (i == 2)
                                        {
                                            _date2 += " ";
                                        }
                                        else if (i == 3 || i == 4)
                                        {
                                            _date2 += ":";
                                        }
                                    }
                                    value = _date2;
                                    break;
                                case "ltime":
                                    value = TimeSpan.FromMilliseconds(Convert.ToInt64(plc.Read(dataT.DataAdress)));
                                    break;
                                case "date":
                                    value = DateTime.Parse(plc.Read(dataT.DataAdress).ToString());
                                    break;
                                case "time_of_day":
                                    value = TimeSpan.Parse(plc.Read(dataT.DataAdress).ToString());
                                    break;
                                case "dtl":
                                    value = DateTime.Parse(plc.Read(dataT.DataAdress).ToString());
                                    break;
                                case "char":
                                    value = Convert.ToChar(plc.Read(dataT.DataAdress));
                                    break;
                                case "wchar":
                                    value = Convert.ToChar(plc.Read(dataT.DataAdress));
                                    break;
                                case "string":
                                    value = plc.Read(dataT.DataAdress).ToString();
                                    break;
                                case "wstring":
                                    value = plc.Read(dataT.DataAdress).ToString();
                                    break;
                                default:
                                    value = $"Unknown data type: {dataT.DataType}";
                                    break;
                            }

                            var dataResponse = new GetDataResponse()
                            {
                                data_title = dataT.DataName,
                                data_value = value,
                                data_description = dataT.DataDescription,
                                data_error = false
                            };

                            ResultResponse.Add(dataResponse);
                        }
                        catch (Exception ex)
                        {
                            ResultResponse.Add(new GetDataResponse()
                            {
                                data_title = dataT.DataName,
                                data_value = ex.Message,
                                data_description = dataT.DataDescription,
                                data_error = true

                            });
                        }
                        finally
                        {
                            plc.Close();
                        }
                    }
                    else
                    {
                        ResultResponse.Add(new GetDataResponse()
                        {
                            data_title = dataT.DataName,
                            data_value = "Plc Not Found",
                            data_description = dataT.DataDescription,
                            data_error = true

                        });
                    }
                }
            }

            return ResultResponse;
        }
    }
}
