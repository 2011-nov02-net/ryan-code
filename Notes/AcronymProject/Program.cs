using System;

namespace AcronymProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get user Input
            Console.WriteLine("Enter name to get acronym: ");
            string input = Console.ReadLine();
            //Split input up
            string[] strArray = input.Split();

            //loop through each word and get capital first letter and display output
            foreach (string word in strArray) {
                Console.Write(word.Substring(0,1).ToUpper());
            }
        }
    }
}
