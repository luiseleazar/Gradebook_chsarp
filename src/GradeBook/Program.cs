using System;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            IBook book = new DiskBook("Eleazar's Grade Book");
            book.GradeAdded += OnGradeAdded;

            //User input
            EnterGrades(book);
            var stats = book.GetStatistics();

            Console.WriteLine($"For the book named {book.Name}");
            Console.WriteLine($"The average grade is {stats.Average:N1}");
            Console.WriteLine($"The highest grade is {stats.High}");
            Console.WriteLine($"The lowest grade is {stats.Low}");
            Console.WriteLine($"The letter grade is {stats.Letter}");
        }
        /// <summary>
        ///  Method <c>Enter Grades</c> loops to enter grades
        /// </summary>
        private static void EnterGrades(IBook book)
        {
            var done = false; //to loop
            while (!done)
            {
                Console.WriteLine("Enter a grade or type 'q' to exit.");
                string? input = Console.ReadLine(); //? is to avoid WATNING CS8600
                if (input == null)
                {
                    throw new ArgumentNullException();
                }
                if (input == "q")
                {
                    done = true;
                }
                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("**");
                }
            }
        }

        /// <summay>
        /// Event <c>OnGradeAdded</c> prints everytime a grade is added
        /// </summary>
        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A grade was added...");
        }
    }
}