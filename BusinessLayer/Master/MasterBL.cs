﻿using DAL.Repository;
using EntityLayer;
using EntityLayer.Brand;
using EntityLayer.Category;
using EntityLayer.VarientAtr;
using EntityLayer.VarientOpt;
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
                //APIResponseModel result = new APIResponseModel();

                APIResponseModel result = new APIResponseModel();
                CategoryRepo _CategoryRepo = new CategoryRepo();
               result = _CategoryRepo.CategoriesActiveDeactive(inputModel);
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


        #region Brand
        public APIResponseModel BrandCrud(BrandInputModel inputModel)
        {
            try
            {
                APIResponseModel result = new APIResponseModel();
                BrandRepo _BrandRepo = new BrandRepo();
                result = _BrandRepo.BrandsInsertUpdate(inputModel);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public APIResponseModel GetBrandDataTable(BrandDatatableInputModel model)
        {
            try
            {
                APIResponseModel result = new APIResponseModel();
                BrandRepo _BrandRepo = new BrandRepo();
                result.Data = JsonConvert.SerializeObject(_BrandRepo.FetchBrand());
                result.ReturnCode = "200";
                result.ReturnMessage = "Success";
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public APIResponseModel BrandActiveDeactive(BrandActiveDeactiveInputModel inputModel)
        {
            try
            {
                //APIResponseModel result = new APIResponseModel();

                APIResponseModel result = new APIResponseModel();
                BrandRepo _BrandRepo = new BrandRepo();
                result = _BrandRepo.BrandsActiveDeactive(inputModel);
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Varient Attribute
        public APIResponseModel VarientAtrCrud(VarientAtrInputModel inputModel)
        {
            try
            {
                APIResponseModel result = new APIResponseModel();
                VarientAtrRepo _VarientAtrRepo = new VarientAtrRepo();
                result = _VarientAtrRepo.VarientAtrInsertUpdate(inputModel);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public APIResponseModel GetVarientAtrDataTable(VarientAtrDatatableInputModel model)
        {
            try
            {
                APIResponseModel result = new APIResponseModel();
                VarientAtrRepo _VarientAtrRepo = new VarientAtrRepo();
                result.Data = JsonConvert.SerializeObject(_VarientAtrRepo.FetchVarientAtr());
                result.ReturnCode = "200";
                result.ReturnMessage = "Success";
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public APIResponseModel VarientAtrActiveDeactive(VarientAtrActiveDeactiveInputModel inputModel)
        {
            try
            {
                //APIResponseModel result = new APIResponseModel();

                APIResponseModel result = new APIResponseModel();
                VarientAtrRepo _VarientAtrRepo = new VarientAtrRepo();
                result = _VarientAtrRepo.VarientAtrActiveDeactive(inputModel);
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Varient Option
        public APIResponseModel VarientOptCrud(VarientOptInputModel inputModel)
        {
            try
            {
                APIResponseModel result = new APIResponseModel();
                VarientOptRepo _VarientOptRepo = new VarientOptRepo();
                result = _VarientOptRepo.VarientOptInsertUpdate(inputModel);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public APIResponseModel GetVarientOptDataTable(VarientOptDatatableInputModel model)
        {
            try
            {
                APIResponseModel result = new APIResponseModel();
                VarientOptRepo _VarientOptRepo = new VarientOptRepo();
                result.Data = JsonConvert.SerializeObject(_VarientOptRepo.FetchVarientOpt());
                result.ReturnCode = "200";
                result.ReturnMessage = "Success";
                return result;
            }

            catch (Exception ex)
            {
                 throw ex;
            }
        }
        public APIResponseModel VarientOptActiveDeactive(VarientOptActiveDeactiveInputModel inputModel)
        {
            try
            {
                //APIResponseModel result = new APIResponseModel();

                APIResponseModel result = new APIResponseModel();
                VarientOptRepo _VarientOptRepo = new VarientOptRepo();
                result = _VarientOptRepo.VarientOptActiveDeactive(inputModel);
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Product
        public APIResponseModel ProductCrud(VarientOptInputModel inputModel)
        {
            try
            {
                APIResponseModel result = new APIResponseModel();
                VarientOptRepo _VarientOptRepo = new VarientOptRepo();
                result = _VarientOptRepo.VarientOptInsertUpdate(inputModel);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public APIResponseModel ProductDataTable(VarientOptDatatableInputModel model)
        {
            try
            {
                APIResponseModel result = new APIResponseModel();
                VarientOptRepo _VarientOptRepo = new VarientOptRepo();
                result.Data = JsonConvert.SerializeObject(_VarientOptRepo.FetchVarientOpt());
                result.ReturnCode = "200";
                result.ReturnMessage = "Success";
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public APIResponseModel ProductActiveDeactive(VarientOptActiveDeactiveInputModel inputModel)
        {
            try
            {
                //APIResponseModel result = new APIResponseModel();

                APIResponseModel result = new APIResponseModel();
                VarientOptRepo _VarientOptRepo = new VarientOptRepo();
                result = _VarientOptRepo.VarientOptActiveDeactive(inputModel);
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
