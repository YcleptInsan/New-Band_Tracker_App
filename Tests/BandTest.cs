using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace WorldTour
{
  [Collection("WorldTour")]
  public class WorldTourTest : IDisposable
  {
    public WorldTourTest()
    {
        DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Band.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equals_ReturnsTrueIfNamesAreTheSame()
    {
      //Arrange, Act
      Band firstBand = new Band("kendrick lamar");
      Band secondBand = new Band("kendrick lamar");

      //Assert
      Assert.Equal(firstBand, secondBand);
    }

    [Fact]
    public void Test_Save_SavesBandToDatabase()
    {
      //Arrange
      Band testBand = new Band("kendrick lamar");
      testBand.Save();

      //Act
      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
      Band testBand = new Band("kendrick lamar");
      testBand.Save();

      //Act
      Band savedBand = Band.GetAll()[0];

      int result = savedBand.GetId();
      int testId = testBand.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsBandInDatabase()
    {
      //Arrange
      Band testBand = new Band("kendrick lamar");
      testBand.Save();

      //Act
      Band foundBand = Band.Find(testBand.GetId());

      //Assert
      Assert.Equal(testBand, foundBand);
    }
    [Fact]
    public void AddVenue_AddsVenueToBand_VenueList()
    {
      //Arrange
      Band testBand = new Band("kendrick lamar");
      testBand.Save();

      Venue testVenue = new Venue("Orange Peel");
      testVenue.Save();

      //Act
      testBand.AddVenue(testVenue);

      List<Venue> result = testBand.GetVenues();
      List<Venue> testList = new List<Venue>{testVenue};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void GetVenues_ReturnsAllBandVenues_VenueList()
    {
      //Arrange
      Band testBand = new Band("kendrick lamar");
      testBand.Save();

      Venue testVenue1 = new Venue("Orange Peel");
      testVenue1.Save();

      Venue testVenue2 = new Venue("Emporium");
      testVenue2.Save();

      //Act
      testBand.AddVenue(testVenue1);
      List<Venue> result = testBand.GetVenues();
      List<Venue> testList = new List<Venue> {testVenue1};

      //Assert
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Delete_DeletesBandAssociationsFromDatabase_BandList()
    {
      //Arrange
      Venue testVenue = new Venue("Orange Peel");
      testVenue.Save();

      Band testBand = new Band("kendrick lamar");
      testBand.Save();

      //Act
      testBand.AddVenue(testVenue);
      testBand.Delete();

      List<Band> resultVenueBands = testVenue.GetBands();
      List<Band> testVenueBands = new List<Band> {};

      //Assert
      Assert.Equal(testVenueBands, resultVenueBands);
    }
    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
