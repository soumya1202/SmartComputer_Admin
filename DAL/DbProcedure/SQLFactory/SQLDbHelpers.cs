namespace DataAccessLayer.DbProcedure.SQLFactory
{
    
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class SQLDbHelpers
    {
        #region Parameter Helper

        /// <summary>
        /// Creates parameters for store procedure.
        /// </summary>
        /// <param name="model">Model/class which holds the value for store procedure parameters</param>
        /// <param name="ProcName">Procedure name to be executed</param>
        /// <returns>Returns object of DAL with parameters declared</returns>
        public static DAL ProcParameterHelper(object model, string ProcName)
        {
            DAL Proc = new DAL(ProcName);
            Proc = SQLDbHelpers.GetParams(Proc, model, model.GetType());
            return Proc;
        }

        /// <summary>
        /// Creates parameters for store procedure.
        /// </summary>
        /// <param name="model">Model/class which holds the value for store procedure parameters</param>
        /// <param name="Proc">Initialized object of DAL</param>
        public static void ProcParameterHelper(object model, DAL Proc, bool IsOutParameter = false)
        {
            if (IsOutParameter)
            {
                Proc = SQLDbHelpers.GetOutParams(Proc, model, model.GetType());
            }
            else
            {
                Proc = SQLDbHelpers.GetParams(Proc, model, model.GetType());
            }
        }

        #endregion



        #region Save Functionality

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="ProcName"></param>
        /// <returns></returns>
        public static T SaveRecord<T>(object model, string ProcName)
        {
            T obj = Activator.CreateInstance<T>();
            using (DAL Proc = new DAL(ProcName))
            {
                ProcParameterHelper(model, Proc);
                ProcParameterHelper(obj, Proc, true);

                //Proc.RunActionQuery(constring);
                Proc.RunActionQuery(defaultconnectionString);

                Type outParamObjType = obj.GetType();

                foreach (PropertyInfo pro in outParamObjType.GetProperties())
                {
                    object resValue = Proc.GetParaValue("@" + pro.Name);
                    if (resValue != null && resValue != DBNull.Value)
                    {
                        object mainValue = Cast(pro.PropertyType, resValue);
                        pro.SetValue(obj, mainValue, null);
                    }
                }
            }
            return obj;
        }

        #endregion

        #region Table Data Fetch From DataTable



        /// <summary>
        /// Get table data as model class from DataTable
        /// </summary>
        /// <typeparam name="T">Model/Class Name</typeparam>
        /// <param name="InParams">Model/class which holds the value for store procedure parameters</param>
        /// <param name="OutParams">Model/class which holds property of out parameters for store procedure parameters</param>
        /// <param name="ProcName">Procedure name to be executed</param>
        /// <returns>Returns object of desired Model/Class with data fetched from datatable</returns>
        public static T DataTableResultAsModel<T>(object InParams, object OutParams, string ProcName)
        {
            T obj = Activator.CreateInstance<T>();

            using (DAL proc = new DAL(ProcName))
            {
                ProcParameterHelper(InParams, proc);
                if(OutParams!=null)
                {
                    ProcParameterHelper(OutParams, proc, true);
                }
                

                // DataTable dt = proc.GetTable(connectionstring);
                DataTable dt = proc.GetTable(defaultconnectionString);
                if (OutParams != null)
                {
                    OutParams.GetType().GetProperty("OUT_STATUS").SetValue(OutParams, proc.out_status);
                    OutParams.GetType().GetProperty("OUT_MESSAGE").SetValue(OutParams, proc.out_message);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    Type temp = typeof(T);

                    foreach (DataColumn column in dt.Columns)
                    {
                        foreach (PropertyInfo pro in temp.GetProperties())
                        {
                            if (pro.Name.ToUpper() == column.ColumnName.ToUpper() && dt.Rows[0][column.ColumnName] != DBNull.Value)
                            {
                                try
                                {
                                    pro.SetValue(obj, dt.Rows[0][column.ColumnName], null);
                                }
                                catch(Exception ex)
                                {

                                }
                            }
                        }
                    }
                }
            }
            return obj;
        }



        /// <summary>
        /// Get table data as list of model class from DataTable
        /// </summary>
        /// <typeparam name="T">Model/Class Name</typeparam>
        /// <param name="InParams">Model/class which holds the value for store procedure parameters</param>
        /// <param name="OutParams">Model/class which holds property of out parameters for store procedure parameters</param>
        /// <param name="ProcName">Procedure name to be executed</param>
        /// <returns>Returns list object of desired Model/Class with data fetched from datatable</returns>
        public static List<T> DataTableResultAsModelList<T>(object InParams, object OutParams, string ProcName)
        {
            List<T> objList = new List<T>();

            using (DAL proc = new DAL(ProcName))
            {
                ProcParameterHelper(InParams, proc);
                ProcParameterHelper(OutParams, proc, true);

                DataTable dt = proc.GetTable(defaultconnectionString);
                OutParams.GetType().GetProperty("OUT_STATUS").SetValue(OutParams, proc.out_status);
                OutParams.GetType().GetProperty("OUT_MESSAGE").SetValue(OutParams, proc.out_message);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Type temp = typeof(T);

                    foreach (DataRow row in dt.Rows)
                    {
                        T obj = Activator.CreateInstance<T>();

                        foreach (DataColumn column in row.Table.Columns)
                        {
                            foreach (PropertyInfo pro in temp.GetProperties())
                            {
                                if (pro.Name == column.ColumnName && row[column.ColumnName] != DBNull.Value)
                                {
                                    try
                                    {
                                        pro.SetValue(obj, row[column.ColumnName], null);
                                    }
                                    catch(Exception ex)
                                    {

                                    }
                                }
                            }
                        }

                        objList.Add(obj);
                    }
                }
            }

            return objList;
        }
        public static T DataTableResultAsModel<T>(object OutParams,string ProcName)
        {
            T obj = Activator.CreateInstance<T>();

            using (DAL proc = new DAL(ProcName))
            {
                //ProcParameterHelper(InParams, proc);
                ProcParameterHelper(OutParams, proc, true);

                DataTable dt = proc.GetTable(defaultconnectionString);
                OutParams.GetType().GetProperty("OUT_STATUS").SetValue(OutParams, proc.out_status);
                OutParams.GetType().GetProperty("OUT_MESSAGE").SetValue(OutParams, proc.out_message);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Type temp = typeof(T);

                    foreach (DataColumn column in dt.Columns)
                    {
                        foreach (PropertyInfo pro in temp.GetProperties())
                        {
                            if (pro.Name.ToUpper() == column.ColumnName.ToUpper() && dt.Rows[0][column.ColumnName] != DBNull.Value)
                            {
                                try
                                {
                                    pro.SetValue(obj, dt.Rows[0][column.ColumnName], null);
                                }
                                catch
                                {

                                }
                            }
                        }
                    }
                }
            }
            return obj;
        }
        public static List<T> DataTableResultAsModelList<T>(object OutParams,string ProcName)
        {
            List<T> objList = new List<T>();

            using (DAL proc = new DAL(ProcName))
            {
                //ProcParameterHelper(InParams, proc);
                if(OutParams!=null)
                {
                    ProcParameterHelper(OutParams, proc, true);
                }
                

                DataTable dt = proc.GetTable(defaultconnectionString);
                if (OutParams != null)
                {
                    OutParams.GetType().GetProperty("OUT_STATUS").SetValue(OutParams, proc.out_status);
                    OutParams.GetType().GetProperty("OUT_MESSAGE").SetValue(OutParams, proc.out_message);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    Type temp = typeof(T);

                    foreach (DataRow row in dt.Rows)
                    {
                        T obj = Activator.CreateInstance<T>();

                        foreach (DataColumn column in row.Table.Columns)
                        {
                            foreach (PropertyInfo pro in temp.GetProperties())
                            {
                                if (pro.Name == column.ColumnName && row[column.ColumnName] != DBNull.Value)
                                {
                                    try
                                    {
                                        pro.SetValue(obj, row[column.ColumnName], null);
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                        }

                        objList.Add(obj);
                    }
                }
            }

            return objList;
        }
        public static DataSet DataTableResultAsModel(object InParams, object OutParams, string ProcName)
        {
            using (DAL proc = new DAL(ProcName))
            {
                ProcParameterHelper(InParams, proc);
                ProcParameterHelper(OutParams, proc, true);

                DataSet ds = proc.GetDataSet(defaultconnectionString);
                OutParams.GetType().GetProperty("OUT_STATUS").SetValue(OutParams, proc.out_status);
                OutParams.GetType().GetProperty("OUT_MESSAGE").SetValue(OutParams, proc.out_message);
                return ds;
            }

        }

        public static T DataTableToObject<T>(DataTable dt)
        {
            T obj = Activator.CreateInstance<T>();
            if (dt != null && dt.Rows.Count > 0)
            {
                Type temp = typeof(T);

                foreach (DataColumn column in dt.Columns)
                {
                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        if (pro.Name.ToUpper() == column.ColumnName.ToUpper() && dt.Rows[0][column.ColumnName] != DBNull.Value)
                        {
                            try
                            {
                                pro.SetValue(obj, dt.Rows[0][column.ColumnName], null);
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }
            return obj;
        }
        public static List<T> DataTableToObjectList<T>(DataTable dt)
        {
            List<T> objList = new List<T>();
            if (dt != null && dt.Rows.Count > 0)
            {
                Type temp = typeof(T);

                foreach (DataRow row in dt.Rows)
                {
                    T obj = Activator.CreateInstance<T>();

                    foreach (DataColumn column in row.Table.Columns)
                    {
                        foreach (PropertyInfo pro in temp.GetProperties())
                        {
                            if (pro.Name == column.ColumnName && row[column.ColumnName] != DBNull.Value)
                            {
                                try
                                {
                                    pro.SetValue(obj, row[column.ColumnName], null);
                                }
                                catch
                                {

                                }
                            }
                        }
                    }

                    objList.Add(obj);
                }
            }
            return objList;
        }

        #endregion

        #region Get Scalar Data

        /// <summary>
        /// Get Scalar Data
        /// </summary>
        /// <param name="ProcName">Procedure name to be executed</param>
        /// <returns>Returns object</returns>
        public static object ScalarResult(string ProcName)
        {
            using (DAL Proc = new DAL(ProcName))
            {
                return Proc.GetScalar();
            }
        }

        /// <summary>
        /// Get Scalar Data
        /// </summary>
        /// <param name="ProcName">Procedure name to be executed</param>
        /// <param name="ConnectionString">Database connection string</param>
        /// <returns>Returns object</returns>
        //public static object ScalarResult(string ProcName, string ConnectionString)
        //{
        //    using (DAL Proc = new DAL(ProcName))
        //    {
        //        return Proc.GetScalar(ConnectionString);
        //    }
        //}

        /// <summary>
        /// Get Scalar Data
        /// </summary>
        /// <param name="Proc">Initialized object of DAL</param>
        /// <returns>Returns object</returns>
        public static object ScalarResult(DAL Proc)
        {
            return Proc.GetScalar();
        }

        /// <summary>
        /// Get Scalar Data
        /// </summary>
        /// <param name="Proc">Initialized object of DAL</param>
        /// <param name="ConnectionString">Database connection string</param>
        /// <returns>Returns object</returns>
        //public static object ScalarResult(DAL Proc, string ConnectionString)
        //{
        //    return Proc.GetScalar(ConnectionString);
        //}

        /// <summary>
        /// Get Scalar Data
        /// </summary>
        /// <param name="model">Model/class which holds the value for store procedure parameters</param>
        /// <param name="ProcName">Procedure name to be executed</param>
        /// <returns>Returns object</returns>
        public static object ScalarResult(object model, string ProcName)
        {
            using (DAL Proc = new DAL(ProcName))
            {
                ProcParameterHelper(model, Proc);
                return Proc.GetScalar();
            }
        }

        /// <summary>
        /// Get Scalar Data
        /// </summary>
        /// <param name="model">Model/class which holds the value for store procedure parameters</param>
        /// <param name="ProcName">Procedure name to be executed</param>
        /// <param name="ConnectionString">Database connection string</param>
        /// <returns>Returns object</returns>
        //public static object ScalarResult(object model, string ProcName, string ConnectionString)
        //{
        //    using (DAL Proc = new DAL(ProcName))
        //    {
        //        ProcParameterHelper(model, Proc);
        //        return Proc.GetScalar(ConnectionString);
        //    }
        //}

        /// <summary>
        /// Get Scalar Data
        /// </summary>
        /// <param name="Proc">Initialized object of DAL</param>
        /// <param name="model">Model/class which holds the value for store procedure parameters</param>
        /// <returns>Returns object</returns>
        public static object ScalarResult(DAL Proc, object model)
        {
            ProcParameterHelper(model, Proc);
            return Proc.GetScalar();
        }

        /// <summary>
        /// Get Scalar Data
        /// </summary>
        /// <param name="Proc">Initialized object of DAL</param>
        /// <param name="model">Model/class which holds the value for store procedure parameters</param>
        /// <param name="ConnectionString">Database connection string</param>
        /// <returns>Returns object</returns>
        //public static object ScalarResult(DAL Proc, object model, string ConnectionString)
        //{
        //    ProcParameterHelper(model, Proc);
        //    return Proc.GetScalar(ConnectionString);
        //}

        #endregion

        #region Private Methods
        private static DAL GetParams(DAL proc, Object myObj, Type temp)
        {
            foreach (PropertyInfo pro in temp.GetProperties())
            {
                object value = myObj.GetType().GetProperty(pro.Name).GetValue(myObj, null);
                proc.AddPara("@" + pro.Name, value);
            }

            return proc;
        }

        private static DAL GetOutParams(DAL proc, Object myObj, Type temp)
        {
            foreach (PropertyInfo pro in temp.GetProperties())
            {
                object value = myObj.GetType().GetProperty(pro.Name).GetValue(myObj, null);
                Type paramType = pro.PropertyType;

                SqlDbType dbType = getSqlDbType(paramType);

                SqlParameter oPara = new SqlParameter("@" + pro.Name, dbType);
                oPara.Direction = ParameterDirection.Output;
                oPara.Value = value;

                if (dbType == SqlDbType.VarChar)
                {
                    string tempVal = value != null ? value.ToString() : string.Empty;
                    oPara.Size = !string.IsNullOrWhiteSpace(tempVal) ? (tempVal.Length + 50) : 200;
                }


                proc.AddPara(oPara);
            }

            return proc;
        }

        private static object Cast(Type Type, object data)
        {
            var DataParam = Expression.Parameter(typeof(object), "data");
            var Body = Expression.Block(Expression.Convert(Expression.Convert(DataParam, data.GetType()), Type));

            var Run = Expression.Lambda(Body, DataParam).Compile();
            var ret = Run.DynamicInvoke(data);
            return ret;
        }

        private static SqlDbType getSqlDbType(Type type)
        {
            SqlDbType resType = SqlDbType.Int;

            if (type == typeof(string) || type == typeof(String))
            {
                resType = SqlDbType.VarChar;
            }
            else if (type == typeof(int) || type == typeof(int?) || type == typeof(Int16) || type == typeof(Int16?)
                || type == typeof(Int32) || type == typeof(Int32?))
            {
                resType = SqlDbType.Int;
            }
            else if (type == typeof(Int64) || type == typeof(Int64?) || type == typeof(long) || type == typeof(long?))
            {
                resType = SqlDbType.Int;
            }
            else if (type == typeof(decimal) || type == typeof(decimal?) || type == typeof(double) || type == typeof(double?))
            {
                resType = SqlDbType.Decimal;
            }
            else if (type == typeof(bool) || type == typeof(bool?) || type == typeof(Boolean) || type == typeof(Boolean?))
            {
                resType = SqlDbType.Bit;
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                resType = SqlDbType.DateTime;
            }

            return resType;
        }
        #endregion

        #region Connection string
        private static string defaultconnectionString = ReadFromAppSettingsJson();

        public static string ReadFromAppSettingsJson()
        {
            dynamic obj = JsonLoader.LoadFromFile<dynamic>("appsettings.json");
            ConnectionStrings connectionStrings = JsonLoader.Deserialize<ConnectionStrings>(Convert.ToString(obj.ConnectionStrings));
            return connectionStrings.DefaultConnection;
        }
        private class ConnectionStrings
        {
            public string DefaultConnection { get; set; }
        }
        #endregion
    }
}
