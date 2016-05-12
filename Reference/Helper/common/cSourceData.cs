using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;

//using System.Drawing;

/// <summary>
/// Summary description for cSourceData
/// </summary>
public class cSourceData
{
    #region " DATA "

    public static String GetConnStr()
    {
        string tConnStr;
        try
        {
            string servername = HttpContext.Current.Request.ServerVariables["SERVER_NAME"] != null ? HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString() : "";
            if (servername.Contains("192.168.0") || servername.Contains("localhost"))
                tConnStr = WebConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            else
                tConnStr = WebConfigurationManager.ConnectionStrings["constr"].ConnectionString;


            return tConnStr;
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public static Boolean ExecuteData(string queryString)
    {
        SqlTransaction oDB_trn;
        SqlCommand oDB_Cmd = new SqlCommand();
        SqlConnection oDB_Conn = new SqlConnection(GetConnStr());

        if (queryString == "") return false;

        if (oDB_Conn.State == ConnectionState.Closed) oDB_Conn.Open();

        oDB_Cmd.Connection = oDB_Conn;
        oDB_Cmd.CommandType = CommandType.Text;
        oDB_Cmd.CommandText = queryString;
        oDB_trn = oDB_Conn.BeginTransaction();
        oDB_Cmd.Transaction = oDB_trn;

        try
        {
            oDB_Cmd.ExecuteNonQuery();
            oDB_trn.Commit();
            oDB_Conn.Close();
            return true;
        }
        catch (Exception ex)
        {
            oDB_trn.Rollback();
            oDB_Conn.Close();
            //MessageBox.Show(ex.Message);
            return false;
        }
    }

    public static string ExecuteData_ReturnString(string queryString)
    {
        SqlTransaction oDB_trn;
        SqlCommand oDB_Cmd = new SqlCommand();
        SqlConnection oDB_Conn = new SqlConnection(GetConnStr());

        if (queryString == "") return "";

        if (oDB_Conn.State == ConnectionState.Closed) oDB_Conn.Open();

        oDB_Cmd.Connection = oDB_Conn;
        oDB_Cmd.CommandType = CommandType.Text;
        oDB_Cmd.CommandText = queryString;
        oDB_trn = oDB_Conn.BeginTransaction();
        oDB_Cmd.Transaction = oDB_trn;

        try
        {
            oDB_Cmd.ExecuteNonQuery();
            oDB_trn.Commit();
            oDB_Conn.Close();
            return "";
        }
        catch (Exception ex)
        {
            oDB_trn.Rollback();
            oDB_Conn.Close();
            //MessageBox.Show(ex.Message);
            return ex.Message;
        }
    }

    public static Boolean ExecuteData(string queryString, SqlCommand Comm)
    {
        SqlTransaction oDB_trn;
        SqlConnection oDB_Conn = new SqlConnection(GetConnStr());

        if (queryString == "") return false;

        if (oDB_Conn.State == ConnectionState.Closed) oDB_Conn.Open();

        Comm.Connection = oDB_Conn;
        Comm.CommandType = CommandType.Text;
        Comm.CommandText = queryString;
        oDB_trn = oDB_Conn.BeginTransaction();
        Comm.Transaction = oDB_trn;
        try
        {
            Comm.ExecuteNonQuery();
            oDB_trn.Commit();
            oDB_Conn.Close();
            return true;
        }
        catch (Exception ex)
        {
             oDB_trn.Rollback();
            oDB_Conn.Close();
            return false;
        }

    }

    public static Boolean ExecuteData(string queryString, SqlCommand Comm, SqlConnection Conn)
    {
        SqlTransaction oDB_trn;

        if (queryString == "") return false;

        Comm.Connection = Conn;
        Comm.CommandType = CommandType.Text;
        Comm.CommandText = queryString;
        oDB_trn = Conn.BeginTransaction();
        Comm.Transaction = oDB_trn;

        try
        {
            Comm.ExecuteNonQuery();
            oDB_trn.Commit();
            Conn.Close();
            return true;
        }
        catch (Exception ex)
        {
             oDB_trn.Rollback();
            Conn.Close();
            return false;
        }
    }

    public static Boolean ExecuteData_Store(string storeName, SqlCommand Comm)
    {
        SqlTransaction oDB_trn;
        SqlConnection oDB_Conn = new SqlConnection(GetConnStr());

        if (storeName == "") return false;

        if (oDB_Conn.State == ConnectionState.Closed) oDB_Conn.Open();

        Comm.Connection = oDB_Conn;
        Comm.CommandType = CommandType.StoredProcedure;
        Comm.CommandText = storeName;
        oDB_trn = oDB_Conn.BeginTransaction();
        Comm.Transaction = oDB_trn;

        try
        {
            Comm.ExecuteNonQuery();
            oDB_trn.Commit();
            oDB_Conn.Close();
            return true;
        }
        catch (Exception ex)
        {
             oDB_trn.Rollback();
            oDB_Conn.Close();
            return false;
        }
    }

    public static DataSet GetData(String queryString, SqlCommand Comm)
    {
        // Retrieve the connection string in cSourceData.GetConnStr()
        String connectionString = GetConnStr();
        SqlConnection connection = new SqlConnection(connectionString);
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataSet ds = new DataSet();

        try
        {
            // Connect to the database and run the query.

            if (connection.State == ConnectionState.Closed) connection.Open();

            Comm.Connection = connection;
            Comm.CommandText = queryString;
            adapter.SelectCommand = Comm;

            // Fill the DataSet.
            adapter.Fill(ds);
            connection.Close();
        }
        catch (Exception ex)
        {
             // The connection failed. Display an error message.
            connection.Close();
            // MessageBox.Show(ex.Message);
            return null;
        }

        return ds;
    }
    public static DataSet GetData(String queryString, SqlCommand Comm, String connectionString)
    {
        // Retrieve the connection string in cSourceData.GetConnStr()

        SqlConnection connection = new SqlConnection(connectionString);
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataSet ds = new DataSet();

        try
        {
            // Connect to the database and run the query.

            if (connection.State == ConnectionState.Closed) connection.Open();

            Comm.Connection = connection;
            Comm.CommandText = queryString;
            adapter.SelectCommand = Comm;

            // Fill the DataSet.
            adapter.Fill(ds);
            connection.Close();
        }
        catch (Exception ex)
        {
             // The connection failed. Display an error message.
            connection.Close();
            // MessageBox.Show(ex.Message);
            return null;
        }

        return ds;
    }
    public static DataSet GetData(String queryString, SqlCommand Comm, SqlConnection Conn)
    {
        // Retrieve the connection string in cSourceData.GetConnStr()
        //String connectionString = cSourceData.GetConnStr();

        DataSet ds = new DataSet();

        try
        {
            // Connect to the database and run the query.

            SqlDataAdapter adapter = new SqlDataAdapter();
            if (Conn.State == ConnectionState.Closed) Conn.Open();
            Comm.Connection = Conn;
            Comm.CommandText = queryString;
            adapter.SelectCommand = Comm;

            // Fill the DataSet.
            adapter.Fill(ds);
            Conn.Close();
        }
        catch (Exception ex)
        {
             // The connection failed. Display an error message.
            Conn.Close();
            // MessageBox.Show(ex.Message);
            return null;
        }

        return ds;
    }

    public static DataSet GetData(String queryString)
    {
        // Retrieve the connection string in cSourceData.GetConnStr()
        String connectionString = GetConnStr();
        SqlConnection connection = new SqlConnection(connectionString);
        DataSet ds = new DataSet();

        try
        {
            // Connect to the database and run the query.
            SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);

            // Fill the DataSet.
            adapter.Fill(ds);
            connection.Close();
        }
        catch (Exception ex)
        {
            connection.Close();
            // The connection failed. Display an error message.
            //MessageBox.Show(ex.Message);
            return null;
        }

        return ds;
    }

    public static DataSet GetData_Store(String StoredProcedureName, SqlCommand Comm)
    {
        // Retrieve the connection string in cSourceData.GetConnStr()
        String connectionString = GetConnStr();
        SqlConnection connection = new SqlConnection(connectionString);
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataSet ds = new DataSet();

        try
        {
            // Connect to the database and run the query.

            if (connection.State == ConnectionState.Closed) connection.Open();

            Comm.Connection = connection;
            Comm.CommandText = StoredProcedureName;
            Comm.CommandType = CommandType.StoredProcedure;
            Comm.CommandTimeout = 360;
            adapter.SelectCommand = Comm;

            // Fill the DataSet.
            adapter.Fill(ds);
            connection.Close();
        }
        catch (Exception ex)
        {
 
            // The connection failed. Display an error message.
            connection.Close();
            return null;
        }

        return ds;
    }
     

    public static double ConvertToDouble(object pObj)
    {
        string strTemp = ConvertToString(pObj);

        if (strTemp != "")
        {
            try
            {
                return double.Parse(pObj.ToString());
            }
            catch
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }

    public static string ConvertToString(object pObj)
    {
        if (pObj == null)
        {
            return string.Empty;
        }
        else
        {
            return Convert.ToString(pObj).Trim();
        }
    }

    public static int ConvertToInt(object pObj)
    {
        string strTemp = ConvertToString(pObj);

        if (strTemp != "")
        {
            try
            {
                return int.Parse(pObj.ToString());
            }
            catch
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }

    public static String ReadSetting(String key, String defaultValue)
    {
        try
        {
            object setting = System.Configuration.ConfigurationSettings.AppSettings[key];
            if (setting != null)
            {
                if (setting.ToString() == "")
                {
                    setting = null;
                }
            }
            return (setting == null) ? defaultValue : (String)setting;
        }
        catch
        {
            return defaultValue;
        }
    }

    #endregion " DATA "

    #region " CheckNull "

    public static string CheckNull(Object obj)
    {
        try
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return obj.ToString();
        }
        catch
        {
            return string.Empty;
        }
    }

    public static string CheckNull(Object obj, string strIfNullReturn)
    {
        try
        {
            if (obj == null)
            {
                return strIfNullReturn;
            }
            else
            {
                if (obj.ToString() == "")
                    return strIfNullReturn;
            }
            return obj.ToString();
        }
        catch
        {
            return strIfNullReturn;
        }
    }

    #endregion " CheckNull "
}