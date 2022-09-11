using CompanyBooksProECom.Services;
using CompanyBooksProToDo.Models.TODO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace CompanyBooksProToDo.Helpers
{

    public static class ApiHelper
    {
        public static ApiClientConfig ApiConfig { get; set; }
        public static HttpContext Context { get; set; }
        public static Employee CurrentUser
        {
            get
            {
                if (Context == null) return null;
                if (!Context.Request.Cookies.ContainsKey("Id_client"))
                    return null;
                return new Employee
                {
                    Id_client = Convert.ToInt32(Context.Request.Cookies["Id_client"]),
                    Client_name = Context.Request.Cookies["Client_name"],
                    Client_number = Context.Request.Cookies["Client_number"],
                };
            }
        }


        #region General info
        public static string GetCompanyName()
        {
            return ApiConfig.AppConfig.APPLICATION_COMPANY_NAME;
        }
        public static string GetWebsiteCopyright()
        {
            return ApiConfig.AppConfig.APPLICATION_WEBSITE_PROPERTIES.COMPANY_WEBSITE_COPYRIGHT;
        }
        #endregion

        #region Languages
        public static List<enumLanguagesEnum> GetLanguages()
        {
            return ApiConfig.AppConfigExtended.APPLICATION_ESHOP_SETTINGS.APPLICATION_ESHOP_LANGUAGES;
        }
        public static enumLanguagesEnum GetDefaultLanguage()
        {
            return ApiConfig.AppConfigExtended.APPLICATION_ESHOP_SETTINGS.APPLICATION_ESHOP_LANGUAGE_DEFAULT;
        }
        public static enumLanguagesEnum GetCurrentLanguage()
        {
            if(Context is null)
                return GetDefaultLanguage();
            if (Context.Request.Cookies.ContainsKey("Language"))
                return (enumLanguagesEnum)Convert.ToInt32(Context.Request.Cookies["Language"]);
            else
                return GetDefaultLanguage();
        }
        public static bool IsCurrentLanguageRightToLeft()
        {
            var lang = GetCurrentLanguage();
            return lang == enumLanguagesEnum.langHebrew || lang == enumLanguagesEnum.langArabic;
        }

        #endregion

        #region Users
        public static Employee GetUser(string username, string password)
        {
            var dataset = ApiConfig.AppConfig.clsDBLayer.ESHOP_TODO_CLIENT_BY_USER_NAME_AND_PASSWORD_DATASET(
                                                                                 ApiConfig.AppConfig,
                                                                                 ApiConfig.AppConfigExtended,
                                                                                 username,
                                                                                 password);
            if (dataset.Tables.Count == 0) return null;
            if(dataset.Tables[0].Rows.Count == 0) return null;

            var row = dataset.Tables[0].Rows[0];
            var user = new Employee
            {
                Id_client = (int)(long)row["Id_client"],
                Client_name = (string)row["Client_name"],
                Client_number = ((string)row["Client_number"]),
            };
            return user;
        }

        public static bool HasUser(string username,string password)
        {
            return GetUser(username, password) != null;
        }
        #endregion

        #region Missions
        public static List<Mission> GetMissions()
        {
            List<Mission> missions = new List<Mission>();

            long todayDtLong = DateTime_Format_YYYYMMDD_From_DateTime(DateTime.Now);
            var dataset = ApiConfig.AppConfig.clsDBLayer.ESHOP_TODO_SELECT_MISSIONS_FOR_EMPLOYEE_AND_ITEMS(
                                                                        ApiConfig.AppConfig,
                                                                        ApiConfig.AppConfigExtended,
                                                                        CurrentUser.Id_client,
                                                                        CurrentUser.Client_name,
                                                                        CurrentUser.Client_number,
                                                                        0,
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        todayDtLong,
                                                                        todayDtLong,
                                                                        todayDtLong,
                                                                        todayDtLong,
                                                                        false);

            foreach(DataRow item in dataset.Tables[0].Rows)
            {
                missions.Add(ParseMission(item));
            }
            return missions;
        }
        public static Mission GetMissionById(long id)
        {
            var dataset = ApiConfig.AppConfig.clsDBLayer.ESHOP_TODO_SELECT_MISSIONS_DATASET_BY_ID(
                                                                        ApiConfig.AppConfig,
                                                                        ApiConfig.AppConfigExtended,
                                                                        id);
            return ParseMission(dataset.Tables[0].Rows[0]);
        }
        public static void UpdateMission(Mission mission)
        {
            UpdateOrCreateMission(mission);
        }
        public static long CreateMission(Mission mission)
        {
            return UpdateOrCreateMission(mission);
        }




        private static long UpdateOrCreateMission(Mission mission)
        {
            long newId = 0;
            ApiConfig.AppConfig.clsDBLayer.ESHOP_TODO_UPDATE_MISSION_ONE_FOR_EMPLOYEE_FOR_ITEM(
                                                                    ApiConfig.AppConfig,
                                                                    ApiConfig.AppConfigExtended,
                                                                    mission.lMISSION_ID,
                                                                    mission.lMISSION_APPLIED_TO_ID,
                                                                    mission.sMISSION_APPLIED_TO_NUMBER,
                                                                    mission.sMISSION_APPLIED_TO_NAME,
                                                                    mission.sMISSION_NAME,
                                                                    mission.sMISSION_TEXT,
                                                                    DateTime_Format_YYYYMMDD_From_DateTime(mission.DATE_MISSION_DATE_OPEN),
                                                                    mission.sMISSION_CATEGORY_LEVEL_0_NAME,
                                                                    CurrentUser.Id_client,
                                                                    CurrentUser.Client_name,
                                                                    CurrentUser.Client_number,
                                                                    enumEmailingTypes.Emailing_NotSend,
                                                                    DateTime_Format_YYYYMMDD_From_DateTime(mission.DATE_MISSION_DATE_CLOSE),
                                                                    mission.bMISSION_IS_REMINDE,
                                                                    DateTime_Format_YYYYMMDD_From_DateTime(mission.DATE_MISSION_DATE_REMINDER),
                                                                    mission.bMISSION_IS_DESTINATION,
                                                                    DateTime_Format_YYYYMMDD_From_DateTime(mission.DATE_MISSION_DATE_DESTINATION),
                                                                    mission.bMISSION_IS_NOT_ACTIVE,
                                                                    mission.sMISSION_COLOR_NAME,
                                                                    mission.lMISSION_COLOR_NUMBER,
                                                                    (int)DateTime_Format_YYYYMMDD_From_DateTime(mission.DATE_MISSION_WORKDAY_TIME_START),
                                                                    (int)DateTime_Format_YYYYMMDD_From_DateTime(mission.DATE_MISSION_WORKDAY_TIME_FINISH),
                                                                    mission.bMISSION_IS_OPEN,
                                                                    mission.bMISSION_IS_CLOSE,
                                                                    mission.sMISSION_DESCRIPTION_1,
                                                                    mission.sMISSION_DESCRIPTION_2,
                                                                    mission.sMISSION_DESCRIPTION_3,
                                                                    mission.sGPS_Location,
                                                                    mission.sGPS_Name,
                                                                    mission.sGPS_Location_2,
                                                                    mission.sGPS_Name_2,
                                                                    out newId);
            return newId;
        }
        private static Mission ParseMission(DataRow row)
        {
            var mission = new Mission
            {
                lMISSION_ID = (long)row["lMISSION_ID"],
                lMISSION_APPLIED_TO_ID = (long)row["lMISSION_APPLIED_TO_ID"],
                sMISSION_APPLIED_TO_NUMBER = (string)row["sMISSION_APPLIED_TO_NUMBER"],
                sMISSION_APPLIED_TO_NAME = (string)row["sMISSION_APPLIED_TO_NAME"],
                sMISSION_NAME = (string)row["sMISSION_NAME"],
                sMISSION_TEXT = (string)row["sMISSION_TEXT"],
                lMISSION_DATE_OPEN = (long)row["lMISSION_DATE_OPEN"],
                sMISSION_CATEGORY_LEVEL_0_NAME = (string)row["sMISSION_CATEGORY_LEVEL_0_NAME"],
                lMISSION_FOR_EMPLOYEE_ID = (long)row["lMISSION_FOR_EMPLOYEE_ID"],
                sMISSION_FOR_EMPLOYEE_NAME = (string)row["sMISSION_FOR_EMPLOYEE_NAME"],
                sMISSION_FOR_EMPLOYEE_NUMBER = (string)row["sMISSION_FOR_EMPLOYEE_NUMBER"],
                lEmailingType = (enumEmailingTypes)row["lEmailingType"],
                lMISSION_DATE_CLOSE = (long)row["lMISSION_DATE_CLOSE"],
                bMISSION_IS_REMINDE = (bool)row["bMISSION_IS_REMINDE"],
                lMISSION_DATE_REMINDER = (long)row["lMISSION_DATE_REMINDER"],
                bMISSION_IS_DESTINATION = (bool)row["bMISSION_IS_DESTINATION"],
                lMISSION_DATE_DESTINATION = (long)row["lMISSION_DATE_DESTINATION"],
                bMISSION_IS_NOT_ACTIVE = (bool)row["bMISSION_IS_NOT_ACTIVE"],
                sMISSION_COLOR_NAME = (string)row["sMISSION_COLOR_NAME"],
                lMISSION_COLOR_NUMBER = (int)row["lMISSION_COLOR_NUMBER"],
                lMISSION_WORKDAY_TIME_START = (int)row["lMISSION_WORKDAY_TIME_START"],
                lMISSION_WORKDAY_TIME_FINISH = (int)row["lMISSION_WORKDAY_TIME_FINISH"],
                bMISSION_IS_OPEN = (bool)row["bMISSION_IS_OPEN"],
                bMISSION_IS_CLOSE = (bool)row["bMISSION_IS_CLOSE"],
                sMISSION_DESCRIPTION_1 = (string)row["sMISSION_DESCRIPTION_1"],
                sMISSION_DESCRIPTION_2 = (string)row["sMISSION_DESCRIPTION_2"],
                sMISSION_DESCRIPTION_3 = (string)row["sMISSION_DESCRIPTION_3"],
                sGPS_Location = (string)row["sGPS_Location"],
                sGPS_Name = (string)row["sGPS_Name"],
                sGPS_Location_2 = (string)row["sGPS_Location_2"],
                sGPS_Name_2 = (string)row["sGPS_Name_2"],
            };
            return mission;
        }

        #endregion

        #region Products
        public static List<ProductModel> GetProductsByParameters()
        {
            List<ProductModel> items = new List<ProductModel>();
            var dataset = ApiConfig.AppConfig.clsDBLayer.ESHOP_TODO_SELECT_ITEMS_BY_PARAMETERS(
                                                                        ApiConfig.AppConfig,
                                                                        ApiConfig.AppConfigExtended,
                                                                        "",
                                                                        enumItemsTypesEnum.itAllItemTypes,
                                                                        "",
                                                                        0,
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        "");
            foreach(DataRow row in dataset.Tables[0].Rows)
            {
                items.Add(ParseProductModel(row));
            }
            return items;
        }
        private static ProductModel ParseProductModel(DataRow row)
        {
            var model = new ProductModel
            {
                ID_ITEM = (int)row["ID_ITEM"],
                ITEM_NAME = (string)row["ITEM_NAME"],
                ITEM_NUMBER = (string)row["ITEM_NUMBER"],
            };
            return model;
        }
        #endregion


        public static long DateTime_Format_YYYYMMDD_From_DateTime(DateTime dt)
        {
            try
            {
                return (dt.Year * 10000) + (dt.Month * 100) + dt.Day;
            }
            catch (Exception Exception1)
            {
                return 0;
            }
        }

        public static DateTime Long8DigitToDateTime(long val)
        {
            string str = val.ToString();

            int year = Convert.ToInt32(str.Substring(0,4));
            int day = Convert.ToInt32(str.Substring(4, 2));
            int month = Convert.ToInt32(str.Substring(6, 2));

            return new DateTime(year, month, day);
        }
    }
}
