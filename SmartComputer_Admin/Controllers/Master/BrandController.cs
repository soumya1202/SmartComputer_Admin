using BusinessLogicLayer.Master;
using EntityLayer.Brand;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using EntityLayer.Category;

namespace SmartComputer_Admin.Controllers.Master
{
    public class BrandController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IWebHostEnvironment Environment;
        public BrandController(IWebHostEnvironment environment)
        {
            //_httpContextAccessor = httpContextAccessor;
            Environment = environment;


        }
        public IActionResult Index()
        {
            return View("Views/Master/Brand.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> BrandCrud()
        {
            APIResponseModel result = new APIResponseModel();
            var sa = new JsonSerializerSettings();
            try
            {
                var IN_Id = Convert.ToString(HttpContext.Request.Form["IN_Id"]);
                var IN_Name = Convert.ToString(HttpContext.Request.Form["IN_Name"]);
                var IN_PrvImage = Convert.ToString(HttpContext.Request.Form["IN_PrvImage"]);
                var IN_Image = HttpContext.Request.Form.Files;
                //var IN_sectionName = "testSection";
                //var IN_sectionId = 1;
                var identity = (System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity;
                // var createdBy = identity.Claims.FirstOrDefault(c => c.Type == "CreatedBy").Value;

                var ImageName = IN_Image.Count > 0 ? string.Concat(Convert.ToString(Guid.NewGuid()) + "_" + IN_Image.FirstOrDefault().FileName) : null;
                if (!string.IsNullOrWhiteSpace(ImageName))
                {
                    var path = Path.Combine(this.Environment.WebRootPath, "Upload", "Brand/", ImageName);

                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await IN_Image.FirstOrDefault().CopyToAsync(stream);
                        stream.Close();
                    }
                }
                BrandInputModel model = new BrandInputModel()
                {
                    Id = string.IsNullOrWhiteSpace(IN_Id) ? null : Convert.ToInt16(IN_Id),
                    BrandName = IN_Name,
                    //SectionId = IN_sectionId,
                    //SectionName = IN_sectionName,
                    CreatedBy = "abc@gmail.com",//createdBy
                    ImagePath = string.IsNullOrWhiteSpace(ImageName) ? IN_PrvImage : ImageName
                };

                using (MasterBL _obj = new MasterBL())
                {
                    result = _obj.BrandCrud(model);
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
        public JsonResult BrandDataTable()
        {

            APIResponseModel result = new APIResponseModel();
            var sa = new JsonSerializerSettings();
            //string draw =  Request.Form["draw"][0];
            //string order = Request.Form["order[0][column]"][0];
            //string orderDir = Request.Form["order[0][dir]"][0];
            //int startIndex = Convert.ToInt32(Request.Form["start"][0]);
            //int limit = Convert.ToInt32(Request.Form["length"][0]);
            //string searchkey = Request.Form["search[value]"][0] == "" ? null : Request.Form["search[value]"][0];
            //Custom column search fields
            string name = "";// !string.IsNullOrWhiteSpace(Convert.ToString(Request.Form["columns[1][search][value]"].FirstOrDefault())) ? Request.Form["columns[1][search][value]"].FirstOrDefault() : null;
            //string description = !string.IsNullOrWhiteSpace(Convert.ToString(Request.Form["columns[2][search][value]"].FirstOrDefault())) ? Request.Form["columns[2][search][value]"].FirstOrDefault() : null;

            BrandDatatableInputModel dataTableparam = new BrandDatatableInputModel()
            {
                IN_LIMITINDEX = 10,
                IN_ORDERCOLUMN = 1, //!string.IsNullOrWhiteSpace(order) ? Convert.ToInt16(order) : 0,
                IN_ORDERDIR = "Dec",
                IN_STARTINDEX = 1,
                IN_NAME = name
            };

            using (MasterBL objBL = new MasterBL())
            {
                result = objBL.GetBrandDataTable(dataTableparam);
            }
            var dataTableData = new List<BrandDataTableViewmodel>();
            if (result != null && result.Data != null)
                dataTableData = (Newtonsoft.Json.JsonConvert.DeserializeObject<List<BrandDataTableViewmodel>>(Convert.ToString(result.Data)));

            long totalRecoreCount = 0;

            if (dataTableData != null && dataTableData.FirstOrDefault() != null)
            {
                //totalRecoreCount = Convert.ToInt64(dataTableData[0].TOTALCOUNT);
                totalRecoreCount = Convert.ToInt64(dataTableData.Count());
            }

            dynamic newtonresult = new
            {
                status = "success",
                draw = 0,//Convert.ToInt32(draw == "" ? "0" : draw),
                recordsTotal = totalRecoreCount,
                recordsFiltered = totalRecoreCount,
                data = dataTableData == null ? null : dataTableData
            };

            return Json(dataTableData);
        }
        [HttpPost]
        public IActionResult BrandActiveDeactive()
        {
            APIResponseModel result = new APIResponseModel();
            var sa = new JsonSerializerSettings();
            try
            {
                var IN_Id = Convert.ToString(HttpContext.Request.Form["IN_Id"]);
                var IN_isActive = Convert.ToBoolean(HttpContext.Request.Form["IN_IsActive"]);
                var IN_Reason = Convert.ToString(HttpContext.Request.Form["IN_Reason"]);

                var identity = (System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity;
                var createdBy = identity.Claims.FirstOrDefault(c => c.Type == "CreatedBy").Value;

                BrandActiveDeactiveInputModel model = new BrandActiveDeactiveInputModel()
                {
                    BrandId = Convert.ToInt16(IN_Id),
                    IsActive = IN_isActive,
                    DeletedBy = "abc@gmail.com",//createdBy
                    DeletionReason = IN_Reason// IN_Reason
                };

                using (MasterBL _obj = new MasterBL())
                {
                    result = _obj.BrandActiveDeactive(model);
                }
                //return Json(result, sa);
                return Json(result);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
