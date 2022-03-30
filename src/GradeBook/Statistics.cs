using System;

namespace GradeBook
{
    /// <summary>
    /// Class <c>Statistics</c> calculates stats from books
    /// </summary>
    public class Statistics
    {
        public double Average
        {
            get
            {
                return Sum / Count;
            }
        }
        public double High;
        public double Low;
        public char Letter
        {
            get
            {
                switch(Average)
                {
                    case var d when d >= 90.0:
                        return 'A';
                    case var d when d >= 80.0:
                        return 'B';
                    case var d when d >= 70.0:
                        return 'C';
                    case var d when d >= 60.0:
                        return 'A';
                    default:
                        return 'F';
                }
            }
        }
        public double Sum;
        public int Count;

        /// <summary>
        /// Method <c>Statistics</c> constructor
        /// </summary>
        public Statistics()
        {
            Count = 0;
            Sum = 0.0;
            High = double.MinValue;
            Low = double.MaxValue;
        }

        /// <summary>
        /// Method <c>Add</c> takes a number and add it to a sum, it count how many number have been added
        /// </summary>
        public void Add(double number)
        {
            Sum += number;
            Count += 1;
            High = Math.Max(number, High);
            Low = Math.Min(number, Low);
        }

    }
}