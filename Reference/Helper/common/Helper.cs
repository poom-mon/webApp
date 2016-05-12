using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Net;

 
    public class cHelper
    {   

        #region CheckLoantype
        public string CheckLoantype(string sType)
        {
            string Result = String.Empty;

            if (!string.IsNullOrEmpty(sType.ToLower()))
            {
                switch (sType.ToLower().Trim())
                {
                    case "refinance": Result = "บ้านกรุงศรีรีไฟแนนซ์";
                        break;
                    case "home for cash": Result = "กรุงศรีโฮมฟอร์แคช";
                        break;
                }

            }
            return Result;
        }
        #endregion

        #region SpaceValue
        public string SpaceValue(int spaceValue)
        {
            string Result = String.Empty;

            if (spaceValue > 0)
            {
                for (int i = 0; i <= spaceValue; i++)
                {
                    Result += "_";
                }
            }
            return Result;
        }

        public string CheckStringValue(string value)
        {
            if (value.Trim() == "")
            {
                value = "-";
            }

            return value;
        }
        #endregion

        #region CalculateAge
        public int CalculateAge(DateTime birthDate, DateTime now, IFormatProvider format)
        {
            birthDate = Convert.ToDateTime(birthDate.ToString("dd-MM-yyyy"), format);
            now = Convert.ToDateTime(now.ToString("dd-MM-yyyy"), format);

            System.TimeSpan diff1 = now.Subtract(birthDate);
            double date = (double)(diff1.TotalDays / 365);
            int age = (int)date;
            //if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) age--;
            return age;
        }
        #endregion

        #region Calculate จำนวน Day Month Year
        public string CalculateDayMonthYear(DateTime dob, DateTime now)
        {

            //dob = Convert.ToDateTime(dob.ToString("dd/MM/yyyy", format));
            //now = Convert.ToDateTime(now.ToString("dd/MM/yyyy", format));

            int days = now.Day - dob.Day;
            if (days < 0)
            {
                now = now.AddMonths(-1);
                days += DateTime.DaysInMonth(now.Year, now.Month);
            }

            int months = now.Month - dob.Month;
            if (months < 0)
            {
                now = now.AddYears(-1);
                months += 12;
            }

            int years = now.Year - dob.Year;

            return string.Format("{0}|{1}|{2}", years, months, days);
        }
        #endregion

        /// <summary>
        /// DateFormat
        /// </summary>
        public static System.IFormatProvider DateFormat = new System.Globalization.CultureInfo("en-US");

        /// <summary>
        /// Roots the path.
        /// </summary>
        /// <returns></returns>
        public static string rootPath()
        {
            string filePath = "";

            string APP_PATH = System.Web.HttpContext.Current.Request.ApplicationPath.ToLower();
            if (APP_PATH == "/")      //a site
                APP_PATH = "/";
            else if (!APP_PATH.EndsWith(@"/")) //a virtual
                APP_PATH += @"/";

            string it = System.Web.HttpContext.Current.Server.MapPath(APP_PATH);
            if (!it.EndsWith(@"\"))
                it += @"\";

            filePath = it;

            return filePath;
        }

        /// <summary>
        /// Sets the len data.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="formatlen">The formatlen.</param>
        /// <returns></returns>
        public static string setLenData(string value, int formatlen)
        {
            string strData = string.Empty;

            if (value.Length < formatlen)
            {
                for (int i = value.Length; i < formatlen; i++)
                {
                    value += " ";
                }
            }

            strData = value;
            return strData;
        }

        /// <summary>
        /// Sets the len with first zero data.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="formatlen">The formatlen.</param>
        /// <returns></returns>
        public static string setLenWithFirstZeroMoneyData(double value, int formatlen, bool includePoint)
        {
            string strData = string.Empty;

            strData = String.Format("{0:0.00}", value);
            if (includePoint)
            {
                strData = strData.Replace(".", "");
            }
            else
            {
                int tmpNum = (int)value;
                strData = tmpNum.ToString();
            }

            if (strData.Length < formatlen)
            {
                for (int i = strData.Length; i < formatlen; i++)
                {
                    strData = "0" + strData;
                }
            }

            return strData;
        }

        /// <summary>
        /// Sets the len with first zero data.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="formatlen">The formatlen.</param>
        /// <returns></returns>
        public static string setLenWithFirstZeroNumberData(string value, int formatlen)
        {
            if (value.Length < formatlen)
            {
                for (int i = value.Length; i < formatlen; i++)
                {
                    value = "0" + value;
                }
            }
            return value;
        }

        /// <summary>
        /// Generates the text file.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="filename">The filename.</param>
        public static void GenerateTextFile(string data, string filename, string folderPath)
        {
            string dateTime = DateTime.Now.Year + "" + (DateTime.Now.Month.ToString().Length < 2 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString()) + "" + (DateTime.Now.Day.ToString().Length < 2 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString());
            data = data.Replace("<br>", "\r\n");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            //Append new text to an existing file
            using (FileStream fs = new FileStream(folderPath + "\\" + filename + dateTime + ".txt", FileMode.Create))
            {
                using (StreamWriter file = new StreamWriter(fs))
                {
                    file.WriteLine(data);
                }
            }
        }

        /// <summary>
        /// Convers the date format.
        /// </summary>
        /// <param name="dateVal">The date val.</param>
        /// <returns></returns>
        public static string converDateFormat(string dateVal)
        {
            string tmpval = " ";
            if (!string.IsNullOrEmpty(dateVal))
            {
                string tmpdate = dateVal;
                string datevalue = tmpdate.Split(' ')[0];
                tmpval = ((int.Parse(datevalue.Split('/')[2]) - 543).ToString()) + (datevalue.Split('/')[1].Length < 2 ? "0" + datevalue.Split('/')[1] : datevalue.Split('/')[1]) + (datevalue.Split('/')[0].Length < 2 ? "0" + datevalue.Split('/')[0] : datevalue.Split('/')[0]);
            }
            return tmpval;
        }

        /// <summary>
        /// Verifies the people ID.
        /// </summary>
        /// <param name="PID">The PID.</param>
        /// <returns></returns>
        public static Boolean VerifyPeopleID(String PID)
        {
            string digit = null;

            //ตรวจสอบว่าทุก ๆ ตัวอักษรเป็นตัวเลข
            if (PID.ToCharArray().All(c => char.IsNumber(c)) == false)
                return false;

            //ตรวจสอบว่าข้อมูลมีทั้งหมด 13 ตัวอักษร
            if (PID.Trim().Length != 13)
                return false;

            int sumValue = 0;
            for (int i = 0; i < PID.Length - 1; i++)
                sumValue += int.Parse(PID[i].ToString()) * (13 - i);

            int v = 11 - (sumValue % 11);

            if (v.ToString().Length == 2)
            {
                digit = v.ToString().Substring(1, 1);
            }
            else
            {
                digit = v.ToString();
            }

            return PID[12].ToString() == digit;
        }

        /// <summary>
        /// Encodes the specified STR mobile.
        /// </summary>
        /// <param name="strMobile">The STR mobile.</param>
        /// <returns></returns>
        public static string Encode(string strMobile)
        {
            // string urlEncodeData = "http://192.168.0.53/Asp/encode_decode.asp?process=encrypt&data=" + strMobile.Replace("+", "%2B");
             string urlEncodeData = "http://www.silkspan.com/Asp/encode_decode.asp?process=encrypt&data=" + strMobile.Replace("+", "%2B");
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                return client.DownloadString(urlEncodeData);
            }
        }

        public static string EmailEncode(string strMobile)
        {
            //string urlEncodeData = "http://192.168.0.53/Asp/encode_decode.asp?process=encrypt&data=" + strMobile.Replace("+", "%2B");
              string urlEncodeData = "http://www.silkspan.com/Asp/encode_decode.asp?process=encrypt&data=" + strMobile.Replace("+", "%2B");
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                return client.DownloadString(urlEncodeData);
            }
        }

        /// <summary>
        /// Decodes the specified STR mobile.
        /// </summary>
        /// <param name="strMobile">The STR mobile.</param>
        /// <returns></returns>
        public static string Decode(string strMobile)
        {
            // string urlDEcodeData = "http://192.168.0.53/ASP/nop/encoding/new.asp?a=" + strMobile.Replace("+", "%2B");
              string urlDEcodeData = "http://www.silkspan.com/Asp/encode_decode.asp?process=decrypt&data=" + strMobile.Replace("+", "%2B").ToString();
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                return client.DownloadString(urlDEcodeData);
            }
        }

        /// <summary>
        /// Writes the log file.
        /// </summary>
        /// <param name="data">The data.</param>
        public static void WriteLogFile(string data, string dirName)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            string fileName = DateTime.Now.ToString("yyyyMMdd") + ".log";

            string absolutePath = rootPath() + "\\GIGNA_LOGS\\" + dirName + "\\" + DateTime.Now.ToString("yyyyMM") + "\\";

            FileMode _fileMode = FileMode.Create;

            if (!Directory.Exists(absolutePath))
            {
                Directory.CreateDirectory(absolutePath);
            }
            else
            {
                _fileMode = FileMode.Append;
            }

            using (FileStream fs = new FileStream(absolutePath + "\\" + fileName, _fileMode))
            {
                using (StreamWriter writer = new StreamWriter(fs, Encoding.Default))
                {
                    writer.Write(data);
                    writer.Write(Environment.NewLine);
                }
            }
        }

        /// <summary>
        /// Files the name.
        /// </summary>
        /// <returns></returns>
        public static string FileName(string reportType)
        {
            string filename = string.Empty;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

            switch (reportType)
            {
                case "EN":
                    filename = "SPN_EN_" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
                    break;

                case "UW":
                    filename = "UWReport_" + DateTime.Now.ToString("ddMMyyyy") + ".csv";
                    break;

                case "QA":
                    filename = "QAIndexingReport_" + DateTime.Now.ToString("ddMMyyyy") + ".csv";
                    break;

                case "RC":
                    filename = "BillingReport_" + DateTime.Now.ToString("ddMMyyyy") + ".txt";
                    break;

                case "RF":
                    filename = "RefundReport_" + DateTime.Now.ToString("ddMMyyyy") + ".csv";
                    break;

                case "PF":
                    filename = "Premium Settlement Report_" + DateTime.Now.ToString("ddMMyyyy") + ".csv";
                    break;
            }

            return filename;
        }

        /// <summary>
        /// Datas the time to string.
        /// </summary>
        /// <returns></returns>
        public static string DataTimeToString()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            return DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        }
         

        public static string Serialize(object obj)
        {
            string @rn = null;
            try
            {
                JavaScriptSerializer ser = new JavaScriptSerializer();
                @rn = ser.Serialize(obj);
                return @rn;
            }
            catch (Exception ex) { throw (ex); }
        }

        public static T Deserialize<T>(string input)
        {
            T @rn = default(T);
            try
            {
                JavaScriptSerializer ser = new JavaScriptSerializer();
                @rn = ser.Deserialize<T>(input);
                return @rn;
            }
            catch (Exception ex) { throw (ex); }
        }

        #region List and DataTable

        public static IList<T> CreateList<T>(IList<DataRow> rows)
        {
            IList<T> rt = null;
            if (rows != null)
            {
                rt = new List<T>();
                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    rt.Add(item);
                }
            }
            return rt;
        }

        public static IList<T> CreateList<T>(DataTable table)
        {
            if (table == null)
                return null;

            List<DataRow> rows = new List<DataRow>();
            foreach (DataRow row in table.Rows)
                rows.Add(row);

            return CreateList<T>(rows);
        }

        public static T CreateItem<T>(DataRow row)
        {
            string columnName;
            T rt = default(T);
            if (row != null)
            {
                rt = Activator.CreateInstance<T>();
                PropertyInfo prop = null;
                object value = null;
                foreach (DataColumn column in row.Table.Columns)
                {
                    columnName = column.ColumnName;

                    //Get property with same columnName
                    prop = rt.GetType().GetProperty(columnName);
                    try
                    {
                        //Get value for the column
                        value = (row[columnName].GetType() == typeof(DBNull)) ? null : row[columnName];

                        //Set property value
                        prop.SetValue(rt, value, null);
                    }
                    catch
                    {
                        throw;

                        //Catch whatever here
                    }
                }
            }
            return rt;
        }

        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
                if (prop.Name == "uID"
                    || prop.Name == "dtCreateDate"
                    || prop.Name == "sModifyBy"
                    || prop.Name == "sModifyIP"
                    || prop.Name == "dtModifyDate")
                {
                }
                else
                {
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }
            return table;
        }

        public static DataTable CreateTable<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (T item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    if (prop.Name == "uID"
                || prop.Name == "dtCreateDate"
                || prop.Name == "sModifyBy"
                || prop.Name == "sModifyIP"
                || prop.Name == "dtModifyDate")
                    {
                    }
                    else
                    {
                        row[prop.Name] = prop.GetValue(item);
                    }
                table.Rows.Add(row);
            }
            return table;
        }

        #endregion List and DataTable

        public static string CalAge(string _birthDate)
        {
            string[] dateTime = _birthDate.Split('/');
            DateTime birthDate = new DateTime(int.Parse(dateTime[2]), int.Parse(dateTime[0]), int.Parse(dateTime[1]));
            int age = (int)Math.Floor((DateTime.Now - birthDate).TotalDays / 365.25D);

            return age.ToString();
        }

        public static string CalDate(string startDate, string endDate)
        {
            string[] dateTime = startDate.Split('/');
            string[] dateTimeend = endDate.Split('/');
            DateTime StartDate1 = new DateTime(int.Parse(dateTime[2]), int.Parse(dateTime[0]), int.Parse(dateTime[1]));
            DateTime EndDate1 = new DateTime(int.Parse(dateTimeend[2]), int.Parse(dateTimeend[0]), int.Parse(dateTimeend[1]));

            int date = (int)Math.Floor((EndDate1 - StartDate1).TotalDays) + 1;

            return date.ToString();
        }

        public static string MaxId(string vTable, string vColumn, bool vRepl)
        {
            string ServerType, sqlMxId;
            ServerType = null;
            sqlMxId = null;
            int result = 0;
            if (vRepl)
            {
                if (HttpContext.Current.Request.ServerVariables["HTTP_HOST"].Contains("192.168.0") || HttpContext.Current.Request.ServerVariables["HTTP_HOST"].Contains("localhost"))
                {
                    ServerType = "L";
                    sqlMxId = "select isnull(max(" + vColumn + ")+2, 1) as MxId from " + vTable + " where cast(" + vColumn + " as int) % 2 <> 0";
                }
                else
                {
                    ServerType = "U";
                    sqlMxId = "select isnull(max(" + vColumn + ")+2, 2) as MxId from " + vTable + " where cast(" + vColumn + " as int) % 2 = 0";
                }
            }
            return sqlMxId;
        }

        public static string localIP()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }

        public static string getIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = "";

            if (context.Request.ServerVariables["HTTP_X_REAL_IP"] == null)
            {
                ipAddress = context.Request.ServerVariables["REMOTE_HOST"].ToString();
            }
            else
            {
                ipAddress = context.Request.ServerVariables["HTTP_X_REAL_IP"].ToString();
            }

            return ipAddress;
        }

        public static string getCurrentUrl() {
            return HttpContext.Current.Request.Url.AbsoluteUri;
        }
    }
 