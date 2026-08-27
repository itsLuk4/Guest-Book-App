using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace GuestBook
{
    internal class BookLogic
    {
        public static void WelcomeGroup()
        {
            Console.WriteLine("Welcome to Joe's Listings.");
            Console.WriteLine("**************************");
        }

        public static bool AlternativeGreeting(bool changeGreet)
        {
            Console.WriteLine();
            string alternativeGreet = changeGreet
                    ? "Would you like to add a group to the list?\nType 'Yes' or type 'No' to exit: "
                    : "Would you like to add another group to the list?\nType 'Yes' or type 'No' to exit: ";
            Console.Write(alternativeGreet);
            return changeGreet;
        }

        public static void GoodbyeGroup()
        {
            Console.WriteLine();
            Console.WriteLine("Thank you for trying out our app.");
            Console.WriteLine("**************************");
        }

        public static string GetGroupName()
        {
            string groupName = "";
            bool guestLoop = false;

            while (!guestLoop)
            {
                Console.WriteLine();
                Console.Write("Please type in a group name to add to the guest list: ");
                groupName = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(groupName))
                {
                    Console.WriteLine();
                    Console.WriteLine("Please do not leave the space empty.");
                }
                else if (groupName.All(c => char.IsLetter(c) || c == ' '))
                {
                    guestLoop = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Please type group name only in letters.");
                }
            }
            return groupName;
        }

        public static int GetGroupNumber()
        {
            string groupNumber = "";
            bool guestLoop = false;
            int number = 0;

            while (!guestLoop)
            {
                Console.WriteLine();
                Console.Write("How many are in the group (max 20/group): ");
                groupNumber = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(groupNumber))
                {
                    Console.WriteLine();
                    Console.WriteLine("Please do not leave the space empty.");
                }
                else if (int.TryParse(groupNumber, out number) && number > 0 && number <= 20)
                {
                    guestLoop = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Please type only numbers in aforementioned range.");
                }
            }
            return number;
        }

        public static void AddPartyGuests(Dictionary<string, string> guests)
        {
            bool duplicateLooper = false;            
            while (!duplicateLooper) // loop helps retyping a duplicated name without restarting from the beginning
            {
                var name = GetGroupName();
                var number = GetGroupNumber();

                if (guests.ContainsKey(name)) // checks if the name is already in the list
                {
                    Console.WriteLine();
                    Console.WriteLine("The group has already been added to the list. Please use a different name.");
                }
                else
                {
                    guests.Add(name, number.ToString());
                    duplicateLooper = true;
                }
            }       
        }

        public static void CheckPartyGuests(Dictionary<string, string> guests)
        {
            int number = 0;
            foreach (var person in guests)
            {
                Console.WriteLine($"Group: {person.Key}\nGuests: {person.Value}\n---------------------");
                if (int.TryParse(person.Value, out int num))
                {
                    number += num;
                }
            }
            Console.WriteLine($"Total number of guests: {number}");
        }

        public static void PartyGuestsList()
        {
            Dictionary<string, string> guests = new Dictionary<string, string>(); // needs new dictionary for TrackGuests method to work
            bool listLoop = false;
            bool changeGreet = true;
            while (!listLoop)
            {
                AlternativeGreeting(changeGreet);
                string alternativeInput = Console.ReadLine().Trim(); // if this works!!

                if (alternativeInput.ToLower() == "yes")
                {
                    AddPartyGuests(guests);
                    changeGreet = false;
                }
                else if (alternativeInput.ToLower() == "no") 
                {
                    GoodbyeGroup();
                    listLoop = true;
                    if (guests.Count > 0)
                    {
                        CheckPartyGuests(guests);
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Please type ONLY 'Yes' or 'No' response.");
                }
            }
        }
    }
}
