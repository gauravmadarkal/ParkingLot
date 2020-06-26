////////////////////////////////////////
/// Author Gaurav Madarkal
/// 25th June 2020
/// Parking Lot problem
////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot.Model
{
    public class ParkingLot
    {
        public int NumberOfSlots;
        public Car[] Cars;
        public ParkingLot(int slots)
        {
            NumberOfSlots = slots;
            Cars = new Car[slots];
        }
    }
}
