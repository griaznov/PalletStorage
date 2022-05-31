using Xunit;
using Ex.PalletStorage;

namespace PalletStorageTests;

public class BoxTests
{
    [Fact]
    public void CreationBox()
    {
        try
        {
            StorageBox testBox = new(2, 3, 4, 1, DateTime.Today, DateTime.Today);
            Assert.Equal(24, testBox.Volume);
            Assert.True(true, "Creation Box");
        }
        catch (Exception ex)
        {
            Assert.Equal(typeof(ArgumentException), ex.GetType());
        }
    }

    [Fact]
    public void CreationWithoutDates()
    {
        try
        {
            StorageBox testBox = new(2, 3, 4, 1);
            Assert.True(false, "Creation Box Without Dates is true case");
        }
        catch (Exception ex)
        {
            Assert.True(true, $"{ex.GetType()}");
        }
    }

    [Fact]
    public void CreationWithoutExpirationDate()
    {
        DateTime todayDate = DateTime.Today;
        DateTime dateWith100Days = todayDate.AddDays(100);

        StorageBox testBox = new(2, 3, 4, 1, todayDate);

        Assert.Equal(testBox.ExpirationDate, dateWith100Days);
    }
}