using System;
using System.Threading;

namespace CodingChallengePhaseTwo

{
    class Program
    {
        static void Main(string[] args)
        {
            string nickName = GetUsersName();
            DateTime today = DateTime.Today;

            DateTime customerNextBirtday;
            int discountPercentage = GetDiscountPecentage(out customerNextBirtday);

            DealWithShipping();

            Console.Clear();

            string questionOne, questionTwo, questionThree, questionFour;
            string answerOne, answerTwo, answerThree, answerFour;

            int surveyScore;
            PerformSurvey(out questionOne, out questionTwo, out questionThree, out questionFour,
                out answerOne, out answerTwo, out answerThree, out answerFour, out surveyScore);

            string suggestedProduct = GetSuggestedProduct(surveyScore);

            DisplayAnswers(nickName, questionOne, questionTwo, questionThree, questionFour, answerOne, answerTwo, answerThree, answerFour, suggestedProduct);
            DisplayFinalMessage(today, customerNextBirtday, discountPercentage);

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        private static void DisplayFinalMessage(DateTime today, DateTime customerNextBirtday, int discountPercentage)
        {
            int daysUntilBirthday = (int)(customerNextBirtday - today).TotalDays;

            Console.WriteLine($"Make sure to pick one up for your birthday in {daysUntilBirthday} days.");

            if (discountPercentage > 0)
            {
                Console.WriteLine($"Don't forget about your {discountPercentage}% discount.");
            }
        }

        private static void DisplayAnswers(string nickName, string questionOne, string questionTwo, string questionThree, string questionFour, string answerOne, string answerTwo, string answerThree, string answerFour, string suggestedProduct)
        {
            Console.WriteLine("Please stand by as we calculate your results....");
            Thread.Sleep(3000);

            Console.Clear();

            Console.WriteLine($"Thank you very much {nickName} for taking our survey.");
            Console.WriteLine("We have a recommendation based on your answers.");
            Console.WriteLine($"{questionOne} : {answerOne} ");
            Console.WriteLine($"{questionTwo} : {answerTwo} ");
            Console.WriteLine($"{questionThree} : {answerThree} ");
            Console.WriteLine($"{questionFour} : {answerFour} ");
            Console.WriteLine();
            Console.WriteLine($"Your recommended product is a {suggestedProduct}");
        }

        private static string GetSuggestedProduct(int surveyScore)
        {
            string suggestedProduct;

            if (surveyScore < 11)
            {
                suggestedProduct = "Shoes made of Swiss Cheese";
            }
            else if (surveyScore < 21)
            {
                suggestedProduct = "Bottle opener shaped bottle opener";
            }
            else if (surveyScore < 31)
            {
                suggestedProduct = "Ben Frankling bowling pin";
            }
            else if (surveyScore < 41)
            {
                suggestedProduct = "Lightning in a jar";
            }
            else if (surveyScore < 51)
            {
                suggestedProduct = "Pickled elephant skull";
            }
            else
            {
                suggestedProduct = "Full-size black and white copy of Van Gogh's \"Starry Night\"";
            }

            return suggestedProduct;
        }

        private static void PerformSurvey(out string questionOne, out string questionTwo, out string questionThree, out string questionFour, out string answerOne, out string answerTwo, out string answerThree, out string answerFour, out int surveyScore)
        {
            Console.WriteLine("Time for a survey." + Environment.NewLine);
            questionOne = "What is your favorite color?";
            questionTwo = "What is your favorite season?";
            questionThree = "Would you rather talk to (1-your mother, 2-your father, 3-a stranger,4- aliens)?";
            questionFour = "Would you rather be (1-wealthy, 2-popular, 3-accommodating, 4- an alien)?";

            Console.WriteLine(questionOne);
            answerOne = Console.ReadLine();
            Console.WriteLine(questionTwo);
            answerTwo = Console.ReadLine();
            Console.WriteLine(questionThree);
            answerThree = Console.ReadLine();
            Console.WriteLine(questionFour);
            answerFour = Console.ReadLine();

            surveyScore = 0;

            // COLOR
            switch (answerOne.ToLower())
            {
                case "red":
                    surveyScore += 8;
                    break;
                case "blue":
                    surveyScore += 3;
                    break;
                case "yellow":
                    surveyScore += 6;
                    break;
                case "purple":
                    surveyScore += 1;
                    break;
                case "black":
                case "white":
                    surveyScore += 0;
                    break;
                default:
                    surveyScore += 5;
                    break;
            }

            // SEASON
            switch (answerTwo.ToLower())
            {
                case "spring":
                    surveyScore += 4;
                    break;
                case "summer":
                    surveyScore += 9;
                    break;
                case "winter":
                    surveyScore += 2;
                    break;
                case "fall":
                    surveyScore += 11;
                    break;
                default:
                    surveyScore += 1;
                    break;
            }

            // TALK
            switch (answerThree.ToLower())
            {
                case "1":
                    surveyScore += 1;
                    break;
                case "2":
                    surveyScore += 3;
                    break;
                case "3":
                    surveyScore += 8;
                    break;
                case "4":
                    surveyScore += 22;
                    break;

            }

            // BE
            switch (answerFour.ToLower())
            {
                case "1":
                    surveyScore += 7;
                    break;
                case "2":
                    surveyScore += 2;
                    break;
                case "3":
                    surveyScore += 10;
                    break;
                case "4":
                    surveyScore += 22;
                    break;
                default:
                    surveyScore += 0;
                    break;

            }
        }

        private static void DealWithShipping()
        {
            Console.WriteLine("What is your country of residence?");
            string country = Console.ReadLine();

            double shippingCost;
            switch (country.ToLower().Trim())
            {
                case "us":
                case "canada":
                case "australia":
                    shippingCost = 0;
                    break;
                case "india":
                case "germany":
                case "brazil":
                case "madagascar":
                    shippingCost = 289;
                    break;
                default:
                    shippingCost = 12.43;
                    break;
            }

            Console.WriteLine($"Because of your country of residence, your shipping cost is {shippingCost}");
            Console.WriteLine("If this is not okay type EXIT, to exit the application.");
            if (Console.ReadLine().Trim().ToLower() == "exit")
            {
                Environment.Exit(0);
            }
        }

        private static int GetDiscountPecentage(out DateTime customerNextBirtday)
        {
            int discountPercentage = 0;

            DateTime today = DateTime.Today;
            Console.WriteLine("Please give me the month and day you were born MM/DD)");
            DateTime input = DateTime.Parse(Console.ReadLine());


            // Set their next birthday for this year
            customerNextBirtday = input;

            // Did their birthday pass this year
            if (today.Month > input.Month || (today.Month == input.Month && today.Day > input.Day))
            {
                customerNextBirtday = customerNextBirtday.AddYears(1);
            }             

            // Is today the customers birthday
            if (customerNextBirtday == today)
            {
                Console.WriteLine("HAPPY BIRTHDAY! You get a 15% discount");
                discountPercentage = 15;
            }
            // Was their birthday within 14 days
            else if (customerNextBirtday < today && customerNextBirtday >= today.AddDays(-14))
            {
                Console.WriteLine("I'm sorry we missed your birthday. You get a 8% discount");
                discountPercentage = 8;
            }
            else if (customerNextBirtday > today && customerNextBirtday <= today.AddDays(14))
            {
                Console.WriteLine("Looks like you have a birthday coming up. Come back and get your birthday discount.");
            }

            return discountPercentage;
        }

        private static string GetUsersName()
        {
            Console.WriteLine("What is your full name?");
            string fullName = Console.ReadLine();
            Console.WriteLine("Do you have a nickname?");

            // Get Nickname
            string nickName = fullName;
            switch (Console.ReadLine().ToLower().Trim())
            {
                case "yes":
                case "y":
                case "yeah":
                    Console.WriteLine("What is your nickname?");
                    nickName = Console.ReadLine();
                    break;
            }

            return nickName;
        }
    }
}