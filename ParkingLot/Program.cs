////////////////////////////////////////
/// Author Gaurav Madarkal
/// 25th June 2020
/// Parking Lot problem
////////////////////////////////////////

using ParkingLot.Constants;
using ParkingLot.CustomException;
using ParkingLot.Model;
using ParkingLot.Router;
using System;

namespace ParkingLot
{
    /// <summary>
    /// entry point to parking lot system
    /// </summary>
    public class Program
    {
        public static Model.ParkingLot ParkingLot;
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(">>>");
                var command = Console.ReadLine();
                try
                {
                    RoutingStatus routingStatus = CommandRouter.RouteCommand(command);
                    switch (routingStatus)
                    {
                        case RoutingStatus.Quit:
                            Console.WriteLine("Exiting...");
                            return;
                        case RoutingStatus.Invalid:
                            Console.WriteLine(ErrorMessage.InvalidCommand);
                            Console.WriteLine(ConstantsKey.CommandHelp);
                            break;
                        case RoutingStatus.Success:
                            break;
                    }
                }
                catch (BaseException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Fatal Error: {0}", e.Message);
                }
            }
        }
    }
}
