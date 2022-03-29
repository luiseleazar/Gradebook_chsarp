using System;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs agrs);

    /// <summary>
    /// Class <c>NamedObject</c> base class for named objects
    /// </summary>
    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name;
        }
        public string? Name
        {
            get;
            set;
        }
    }
    /// <summary>
    /// Class <c>Book</c> models a student's book (derived from NamedObject)
    /// </summary>
    public abstract class Book : NamedObject
    {
        protected Book(string name) : base(name)
        {
            //..
        }

        public abstract void AddGrade(double grade); 
    }
    /// <summary>
    /// Class <c>InMemoryBook</c> models a book which stores data in memory (derived from Book)
    /// </summary>
    public class InMemoryBook : Book
    {
        /// <summary>
        /// Method <c>InMemoryBook</c> constructor
        /// </summary>
        #pragma warning disable CS8618
        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
            Name = name;
        }
        #pragma warning restore CS8618

        /// <summary>
        /// Method <c>AddGrade</c> add a letter grade
        /// </summary>
        public void AddGrade(char letter)
        {
            switch(letter)
            {
                case 'A':
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;
                case 'D':
                    AddGrade(60);
                    break;
                case 'F':
                    AddGrade(50);
                    break;
                default:
                    AddGrade(0);
                    break;
            }
        }

        /// <summary>
        /// Method <c>AddGrade</c> adds a grade (in range 0-100) to the book
        /// </summary>
        public override void AddGrade(double grade)
        {
            if(grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        public event GradeAddedDelegate GradeAdded;

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

            switch(result.Average)
            {
                case var d when d >= 90.0:
                    result.Letter = 'A';
                    break;
                case var d when d >= 80.0:
                    result.Letter = 'B';
                    break;
                case var d when d >= 70.0:
                    result.Letter = 'C';
                    break;
                case var d when d >= 60.0:
                    result.Letter = 'A';
                    break;
                default:
                    result.Letter = 'F';
                    break;
            }

            return result;
        }
        private List<double> grades;

        public const string CATEGORY = "Science";
    }
}