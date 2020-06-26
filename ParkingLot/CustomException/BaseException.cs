////////////////////////////////////////
/// Author Gaurav Madarkal
/// 25th June 2020
/// Parking Lot problem
////////////////////////////////////////


using System;

namespace ParkingLot.CustomException
{
    /// <summary>
    /// Custom exception class which includes a custom error message
    /// </summary>
    [Serializable]
    public class BaseException : Exception
    {
        public string ErrorMessage;
        public BaseException()
        {
        }
        public BaseException(string message) : base(message)
        {
            this.ErrorMessage = message;
        }
        public BaseException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
