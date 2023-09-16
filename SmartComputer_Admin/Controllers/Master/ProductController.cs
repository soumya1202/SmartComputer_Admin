using BusinessLogicLayer.Master;
using EntityLayer.VarientOpt;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SmartComputer_Admin.Controllers.Master
{
    public class ProductController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IWebHostEnvironment Environment;
        public ProductController(IWebHostEnvironment environment)
        {
            //_httpContextAccessor = httpContextAccessor;
            Environment = environment;
        }
        public IActionResult Index()
        {
            return View("Views/Master/Product.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> ProductCrud()
        {
            APIResponseModel result = new APIResponseModel();
            var sa = new JsonSerializerSettings();
            try
            {
                var IN_Id = Convert.ToString(HttpContext.Request.Form["IN_Id"]);
                var IN_Name = Convert.ToString(HttpContext.Request.Form["IN_Name"]);
                //var IN_PrvImage = Convert.ToString(HttpContext.Request.Form["IN_PrvImage"]);
                //var IN_Image = HttpContext.Request.Form.Files;
                //var IN_sectionName = "testSection";
                //var IN_sectionId = 1;
                var identity = (System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity;
                // var createdBy = identity.Claims.FirstOrDefault(c => c.Type == "CreatedBy").Value;

                //var ImageName = IN_Image.Count > 0 ? string.Concat(Convert.ToString(Guid.NewGuid()) + "_" + IN_Image.FirstOrDefault().FileName) : null;
                //if (!string.IsNullOrWhiteSpace(ImageName))
                //{
                //    var path = Path.Combine(this.Environment.WebRootPath, "Upload", "Brand/", ImageName);

                //    using (FileStream stream = new FileStream(path, FileMode.Create))
                //    {
                //        await IN_Image.FirstOrDefault().CopyToAsync(stream);
                //        stream.Close();
                //    }
                //}
                VarientOptInputModel model = new VarientOptInputModel()
                {
                    Id = string.IsNullOrWhiteSpace(IN_Id) ? null : Convert.ToInt16(IN_Id),
                    OptionName = IN_Name,
                    //SectionId = IN_sectionId,
                    //SectionName = IN_sectionName,
                    CreatedBy = "abc@gmail.com",//createdBy
                    //ImagePath = string.IsNullOrWhiteSpace(ImageName) ? IN_PrvImage : ImageName
                };

                using (MasterBL _obj = new MasterBL())
                {
                    result = _obj.VarientOptCrud(model);
                }
                //return Json(result, sa);
                return Json(result);

            }
            catch (Exception ex)
            {
                //return Json(result, sa);
                throw ex;
            }
        }
    }
}
