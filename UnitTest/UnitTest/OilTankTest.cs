using NUnit.Framework;
using Tanklager_1;
using TanklagerLibraryv2;

namespace UnitTest;



[TestFixture]
public class OilTankTest
{
    public string input;
    [Test]
    public void CheckFillTank()
    {
        //Arrange
        OilTank t = new OilTank(1, "tank-1", DateTime.Now, 300,0,false);
        
        //Act
        int overflow = t.FillTank(100);
        
       
        //Assert
        Assert.That(t.oilAmount == 100);
        Assert.That(overflow == -200);
        Assert.That(t.GetSpace() == 200);

    }
    
    [Test]
    public void CheckFillTankTooMuch()
    {
        //Arrange
        OilTank t = new OilTank(1, "tank-1", DateTime.Now, 200,0,false);
        
        //Act
        int overflow = t.FillTank(250);

       
        //Assert
        Assert.That(t.oilAmount == 200);
        Assert.That(t.GetSpace() == 0);
        Assert.That(overflow == 50);

    }


    [Test]
    public void TestOptimizedFill()
    {
        //Arrange
        OilTank testobject = new OilTank(1, "Tank", DateTime.Now, 500, 0, true);
        
        //Act
        testobject.FillTank(200);
        
        //Assert  
    }
    [Test]
    public void CheckEmptyTank()
    {
        //Arrange
        OilTank t = new OilTank(1, "tank-1", DateTime.Now, 200,100,false);
        
        //Act
        int overflow = t.EmptyTank(50);

       
        //Assert
        Assert.That(t.oilAmount == 50);
        Assert.That(t.GetSpace() == 150); 
        Assert.That(overflow == -50);

    }
    
    [Test]
    public void CheckEmptyTankTooMuch()
    {
        //Arrange
        OilTank t = new OilTank(1, "tank-1", DateTime.Now, 200,100,false);
        
        //Act
        int overflow = t.EmptyTank(150);

       
        //Assert
        Assert.That(t.oilAmount == 0);
        Assert.That(t.GetSpace() == 200); 
        Assert.That(overflow == 50);

    }
    

    
    
    [Test]
    public void CheckSetMaintenance()
    {
        //Arrange
        OilTank t = new OilTank(1, "tank-1", DateTime.Now, 200,100,false);
        
        //Act


       
        t.SetMaintenance(true);

       
        //Assert
        Assert.That(t.isInMaintenance == true);
        
        

    }



    /// <summary>
    /// Todo: Ein Tank der in Maintenance muss eine Exception werfen, wenn die Fill-methode aufgerufen wird.
    /// </summary>
    [Test]
    public void TestFillTankWhichIsInMaintenance()
    {
        //Arrange
        //create tank
        //set tank in maint
        
        //Act
        //fill Tank
    }
    
    /// <summary>
    /// Todo: Ein Tank darf keine negative Füllmenge annehmen
    /// </summary>
    [Test]
    public void TestFillTankWithNegativeAmount()
    {
        
    }
    
    /// <summary>
    /// Todo: Ein Tank kann nicht mit negativer Kapazität erstellt werden
    /// </summary>
    [Test]
    public void TestCreateTankWithNegativeCapacity()
    {
        
    }
}