using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Comparison
{
    class Program
    {
        class Car // defines what properties cares should have
        {
            public string Make { get; set; } // set the Make
            public string Model { get; set; } // set the Model
            public string Color { get; set; } // set the Color
            public int Year { get; set; } // set the Year
            public decimal Price { get; set; } // set the Price
            public double TCCR { get; set; } // set the TCCR
            public int MPG { get; set; } // set the MPG

            public Car()
            {
                Console.WriteLine("Car Created!"); // verify the car has been created.
                
            }
            
            
        }

        class ParkingLot // stores data of cars we are working with.
        {
            public List<Car> Cars;

            public ParkingLot()
            {
                CreateInventory(); // This is used to create the inventory with the information provided for later use. Passes the info to the Car class and makes a Car
            }

            public Car AddCar(String make, string model, string color, int year, decimal price, double tccr, int mpg)
            {
                var car = new Car()
                {
                    Make = make,
                    Model = model,
                    Color = color,
                    Year = year,
                    Price = price,
                    TCCR = tccr,
                    MPG = mpg
                };
                Cars.Add(car);
                Console.WriteLine($"A {car.Year} {car.Color} {car.Make} {car.Model} has been parked in the parking lot!"); // verify the car has been Parked in the lot for later use with functions for sorting.
                return car;
            }

            private void CreateInventory() // used to hold the list of Cars.
            { 
                Cars = new List<Car>(); //list cars can populate

                AddCar("Honda", "CRV", "Green", 2016, 23845, 8, 33);
                AddCar("Ford", "Escape", "Red", 2017, 23590, 7.8, 32);
                AddCar("Hyundai", "Santa Fe", "Grey", 2016, 24950, 8, 27);
                AddCar("Mazda", "CX-5", "Red", 2017, 21795, 8, 35);
                AddCar("Subaru", "Forester", "Blue", 2016, 22395, 8.4, 32);

            }

            public void ListVehiclesByYear() // method for sorting cars by year
            {
                Console.WriteLine("Here's a list of cars sorted by year, newest first.  \n");
                var sortedCars = Cars.OrderByDescending(car => car.Year).ToList(); // Making a list based off the by year

                foreach(Car car in sortedCars) // loop to make sure we go through each car in the list
                {
                    Console.WriteLine($"Make: {car.Make} Model: {car.Model} Year: {car.Year}"); // format we are writing out the car info in the list
                }
            }

            public void ListVehiclesByModel() // method to list vehicle models form A-Z
            {
                Console.WriteLine("\nHere's a list of cars sorted in alphabetical order. \n");
                var sortedCars = Cars.OrderBy(car => car.Model).ToList(); // making a list of cars by model

                foreach (Car car in sortedCars) // loop to make sure we go through each car in the list
                {
                    Console.WriteLine($"Model: {car.Model}"); // format we are writing out the car info in the list
                }
            }

            public void ListVehiclesByPrice() // method to list cars by price
            {
                Console.WriteLine("\nHere's a list of cars sorted in order of price. \n");
                var sortedCars = Cars.OrderBy(car => car.Price).ToList(); // making a list of cars by price

                foreach (Car car in sortedCars) // loop to make sure we go through each car in the list
                {
                    Console.WriteLine($"Make: {car.Make} Model: {car.Model} Year: {car.Year} Price: ${car.Price}"); // format we are writing out the car info in the list
                }
            }

            public void ListVehiclesByBestValue() // method to list cars by best value
            {
                Console.WriteLine("\nHere's a list of cars sorted in order of best value. \n");
                var sortedCars = Cars.OrderBy(car => car.TCCR).ThenBy((car) => car.MPG).ToList(); // making a list of cars by TCCR followed by MPG

                foreach (Car car in sortedCars) // loop to make sure we go through each car in the list
                {
                    Console.WriteLine($"Make: {car.Make} Model: {car.Model} Year: {car.Year} Price: ${car.Price}"); // format we are writing out the car info in the list
                }
            }

            public void GetMilage()
            {
                try
                {
                Console.WriteLine("\nHow many miles will you be traveling?"); //ask user for number of miles
                double x = double.Parse(Console.ReadLine()); //need to add a try catch method or switch back to integer
                ListVehiclesFuelConsumption(x);
                }
                catch
                {
                    Console.WriteLine("Please use numbers!\n");
                    GetMilage();
                }
            }
            public void ListVehiclesFuelConsumption(double x)
            {
                /*Console.WriteLine("How many miles will you be traveling?"); //ask user for number of miles
                double x = double.Parse(Console.ReadLine()); //need to add a try catch method or switch back to integer*/
                Console.WriteLine($"\nWell if you are going {x} miles; The fuel consumption per car goes as follows:\n"); 

                foreach (Car car in Cars)
                {
                    var fuelconsumption = Math.Round((x / car.MPG), 2); // added a variable to contain fuel consumption so i could adjust the formatting / precision as int's couldn't go under 1, and floats / doubles are too long by default. looks messy
                    Console.WriteLine($"The {car.Model} will use {fuelconsumption} Gallons of fuel.");
                }
            }

            public void RandomCar() // method to pick a random car
            {
                var random = new Random(); 
                var chosenIndex = random.Next(Cars.Count);
                var ChosenCar = Cars[chosenIndex];
                Console.WriteLine($"\nTodays randomly featured car is the {ChosenCar.Model}! \n");

            }

            public void YearlyAverageMPG()
            {
                var CarAVGMPG = from car in Cars // declare variable and tell it to pull from car objects in cars list
                                group car by car.Year into yearGroup //telling it to group the cars by year into a group called yearGroup
                                orderby yearGroup.Key
                                select new
                                {
                                    Year = yearGroup.Key,
                                    AverageMPG = Math.Round(yearGroup.Average(x => x.MPG), 2), // calculates average of group and rounds it to 2 places to keep it clean looking
                                };

                Console.WriteLine("Here is a list of Average MPG by year.\n");
                
                foreach(var LookupYear in CarAVGMPG)
                {
                    Console.WriteLine($"Year: {LookupYear.Year} - {LookupYear.AverageMPG} Average MPG");
                }

            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Here's some new Cars for Mariner's Parking Lot! \n");

            var MarinerParkingLot = new ParkingLot(); //Instantiating a parking lot

            Console.WriteLine("Cars are done parking  \n");

            MarinerParkingLot.ListVehiclesByYear(); // to call function to list vehicles by year (newest)
            
            MarinerParkingLot.ListVehiclesByModel(); // to call function to list vehicles from A - Z by model

            MarinerParkingLot.ListVehiclesByPrice(); // to call function to list vehicles from A - Z by model

            MarinerParkingLot.ListVehiclesByBestValue(); // to call function to list vehicles by best value

            MarinerParkingLot.GetMilage(); //requests milage to set up for fuel consumption and adds validation

            MarinerParkingLot.RandomCar(); // to call function to call a random vehicle

            MarinerParkingLot.YearlyAverageMPG(); // to call function to list vehicles and fuel consumption based on use defined milage


        }
    }
}
