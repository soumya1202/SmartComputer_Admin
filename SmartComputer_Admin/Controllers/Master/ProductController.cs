﻿using BusinessLogicLayer.Master;
using EntityLayer.VarientOpt;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static EntityLayer.Product.ProductEL;

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
                var IN_PrvImage = Convert.ToString(HttpContext.Request.Form["IN_PrvImage"]);
                var IN_Image = HttpContext.Request.Form.Files;
                //var IN_sectionName = "testSection";
                //var IN_sectionId = 1;
                var identity = (System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity;
                // var createdBy = identity.Claims.FirstOrDefault(c => c.Type == "CreatedBy").Value;

                var ImageName = IN_Image.Count > 0 ? string.Concat(Convert.ToString(Guid.NewGuid()) + "_" + IN_Image.FirstOrDefault().FileName) : null;
                if (!string.IsNullOrWhiteSpace(ImageName))
                {
                    var path = Path.Combine(this.Environment.WebRootPath, "Upload", "Product/", ImageName);

                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await IN_Image.FirstOrDefault().CopyToAsync(stream);
                        stream.Close();
                    }
                }
                ProductInputModel model = new ProductInputModel()
                {
                    Id = string.IsNullOrWhiteSpace(IN_Id) ? null : Convert.ToInt16(IN_Id),
                    ProductName = IN_Name,
                    //SectionId = IN_sectionId,
                    //SectionName = IN_sectionName,
                    CreatedBy = "abc@gmail.com",//createdBy
                    //ImagePath = string.IsNullOrWhiteSpace(ImageName) ? IN_PrvImage : ImageName
                };

                using (MasterBL _obj = new MasterBL())
                {
                    result = _obj.ProductCrud(model);
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
        public JsonResult ProductDataTable()
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

            ProductDatatableInputModel dataTableparam = new ProductDatatableInputModel()
            {
                IN_LIMITINDEX = 10,
                IN_ORDERCOLUMN = 1, //!string.IsNullOrWhiteSpace(order) ? Convert.ToInt16(order) : 0,
                IN_ORDERDIR = "Dec",
                IN_STARTINDEX = 1,
                IN_NAME = name
            };

            using (MasterBL objBL = new MasterBL())
            {
                result = objBL.GetProductDataTable(dataTableparam);
            }
            var dataTableData = new List<ProductDataTableViewmodel>();
            if (result != null && result.Data != null)
                dataTableData = (Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductDataTableViewmodel>>(Convert.ToString(result.Data)));

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

                ProductActiveDeactiveInputModel model = new ProductActiveDeactiveInputModel()
                {
                    ProductId = Convert.ToInt16(IN_Id),
                    IsActive = IN_isActive,
                    DeletedBy = "abc@gmail.com",//createdBy
                    DeletionReason = IN_Reason// IN_Reason
                };

                using (MasterBL _obj = new MasterBL())
                {
                    result = _obj.ProductActiveDeactive(model);
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
