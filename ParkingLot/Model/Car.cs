using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot.Model
{
    /// <summary>
    /// model class, denotes one instance of a car
    /// </summary>
    public class Car
    {
        public string RegistrationNumber { get; set; }
        public string Color { get; set; }
        public int SlotNumber { get; set; }
    }
}
