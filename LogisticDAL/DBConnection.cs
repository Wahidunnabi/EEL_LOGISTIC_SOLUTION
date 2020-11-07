
using System.Collections.Generic;
using System.Linq;

using System;
using System.Configuration;

namespace LOGISTIC.DAL
{
   public class DBConnection
    {
       
            //private static string _sqlServerName, _databaseName, _userId, _pass;

            public static string _dbEntityConnectionString = ConfigurationManager.ConnectionStrings["Logisticentities"].ConnectionString;

                       //public static string DatabaseConnectionString
                       //{
                       //    get
                       //    {
                       //        return GenerateFullConnectionString(_databaseConnectionString);
                       //    }
                       //}
   //     public static string dbEntityConnectionString
   //     {
   //         get
   //         {
   //             return GenerateFullConnectionString(_dbEntityConnectionString);
   //         }
   //     }
   //     public static string DatabaseOLEDBConnectionString
   //     {
   //         get
   //         {
   //             return GenerateFullConnectionString(_dbOLEDBConnectionString);
   //         }
   //     }


   //     public static string SqlServerName
   //     {
   //         get
   //         {
   //             var aSecurityUtility = new SecurityUtility();
   //             return _sqlServerName = aSecurityUtility.Decode(ConfigurationManager.AppSettings["SqlServerName"]);
   //         }
   //     }
   //     public static string DatabaseName
   //     {
   //         get
   //         {
   //             var aSecurityUtility = new SecurityUtility();
   //             return _sqlServerName = aSecurityUtility.Decode(ConfigurationManager.AppSettings["DatabaseName"]);
   //         }
   //     }
   //     public static string UserId
   //     {
   //         get
   //         {
   //             var aSecurityUtility = new SecurityUtility();
   //             return _sqlServerName = aSecurityUtility.Decode(ConfigurationManager.AppSettings["UserId"]);
   //         }
   //     }
   //     public static string Password
   //     {
   //         get
   //         {
   //             var aSecurityUtility = new SecurityUtility();
   //             return _sqlServerName = aSecurityUtility.Decode(ConfigurationManager.AppSettings["Password"]);
   //         }
   //     }

   //     private static string GenerateFullConnectionString(string partialConnectionString)
   //     {
   //         _sqlServerName = ConfigurationManager.AppSettings["SqlServerName"];
   //         _databaseName = ConfigurationManager.AppSettings["DatabaseName"];
   //         _userId = ConfigurationManager.AppSettings["UserId"];
   //         _pass = ConfigurationManager.AppSettings["Password"];

   //         var aSecurityUtility = new SecurityUtility();
   //         _sqlServerName = aSecurityUtility.Decode(_sqlServerName);
   //         _databaseName = aSecurityUtility.Decode(_databaseName);
   //         _userId = aSecurityUtility.Decode(_userId);
   //         _pass = aSecurityUtility.Decode(_pass);


   //         string fullStr = partialConnectionString;
   //         fullStr = fullStr.Replace("*SQL_SERVER*", _sqlServerName);
   //         fullStr = fullStr.Replace("*DB_NAME*", _databaseName);
   //         fullStr = fullStr.Replace("*USER*", _userId);
   //         fullStr = fullStr.Replace("*PASSWORD*", _pass);

   //         return fullStr;
   //     }
   //     public static EntityConnection BobEntityConnection
   //     {
   //         get { return new EntityConnection(GenerateFullConnectionString(_dbEntityConnectionString)); }
   //     }
    }
}
