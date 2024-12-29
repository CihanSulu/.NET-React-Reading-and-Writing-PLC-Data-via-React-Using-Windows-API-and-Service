using Microsoft.AspNetCore.Mvc;
using Web_API.Dto.Error;
using Web_API.Dto.GetPlc;
using Web_API.Entities;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("api/errors")]
    public class ErrorController : ControllerBase
    {

        [HttpGet]
        public IActionResult AddStop(int page, float genel, float mekanik, float elektrik, float isletme)
        {
            try
            {
                using (OmgContext db = new OmgContext())
                {
                    // Hangi sayfaların eşleştiğini belirle
                    var relatedPages = new List<int>();
                    if (page == 1 || page == 2)
                    {
                        relatedPages = new List<int> { 1, 2 };
                    }
                    else if (page == 3 || page == 4)
                    {
                        relatedPages = new List<int> { 3, 4 };
                    }

                    foreach (var relatedPage in relatedPages)
                    {
                        var getError = db.OErrors.Where(x => x.PageId == relatedPage).FirstOrDefault();
                        if (getError == null)
                        {
                            var newData = new OError()
                            {
                                PageId = relatedPage,
                                GenelAriza = genel,
                                MekanikAriza = mekanik,
                                ElektrikAriza = elektrik,
                                IsletmeAriza = isletme
                            };
                            db.OErrors.Add(newData);
                        }
                        else
                        {
                            getError.GenelAriza = genel;
                            getError.IsletmeAriza = isletme;
                            getError.MekanikAriza = mekanik;
                            getError.ElektrikAriza = elektrik;
                        }
                    }

                    // Güncelleme durumunu işaretle
                    var updated = db.OErrorsUpdates.FirstOrDefault();
                    if (updated != null)
                    {
                        updated.Updated = 1;
                    }

                    db.SaveChanges();
                }

                var result = new { message = "Duraklama başarıyla eklendi!", success = true };
                return Ok(result);
            }
            catch (Exception e)
            {
                var result = new { message = e.Message, success = false };
                return Ok(result);
            }
        }


        [HttpPost]
        public GetErrorResponse GetStop([FromBody] GetErrorRequest req)
        {
            using (OmgContext db = new OmgContext())
            {
                var getError = db.OErrors.Where(x => x.PageId == req.page_id).FirstOrDefault();
                if (getError != null)
                {
                    return new GetErrorResponse()
                    {
                        genel_ariza = Convert.ToDouble(getError.GenelAriza),
                        elektrik_ariza = Convert.ToDouble(getError.ElektrikAriza),
                        isletme_ariza = Convert.ToDouble(getError.IsletmeAriza),
                        mekanik_ariza = Convert.ToDouble(getError.MekanikAriza)
                    };
                }
                else
                {
                    return new GetErrorResponse()
                    {
                        genel_ariza = 0.0,
                        elektrik_ariza = 0.0,
                        isletme_ariza = 0.0,
                        mekanik_ariza = 0.0
                    };
                }
            }

        }



    }
}
