using System;
using Xunit;

namespace GradeBook.Tests
{
    public delegate string WriteLogDelegate(string logMessage);
    public class TypeTests
    { 
        int count = 0;

        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log = ReturnMesage;

            //log = new WriteLogDelegate(ReturnMesage);
            log += ReturnMesage; //Same as above
            log += IncrementCount;

            var result = log("Hello!");
            Assert.Equal(3, count);
        }
        string IncrementCount(string message)
        {
            count++;
            return message.ToLower();
        }
        string ReturnMesage(string message)
        {
            count++;
            return message;
        }
        
        [Fact]
        public void ValueTypesAlsoPassByValue()
        {
            var x = GetInt();
            SetInt(out x);
            Assert.Equal(10, x);
        }

        private void SetInt(out int z)
        {
            z = 10;
        }

        private int GetInt()
        {
            return 3;
        }

        [Fact]
        public void CSharpCanPassByRef()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(out book1, "New Name");

            Assert.Same("New Name", book1.Name);
        }

        /// <summary>
        /// Method <c>GetBookSetName</c> gets an object ref and assign a name
        /// </summary>
        private void GetBookSetName(out InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }
        [Fact]
        public void CSharpIsPassedByValue()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New Name");

            Assert.Same("Book 1", book1.Name);
        }

        /// <summary>
        /// Method <c>GetBookSetName</c> constructus a new book instance and assign a name
        /// </summary>
        private void GetBookSetName(InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }
        [Fact]
        public void CanSetNameFromReference()
        {
            var book1 = GetBook("Book 1");
            SetName(book1, "New Name");

            Assert.Same("New Name", book1.Name);
        }

        /// <summary>
        /// Method <c>SetName</c> sets a new name to a book object
        /// </summary>
        private void SetName(InMemoryBook book, string name)
        {
            book.Name = name;
        }
        [Fact]
        public void StringsBehaveLikeValueType()
        {
            //Strings are immuteable
            string name = "Luis";
            var upper = MakeUpperCase(name);

            Assert.Equal("Luis", name);
            Assert.Equal("LUIS", upper);
        }

        private string MakeUpperCase(string parameter)
        {   
            return parameter.ToUpper();
        }

        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
        }

        [Fact]
        public void TwoVariablesCanReferenceSameObject()
        {
            var book1 = GetBook("Book 1");
            var book2 = book1;

            Assert.Same(book1, book2);
            //Same as Assert.True(Object.ReferenceEquals(book1, book2));
        }
        /// <summary>
        /// Method <c>GetBook</c> retuns a Book object
        /// </summary>
        InMemoryBook GetBook(string name)
        {  
            return new InMemoryBook(name);
        }
    }
}
