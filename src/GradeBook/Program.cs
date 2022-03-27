using System;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new Book("Eleazar's Grade Book");
            book.GradeAdded += OnGradeAdded;
            
            //User input
            var done = false; //to loop
            while(!done)
            {
                Console.WriteLine("Enter a grade or type 'q' to exit.");
                string? input = Console.ReadLine(); //? is to avoid WATNING CS8600
                if(input == null)
                {
                    throw new ArgumentNullException();
                }
                if(input == "q")
                {
                    done = true;
                }
                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch(FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("**");
                }      
            }
            
            var stats = book.GetStatistics();

            Console.WriteLine(Book.CATEGORY);
            Console.WriteLine($"For the book named {book.Name}");
            Console.WriteLine($"The average grade is {stats.Average:N1}");
            Console.WriteLine($"The highest grade is {stats.High}");
            Console.WriteLine($"The lowest grade is {stats.Low}");
            Console.WriteLine($"The letter grade is {stats.Letter}");
        }
        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A grade was added...");
        }
    }
}