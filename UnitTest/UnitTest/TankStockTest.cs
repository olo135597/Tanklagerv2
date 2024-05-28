using NUnit.Framework;
using Tanklager_1;
using TanklagerLibraryv2;

namespace UnitTest;

[TestFixture]
public class TankStockTest
{
    public string input;
    
    [Test]
    public void CheckCreateTanks()
    {
        
        //Arrange
   
        TankStock ts = new TankStock();
        
        //Act 
        ts.CreateTank(1, "tank-1", 200, false);
        var CreatedTank = ts.tanks
            .FirstOrDefault(t => t.id == 1); 
        
        
        //Assert
        Assert.That(ts.tanks.Count == 1);
        Assert.That(CreatedTank.capacity == 200);
       

    }
    
    [Test]
    public void CheckSameTanksId()
    {
        
        //Arrange
     
        TankStock ts = new TankStock();
        //Act 
        ts.CreateTank(1, "tank-1", 200, false);
        bool CheckIdNotSame = ts.CheckId(1);
        
        
        //Assert
        Assert.That(ts.tanks.Count == 1);
        Assert.That(CheckIdNotSame == false);
       

    }
    
    [Test]
    public void CheckSameTanksName()
    {
        
        //Arrange
   
        TankStock ts = new TankStock();
        //Act 
        ts.CreateTank(1, "tank-1", 200, false);
        bool CheckNameNotSame = ts.CheckName("tank-1");
        
        
        //Assert
        Assert.That(ts.tanks.Count == 1);
        Assert.That(CheckNameNotSame == false);
       

    }
    
    [Test]
    public void CheckDeleteTank()
    {
        
        //Arrange
        List<OilTank> tanks = new List<OilTank>();
        TankStock ts = new TankStock();
        
        //Act 
        ts.CreateTank(1, "tank-1", 200, false);
        ts.CreateTank(2, "tank-2", 300, false);
        var DeletedTank = ts.tanks
            .FirstOrDefault(t => t.id == 1);
        ts.DeleteTank(DeletedTank.id);
        
        
        //Assert
        Assert.That(ts.tanks.Count == 1);
        Assert.That(ts.GetAllCapacity() == 300);
       

    }
    
    [Test]
    public void CheckFillTanks()
    {
        
        //Arrange
        List<OilTank> tanks = new List<OilTank>();
        TankStock ts = new TankStock();
        
        //Act 
        ts.CreateTank(1, "tank-1", 200, false);
        ts.CreateTank(2, "tank-2", 300, false);
        int overflow = ts.FillTanks(250);

        var firstTank = ts.tanks.FirstOrDefault(t => t.id == 1);
        var secondTank = ts.tanks.FirstOrDefault(t => t.id == 2);
        
        //Assert
        Assert.That(ts.tanks.Count == 2);
        Assert.That(ts.GetAllCapacity() == 500);
        Assert.That(ts.GetAllSpace() == 250);
        Assert.That(firstTank.oilAmount == 200);
        Assert.That(secondTank.oilAmount == 50);
        
        
    }
    
    [Test]
    public void CheckFillTanksTooMuch()
    {
        
        //Arrange
        List<OilTank> tanks = new List<OilTank>();
        TankStock ts = new TankStock();
        
        //Act 
        ts.CreateTank(1, "tank-1", 200, false);
        ts.CreateTank(2, "tank-2", 300, false);
        int overflow = ts.FillTanks(570);

       
        var firstTank = ts.tanks.FirstOrDefault(t => t.id == 1);
        var secondTank = ts.tanks.FirstOrDefault(t => t.id == 2);

        
        //Assert
        Assert.That(ts.tanks.Count == 2);
        Assert.That(ts.GetAllCapacity() == 500);
        Assert.That(ts.GetAllSpace() == 0);
        Assert.That(firstTank.GetSpace() == 0);
        Assert.That(secondTank.GetSpace() == 0);
        Assert.That(overflow == 70);
        
    }

}