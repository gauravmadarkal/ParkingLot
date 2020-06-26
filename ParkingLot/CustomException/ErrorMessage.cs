////////////////////////////////////////
/// Author Gaurav Madarkal
/// 25th June 2020
/// Parking Lot problem
////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot.CustomException
{
    /// <summary>
    /// Specific error messages defined for parking lot problem
    /// </summary>
    public static class ErrorMessage
    {
        public const string EmptySlots = "Please mention the number of slots";
        public const string InvalidValueForSlots = "Please mention a positive integer value for slots";
        public const string ParkingLotError = "Parking lot already exists, cannot create another, type Quit to exit";
        public const string ParkingDoesNotExist = "Parking lot does not exist, Please create a parking lot";
        public const string ParkingLotFull = "Sorry, parking lot is full";
        public const string SlotDoesNotExist = "Specified Slot does not exist";
        public const string InvalidCommand = "Command entered is invalid";
        public const string CarAlreadyExists = "Car already exists in the lot, duplicate entry";
    }
}
