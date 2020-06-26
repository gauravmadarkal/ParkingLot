////////////////////////////////////////
/// Author Gaurav Madarkal
/// 25th June 2020
/// Parking Lot problem
////////////////////////////////////////


namespace ParkingLot.Constants
{
    /// <summary>
    /// frequently used constants and string messages
    /// </summary>
    static class ConstantsKey
    {
        public const string QuitCmd = "QUIT";
        public const string parkingLotCreated = "Created a parking lot with {0} slots";
        public const string CarParked = "Allocated slot number: {0}";
        public const string SpotFree = "Slot number {0} is free";
        public const string NotFound = "Not found";
        public const string CommandHelp = "Available commands: \n Task commands: \n create_parking_lot, \n park, \n leave, \n status, " +
            "\nQuery Commands: \n registration_numbers_for_cars_with_colour, \n slot_numbers_for_cars_with_colour, \n slot_number_for_registration_number, \nQUIT, \nHELP";
        public const string HelpCmd = "HELP";
        public const string whitespace = " ";
    }
}
