////////////////////////////////////////
/// Author Gaurav Madarkal
/// 25th June 2020
/// Parking Lot problem
////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot.Interface
{
    /// <summary>
    /// commands to run on the parking lot
    /// method names match the command for compatibility with reflections
    /// </summary>
    public interface ICommands
    {
        public void create_parking_lot(string numberofslots);
        public void park(string regNo, string color);
        public void leave(string slotNumber);
        public void status();
        public List<string> registration_numbers_for_cars_with_colour(string color);
        public List<int> slot_numbers_for_cars_with_colour(string color);
        public int slot_number_for_registration_number(string regNo);
    }
}
