using PalletStorage;

namespace PalletStorageTests;

public class BoxTests
{
    [Fact(DisplayName = "1. Creating a normal box and checking the volume count")]
    public void CreationBox()
    {
        try
        {
            StorageBox testBox = new(2, 3, 4, 1, DateTime.Today, DateTime.Today);
            Assert.Equal(24, testBox.Volume);
        }
        catch (Exception ex)
        {
            Assert.Equal(typeof(ArgumentException), ex.GetType());
        }
    }

    [Fact(DisplayName = "1.1 Creating a normal box and checking the volume count by Create()")]
    public void CreationBoxByCreate()
    {
        try
        {
            StorageBox? testBox = StorageBox.Create(2, 3, 4, 1, DateTime.Today, DateTime.Today);

            if (testBox == null)
            {
                Assert.True(false, "False with Create() StorageBox!");
            }
            else
            {
                Assert.Equal(24, testBox.Volume);
            }
        }
        catch (Exception ex)
        {
            Assert.Equal(typeof(ArgumentException), ex.GetType());
        }
    }

    [Fact(DisplayName = "2. Creating a box without dates")]
    public void CreationWithoutDates()
    {
        try
        {
            StorageBox testBox = new(2, 3, 4, 1);
            Assert.True(false, "Creating a box without dates must trow Exception!");
        }
        catch (Exception ex)
        {
            Assert.Equal(typeof(ArgumentOutOfRangeException), ex.GetType());
        }
    }

    [Fact(DisplayName = "3. Creating a box without expiration date")]
    public void CreationWithoutExpirationDate()
    {
        DateTime todayDate = DateTime.Today;
        DateTime dateWith100Days = todayDate.AddDays(100);

        StorageBox testBox = new(2, 3, 4, 1, todayDate);

        Assert.Equal(testBox.ExpirationDate, dateWith100Days);
    }
}