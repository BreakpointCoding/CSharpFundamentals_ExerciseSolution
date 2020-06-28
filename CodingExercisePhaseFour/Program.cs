using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingChallengePhaseFour
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayApplicationInformation();

            string userName = GetCustomerInformation();

            userName = GetNickname();

            PauseApplication();          

            Dictionary<string, string> customerFavorites = new Dictionary<string, string>();
            customerFavorites.Add("color", "");
            customerFavorites.Add("number", "");
            customerFavorites.Add("music group", "");
            customerFavorites.Add("TV show", "");
            customerFavorites.Add("insect", "");
            customerFavorites.Add("dead celebrity", "");
            customerFavorites.Add("constellation", "");

            Console.Clear();
            GetCustomerFavorites(userName, customerFavorites);
            PauseApplication();

            Dictionary<string, int> customerRatings = new Dictionary<string, int>();
            customerRatings.Add("Online shopping is fun.", 0);
            customerRatings.Add("Australia is a great country.", 0);
            customerRatings.Add("Dogs are better than cats.", 0);
            customerRatings.Add("eSports are not a sport.", 0);
            customerRatings.Add("Aliens are amoung us.", 0);
            

            Console.Clear();
            double averageRating = GetCustomerRatings(userName, customerRatings);
            PauseApplication();

            Console.Clear();
            DisplayCustomerAnswers(userName, customerFavorites, customerRatings);

            Console.WriteLine("Press any key to exit the application....");
            Console.ReadKey();
        }

        private static string GetNickname()
        {
            string nickName;
            Console.WriteLine("What's your nickname?");
            nickName = Console.ReadLine();
            return nickName;

        }

        private static void DisplayCustomerAnswers(string userName, Dictionary<string, string> customerFavorites, Dictionary<string, int> customerRatings)
        {
            Console.WriteLine($"Thank you {userName} for helping us get to know you.");
            Console.WriteLine("Following is the information we have gathered from you today.");
            Console.WriteLine(new string('*',50));
            Console.WriteLine();
            foreach (var item in customerFavorites)
            {
                Console.WriteLine($"Your favorite {item.Key} is {item.Value}.");
            }
            Console.WriteLine();
            foreach (var item in customerRatings)
            {
                Console.WriteLine($"For the statement: {item.Key}");
                string agreement = "";

                switch (item.Value)
                {
                    case 1:
                        agreement = "Strongly Disagree";
                        break;
                    case 2:
                        agreement = "Midly Disagree";
                        break;
                    case 3:
                        agreement = "Are Neutral";
                        break;
                    case 4:
                        agreement = "Mildly Agree";
                        break;
                    case 5:
                        agreement = "Strongly Agree";
                        break;
                }
                Console.WriteLine($"You said you {agreement}.");

            }
            Console.WriteLine();
            Console.WriteLine($"Your average rating was {customerRatings.Values.Average()}");
            Console.WriteLine();


        }

        private static double GetCustomerRatings(string userName, Dictionary<string, int> customerRatings)
        {
            Console.WriteLine($"{userName}, it is now time for you rank the following statements.");
            Console.WriteLine("Please rank each comment from 1-5. 1 = Totally Disagree, 5 = Totally Agree.");


            string[] rankedItems = customerRatings.Keys.ToArray();

            foreach (var item in rankedItems)
            {
                Console.WriteLine();
                Console.WriteLine(item);
                Console.WriteLine("Do you agree or disagree 1-5). An invalid answer will default to 3 - Neutral.");
                
                int value;
                try
                {
                    value = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    value = 3;
                }
                customerRatings[item] = value;
            }

            Console.WriteLine($"Thank you {userName} for sharing your rankings with us.");

            return customerRatings.Values.Average();
        }

        private static void GetCustomerFavorites(string userName, Dictionary<string, string> customerFavorites)
        {
            Console.WriteLine($"Thank you for your information, {userName}. Now it's time to find out your favorites.");
            string[] favoriteItems = customerFavorites.Keys.ToArray();

            foreach (var item in favoriteItems)
            {
                Console.WriteLine($"What is your favorite {item}?");
                string answer = Console.ReadLine();
                customerFavorites[item] = answer;
            }

            Console.WriteLine($"Thank you {userName} for sharing your favorites with us.");
        }

        private static string GetCustomerInformation()
        {
          
            Console.WriteLine("We are going to start off by asking you some basic questions.");
            Console.WriteLine("What is the name you preferred to be called?");
            string userName = Console.ReadLine();

            Console.WriteLine("What is your current address?");
            string address = Console.ReadLine();
            Console.WriteLine("What type of building to you live in?");
            string buildingType = Console.ReadLine();

            Console.WriteLine("Is your home built on sacred ground (y/n)?");
            string input = Console.ReadLine().ToLower().Trim();
            bool isSacredGround = input == "y" ? true : false;

            return userName;
        }

        private static void PauseApplication()
        {
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
        }

        private static void DisplayApplicationInformation()
        {
            DisplayBanner("Greatest Application Ever");

            DisplayBanner("Application");


            DisplayBanner("Greatest Application Everrrrrrrrrrrrrrrrrrr");

            Console.WriteLine("Welcome to this great application.");
            Console.WriteLine("This application was created to gather information from you, our valued customer.");
            Console.WriteLine("With the answers you provide us, we hope to create an amazing shopping experience for you.");

          
         }

        private static void DisplayBanner(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(new string('*', 50));
            int buffer = 24 - message.Length / 2;
            Console.Write("*" + new string(' ', buffer));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(new string(' ', buffer - 1) + "*" + Environment.NewLine);
            Console.WriteLine(new string('*', 50));
            Console.ForegroundColor = ConsoleColor.Gray;

        }
    }
}
