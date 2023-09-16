using DataAccessLayer.DbProcedure.SQLFactory;
using EntityLayer.VarientAtr;
using EntityLayer;
using Persistence.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Brand;
//using EntityLayer.Brand;

namespace DAL.Repository
{
    public class VarientAtrRepo
    {
        public List<FetchVarientAtr> FetchVarientAtr()
        {
            var result = SQLDbHelpers.DataTableResultAsModelList<FetchVarientAtr>(null, SQLProcedureNames.PROC_FETCH_VARIENTATR);
            return result;
        }
        public APIResponseModel VarientAtrInsertUpdate(VarientAtrInputModel inputModel)
        {
            APIResponseModel result = new APIResponseModel();
            VarientAtrInputDBModel DBinputmodel = new VarientAtrInputDBModel(inputModel);
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
        public APIResponseModel VarientAtrActiveDeactive(VarientAtrActiveDeactiveInputModel inputModel)
        {
            APIResponseModel result = new APIResponseModel();
            VarientAtrInputDBModel DBinputmodel = new VarientAtrInputDBModel(inputModel);

            DBinputmodel.IN_ACTION = "ACTIVE/INACTIVE";

            var result1 = SQLDbHelpers.SaveRecord<APIResponseBaseModel>(DBinputmodel, SQLProcedureNames.PROC_ACTIVEDACTIVE_VARIENTATR);
            result.ReturnCode = result1.ReturnCode;
            result.ReturnMessage = result1.ReturnMessage;
            return result;
        }
    }
}
