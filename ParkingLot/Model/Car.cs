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
        /// <summary>
        /// unique value for each car
        /// </summary>
        public string RegistrationNumber { get; set; }
        /// <summary>
        /// color of the car
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// parking slot number allocated
        /// </summary>
        public int SlotNumber { get; set; }
    }
}
