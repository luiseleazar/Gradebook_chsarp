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
    ///<summary>
    /// Interface <c>IBook</c>
    ///</summary>
    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string? Name{ get; }
        event GradeAddedDelegate GradeAdded;   
    }

    /// <summary>
    /// Class <c>Book</c> models a student's book (derived from NamedObject)
    /// </summary>
    public abstract class Book : NamedObject, IBook
    {
        protected Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;
        public abstract void AddGrade(double grade); //Abastract is implicit virtual
        public abstract Statistics GetStatistics();
    }

    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        { 
        }

        public override event GradeAddedDelegate GradeAdded;
        public override void AddGrade(double grade)
        {
            //Common pattern in C# of disposable objects
            using(var writer = File.AppendText($"{Name}.txt"))
            {
                writer.WriteLine(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            //When curly braces above are closed, the C# compiler guarantees that
            //it will call dispose on this IDisponsable object
        }
        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            using(var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while(line != null)
                {
                    var number = double.Parse(line);
                    result.Add(number);
                    line = reader.ReadLine();
                }
            }

            return result;
        }
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

        public override event GradeAddedDelegate GradeAdded;

        /// <summary>
        /// Method <c>GetStatistics</c> get Statistics class members
        /// </summary>
        public override Statistics GetStatistics()
        {
            var result =  new Statistics();

            for(var i = 0; i < grades.Count; i++)
            {
                result.Add(grades[i]);
            }

            return result;
        }
        private List<double> grades;

        public const string CATEGORY = "Science";
    }
}