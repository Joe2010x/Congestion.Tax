using System;
using System.Collections.Generic;
using congestion.calculator;
public class CongestionTaxCalculator
{
    /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that day
         */
    private static readonly string City = "Gothenburg";
    
    // private static readonly string City = "Stockholm";

    public int GetTax(Vehicle vehicle, DateTime[] dates)
    {
        if (dates == null || dates.Length == 0) return 0;
        if (dates.Length == 1) return GetTaxSameDay (vehicle, dates);

        var DateTimeList = new List<DateTime>();
        var totalFee =0;
        var intervalStart = dates[0];
        DateTimeList.Add(intervalStart);

        for (var i = 1; i< dates.Length; i++)
        {
            if (intervalStart.Date != dates[i].Date) 
            {
                totalFee = totalFee + GetTaxSameDay(vehicle,DateTimeList.ToArray());
                intervalStart = dates[i];
                DateTimeList.Clear();
            }
                DateTimeList.Add(dates[i]);
        }

        return totalFee + GetTaxSameDay(vehicle,DateTimeList.ToArray());
    }

    public int GetTaxSameDay(Vehicle vehicle, DateTime[] dates)
    {
        DateTime intervalStart = dates[0];
        int totalFee = 0;
        int tempFee = 0;
        int nextFee = 0;
        bool newInterval = true;
        foreach (DateTime date in dates)
        {
            if (newInterval) 
                {
                    tempFee = GetTollFee(intervalStart, vehicle);
                }

            nextFee = GetTollFee(date, vehicle);
            double minutes = date.Subtract(intervalStart).TotalMinutes;

            if (minutes <= 60)
            {   
                newInterval = false;
                if (totalFee > 0) totalFee = totalFee - tempFee;
                if (nextFee > tempFee) tempFee = nextFee;
                totalFee = totalFee + tempFee;
            }
            else
            {
                totalFee = totalFee + nextFee;
                intervalStart = date;
                newInterval = true;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        if (vehicle == null) return false;
        String vehicleType = vehicle.GetVehicleType();
        return vehicleType.Equals(TollFreeVehicles.Motorcycle.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Military.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Bus.ToString());
    }

    public int GetTollFee(DateTime date, Vehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

        int hour = date.Hour;
        int minute = date.Minute;
        if (City == "Gothenburg") {
            if (hour == 6 && minute >= 0 && minute <= 29) return 8;
            else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
            else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
            else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
            else if ((hour == 8 && minute >= 30) || (hour == 14  && minute <= 59) || (hour >8 && hour <14) ) return 8;
            else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
            else if ((hour == 15 && minute >= 30) || (hour == 16 && minute <= 59)) return 18;
            else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
            else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
            else return 0;
        }

        if (City == "Stockholm") {
            if (hour == 6 && minute >= 0 && minute <= 29) return 8 + 2;
            else if (hour == 6 && minute >= 30 && minute <= 59) return 13 + 2;
            else if (hour == 7 && minute >= 0 && minute <= 59) return 18 + 2;
            else if (hour == 8 && minute >= 0 && minute <= 29) return 13 + 2 ;
            else if  ((hour == 8 && minute >= 30) || (hour == 14  && minute <= 59) || (hour >8 && hour <14) ) return 8+2;
            else if (hour == 15 && minute >= 0 && minute <= 29) return 13 + 2;
            else if ((hour == 15 && minute >= 30) || (hour == 16 && minute <= 59)) return 18+2;
            else if (hour == 17 && minute >= 0 && minute <= 59) return 13 + 2;
            else if (hour == 18 && minute >= 0 && minute <= 29) return 8 + 2;
            else return 0;
        }

        return 0;
    }

    private bool IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (year == 2013)
        {
            if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 20 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }
        }
        return false;
    }

    private enum TollFreeVehicles
    {
        Motorcycle = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5,
        Bus = 6
    }
}