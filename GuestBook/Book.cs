using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace GuestBook
{

    internal class Book
    {
        // ask the user for their name and how many are in their party
        public static (string groupName, string groupNumber) GuestInput() // using tuple; this asks for input and checks them
        {
            string groupName = "";
            string groupNumber = "";
            bool guestLoop = false;

            while (!guestLoop)
            {
                Console.WriteLine("Please type in a group name to add to the guest list: ");
                groupName = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(groupName))
                {
                    Console.WriteLine("Please do not leave the space empty.");
                }
                else if (groupName.All(c => char.IsLetter(c) || c == ' '))
                {
                    guestLoop = true;
                }
                else
                {
                    Console.WriteLine("Please type group name only in letters.");
                }
            }

            guestLoop = false;
            while (!guestLoop)
            {
                Console.WriteLine("How many are in the group (maximum guests allowed in a group is 20): ");
                groupNumber = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(groupNumber))
                {
                    Console.WriteLine("Please do not leave the space empty.");
                }
                else if (int.TryParse(groupNumber, out int number) && number > 0 && number <= 20)
                {
                    guestLoop = true;
                }
                else
                {
                    Console.WriteLine("Please type only numbers in aforementioned range.");
                }
            }
            return (groupName, groupNumber);
        }

        // Keeping track of how many people are at the party
        public static void TrackGuests(Dictionary<string, string> guests) // this adds guests
        {
            bool duplicateLooper = false;            
            while (!duplicateLooper) // loop helps retyping a duplicated name without restarting from the beginning
            {
                var (name, number) = GuestInput(); // this is how you call a tupple for variables
                if (guests.ContainsKey(name)) // checks if the name is already in the list
                {
                    Console.WriteLine("The group has already been added to the list. Please use a different name.");
                }
                else
                {
                    guests.Add(name, number);
                    duplicateLooper = true;
                }
            }       
        }

        // Printing out the guest list and the total number of guests
        public static void GuestList() // This is used to call and start and end the program
        {
            Dictionary<string, string> guests = new Dictionary<string, string>(); // needs new dictionary for TrackGuests method to work
            bool changeGreet = true;
            bool listLoop = false;
            Console.WriteLine("Welcome to Joe's Listings.");

            while (!listLoop)
            {
                string alternativeGreet = changeGreet 
                    ? "Would you like to add a group to the list?\nType 'Yes' or type 'No' to exit: " 
                    : "Would you like to add another group to the list?\nType 'Yes' or type 'No' to exit: ";
                Console.WriteLine(alternativeGreet);
                string input = Console.ReadLine().Trim();

                if (input.ToLower() == "yes")
                {                    
                    TrackGuests(guests);
                    changeGreet = false;
                }
                else if (input.ToLower() == "no") 
                {
                    Console.WriteLine("Thank you for trying out our app.");
                    listLoop = true;
                    if (guests.Count > 0)
                    {
                        int number = 0;
                        Console.WriteLine("\n----------");
                        foreach (var person in guests)
                        {
                            Console.WriteLine($"Group: {person.Key}\nGuests: {person.Value}\n----------");
                            if (int.TryParse(person.Value, out int num))
                            {
                                number += num;
                            }
                        }
                        Console.WriteLine($"Total number of guests: {number}");
                    }
                }
                else
                {
                    Console.WriteLine("Please type 'Yes' or 'No' response.");
                }
            }
            
            
            
        }
    }
}
