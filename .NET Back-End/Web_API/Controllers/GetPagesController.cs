using Microsoft.AspNetCore.Mvc;
using Web_API.Dto.GetPages;
using Web_API.Entities;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("api/getpages")]
    public class GetPagesController : ControllerBase
    {
        [HttpGet]
        public List<GetPagesResponse> GetPages()
        {
            using (OmgContext db = new OmgContext())
            {
                var pages = db.OPages.ToList();
                List<GetPagesResponse> Pages = new List<GetPagesResponse>();
                foreach (var page in pages)
                {
                    var getPage = new GetPagesResponse()
                    {
                        page_id = page.PageId,
                        page_title = page.PageTitle,
                        page_description = page.PageDescription
                    };
                    Pages.Add(getPage);
                }
                return Pages;
            }
        }
    }
}
