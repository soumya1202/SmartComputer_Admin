using DataAccessLayer.DbProcedure.SQLFactory;
using EntityLayer.Brand;
using EntityLayer;
using Persistence.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EntityLayer.Product.ProductEL;

namespace DAL.Repository
{
    public class ProductRepo
    {
        public List<FetchProduct> FetchProduct()
        {
            var result = SQLDbHelpers.DataTableResultAsModelList<FetchProduct>(null, SQLProcedureNames.PROC_FETCH_BRAND);
            return result;
        }
        public APIResponseModel ProductInsertUpdate(ProductInputModel inputModel)
        {
            APIResponseModel result = new APIResponseModel();
            ProductInputDBModel DBinputmodel = new ProductInputDBModel(inputModel);
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
        public APIResponseModel ProductActiveDeactive(ProductActiveDeactiveInputModel inputModel)
        {
            APIResponseModel result = new APIResponseModel();
            ProductInputDBModel DBinputmodel = new ProductInputDBModel(inputModel);

            DBinputmodel.IN_ACTION = "ACTIVE/INACTIVE";

            var result1 = SQLDbHelpers.SaveRecord<APIResponseBaseModel>(DBinputmodel, SQLProcedureNames.PROC_INSERTUPDATE_BRAND);
            result.ReturnCode = result1.ReturnCode;
            result.ReturnMessage = result1.ReturnMessage;
            return result;
        }
    }
}
