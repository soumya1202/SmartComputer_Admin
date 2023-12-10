using DataAccessLayer.DbProcedure.SQLFactory;
using EntityLayer.Category;
using EntityLayer;
using Persistence.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Master;

namespace DAL.Repository
{
    public class SubCategoryRepo
    {
        public List<FetchSubCategory> FetchSubCategories()
        {
            var result = SQLDbHelpers.DataTableResultAsModelList<FetchSubCategory>(null, SQLProcedureNames.PROC_FETCH_SUBCATEGORY);
            return result;
        }
        public APIResponseModel SubCategoriesInsertUpdate(SubCategoryInputModel inputModel)
        {
            APIResponseModel result = new APIResponseModel();
            SubCategoryInputDBModel DBinputmodel = new SubCategoryInputDBModel(inputModel);
            if (DBinputmodel.IN_ID > 0)
            {
                DBinputmodel.IN_ACTION = "UPDATE";
            }
            else
            {
                DBinputmodel.IN_ID = 0;
                DBinputmodel.IN_ACTION = "INSERT";
            }
            var result1 = SQLDbHelpers.SaveRecord<APIResponseBaseModel>(DBinputmodel, SQLProcedureNames.PROC_SUBCATEGORY_CUD);
            result.ReturnCode = result1.ReturnCode;
            result.ReturnMessage = result1.ReturnMessage;
            return result;
        }
        public APIResponseModel SubCategoriesActiveDeactive(SubCategoryActiveDeactiveInputModel inputModel)
        {
            APIResponseModel result = new APIResponseModel();
            SubCategoryInputDBModel DBinputmodel = new SubCategoryInputDBModel(inputModel);

            DBinputmodel.IN_ACTION = "ACTIVE/INACTIVE";

            var result1 = SQLDbHelpers.SaveRecord<APIResponseBaseModel>(DBinputmodel, SQLProcedureNames.PROC_SUBCATEGORY_CUD);
            result.ReturnCode = result1.ReturnCode;
            result.ReturnMessage = result1.ReturnMessage;
            return result;
        }
    }
}
