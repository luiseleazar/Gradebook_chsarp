using System;

namespace GradeBook
{
    /// <summary>
    /// Class <c>Book</c> models a book of student grades
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Method <c>Book</c> constructor
        /// </summary>
        public Book(string name)
        {
            grades = new List<double>();
            Name = name;
        }
        /// <summary>
        /// Method <c>AddGrade</c> adds a grade to the book
        /// </summary>
        public void AddGrade(double grade)
        {
            grades.Add(grade);
        }

        /// <summary>
        /// Method <c>GetStatistics</c> get Statistics class members
        /// </summary>
        public Statistics GetStatistics()
        {
            var result =  new Statistics();
            result.Average = 0.0;
            result.High = double.MinValue;
            result.Low = double.MaxValue;

            foreach(var grade in grades)
            {
                result.High = Math.Max(grade, result.High);
                result.Low = Math.Min(grade, result.Low);
                result.Average += grade;
            }
            result.Average /= grades.Count;

            return result;
        }

        private List<double> grades;
        public string Name;
    }
}