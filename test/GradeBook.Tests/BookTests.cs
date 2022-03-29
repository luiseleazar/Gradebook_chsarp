using System;
using Xunit;

namespace GradeBook.Tests;

public class BookTests
{
    [Fact]
    public void BookCalculatesAverageGrades()
    {
        var book = new InMemoryBook("");
        book.AddGrade(89.1);
        book.AddGrade(90.5);
        book.AddGrade(77.3);

        var result = book.GetStatistics();

        Assert.Equal(85.6, result.Average, 1);
        Assert.Equal(90.5, result.High);
        Assert.Equal(77.3, result.Low);

    }
    /**********************************************************************/
    //This test needs Book.List member to be public
    
    //[Fact]
    // public void CheckAddGradeRange()
    // {
    //     var book = new Book("book 1");
    //     book.AddGrade(105);
    //     book.AddGrade(95);
    //     book.AddGrade(80);

    //     foreach (var grade in book.grades)
    //     {
    //         Assert.InRange(grade, 0,100);
    //     }
    // }
    /**********************************************************************/
    [Fact]
    public void BookSetLetterGrade()
    {
        var book = new InMemoryBook("book 1");
        book.AddGrade(89.1);
        book.AddGrade(90.5);
        book.AddGrade(77.3);

        var result = book.GetStatistics();

        Assert.Equal('B', result.Letter);

    }
}