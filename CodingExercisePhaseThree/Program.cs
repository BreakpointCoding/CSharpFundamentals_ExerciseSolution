using System;

namespace CodingChallengePhaseThree
{
    class Program
    {
        static void Main(string[] args)
        {

            double milesPerGallon = 35, pricePerGallon = 1.87;
            int milesToDestination = 2032, tankCapacity = 20;
            double totalCostOfStay = 0;

            int milesPerTank = tankCapacity * (int)milesPerGallon;

            double totalMiles;
            double costToDestination = 0;

            costToDestination = CalculateCostToDestination(pricePerGallon, milesToDestination, tankCapacity, milesPerTank);
            totalCostOfStay = CalculateCostOfStay(totalCostOfStay, costToDestination);
            double costToGetHome = CalculateCostToGetHome(pricePerGallon, milesToDestination, tankCapacity, milesPerTank, ref costToDestination);

            Console.WriteLine($"Your total cost for the trip is {(costToDestination + totalCostOfStay + costToGetHome).ToString("C")}.");

            Console.ReadKey();
        }

        private static double CalculateCostToGetHome(double pricePerGallon, int milesToDestination, int tankCapacity, int milesPerTank, ref double costToDestination)
        {
            double costToGetHome = 0;

            // Start with full tank
            costToGetHome = costToGetHome + (tankCapacity * pricePerGallon);

            int currentMilesThisTank = 0;

            for (int i = milesToDestination; i > 0; i--)
            {
                currentMilesThisTank++;

                if (currentMilesThisTank == milesPerTank)
                {
                    // Out of Gas
                    Console.WriteLine("You have run out of gas, lucky you are at a gas station.");

                    // Fill Tank
                    Console.WriteLine($"You fill up your tanks for a cost of {(tankCapacity * pricePerGallon).ToString("C")}.");
                    costToDestination = costToGetHome + (tankCapacity * pricePerGallon);
                    currentMilesThisTank = 0;

                    // Get Snacks
                    costToDestination = GetSnacks(costToDestination);
                }

                // for every mile, this code will run
                if (i > 1)
                {
                    Console.WriteLine($"You have {i} miles left.");
                }
                else
                {
                    Console.WriteLine("Only one mile to go.");
                }
            } // iterator

            Console.WriteLine("You have made it home!");
            Console.WriteLine($"You have spent {costToGetHome.ToString("C")} to get home.");
            return costToGetHome;
        }

        private static double CalculateCostOfStay(double totalCostOfStay, double costToDestination)
        {
     
            double hotelPerNight = 350;
            int numberOfNight = 3, numberOfMeals = 4;

            // Calculate daily costs
            for (int i = 1; i <= numberOfNight; i++)
            {
                Console.WriteLine($"Lets calcultate your costs for day {i}.");

                Console.WriteLine($"Your hotel cost for the day is {hotelPerNight.ToString("C")}.");

                // Add Hotel Cost
                totalCostOfStay = totalCostOfStay + hotelPerNight;

                // Add costs for each meal
                for (int j = 1; j <= numberOfMeals; j++)
                {
                    while (true)
                    {
                        try
                        {
                            Console.WriteLine($"What will you spend on meal #{j}");
                            double mealCost = double.Parse(Console.ReadLine());
                            totalCostOfStay = totalCostOfStay + mealCost;
                            break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("You need to enter a decimal value.");
                        }
                    }

                }

            }

            // Oddity cost
            int oddityCount = 6;

            for (int i = 1; i <= oddityCount; i++)
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine($"What will you spend on oddity #{i}");
                        double oddityCost = double.Parse(Console.ReadLine());
                        totalCostOfStay = totalCostOfStay + oddityCost;
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("You need to enter a decimal value.");
                    }
                }
            }

            Console.WriteLine("Are there any misc business expenses (y/n)?");
            if (Console.ReadLine().Equals("y", StringComparison.OrdinalIgnoreCase))
            {

                while (true)
                {
                    try
                    {
                        Console.WriteLine($"How much did you spend?");
                        double miscCost = double.Parse(Console.ReadLine());
                        totalCostOfStay = totalCostOfStay + miscCost;
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("You need to enter a decimal value.");
                    }
                }
            }

            Console.WriteLine($"You have spent {totalCostOfStay.ToString("C")} while staying in town.");
            Console.WriteLine($"Your total cost so far is {(totalCostOfStay + costToDestination).ToString("C")}");
            return totalCostOfStay;
        }

        private static double CalculateCostToDestination(double pricePerGallon, int milesToDestination, int tankCapacity, int milesPerTank)
        {

            // Start with full tank
            double costToDestination = (tankCapacity * pricePerGallon);

            int currentMilesThisTank = 0;

            for (int i = milesToDestination; i > 0; i--)
            {
                currentMilesThisTank++;

                if (currentMilesThisTank == milesPerTank)
                {
                    // Out of Gas
                    Console.WriteLine("You have run out of gas, lucky you are at a gas station.");

                    // Fill Tank
                    Console.WriteLine($"You fill up your tanks for a cost of {(tankCapacity * pricePerGallon).ToString("C")}.");
                    costToDestination = costToDestination + (tankCapacity * pricePerGallon);
                    currentMilesThisTank = 0;

                    // Get Snacks
                    costToDestination =  GetSnacks(costToDestination);
                }

                // for every mile, this code will run
                if (i > 1)
                {
                    Console.WriteLine($"You have {i} miles left.");
                }
                else
                {
                    Console.WriteLine("Only one mile to go.");
                }
            } // iterator

            Console.WriteLine("You have made it!");
            Console.WriteLine($"So far you have spend {costToDestination.ToString("C")}.");


            return costToDestination;
        }

        private static double GetSnacks(double costToDestination)
        {
            bool wantsSnacks = false;

            while (wantsSnacks == false)
            {
                Console.WriteLine("Would you like some snacks (y/n)");
                if (Console.ReadLine().Equals("y", StringComparison.OrdinalIgnoreCase))
                {
                    wantsSnacks = true;

                    while (true)
                    {
                        try
                        {
                            Console.WriteLine("How much will you spend on snacks?");
                            double snackCost = double.Parse(Console.ReadLine());
                            costToDestination = costToDestination + snackCost;
                            break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("You need to enter a decimal value.");
                        }
                    }

                }
                else
                {
                    Console.WriteLine("You are a liar!");
                }
            }

            return costToDestination;
        }
    }
}
