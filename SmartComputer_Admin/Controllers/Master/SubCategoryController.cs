using BusinessLogicLayer.Master;
using EntityLayer.Category;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using EntityLayer.Master;

namespace Flowrista_Admin.Controllers.Master
{
    public class SubCategoryController : Controller
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public SubCategoryController()
        {
           // _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View("Views/Master/SubCategory.cshtml");
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

            SubCategoryDatatableInputModel dataTableparam = new SubCategoryDatatableInputModel()
            {
                IN_LIMITINDEX = 10,
                IN_ORDERCOLUMN = 1, //!string.IsNullOrWhiteSpace(order) ? Convert.ToInt16(order) : 0,
                IN_ORDERDIR = "Dec",
                IN_STARTINDEX = 1,
                IN_NAME = name
            };

            //using (MasterBL objBL = new MasterBL(_httpContextAccessor))
            //{
            //    result = objBL.GetCategoryDataTable(dataTableparam);
            //}
            result.Data = "[\r\n  {\r\n    \"Id\": 3,\"CategoryName\": \"SubCategory1\", \"ImagePath\": \"837efeab-f05b-4b65-b4b3-8ca9b4c329b2_Med_Bill1.jpeg\",\"IsActive\": true\r\n    \r\n  },\r\n  {\r\n    \"Id\": 4,\"CategoryName\": \"SubCategory2\",\"ImagePath\": \"dd1ab639-2869-47e3-8237-0a88d59e4a51_Med_Bill1.jpeg\",\"IsActive\": true\r\n    \r\n  },\r\n  {\r\n    \"Id\": 5,\"CategoryName\": \"SubCategory3\",\"ImagePath\": \"60628172-e7ef-425e-b996-f8d2fea0cc6a_USG.jpeg\",\"IsActive\": true    \r\n  },\r\n  {\r\n    \"Id\": 6,\"CategoryName\": \"SubCategory4\", \"ImagePath\": \"e4fa1dca-e694-4ace-af1f-5cfe940399d9_Doctor Visit.jpeg\",\"IsActive\": false,\r\n    \r\n  }\r\n]  ";
            result.ReturnCode = "200";
            result.ReturnMessage = "Success";
            var dataTableData = new List<SubCategoryDataTableViewmodel>();
            if (result != null && result.Data != null)
                dataTableData = (Newtonsoft.Json.JsonConvert.DeserializeObject<List<SubCategoryDataTableViewmodel>>(Convert.ToString(result.Data)));

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
    }
}
