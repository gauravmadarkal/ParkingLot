# ParkingLot
This is a parking ticket issuing system built in c# .net core 3.1

This system is built with the logic of passing each command interactively from command line and processing the command based on reflection,
each command is mapped to a equivalent reflection method, making it cleaner and easier to implement when new commands are added.
example:
  >>
  >create-parking-lot 10 
  
######  THIS STATEMENT COMPRISES OF TWO PART, 
######    1. COMMAND
######    2. PARAMETER
  
  The command is mapped to a equivalent method, in this case method signature will be like
  
>  create-parking-lot(string numberOfSlots);
  
> THE COMMANDS ARE CATEGORIZED INTO TWO:
######  1. TASK COMMANDS
######  2. QUERY COMMANDS
 
> LOGIC OF CREATING A PARKING LOT:
  1. An object consisting of an array of cars will be created, the size of array will be the number of slots given.
  2. When any car is parked the first empty slot in the array will be allocated.
  3. No complex data structure is needed to slove this problem, a simple array of objects will suffice
  4. When any park leaves the lot, that particular indexin array is set to null
  5. The next parked car will be allocated a spot based on first nullvalue index in the array
  6. This solution assumes that each car is parked at the given spot, and spot allocations are done sequentially
