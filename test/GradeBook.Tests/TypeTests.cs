using System;
using Xunit;

namespace GradeBook.Tests;

public class TypeTests
{
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
    private void GetBookSetName(out Book book, string name)
    {
        book = new Book(name);
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
    private void GetBookSetName(Book book, string name)
    {
        book = new Book(name);
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
    private void SetName(Book book, string name)
    {
        book.Name = name;
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
    Book GetBook(string name)
    {  
        return new Book(name);
    }
}