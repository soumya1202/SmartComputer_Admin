using DAL.Repository;
using EntityLayer;
using EntityLayer.Category;
using Newtonsoft.Json;

namespace BusinessLogicLayer.Master
{
    public class MasterBL : IDisposable
    {
       
        #region category
        public MasterBL()
        {
            
        }
        public string accesstoken
        {
            get
            {
                return GetSessionValue("AuthToken");
            }

        }
        public APIResponseModel CategoryCrud(CategoryInputModel inputModel)
        {
            try
            {
                APIResponseModel result = new APIResponseModel();
                CategoryRepo _CategoryRepo = new CategoryRepo();
                result = _CategoryRepo.CategoriesInsertUpdate(inputModel);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public APIResponseModel CategoryActiveDeactive(CategoryActiveDeactiveInputModel inputModel)
        {
            try
            {
                APIResponseModel result = new APIResponseModel();

                APIResponseModel result = new APIResponseModel();
                CategoryRepo _CategoryRepo = new CategoryRepo();
                result = _CategoryRepo.CategoriesInsertUpdate(inputModel);
                return result;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public APIResponseModel GetCategory(int? Id)
        //{
        //    try
        //    {
        //        APIResponseModel result = new APIResponseModel();

        //        WebApiCallResult apiRes = WebApiRequest.GetRequest(WebApiNames.GetCategoryURL + "?Id=" + Id, accesstoken);

        //        if (apiRes != null)
        //        {
        //            result = new APIResponseModel()
        //            {
        //                IsSuccess = apiRes.IsSuccess,
        //                Message = apiRes.Message,
        //                ResponseResult = apiRes.ResponseResult
        //            };
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public APIResponseModel GetCategoryDataTable(CategoryDatatableInputModel model)
        {
            try
            {
                APIResponseModel result=new APIResponseModel();
                CategoryRepo _CategoryRepo = new CategoryRepo();
                result.Data = JsonConvert.SerializeObject( _CategoryRepo.FetchCategories());
                result.ReturnCode = "200";
                result.ReturnMessage = "Success";
                return result;
            }
            
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
       
        public string GetSessionValue(string sessionkey)
        {
            var sessionValue = string.Empty;
            //if (_httpContextAccessor.HttpContext.Session.TryGetValue(sessionkey, out byte[] value))
            //{
            //    sessionValue = System.Text.Encoding.UTF8.GetString(value);
            //}
            return sessionValue;
        }
        public void Dispose()
        {

        }
    }
}
