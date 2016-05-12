using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    public class cCommon
    {
        public static string cMonthThai(int month){

             string _cmonth ="";
             switch (month)
	         {
                case 1: _cmonth = "มกราคม"; break;
                case 2: _cmonth = "กุมภาพันธ์"; break;
                case 3: _cmonth = "มีนาคม"; break;
                case 4: _cmonth = "เมษายน"; break; 
                case 5: _cmonth = "พฤษภาคม"; break;
                case 6: _cmonth = "มิถุนายน"; break;
                case 7: _cmonth = "กรกฎาคม"; break;
                case 8: _cmonth = "สิงหาคม"; break;
                case 9: _cmonth = "กันยายน"; break;
                case 10: _cmonth = "ตุลาคม"; break;
                case 11: _cmonth = "พฤศจิกายน"; break;
                case 12: _cmonth = "ธันวาคม"; break;  
	         } 

            return _cmonth; 
        }
        public static string cYearThai(int year)
        {
            return (year + 543).ToString();
        }
    }
}
