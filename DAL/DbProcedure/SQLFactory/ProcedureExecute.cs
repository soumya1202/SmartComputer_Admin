namespace DataAccessLayer.DbProcedure.SQLFactory
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;

    public enum QueryParameterDirection : int
    {
        Input = 1,
        Output = 2,
        Return = 3
    }
    public enum ActionEnumType
    {
        Add = 1,
        Edit = 2,
        Delete = 3,
        List = 4
    }

    public class DAL : IDisposable
    {
        private string strCommandText = string.Empty;
        private bool blnSP = true;
        private ArrayList oParameters = new ArrayList();
        private bool blnLocalConn = true;
        public int out_status = 0;
        public string out_message = "";
        #region Constrator

        public DAL()
        {
        }

        public DAL(string StoredProcName)
            : this(StoredProcName, false)
        {
        }

        public DAL(string SqlString, bool IsTextQuery)
        {
            blnSP = !IsTextQuery;
            strCommandText = SqlString;
        }

        #region Property
        private string sqlErrorMsg = "";
        public string propsqlErrorMsg
        {
            get
            {
                return sqlErrorMsg;
            }
            set
            {
                sqlErrorMsg = value;
            }
        }
        #endregion

        #endregion Constrator

        #region DataTable

        // REturn a Datatable
        public DataTable GetTable()
        {
            DataTable dt = null;
            SqlCommand oCmd = new SqlCommand();
            this.InitQuery(oCmd);

            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();

            da.Fill(ds);
            if ((null != ds) && (ds.Tables.Count > 0))
            {
                dt = ds.Tables[0];
            }

            if (this.blnLocalConn)
            {
                this.oConn.Close();
            }
            oCmd.Dispose();
            return dt;
        }

        public DataTable GetTable(string ConnectionString)
        {
            DataTable dt = null;
            SqlCommand oCmd = new SqlCommand();
            this.InitQuery(oCmd, ConnectionString);

            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
           
            da.Fill(ds);
            SetoutParams(oCmd);
            if ((null != ds) && (ds.Tables.Count > 0))
            {
                dt = ds.Tables[0];
            }

            if (this.blnLocalConn)
            {
                this.oConn.Close();
            }
            oCmd.Dispose();
            return dt;
        }

        #endregion DataTable      

        #region NonQuery

        public int RunActionQuery(string ConnectionString)
        {
            int intRowsAffected = -1;

            SqlCommand oCmd = new SqlCommand();
            this.InitQuery(oCmd, ConnectionString);

            try
            {
                intRowsAffected = oCmd.ExecuteNonQuery();
                SetoutParams(oCmd);
                oCmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.blnLocalConn)
                {
                    this.oConn.Close();
                }
                oCmd.Dispose();
            }

            return intRowsAffected;
        }

        #endregion NonQuery
        #region SET OUT PARAMETER
        private void SetoutParams(SqlCommand oCmd)
        {
            if (oCmd.Parameters.Contains("@OUT_STATUS"))
            {
                out_status = Convert.ToInt32(oCmd.Parameters["@OUT_STATUS"].Value);
            }
            if (oCmd.Parameters.Contains("@OUT_MESSAGE"))
            {
                out_message = Convert.ToString(oCmd.Parameters["@OUT_MESSAGE"].Value);
            }
        }
        #endregion NonQuery
        #region DataSet

        // REturn a Datatable
        public DataSet GetDataSet()
        {
            SqlCommand oCmd = new SqlCommand();
            this.InitQuery(oCmd);

            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();

            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.blnLocalConn)
                {
                    this.oConn.Close();
                }
                oCmd.Dispose();
            }

            return ds;
        }

        public DataSet GetDataSet(string ConnectionString)
        {
            SqlCommand oCmd = new SqlCommand();
            this.InitQuery(oCmd, ConnectionString);

            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();

            try
            {
                da.Fill(ds);

                SetoutParams(oCmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.blnLocalConn)
                {
                    this.oConn.Close();
                }
                oCmd.Dispose();
            }

            return ds;
        }





        #endregion DataSet
        #region Scalar

        public object GetScalar()
        {
            object oRetVal = null;

            SqlCommand oCmd = new SqlCommand();
            this.InitQuery(oCmd);

            try
            {
                oRetVal = oCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.blnLocalConn)
                {
                    this.oConn.Close();
                }
                oCmd.Dispose();
            }

            return oRetVal;
        }
        public int GetScalarvaluereturn(string ConnectionString)
        {
            int i = 0;

            SqlCommand oCmd = new SqlCommand();
            this.InitQuery(oCmd, ConnectionString);

            try
            {
                //i = oCmd.ExecuteScalar();
                i = Convert.ToInt32(oCmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.blnLocalConn)
                {
                    this.oConn.Close();
                }
                oCmd.Dispose();
            }

            return i;
        }

        public object GetScalar(string ConnectionString)
        {
            object oRetVal = null;

            SqlCommand oCmd = new SqlCommand();
            this.InitQuery(oCmd, ConnectionString);

            try
            {
                oRetVal = oCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.blnLocalConn)
                {
                    this.oConn.Close();
                }
                oCmd.Dispose();
            }

            return oRetVal;
        }

        #endregion Scalar
        #region Initializes a Query

        private void InitQuery(SqlCommand oCmd)
        {
            blnLocalConn = (this.oConn == null);
            if (blnLocalConn)
            {
                string conn = "";//ConfigurationManager.AppSettings["ConnStr"];
                Open(conn);
                blnLocalConn = true;
            }
            oCmd.Connection = oConn;

            oCmd.CommandText = this.strCommandText;
            oCmd.CommandType = (this.blnSP ? CommandType.StoredProcedure : CommandType.Text);

            oCmd.CommandTimeout = (24 * 60 * 60);	// 1 Day

            foreach (object oItem in this.oParameters)
            {
                oCmd.Parameters.Add((SqlParameter)oItem);
            }
        }

        private void InitQuery(SqlCommand oCmd, string ConnectionString)
        {
            blnLocalConn = (this.oConn == null);
            if (blnLocalConn)
            {
                string conn = ConnectionString;
                Open(conn);
                blnLocalConn = true;
            }
            oCmd.Connection = oConn;

            oCmd.CommandText = this.strCommandText;
            oCmd.CommandType = (this.blnSP ? CommandType.StoredProcedure : CommandType.Text);

            oCmd.CommandTimeout = (24 * 60 * 60);	// 1 Day

            foreach (object oItem in this.oParameters)
            {
                oCmd.Parameters.Add((SqlParameter)oItem);
            }
        }

        #endregion Initializes a Query

        #region Parameter handling

        #region Type: General

        public void AddPara(string ParameterName, object value)
        {
            SqlParameter oPara = new SqlParameter(ParameterName, value);
            this.oParameters.Add(oPara);
        }

        public void AddPara(string ParameterName, SqlDbType type, int size)
        {
            SqlParameter oPara = new SqlParameter(ParameterName, type, size);
            this.oParameters.Add(oPara);
        }

        public void AddPara(string ParameterName, SqlDbType type, int size, ParameterDirection direction)
        {
            SqlParameter oPara = new SqlParameter(ParameterName, type, size);
            oPara.Direction = direction;
            this.oParameters.Add(oPara);
        }

        public void AddPara(string ParameterName, SqlDbType type, ParameterDirection direction)
        {
            SqlParameter oPara = new SqlParameter(ParameterName, type);
            oPara.Direction = direction;
            this.oParameters.Add(oPara);
        }

        public void AddPara(string ParameterName, SqlDbType type, ParameterDirection direction, object value)
        {
            SqlParameter oPara = new SqlParameter(ParameterName, type);
            oPara.Direction = direction;
            oPara.Value = value;
            this.oParameters.Add(oPara);
        }

        public void AddPara(SqlParameter oPara)
        {
            this.oParameters.Add(oPara);
        }

        //public void AddPara(string ParameterName, ParameterDirection direction, object value)
        //{
        //    SqlParameter oPara = new SqlParameter(ParameterName, value == null ? DBNull.Value : value);
        //    oPara.Direction = direction;
        //    this.oParameters.Add(oPara);
        //}

        #endregion

        #region Type: Integer

        public void AddIntegerPara(string Name, int Value)
        {
            AddIntegerPara(Name, Value, QueryParameterDirection.Input);
        }

        public void AddIntegerPara(string Name, int? Value, QueryParameterDirection Direction)
        {
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.Int);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = Value;
            this.oParameters.Add(oPara);
        }

        public void AddIntegerNullPara(string Name)
        {
            AddIntegerNullPara(Name, QueryParameterDirection.Input);
        }

        public void AddIntegerNullPara(string Name, QueryParameterDirection Direction)
        {
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.Int);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = DBNull.Value;
            this.oParameters.Add(oPara);
        }

        #endregion Type: Integer

        #region Type: BigInt

        public void AddBigIntegerPara(string Name, long Value)
        {
            AddBigIntegerPara(Name, Value, QueryParameterDirection.Input);
        }

        public void AddBigIntegerPara(string Name, long Value, QueryParameterDirection Direction)
        {
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.Int);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = Value;
            this.oParameters.Add(oPara);
        }

        public void AddBigIntegerNullPara(string Name)
        {
            AddIntegerNullPara(Name, QueryParameterDirection.Input);
        }

        public void AddBigIntegerNullPara(string Name, QueryParameterDirection Direction)
        {
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.Int);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = DBNull.Value;
            this.oParameters.Add(oPara);
        }

        #endregion Type: BigInt

        #region Type: Char

        public void AddCharPara(string Name, int Size, char Value)
        {
            AddCharPara(Name, Size, Value, QueryParameterDirection.Input);
        }

        public void AddCharPara(string Name, int Size, char Value, QueryParameterDirection Direction)
        {
            object oValue = (object)Value;
            if (Value.Equals(null))
            {
                oValue = DBNull.Value;
            }
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.VarChar, Size);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = oValue;
            this.oParameters.Add(oPara);
        }

        #endregion Type: Char

        #region Type: NChar


        

        #endregion Type: NChar

        #region Type: Varchar

        public void AddVarcharPara(string Name, int Size, string Value)
        {
            AddVarcharPara(Name, Size, Value, QueryParameterDirection.Input);
        }

        public void AddVarcharPara(string Name, int Size, string Value, QueryParameterDirection Direction)
        {
            object oValue = (object)Value;
            if (Value == null)
            {
                oValue = DBNull.Value;
            }

            SqlParameter oPara = new SqlParameter(Name, SqlDbType.VarChar, Size);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = oValue;
            this.oParameters.Add(oPara);
        }

        #endregion Type: Varchar

        #region Type: NVarchar

        public void AddNVarcharPara(string Name, int Size, string Value)
        {
            AddNVarcharPara(Name, Size, Value, QueryParameterDirection.Input);
        }

        public void AddNVarcharPara(string Name, string Value)
        {
            AddNVarcharPara(Name, Value, QueryParameterDirection.Input);
        }

        public void AddNVarcharPara(string Name, int Size, string Value, QueryParameterDirection Direction)
        {
            object oValue = (object)Value;
            if (Value == null)
            {
                oValue = DBNull.Value;
            }
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.VarChar, Size);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = oValue;
            this.oParameters.Add(oPara);
        }

        public void AddNVarcharPara(string Name, string Value, QueryParameterDirection Direction)
        {
            object oValue = (object)Value;
            if (Value == null)
            {
                oValue = DBNull.Value;
            }
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.VarChar);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = oValue;
            this.oParameters.Add(oPara);
        }

        #endregion Type: NVarchar

        #region Type: Boolean

        public void AddBooleanPara(string Name, bool Value)
        {
            AddBooleanPara(Name, Value, QueryParameterDirection.Input);
        }

        public void AddBooleanPara(string Name, bool Value, QueryParameterDirection Direction)
        {
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.Bit);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = Value;
            this.oParameters.Add(oPara);
        }

        #endregion Type: Boolean

        #region Type: DateTime

        public void AddDateTimePara(string Name, DateTime Value)
        {
            AddDateTimePara(Name, Value, QueryParameterDirection.Input);
        }

        public void AddDateTimePara(string Name, DateTime Value, QueryParameterDirection Direction)
        {
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.DateTime);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = Value;
            this.oParameters.Add(oPara);
        }

        #endregion Type: DateTime

        #region Type: Text

        public void AddTextPara(string Name, string Value)
        {
            AddTextPara(Name, Value, QueryParameterDirection.Input);
        }

        public void AddTextPara(string Name, string Value, QueryParameterDirection Direction)
        {
            object oValue = (object)Value;
            if (Value == null)
            {
                oValue = DBNull.Value;
            }
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.Text);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = oValue;
            this.oParameters.Add(oPara);
        }

        #endregion Type: Text

        #region Type: NText

        public void AddNTextPara(string Name, string Value)
        {
            AddNTextPara(Name, Value, QueryParameterDirection.Input);
        }

        public void AddNTextPara(string Name, string Value, QueryParameterDirection Direction)
        {
            object oValue = (object)Value;
            if (Value == null)
            {
                oValue = DBNull.Value;
            }
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.Text);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = oValue;
            this.oParameters.Add(oPara);
        }

        #endregion Type: NText

        #region Type: Decimal

        public void AddDecimalPara(string Name, byte Scale, byte Precision, decimal Value)
        {
            AddDecimalPara(Name, Scale, Precision, Value, QueryParameterDirection.Input);
        }

        public void AddDecimalPara(string Name, byte Scale, byte Precision, decimal Value, QueryParameterDirection Direction)
        {
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.Decimal);
            oPara.Scale = Scale;
            oPara.Precision = Precision;
            oPara.Direction = GetParaType(Direction);
            oPara.Value = Value;
            this.oParameters.Add(oPara);
        }

        #endregion Type: Decimal

       

        #region Adds a NULL value Parameter

        public void AddNullValuePara(string Name)
        {
            SqlParameter oPara = new SqlParameter(Name, DBNull.Value);
            oPara.Direction = ParameterDirection.Input;
            this.oParameters.Add(oPara);
        }

        public void AddNullValuePara(string Name, QueryParameterDirection Direction)
        {
            SqlParameter oPara = new SqlParameter(Name, DBNull.Value);
            oPara.Direction = GetParaType(Direction);
            this.oParameters.Add(oPara);
        }

        #endregion Adds a NULL value Parameter

        #region Adds the Return Parameter

        public void AddReturnPara()
        {
            this.AddIntegerPara("ReturnIntPara", 0, QueryParameterDirection.Return);
        }

        #endregion Adds the Return Parameter

        #region Returns the value of the passed parameter

        public object GetParaValue(string ParaName)
        {
            object oValue = null;
            SqlParameter oPara = null;

            ParaName = ParaName.Trim().ToLower();
            foreach (object oItem in this.oParameters)
            {
                oPara = (SqlParameter)oItem;
                if (oPara.ParameterName.ToLower() == ParaName)
                {
                    oValue = oPara.Value;
                    break;
                }
            }

            return oValue;
        }

        #endregion Returns the value of the passed parameter

        #region Returns the value of the Return Parameter

        public object GetReturnParaValue()
        {
            return this.GetParaValue("ReturnIntPara");
        }

        #endregion Returns the value of the Return Parameter

        #region Clears the parameters

        public void ClearParameters()
        {
            this.oParameters.Clear();
        }

        #endregion Clears the parameters

        #region Converts enum to parameter direction

        private ParameterDirection GetParaType(QueryParameterDirection Direction)
        {
            switch (Direction)
            {
                case QueryParameterDirection.Output:
                    return ParameterDirection.InputOutput;
                case QueryParameterDirection.Return:
                    return ParameterDirection.ReturnValue;
                default:
                    return ParameterDirection.Input;
            }
        }

        #endregion Converts enum to parameter direction

        #endregion Parameter handling

        #region Dispose

        public void Dispose()
        {
            this.oConn.Dispose();
            this.oParameters.Clear();
        }

        #endregion Dispose

        #region Opens a connection

        public bool Open(string ConnectionString)
        {
            blnIsOpen = false;
            oConn = new SqlConnection(ConnectionString);
            //oConn.InfoMessage += new SqlInfoMessageEventHandler(conn_InfoMessage);
            //oConn.FireInfoMessageEventOnUserErrors = true;
            oConn.Open();
            blnIsOpen = true;
            return blnIsOpen;
        }

        //public void conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        //{
        //    if (sqlErrorMsg == "")
        //    {
        //        sqlErrorMsg = e.errors.ToString();
        //        sqlErrorMsg = sqlErrorMsg.Split('.')[0];
        //    }
        //}

        #endregion Opens a connection

        #region Connection

        private SqlConnection oConn = null;

        public SqlConnection Connection
        {
            set
            {
                oConn = value;
            }
        }

        #endregion Connection

        #region IsOpen

        private bool blnIsOpen = false;

        public bool IsOpen
        {
            get
            {
                return blnIsOpen;
            }
        }

        #endregion IsOpen
    }
}
