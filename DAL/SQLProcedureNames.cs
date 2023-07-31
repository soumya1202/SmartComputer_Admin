namespace Persistence.Helper
{
    public class SQLProcedureNames
    {
        public const string PROC_CHECK_CUSTOMER_EMAILEXISTS = "PROC_CHECK_CUSTOMER_EMAILEXISTS";
        public const string PROC_REGISTER_CUSTOMER = "PROC_REGISTER_CUSTOMER";
        public const string PROC_GET_CUSTOMER_PASSWORD_INFO = "PROC_GET_CUSTOMER_PASSWORD_INFO";
        #region Category
        public const string PROC_FETCH_CATEGORY = "PROC_FETCH_CATEGORY";
        public const string PROC_INSERTUPDATE_CATEGORY = "PROC_INSERTUPDATE_CATEGORY";
        #endregion
        #region Sub Category
        public const string PROC_SUBCATEGORY_CUD = "PROC_SUBCATEGORY_CUD";
        public const string PROC_FETCH_SUBCATEGORY = "PROC_FETCH_SUBCATEGORY";
        #endregion
        #region Section
        public const string PROC_SECTION_CUD = "PROC_SECTION_CUD";
        public const string PROC_FETCH_SECTION = "PROC_FETCH_SECTION";
        #endregion
        #region Brand
        public const string PROC_FETCH_BRAND = "PROC_FETCH_BRAND";
        public const string PROC_INSERTUPDATE_BRAND = "PROC_BRAND_CUD";
        #endregion
        public const string PROC_SECTIONMANAGEMENT_CUD = "PROC_SECTIONMANAGEMENT_CUD";
        public const string PROC_SECTIONMANAGEMENT_GET = "PROC_SECTIONMANAGEMENT_GET";
        public const string PROC_FETCH_ADMINUSER = "PROC_FETCH_ADMINUSER";
        public const string PROC_UPDATE_ADMINUSER_PSW = "PROC_UPDATE_ADMINUSER_PSW";
        public const string PROC_CREATE_UPDATE_SUBCATEGORY = "PROC_CREATE_UPDATE_SUBCATEGORY";

    }
}
