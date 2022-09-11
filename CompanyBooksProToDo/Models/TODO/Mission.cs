using CompanyBooksProToDo.Helpers;
using System;

namespace CompanyBooksProToDo.Models.TODO
{
    public class Mission
    {
        public long lMISSION_ID { get; set; }

        public long lMISSION_APPLIED_TO_ID { get; set; }    //ID_ITEM //Planned Group
        public string sMISSION_APPLIED_TO_NUMBER { get; set; }//ITEM_NUMBER
        public string sMISSION_APPLIED_TO_NAME { get; set; } //ITEM_NAME

        public string sMISSION_NAME { get; set; } //ITEM_NAME
        public string sMISSION_TEXT { get; set; } //sMISSION_FOR_EMPLOYEE_NAME + " " + sMISSION_NAME + Date Time 1 + Time 2


        public long lMISSION_DATE_OPEN { get; set; }
        public DateTime DATE_MISSION_DATE_OPEN => ApiHelper.Long8DigitToDateTime(lMISSION_DATE_OPEN);


        public string sMISSION_CATEGORY_LEVEL_0_NAME { get; set; }

        public long lMISSION_FOR_EMPLOYEE_ID { get; set; }
        public string sMISSION_FOR_EMPLOYEE_NAME { get; set; }
        public string sMISSION_FOR_EMPLOYEE_NUMBER { get; set; }

        public enumEmailingTypes lEmailingType { get; set; }



        public long lMISSION_DATE_CLOSE { get; set; }
        public DateTime DATE_MISSION_DATE_CLOSE => ApiHelper.Long8DigitToDateTime(lMISSION_DATE_CLOSE);


        public bool bMISSION_IS_REMINDE { get; set; }
        public long lMISSION_DATE_REMINDER { get; set; }
        public DateTime DATE_MISSION_DATE_REMINDER => ApiHelper.Long8DigitToDateTime(lMISSION_DATE_REMINDER);


        public bool bMISSION_IS_DESTINATION { get; set; }
        public long lMISSION_DATE_DESTINATION { get; set; }
        public DateTime DATE_MISSION_DATE_DESTINATION => ApiHelper.Long8DigitToDateTime(lMISSION_DATE_DESTINATION);


        public bool bMISSION_IS_NOT_ACTIVE { get; set; }

        public string sMISSION_COLOR_NAME { get; set; }
        public int lMISSION_COLOR_NUMBER { get; set; }

        public int lMISSION_WORKDAY_TIME_START { get; set; } //1201
        public DateTime DATE_MISSION_WORKDAY_TIME_START => ApiHelper.Long8DigitToDateTime(lMISSION_WORKDAY_TIME_START);

        public int lMISSION_WORKDAY_TIME_FINISH { get; set; } //1830
        public DateTime DATE_MISSION_WORKDAY_TIME_FINISH => ApiHelper.Long8DigitToDateTime(lMISSION_WORKDAY_TIME_FINISH);


        public bool bMISSION_IS_OPEN { get; set; }
        public bool bMISSION_IS_CLOSE { get; set; }

        public string sMISSION_DESCRIPTION_1 { get; set; }
        public string sMISSION_DESCRIPTION_2 { get; set; }
        public string sMISSION_DESCRIPTION_3 { get; set; }

        public string sGPS_Location { get; set; }
        public string sGPS_Name { get; set; }

        public string sGPS_Location_2 { get; set; }
        public string sGPS_Name_2 { get; set; }

        
    }
}
