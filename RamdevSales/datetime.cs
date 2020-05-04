using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;

namespace RamdevSales
{
     class datetime
    {
       internal string convertdate(string date1, string dateformate, string returndateformate,char splitchar)
       {
           CultureInfo en = new CultureInfo("en-US");
           Thread.CurrentThread.CurrentCulture = en;
           string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
           string[] dt = date1.Split(splitchar);
           string[] dtformate = dateformate.Split(splitchar);
           string[] returndtformate = returndateformate.Split(splitchar);
           string strdate="";
           bool flag = false;

           for (int i = 0; i < returndtformate.Length; i++)
           {
               if (returndtformate[i].ToUpper() == "MMM")
               {
                   flag = true;
               }
               for (int j = 0; j < dtformate.Length; j++)
               {
                   if (dtformate[j].ToUpper() == "MMM" & flag==false)
                   {
                       dtformate[j] = "MM";
                       dt[j] = getmonth(dt[j]);
                   }
                   if (dtformate[j].ToUpper() == "MM" & flag == true)
                   {
                       dtformate[j] = "MMM";
                       dt[j] = getmonth(dt[j]);
                   }
                  
                   if (returndtformate[i].ToUpper() == dtformate[j].ToUpper())
                   {
                       strdate += dt[j];
                       if (i < returndtformate.Length - 1)
                       {
                           strdate += splitchar;
                       }
                   }
               }
           }
           return strdate;

           //string fy = "";
           //if (sysFormat == "dd-MM-yyyy")
           //{
           //    string sqlformate = "yyyy-MM-dd";
           //    string dateString = dateformate; // Modified from MSDN
           //    fy = DateTime.Parse(dateString).ToString(sqlformate);

           //}
           //return fy;
       }

       public string getmonth(string month)
       {
           string ans="";
           if (month == "01" || month == "1")
           {
               ans="Jan";
           }
           else if (month == "02" || month == "2")
           {
               ans="Feb";
           }
           else if (month == "03" || month == "3")
           {
               ans = "Mar";
           }
           else if (month == "04" || month == "4")
           {
               ans = "Apr";
           }
           else if (month == "05" || month == "5")
           {
               ans = "May";
           }
           else if (month == "06" || month == "6")
           {
               ans = "Jun";
           }
           else if (month == "07" || month == "7")
           {
               ans = "July";
           }
           else if (month == "08" || month == "8")
           {
               ans = "Aug";
           }
           else if (month == "09" || month == "9")
           {
               ans = "Sep";
           }
           else if (month == "10")
           {
               ans = "Oct";
           }
           else if (month == "11")
           {
               ans = "Nov";
           }
           else if (month == "12")
           {
               ans = "Dec";
           }
           else if (month.ToUpper() == "JAN")
           {
               ans = "01";
           }
           else if (month.ToUpper() == "FEB")
           {
               ans = "02";
           }
           else if (month.ToUpper() == "MAR")
           {
               ans = "03";
           }
           else if (month.ToUpper() == "APR")
           {
               ans = "04";
           }
           else if (month.ToUpper() == "MAY")
           {
               ans = "05";
           }
           else if (month.ToUpper() == "JUN")
           {
               ans = "06";
           }
           else if (month.ToUpper() == "JULY")
           {
               ans = "07";
           }
           else if (month.ToUpper() == "AUG")
           {
               ans = "08";
           }
           else if (month.ToUpper() == "SEP")
           {
               ans = "09";
           }
           else if (month.ToUpper() == "OCT")
           {
               ans = "10";
           }
           else if (month.ToUpper() == "NOV")
           {
               ans = "11";
           }
           else if (month.ToUpper() == "DEC")
           {
               ans = "12";
           }
           return ans;
       }
    
    }
}
