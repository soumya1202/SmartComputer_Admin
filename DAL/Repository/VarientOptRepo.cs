using DataAccessLayer.DbProcedure.SQLFactory;
using EntityLayer.VarientOpt;
using EntityLayer;
using Persistence.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class VarientOptRepo
    {
        public List<FetchVarientOpt> FetchVarientOpt()
        {
            var result = SQLDbHelpers.DataTableResultAsModelList<FetchVarientOpt>(null, SQLProcedureNames.PROC_FETCH_ITEM_VARIENT_OPTION);
            return result;
        }
        public APIResponseModel VarientOptInsertUpdate(VarientOptInputModel inputModel)
        {
            APIResponseModel result = new APIResponseModel();
            VarientOptInputDBModel DBinputmodel = new VarientOptInputDBModel(inputModel);
            if (DBinputmodel.IN_ID > 0)
            {
                DBinputmodel.IN_ACTION = "UPDATE";
            }
            else
            {
                DBinputmodel.IN_ID = 0;
                DBinputmodel.IN_ACTION = "INSERT";
            }
            var result1 = SQLDbHelpers.SaveRecord<APIResponseBaseModel>(DBinputmodel, SQLProcedureNames.PROC_INSERTUPDATE_VARIENTATR);
            result.ReturnCode = result1.ReturnCode;
            result.ReturnMessage = result1.ReturnMessage;
            return result;
        }
        public APIResponseModel VarientOptActiveDeactive(VarientOptActiveDeactiveInputModel inputModel)
        {
            APIResponseModel result = new APIResponseModel();
            VarientOptInputDBModel DBinputmodel = new VarientOptInputDBModel(inputModel);

            DBinputmodel.IN_ACTION = "ACTIVE/INACTIVE";

            var result1 = SQLDbHelpers.SaveRecord<APIResponseBaseModel>(DBinputmodel, SQLProcedureNames.PROC_ACTIVEDACTIVE_VARIENTATR);
            result.ReturnCode = result1.ReturnCode;
            result.ReturnMessage = result1.ReturnMessage;
            return result;
        }
    }
}
