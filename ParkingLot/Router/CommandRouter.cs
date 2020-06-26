////////////////////////////////////////
/// Author Gaurav Madarkal
/// 25th June 2020
/// Parking Lot problem
////////////////////////////////////////


using ParkingLot.Constants;
using ParkingLot.CustomException;
using ParkingLot.Interface;
using ParkingLot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ParkingLot.Router
{
    /// <summary>
    /// router class which handles command routing to respective workers
    /// </summary>
    public class CommandRouter : ICommands
    {

        #region routers
        /// <summary>
        /// Entry point for all commands sent using CLI
        /// </summary>
        /// <param name="command">command which is routed into respective worker</param>
        /// <returns>Status of the workers job</returns>
        public static RoutingStatus RouteCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
                return RoutingStatus.Invalid;
            if (command.Equals(ConstantsKey.QuitCmd, StringComparison.InvariantCultureIgnoreCase))
                return RoutingStatus.Quit;
            if (command.Equals(ConstantsKey.HelpCmd, StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine(ConstantsKey.CommandHelp);
                return RoutingStatus.Success;
            }
            return ProcessCommand(command);
        }

        /// <summary>
        /// Function validates command, fetches parameters, calls the appropriate worker
        /// </summary>
        /// <param name="command">indicates the job to perform</param>
        /// <returns>Success or Invalid based on command and parameters</returns>
        private static RoutingStatus ProcessCommand(string command)
        {
            command = command.Trim();
            string[] args = command.Split(ConstantsKey.whitespace);
            //removing additional whitespaces if given any
            args = args.Select(a => a.Trim()).ToArray();

            if (string.IsNullOrEmpty(args[0]))
                return RoutingStatus.Invalid;

            //command
            string methodName = args[0];

            //fetch command parameters
            string[] parameters = ValidateParameters(args);

            //calling method with reflection which simplifies and results in a cleaner code
            //method names will be same as the command name
            Type type = typeof(CommandRouter);
            object obj = Activator.CreateInstance(type);
            var method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
            try
            {
                if (method != null)
                {
                    method.Invoke(obj, parameters);
                    return RoutingStatus.Success;
                }
                return RoutingStatus.Invalid;
            }
            catch (Exception e)
            {
                if(e.InnerException != null && e.InnerException.GetType().Equals(typeof(BaseException)))
                {
                    throw e.InnerException;
                }
                throw;
            }
        }
        #endregion

        #region taskcommands
        /// <summary>
        /// function used to create an entire parking lot
        /// </summary>
        /// <param name="numberofslots">indicates the number of slots to be allocated</param>
        public void create_parking_lot(string numberofslots)
        {
            int slots;
            slots = PreCheck(numberofslots);
            if (Program.ParkingLot == null)
            {
                Program.ParkingLot = new Model.ParkingLot(slots);
                Console.WriteLine(ConstantsKey.parkingLotCreated, slots);
            }
            else
                throw new BaseException(ErrorMessage.ParkingLotError);
        }

        /// <summary>
        /// function used to assign a car to a parking lot
        /// </summary>
        /// <param name="regNo">Registration number of the car</param>
        /// <param name="color">Color of the car</param>
        public void park(string regNo, string color)
        {
            if (Program.ParkingLot == null)
                throw new BaseException(ErrorMessage.ParkingDoesNotExist);

            int freeSlot = FindFreeParkingSlot();
            if (freeSlot == -1)
                throw new BaseException(ErrorMessage.ParkingLotFull);

            //check if car already exists 
            if (ValidateCarExistance(regNo))
            {
                Car car = new Car
                {
                    Color = color,
                    RegistrationNumber = regNo,
                    SlotNumber = freeSlot + 1
                };
                Program.ParkingLot.Cars[freeSlot] = car;
                //array index starts from 0, adding 1 for userfriendly slotnumber
                Console.WriteLine(ConstantsKey.CarParked, freeSlot + 1);
            }
        }

        /// <summary>
        /// function denotes the task of a car leaving the lot
        /// </summary>
        /// <param name="slotNumber">slot number which is emptied</param>
        public void leave(string slotNumber)
        {
            int slot = PreCheck(slotNumber) - 1;
            try
            {
                Program.ParkingLot.Cars[slot] = null;
                Console.WriteLine(ConstantsKey.SpotFree, slot + 1);
            }
            catch (IndexOutOfRangeException)
            {
                throw new BaseException(ErrorMessage.SlotDoesNotExist);
            }
        }

        /// <summary>
        /// function is used to query the current status of the parking lot
        /// </summary>
        public void status()
        {
            if (Program.ParkingLot == null)
                throw new BaseException(ErrorMessage.ParkingDoesNotExist);
            Console.WriteLine("Slot No.\tRegistration No.\tColor");
            foreach(Car car in Program.ParkingLot.Cars)
            {
                if(car != null)
                {
                    Console.WriteLine("{0}\t{1}\t{2}", car.SlotNumber, car.RegistrationNumber, car.Color);
                }
            }
        }

        #endregion

        #region querycommands
        /// <summary>
        /// queries the parking lot and fetches registration number of cars
        /// </summary>
        /// <param name="color">The color of the car to be fetched</param>
        public List<string> registration_numbers_for_cars_with_colour(string color)
        {
            if (Program.ParkingLot == null)
                throw new BaseException(ErrorMessage.ParkingDoesNotExist);
            List<string> regNos = Program.ParkingLot.Cars
                .Where(c => c != null && c.Color.Equals(color, StringComparison.InvariantCultureIgnoreCase))
                .Select(q => q.RegistrationNumber).ToList();
            if (regNos.Any())
            {
                Console.WriteLine(string.Join(", ", regNos));
                return regNos;
            }
            else
            {
                Console.WriteLine(ConstantsKey.NotFound);
                return null;
            }
        }

        /// <summary>
        /// Queries the slot-numbers for all cars with given color
        /// </summary>
        /// <param name="color">color for which slot number needs to be fetched</param>
        public List<int> slot_numbers_for_cars_with_colour(string color)
        {
            if (Program.ParkingLot == null)
                throw new BaseException(ErrorMessage.ParkingDoesNotExist);
            List<int> slotNos = Program.ParkingLot.Cars
                .Where(c => c != null && c.Color.Equals(color, StringComparison.InvariantCultureIgnoreCase))
                .Select(q => q.SlotNumber).ToList();
            if (slotNos.Any())
            {
                Console.WriteLine(string.Join(", ", slotNos));
                return slotNos;
            }
            else
            {
                Console.WriteLine(ConstantsKey.NotFound);
                return null;
            }
        }

        /// <summary>
        /// Queries tha parking lot for slotnumber
        /// </summary>
        /// <param name="regNo">registration number of the car</param>
        public int slot_number_for_registration_number(string regNo)
        {
            if (Program.ParkingLot == null)
                throw new BaseException(ErrorMessage.ParkingDoesNotExist);
            try
            {
                int slotNo = Program.ParkingLot.Cars
                    .Where(c => c != null && c.RegistrationNumber.Equals(regNo, StringComparison.InvariantCultureIgnoreCase))
                    .Select(q => q.SlotNumber).FirstOrDefault();
                if (slotNo != 0)
                {
                    Console.WriteLine(slotNo);
                    return slotNo;
                }
                else
                    throw new BaseException(ErrorMessage.SlotDoesNotExist);
            }
            catch (BaseException)
            {
                Console.WriteLine(ConstantsKey.NotFound);
                return -1;
            }
        }
        #endregion

        #region utilities
        /// <summary>
        /// utility is used to find the first index of parking slot which has no car
        /// no car is indicated by a null entry in the array
        /// </summary>
        /// <returns></returns>
        private int FindFreeParkingSlot()
        {
            return Array.IndexOf(Program.ParkingLot.Cars, null);
        }

        /// <summary>
        /// this utility is used to verify the parameters are not null
        /// it also validates and parses the slot value to integer
        /// </summary>
        /// <param name="value">string value to parse</param>
        /// <returns>parsed integer value</returns>
        private int PreCheck(string value)
        {
            int parsedValue;
            if (value == null)
                throw new BaseException(ErrorMessage.EmptySlots);
            try
            {
                parsedValue = int.Parse(value);
                return parsedValue;
            }
            catch (FormatException)
            {
                throw new BaseException(ErrorMessage.InvalidValueForSlots);
            }
        }
        /// <summary>
        /// utility function is used to validate the command passed from console
        /// </summary>
        /// <param name="args">arguments passed from console</param>
        /// <returns>a string array of parameters excluding the command</returns>
        private static string[] ValidateParameters(string[] args)
        {
            string[] parameters;
            if (args.Length > 1)
            {
                parameters = new string[args.Length - 1];
                Array.Copy(args, 1, parameters, 0, parameters.Length);
            }
            else
            {
                parameters = null;
            }
            return parameters;
        }

        /// <summary>
        /// checks if the car is already in the parking lot
        /// </summary>
        /// <param name="regNo">This is a unque value, registration number of car to be parked</param>
        /// <returns>conditional block to allow a car to be parked</returns>
        private bool ValidateCarExistance(string regNo)
        {
            Car car = Program.ParkingLot.Cars.Where(c => c != null && c.RegistrationNumber.Equals(regNo, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (car != null)
                throw new BaseException(ErrorMessage.CarAlreadyExists);
            return true;
        }
        #endregion
    }
}
