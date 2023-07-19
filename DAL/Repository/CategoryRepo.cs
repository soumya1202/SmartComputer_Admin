using DataAccessLayer.DbProcedure.SQLFactory;
using EntityLayer;
using EntityLayer.Category;
using Persistence.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class CategoryRepo
    {
        public List<FetchCategory> FetchCategories()
        {
            var result = SQLDbHelpers.DataTableResultAsModelList<FetchCategory>(null, SQLProcedureNames.PROC_FETCH_CATEGORY);
            return result;
        }
        public APIResponseModel CategoriesInsertUpdate(CategoryInputModel inputModel)
        {
            APIResponseModel result = new APIResponseModel();
            CategoryInputDBModel DBinputmodel = new CategoryInputDBModel(inputModel);
            if(DBinputmodel.IN_ID>0)
            {
                DBinputmodel.IN_ACTION = "UPDATE";
            }
            else
            {
                DBinputmodel.IN_ID= 0;
                DBinputmodel.IN_ACTION = "INSERT";
            }
            var result1 = SQLDbHelpers.SaveRecord<APIResponseBaseModel>(DBinputmodel, SQLProcedureNames.PROC_INSERTUPDATE_CATEGORY);
            result.ReturnCode= result1.ReturnCode;
            result.ReturnMessage= result1.ReturnMessage;
            return result;
        }
        public APIResponseModel CategoriesActiveDeactive(CategoryInputModel inputModel)
        {
            APIResponseModel result = new APIResponseModel();
            CategoryInputDBModel DBinputmodel = new CategoryInputDBModel(inputModel);
            
            DBinputmodel.IN_ACTION = "ACTIVE/INACTIVE";
            
            var result1 = SQLDbHelpers.SaveRecord<APIResponseBaseModel>(DBinputmodel, SQLProcedureNames.PROC_INSERTUPDATE_CATEGORY);
            result.ReturnCode = result1.ReturnCode;
            result.ReturnMessage = result1.ReturnMessage;
            return result;
        }

    }
}
