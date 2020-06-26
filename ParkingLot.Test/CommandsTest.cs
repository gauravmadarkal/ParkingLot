////////////////////////////////////////
/// Author Gaurav Madarkal
/// 25th June 2020
/// Parking Lot problem
////////////////////////////////////////


using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingLot.CustomException;
using ParkingLot.Interface;
using ParkingLot.Model;
using ParkingLot.Router;
using System;
using System.Collections.Generic;

namespace ParkingLot.Test
{
    [TestClass]
    public class CommandsTest
    {
        Model.ParkingLot sampleLot;

        #region positive scenarios - START
        [TestMethod]
        public void routingCommandTest()
        {
            //clearing the parking lot before creating another
            Program.ParkingLot = null;
            Assert.IsTrue(CommandRouter.RouteCommand("create_parking_lot 10") == RoutingStatus.Success);
        }

        [TestMethod]
        public void routingQuitCommandTest()
        {
            Assert.IsTrue(CommandRouter.RouteCommand("Quit") == RoutingStatus.Quit);
        }

        [TestMethod]
        public void CreateParkLotTest()
        {
            ICommands commands = new CommandRouter();
            commands.create_parking_lot("4");
            Assert.IsTrue(Program.ParkingLot != null && Program.ParkingLot.Cars.Length == 4);
        }
        [TestMethod]
        public void ParkCarTest()
        {
            Car[] cars = new Car[5]
            {
                new Car
                {
                    Color = "white",
                    RegistrationNumber = "KA-01-HH-1234",
                    SlotNumber = 1
                },
                new Car
                {
                    Color = "red",
                    RegistrationNumber = "KA-01-HH-9876",
                    SlotNumber = 2
                },
                new Car
                {
                    Color = "blue",
                    RegistrationNumber = "KA-01-KH-1762",
                    SlotNumber = 3
                },
                null,
                new Car
                {
                    Color = "brown",
                    RegistrationNumber = "KA-01-ET-1111",
                    SlotNumber = 5
                }
            };
            sampleLot = new Model.ParkingLot(5)
            {
                Cars = cars
            };
            ICommands commands = new CommandRouter();
            Program.ParkingLot = sampleLot;
            Assert.IsTrue(Program.ParkingLot.Cars[3] == null);
            commands.park("KA-01-ET-8787", "white");
            Assert.IsTrue(Program.ParkingLot.Cars[3] != null && Program.ParkingLot.Cars[3].Color.Equals("white"));
        }
        [TestMethod]
        public void VacateCarTest()
        {
            Car[] cars = new Car[4]
            {
                new Car
                {
                    Color = "white",
                    RegistrationNumber = "KA-01-HH-1234",
                    SlotNumber = 1
                },
                new Car
                {
                    Color = "red",
                    RegistrationNumber = "KA-01-HH-9876",
                    SlotNumber = 2
                },
                new Car
                {
                    Color = "blue",
                    RegistrationNumber = "KA-01-KH-1762",
                    SlotNumber = 3
                },
                new Car
                {
                    Color = "brown",
                    RegistrationNumber = "KA-01-ET-1111",
                    SlotNumber = 4
                }
            };
            sampleLot = new Model.ParkingLot(4)
            {
                Cars = cars
            };
            ICommands commands = new CommandRouter();
            Program.ParkingLot = sampleLot;
            Assert.IsTrue(Program.ParkingLot.Cars[2] != null);
            commands.leave("3");
            Assert.IsTrue(Program.ParkingLot.Cars[2] == null);
        }
        [TestMethod]
        public void StatusCheckTest()
        {
            Car[] cars = new Car[2]
            {
                new Car
                {
                    Color = "white",
                    RegistrationNumber = "KA-01-HH-1234",
                    SlotNumber = 1
                },
                new Car
                {
                    Color = "red",
                    RegistrationNumber = "KA-01-HH-9876",
                    SlotNumber = 2
                }
            };
            sampleLot = new Model.ParkingLot(4)
            {
                Cars = cars
            };
            ICommands commands = new CommandRouter();
            Program.ParkingLot = sampleLot;
            commands.status();

            Program.ParkingLot = null;
            Assert.ThrowsException<BaseException>(() => commands.status());
        }
        [TestMethod]
        public void RegNosWithColorTest()
        {
            Car[] cars = new Car[5]
            {
                new Car
                {
                    Color = "white",
                    RegistrationNumber = "KA-01-HH-1234",
                    SlotNumber = 1
                },
                new Car
                {
                    Color = "red",
                    RegistrationNumber = "KA-01-HH-9876",
                    SlotNumber = 2
                },
                new Car
                {
                    Color = "blue",
                    RegistrationNumber = "KA-01-KH-1762",
                    SlotNumber = 3
                },
                null,
                new Car
                {
                    Color = "red",
                    RegistrationNumber = "KA-01-ET-1111",
                    SlotNumber = 5
                }
            };
            sampleLot = new Model.ParkingLot(4)
            {
                Cars = cars
            };
            ICommands commands = new CommandRouter();
            Program.ParkingLot = sampleLot;
            List<string> regsNos = commands.registration_numbers_for_cars_with_colour("red");
            Assert.IsTrue(regsNos.Count == 2 && regsNos.Contains("KA-01-ET-1111"));
        }
        [TestMethod]
        public void slotNumbersForCarswithColorTest()
        {
            Car[] cars = new Car[5]
            {
                new Car
                {
                    Color = "white",
                    RegistrationNumber = "KA-01-HH-1234",
                    SlotNumber = 1
                },
                new Car
                {
                    Color = "red",
                    RegistrationNumber = "KA-01-HH-9876",
                    SlotNumber = 2
                },
                new Car
                {
                    Color = "blue",
                    RegistrationNumber = "KA-01-KH-1762",
                    SlotNumber = 3
                },
                null,
                new Car
                {
                    Color = "blue",
                    RegistrationNumber = "KA-01-ET-1111",
                    SlotNumber = 5
                }
            };
            sampleLot = new Model.ParkingLot(4)
            {
                Cars = cars
            };
            ICommands commands = new CommandRouter();
            Program.ParkingLot = sampleLot;
            List<int> slotNos = commands.slot_numbers_for_cars_with_colour("blue");
            Assert.IsTrue(slotNos.Count == 2 && slotNos.Contains(5));
        }
        [TestMethod]
        public void slotNumberForRegNoTest()
        {
            Car[] cars = new Car[3]
            {
                new Car
                {
                    Color = "white",
                    RegistrationNumber = "KA-01-HH-1234",
                    SlotNumber = 1
                },
                new Car
                {
                    Color = "red",
                    RegistrationNumber = "KA-01-HH-9876",
                    SlotNumber = 2
                },
                new Car
                {
                    Color = "blue",
                    RegistrationNumber = "KA-01-KH-1762",
                    SlotNumber = 3
                }
            };
            sampleLot = new Model.ParkingLot(4)
            {
                Cars = cars
            };
            ICommands commands = new CommandRouter();
            Program.ParkingLot = sampleLot;
            int slotNo = commands.slot_number_for_registration_number("KA-01-HH-9876");
            Assert.IsTrue(slotNo == 2);
        }

        #endregion positive scenarios - START

        #region negative scenarios - START

        [TestMethod]
        public void routingCommand_NegTest()
        {
            Assert.IsTrue(CommandRouter.RouteCommand(null) == RoutingStatus.Invalid);
        }
        [TestMethod]
        public void ParkCar_NegTest()
        {
            Car[] cars = new Car[5]
            {
                new Car
                {
                    Color = "white",
                    RegistrationNumber = "KA-01-HH-1234",
                    SlotNumber = 1
                },
                new Car
                {
                    Color = "red",
                    RegistrationNumber = "KA-01-HH-9876",
                    SlotNumber = 2
                },
                new Car
                {
                    Color = "blue",
                    RegistrationNumber = "KA-01-KH-1762",
                    SlotNumber = 3
                },
                null,
                new Car
                {
                    Color = "brown",
                    RegistrationNumber = "KA-01-ET-1111",
                    SlotNumber = 5
                }
            };
            sampleLot = new Model.ParkingLot(5)
            {
                Cars = cars
            };
            ICommands commands = new CommandRouter();
            Program.ParkingLot = sampleLot;
            Assert.ThrowsException<BaseException>(() => commands.park("KA-01-ET-1111", "brown"))
                .ErrorMessage.Equals(ErrorMessage.CarAlreadyExists, StringComparison.InvariantCultureIgnoreCase);
        }
        [TestMethod]
        public void VacateCar_NegTest()
        {
            Car[] cars = new Car[4]
            {
                new Car
                {
                    Color = "white",
                    RegistrationNumber = "KA-01-HH-1234",
                    SlotNumber = 1
                },
                new Car
                {
                    Color = "red",
                    RegistrationNumber = "KA-01-HH-9876",
                    SlotNumber = 2
                },
                new Car
                {
                    Color = "blue",
                    RegistrationNumber = "KA-01-KH-1762",
                    SlotNumber = 3
                },
                new Car
                {
                    Color = "brown",
                    RegistrationNumber = "KA-01-ET-1111",
                    SlotNumber = 4
                }
            };
            sampleLot = new Model.ParkingLot(4)
            {
                Cars = cars
            };
            ICommands commands = new CommandRouter();
            Program.ParkingLot = sampleLot;
            Assert.ThrowsException<BaseException>(() => commands.leave("10")).ErrorMessage.Equals(ErrorMessage.SlotDoesNotExist);
        }
        [TestMethod]
        public void RegNosWithColor_NegTest()
        {
            Car[] cars = new Car[5]
            {
                new Car
                {
                    Color = "white",
                    RegistrationNumber = "KA-01-HH-1234",
                    SlotNumber = 1
                },
                new Car
                {
                    Color = "red",
                    RegistrationNumber = "KA-01-HH-9876",
                    SlotNumber = 2
                },
                new Car
                {
                    Color = "blue",
                    RegistrationNumber = "KA-01-KH-1762",
                    SlotNumber = 3
                },
                null,
                new Car
                {
                    Color = "red",
                    RegistrationNumber = "KA-01-ET-1111",
                    SlotNumber = 5
                }
            };
            sampleLot = new Model.ParkingLot(4)
            {
                Cars = cars
            };
            ICommands commands = new CommandRouter();
            Program.ParkingLot = sampleLot;
            List<string> regsNos = commands.registration_numbers_for_cars_with_colour("violet");
            Assert.IsTrue(regsNos == null);
        }
        [TestMethod]
        public void slotNumbersForCarswithColor_NegTest()
        {
            Car[] cars = new Car[5]
            {
                new Car
                {
                    Color = "white",
                    RegistrationNumber = "KA-01-HH-1234",
                    SlotNumber = 1
                },
                new Car
                {
                    Color = "red",
                    RegistrationNumber = "KA-01-HH-9876",
                    SlotNumber = 2
                },
                new Car
                {
                    Color = "blue",
                    RegistrationNumber = "KA-01-KH-1762",
                    SlotNumber = 3
                },
                null,
                new Car
                {
                    Color = "blue",
                    RegistrationNumber = "KA-01-ET-1111",
                    SlotNumber = 5
                }
            };
            sampleLot = new Model.ParkingLot(4)
            {
                Cars = cars
            };
            ICommands commands = new CommandRouter();
            Program.ParkingLot = sampleLot;
            List<int> slotNos = commands.slot_numbers_for_cars_with_colour("purple");
            Assert.IsTrue(slotNos == null);
        }
        [TestMethod]
        public void slotNumberForRegNo_NegTest()
        {
            Car[] cars = new Car[3]
            {
                new Car
                {
                    Color = "white",
                    RegistrationNumber = "KA-01-HH-1234",
                    SlotNumber = 1
                },
                new Car
                {
                    Color = "red",
                    RegistrationNumber = "KA-01-HH-9876",
                    SlotNumber = 2
                },
                new Car
                {
                    Color = "blue",
                    RegistrationNumber = "KA-01-KH-1762",
                    SlotNumber = 3
                }
            };
            sampleLot = new Model.ParkingLot(4)
            {
                Cars = cars
            };
            ICommands commands = new CommandRouter();
            Program.ParkingLot = sampleLot;
            int slotNo = commands.slot_number_for_registration_number("KA-01-HH-0000");
            Assert.IsTrue(slotNo == -1);
        }

        #endregion negative scenarios - START

    }
}
