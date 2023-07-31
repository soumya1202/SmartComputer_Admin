using DataAccessLayer.DbProcedure.SQLFactory;
using EntityLayer.Brand;
using EntityLayer;
using Persistence.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Category;

namespace DAL.Repository
{
    public class BrandRepo
    {
        public List<FetchBrand> FetchBrand()
        {
            var result = SQLDbHelpers.DataTableResultAsModelList<FetchBrand>(null, SQLProcedureNames.PROC_FETCH_BRAND);
            return result;
        }
        public APIResponseModel BrandsInsertUpdate(BrandInputModel inputModel)
        {
            APIResponseModel result = new APIResponseModel();
            BrandInputDBModel DBinputmodel = new BrandInputDBModel(inputModel);
            if (DBinputmodel.IN_ID > 0)
            {
                DBinputmodel.IN_ACTION = "UPDATE";
            }
            else
            {
                DBinputmodel.IN_ID = 0;
                DBinputmodel.IN_ACTION = "INSERT";
            }
            var result1 = SQLDbHelpers.SaveRecord<APIResponseBaseModel>(DBinputmodel, SQLProcedureNames.PROC_INSERTUPDATE_BRAND);
            result.ReturnCode = result1.ReturnCode;
            result.ReturnMessage = result1.ReturnMessage;
            return result;
        }
        public APIResponseModel BrandsActiveDeactive(BrandActiveDeactiveInputModel inputModel)
        {
            APIResponseModel result = new APIResponseModel();
            BrandInputDBModel DBinputmodel = new BrandInputDBModel(inputModel);

            DBinputmodel.IN_ACTION = "ACTIVE/INACTIVE";

            var result1 = SQLDbHelpers.SaveRecord<APIResponseBaseModel>(DBinputmodel, SQLProcedureNames.PROC_INSERTUPDATE_BRAND);
            result.ReturnCode = result1.ReturnCode;
            result.ReturnMessage = result1.ReturnMessage;
            return result;
        }
    }
    
}
