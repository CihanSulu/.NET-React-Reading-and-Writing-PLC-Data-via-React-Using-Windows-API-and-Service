using Microsoft.AspNetCore.Mvc;
using Web_API.Dto.GetFilter;
using Web_API.Entities;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("api/getsqldata")]
    public class GetPlcSqlDataController : ControllerBase
    {
        [HttpPost]
        public IActionResult GetSqlData([FromBody] GetFilterRequest req)
        {
            using (OmgContext db = new OmgContext())
            {
                List<OData> getDatas;

                DateTime? startDate = null;
                DateTime? endDate = null;

                if (!string.IsNullOrEmpty(req.date1))
                    startDate = DateTime.Parse(req.date1);

                if (!string.IsNullOrEmpty(req.date2))
                    endDate = DateTime.Parse(req.date2);


                if (!req.last50)
                {
                    var query = db.ODatas.Where(x => x.PageId == req.page);

                    if (startDate.HasValue && endDate.HasValue)
                        query = query.Where(x => x.Exportdate >= startDate.Value && x.Exportdate <= endDate.Value);

                    if (req.number1.HasValue && req.number2.HasValue)
                        query = query.Where(x => x.DokumNo >= req.number1 && x.DokumNo <= req.number2);

                    getDatas = query.OrderByDescending(x => x.DataId).ToList();
                }
                else
                    getDatas = db.ODatas.Where(x => x.PageId == req.page).OrderByDescending(x => x.DataId).Take(50).ToList();

                return Ok(getDatas);
            }
        }


        [HttpPut]
        public IActionResult GetSqlData([FromBody] UpdateFilterRequest req)
        {
            using (OmgContext db = new OmgContext())
            {
                try
                {
                    var data = db.ODatas.Where(x => x.DataId == req.dataId).FirstOrDefault();
                    data.GenelAriza = req.genel_ariza;
                    data.IsletmeAriza = req.isletme_ariza;
                    data.ElektrikAriza = req.elektrik_ariza;
                    data.MekanikAriza = req.mekanik_ariza;
                    db.SaveChanges();
                    var result = new { message = "Duraklama başarıyla güncellendi!", success = true };
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    var result = new { message = "Duraklama güncellenirken hata yaşandı!", success = false };
                    return Ok(result);
                }
            }
        }

    }




}
