using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.ComponentModel;

namespace iEmpPower.Old_App_Code.iEmpPowerBL.Common
{
    public class AllEnum
    {
        public static IEnumerable<T> EnumToList<T>()
        {
            Type enumType = typeof(T);
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            Array enumValArray = Enum.GetValues(enumType);
            List<T> enumValList = new List<T>(enumValArray.Length);

            foreach (int val in enumValArray)
            {
                enumValList.Add((T)Enum.Parse(enumType, val.ToString()));
            }
            return enumValList;
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }

    public enum ReuisitionStatus
    {
        [Description("New requisition")]
        New = 1,                                            //1
        [Description("Requisition cancelled by traveller")]
        RequisitionCancelledByTraveller,                    //2
        [Description("Requisition approved")]
        RequisitionApprroved,                               //3
        [Description("Requisition rejected")]
        RequisitionRejected,                                //4
        [Description("Requisition cancelled by TD")]
        RequisitionCancelledByTD,                           //5
        [Description("New proposal")]
        NewProposal,                                        //6
        [Description("Proposal rejected")]
        ProposalReject,                                     //7
        [Description("Not booked")]
        NotBooked,                                          //8
        [Description("Booked")]
        Booked,                                             //9
        [Description("Proposal cancelled")]
        ProposalCancelled,                                  //10
        [Description("Requisition cancelled by HOD")]
        RequisitionCancelledByHOD,                          //11
        [Description("Correction")]
        ReqSentForCorrections                               //12
    }

    public enum TravelType
    {
        All = 0,
        Domestic,
        International
    }

    public enum LocalRequisitionType
    {
        All = 0,
        Single,
        Group,
        Guest
    }
    public enum SeatPreference
    {
        //ASILE = 1,
        //MIDDLE = 2,
        //WINDOW = 3
        [Description("ASILE")]
        ASILE = 'A',
        [Description("MIDDLE")]
        MIDDLE = 'M',
        [Description("WINDOW")]
        WINDOW = 'W',
        [Description("EMERGENCY ASILE")]
        EMERGENCYASILE = 'E',
        [Description("EMERGENCY EXIT")]
        EMERGENCYEXIT = 'X'


    }
    public enum MealPreference
    {
        //[Description("ASIAN VEG MEAL")]
        //ASIANVEGMEAL = 1,
        //[Description("NON VEG MEAL")]
        //NONVEGMEAL = 2
        [Description("ASIAN VEGETARIAN MEAL")]
        ASIANVEGETARIANMEAL = 1,
        [Description("INFANT/BABY FOOD")]
        INFANTBABYFOOD = 2,
        [Description("CHILD MEAL")]
        CHILDMEA = 3,
        [Description("DIABETIC MEAL")]
        DIABETICMEAL = 4,
        [Description("FRUIT PLATTER")]
        FRUITPLATTER =5,
        [Description("HINDU (NON VEGETARIAN) MEAL")]
        HINDUNONVEGETARIANMEAL = 6,
        [Description("LOW CALORIE MEAL")]
        LOWCALORIEMEAL = 7,
        [Description("LOW CHOLESTEROL/LOW FAT MEAL")]
        LOWCHOLESTEROLLOWFATMEAL = 8,
        [Description("LOW SODIUM, NO SALT ADDED")]
        LOWSODIUMNOSALTADDED = 9,
        [Description("MOSLEM MEAL")]
        MOSLEMMEAL =10,
        [Description("NO FISH MEAL (LH SPECIFIC)")]
        NOFISHMEALLHSPECIFIC = 11,
        [Description("NON LACTOSE MEAL")]
        NONLACTOSEMEAL = 12,
        [Description("ORIENTAL MEAL")]
        ORIENTALMEAL = 13,
        [Description("RAW VEGETARIAN MEAL")]
        RAWVEGETARIANMEAL = 14,
        [Description("SEA FOOD MEAL")]
        SEAFOODMEAL = 15,
        [Description("SPECIAL MEAL, SPECIFY FOOD")]
        SPECIALMEALSPECIFYFOOD = 16,
        [Description("VEGETARIAN MEAL (NON-DAIRY)")]
        VEGETARIANMEALNONDAIRY = 17,
        [Description("VEGETARIAN MEAL (LACTO-OVO)")]
        VEGETARIANMEALLACTOOVO = 18,
        [Description("VEGETARIAN ORIENTAL MEAL")]
        VEGETARIANORIENTALMEAL = 19
    }
    public enum Baggage
    {
        //[Description("1KG")]
        //KG2 = 1,
        //[Description("2KG")]
        //KG4 = 2,
        //[Description("3KG")]
        //KG6 = 3,
        //[Description("4KG")]
        //KG8 = 4

        [Description("10 KG")]
        KG2 = 'C',
        [Description("15 KG")]
        KG4 = 'D',
        [Description("20 KG")]
        KG6 = 'E',
        [Description("23 KG")]
        KG8 = 'F',
        [Description("25 KG")]
        KG10 = 'G',
        [Description("30 KG")]
        KG12 = 'H',
        [Description("40 KG")]
        KG13 = 'I',
        [Description("1 PC")]
        KG14 = 'J',
        [Description("2 PC")]
        KG15 = 'K'

    }
    public enum Hand
    {
        //[Description("1KG")]
        //KG2 = 1,
        //[Description("2KG")]
        //KG4 = 2,
        //[Description("3KG")]
        //KG6 = 3,
        //[Description("4KG")]
        //KG8 = 4
        [Description("5 KG")]
        KG2 = 'A',
        [Description("7 KG")]
        KG4 = 'B'


    }
    public enum VehicleName
    {
        [Description("ABCD Express 12345")]
        ABCDExpress12345 = 1,
        [Description("XYZ Fast Passenger 85235")]
        XYZFastPassenger85235 = 2,
        [Description("Chamundi Express 56478")]
        ChamundiExpress56478 = 3,
        [Description("Udyan Express 63636")]
        UdyanExpress63636 = 4,
        [Description("Rajdani 36365")]
        Rajdani36365 = 5,
        [Description("Shatabdi 11111")]
        Shatabdi11111 = 6
    }
    
    public enum RoomCategory
    {
         [Description("Delux")]
        Delux = 1,
         [Description("AC")]
        AC,
         [Description("Non AC")]
        NonAC
    }

    public enum  HotelName
    {
        Prince = 1,
        Regalis,
        Kings
    }
    public enum HotelCategory
    {
        [Description("1 Star")]
        Star1 = 1,
        [Description("2 Star")]
        Star2,
        [Description("3 Star")]
        Star3,
        [Description("Budget")]
        Budget,
    }
}