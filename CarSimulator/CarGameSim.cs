using ServiceLibrary;
using ServiceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSimulator
{
    public class CarSimulator
    {

        public class CarSimulatorService
        {

            private int fuelStatus = 10;
            private int tiredness = 0;
            private Driver currentRandomDriver; // Store the current random driver
            private readonly IRandomDriverApiService randomDriverApiService;

            public CarSimulatorService(IRandomDriverApiService randomDriverApiService)
            {
                this.randomDriverApiService = randomDriverApiService;
            }


            public void Run()
            {
                while (true)
                {
                    Console.Clear();
                    DisplayUI();

                    Console.WriteLine("\nWhat do you want to do next? (1-7)");
                    string userInput = Console.ReadLine();

                    ProcessUserInput(userInput);
                }
            }

            private void DisplayUI()
            {
                Console.WriteLine("1: Turn left");
                Console.WriteLine("2: Turn right");
                Console.WriteLine("3: Drive forward");
                Console.WriteLine("4: Reverse");
                Console.WriteLine("5: Take a rest");
                Console.WriteLine("6: Refill gas");
                Console.WriteLine("7: End game");

                Console.WriteLine("\n\n"); // Two new lines

                var randomDriverTask = DisplayRandomDriver();
                Console.WriteLine(randomDriverTask.Result); // Wait for the task to complete and display the result
                Console.WriteLine($"Fuel status: {fuelStatus}/10");
                Console.WriteLine($"Driver's tiredness: {tiredness}/10");

                CheckTirednessWarnings();
            }

            private async Task<string> DisplayRandomDriver()
            {
                if (currentRandomDriver == null)
                {
                    // Generate a new random driver only if it's null (first time or after game reset)
                    currentRandomDriver = await randomDriverApiService.GetRandomDriver();
                }

                return $"Random Driver: {currentRandomDriver.GivenName} {currentRandomDriver.SurName}";
            }

            private void ProcessUserInput(string userInput)
            {
                if (fuelStatus == 0 && (userInput == "1" || userInput == "2" || userInput == "3" || userInput == "4"))
                {
                    Console.WriteLine("Car out of fuel. Refill the gas to continue driving.");
                }
                else
                {
                    switch (userInput)
                    {
                        case "1":
                            TurnLeft();
                            break;
                        case "2":
                            TurnRight();
                            break;
                        case "3":
                            DriveForward();
                            break;
                        case "4":
                            Reverse();
                            break;
                        case "5":
                            TakeRest();
                            break;
                        case "6":
                            RefillGas();
                            break;
                        case "7":
                            EndGame();
                            break;
                        default:
                            Console.WriteLine("Invalid input. Please enter a number between 1 and 7.");
                            break;
                    }
                }

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }

            public void TurnLeft()
            {
                Console.WriteLine("Turning left...");
                FuelAndTirednessUpdate();
            }

            public void TurnRight()
            {
                Console.WriteLine("Turning right...");
                FuelAndTirednessUpdate();
            }

            public void DriveForward()
            {
                Console.WriteLine("Driving forward...");
                FuelAndTirednessUpdate();
            }

            public void Reverse()
            {
                Console.WriteLine("Reversing...");
                FuelAndTirednessUpdate();
            }

            public void TakeRest()
            {
                Console.WriteLine("Taking a rest...");
                tiredness = 0;
                CheckTirednessWarnings();
            }

            public void RefillGas()
            {
                Console.WriteLine("Refilling gas...");
                fuelStatus = 10;
            }

            public void EndGame()
            {
                Console.WriteLine("Ending game. Goodbye!");
                Environment.Exit(0);
            }

            public void FuelAndTirednessUpdate()
            {
                if (fuelStatus > 0)
                {
                    fuelStatus--;
                    tiredness++; // Increment tiredness when performing an action
                 
                }
                else
                {
                    Console.WriteLine("Car out of fuel. Refill the gas to continue driving.");
                }
            }

            public void CheckTirednessWarnings()
            {
                if (tiredness >= 6 && tiredness < 8)
                {
                    Console.WriteLine("Warning: You are getting tired. You should rest.");
                }
                else if (tiredness >= 8 && tiredness < 10)
                {
                    Console.WriteLine("Warning: YOU ARE ABOUT TO FALL ASLEEP, TAKE A REST.");
                }
                else if (tiredness >= 10)
                {
                    Console.WriteLine("You fell asleep and crashed. Game over.");
                    ResetGame();
                }
               
            }

            public void ResetGame()
            {
                // Reset fuel and tiredness levels
                fuelStatus = 10;
                tiredness = 0;

                // Reset the current random driver
                currentRandomDriver = null;
            }
            public int GetFuelStatus()
            {
                return fuelStatus;
            }

            public int GetTiredness()
            {
                return tiredness;
            }

            public Driver GetCurrentRandomDriver()
            {
                return currentRandomDriver;
            }

            public void SetCurrentRandomDriver(Driver driver)
            {
                currentRandomDriver = driver;
            }



        }
    }

}