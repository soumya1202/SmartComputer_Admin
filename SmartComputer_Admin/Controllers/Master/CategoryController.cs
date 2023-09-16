﻿using BusinessLogicLayer.Master;
using EntityLayer;
using EntityLayer.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SmartComputer_Admin.Controllers.Master
{
    public class CategoryController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IWebHostEnvironment Environment; 
        public CategoryController( IWebHostEnvironment environment)
        {
            //_httpContextAccessor = httpContextAccessor;
            Environment = environment;
             

        }
        [Route("Index")]
        public IActionResult Index()
        {

            return View("Views/Master/Category.cshtml");
        }


        [HttpPost]
        public async Task<IActionResult> CategoryCrud(CategoryInputModel _CategoryInputModel)
        {
            APIResponseModel result = new APIResponseModel();
            var sa = new JsonSerializerSettings();
            try
            {
               
                  var identity = (System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity;
                // var createdBy = identity.Claims.FirstOrDefault(c => c.Type == "CreatedBy").Value;
                IFormFileCollection IN_Image = HttpContext.Request.Form.Files;
                var ImageName = _CategoryInputModel.Image.Count>0? string.Concat(Convert.ToString(Guid.NewGuid()) + "_" + _CategoryInputModel.Image.FirstOrDefault().FileName) :null;
                if (!string.IsNullOrWhiteSpace(ImageName))
                {
                    var path = Path.Combine(this.Environment.WebRootPath, "Upload", "Category/", ImageName);

                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                       await _CategoryInputModel.Image.FirstOrDefault().CopyToAsync(stream);
                        stream.Close();
                    }
                }
                _CategoryInputModel.ImagePath = string.IsNullOrWhiteSpace(ImageName) ? _CategoryInputModel.PrevImage : ImageName;
                

                using (MasterBL _obj = new MasterBL())
                {
                    result = _obj.CategoryCrud(_CategoryInputModel);
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

        public JsonResult CategoryDataTable()
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

            CategoryDatatableInputModel dataTableparam = new CategoryDatatableInputModel()
            {
                IN_LIMITINDEX = 10,
                IN_ORDERCOLUMN = 1, //!string.IsNullOrWhiteSpace(order) ? Convert.ToInt16(order) : 0,
                IN_ORDERDIR = "Dec",
                IN_STARTINDEX = 1,
                IN_NAME = name
            };

            using (MasterBL objBL = new MasterBL())
            {
                result = objBL.GetCategoryDataTable(dataTableparam);
            }
            var dataTableData = new List<CategoryDataTableViewmodel>();
            if (result != null && result.Data != null)
                dataTableData = (Newtonsoft.Json.JsonConvert.DeserializeObject<List<CategoryDataTableViewmodel>>(Convert.ToString(result.Data)));

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
        public IActionResult CategoryActiveDeactive(CategoryActiveDeactiveInputModel _inputmodel)
        {
            APIResponseModel result = new APIResponseModel();
            var sa = new JsonSerializerSettings();
            try
            {
                //var IN_Id = Convert.ToString(HttpContext.Request.Form["IN_Id"]);
                //var IN_isActive = Convert.ToBoolean(HttpContext.Request.Form["IN_IsActive"]);
                //var IN_Reason = Convert.ToString(HttpContext.Request.Form["IN_Reason"]);

                //var identity = (System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity;
                //var createdBy = identity.Claims.FirstOrDefault(c => c.Type == "CreatedBy").Value;

                //CategoryActiveDeactiveInputModel model = new CategoryActiveDeactiveInputModel()
                //{
                //    CategoryId =  Convert.ToInt16(IN_Id),
                //    IsActive = IN_isActive,
                //    DeletedBy = "abc@gmail.com",//createdBy
                //    DeletionReason = IN_Reason// IN_Reason
                //};

                using (MasterBL _obj = new MasterBL())
                {
                    result = _obj.CategoryActiveDeactive(_inputmodel);
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
