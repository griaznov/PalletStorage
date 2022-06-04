public class StorageTests
{
    [Fact(DisplayName = "1. Creating a simple storage")]
    public void CreationStorage()
    {
        try
        {

        }
        catch (Exception ex)
        {
            Assert.Equal(typeof(ArgumentException), ex.GetType());
        }
    }
}
