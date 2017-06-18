using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace WorldTour
{
  [Collection("WorldTour")]
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_VenuesEmptyAtFirst()
    {
      //Arrange, Act
      int result = Venue.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Venue firstVenue = new Venue("Orange Peel");
      Venue secondVenue = new Venue("Orange Peel");

      //Assert
      Assert.Equal(firstVenue, secondVenue);
    }

    [Fact]
    public void Test_Save_SavesVenueToDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("Emporium");
      testVenue.Save();

      //Act
      List<Venue> result = Venue.GetAll();
      List<Venue> testList = new List<Venue>{testVenue};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToVenueObject()
    {
      //Arrange
      Venue testVenue = new Venue("Emporium");
      testVenue.Save();

      //Act
      Venue savedVenue = Venue.GetAll()[0];

      int result = savedVenue.GetId();
      int testId = testVenue.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsVenueInDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("Emporium");
      testVenue.Save();

      //Act
      Venue foundVenue = Venue.Find(testVenue.GetId());

      //Assert
      Assert.Equal(testVenue, foundVenue);
    }
      [Fact]
      public void Test_Update_UpdatesVenueInDatabase()
      {
        //Arrange
        string name = "Oragnes Peel";
        Venue testVenue = new Venue(name);
        testVenue.Save();
        string newName = "Orange Peel";

        //Act
        testVenue.Update(newName);

        string result = testVenue.GetName();

        //Assert
        Assert.Equal(newName, result);
      }

      [Fact]
      public void Test_Delete_DeletesVenueFromDatabase()
      {
        //Arrange
        string name1 = "Emporium";
        Venue testVenue1 = new Venue(name1);
        testVenue1.Save();

        string name2 = "Orange Peel";
        Venue testVenue2 = new Venue(name2);
        testVenue2.Save();

        Band testBand1 = new Band("Kendrick Lamar", testVenue1.GetId());
        testBand1.Save();
        Band testBand2 = new Band("Moldy Peaches", testVenue2.GetId());
        testBand2.Save();

        //Act
        testVenue1.Delete();
        List<Venue> resultVenues = Venue.GetAll();
        List<Venue> testVenueList = new List<Venue> {testVenue2};
        //Assert
        Assert.Equal(testVenueList, resultVenues);
      }
      [Fact]
      public void Test_AddBand_AddsToVenue()
      {
        //Arrange
        Venue testVenue = new Venue("Emporium");
        testVenue.Save();

        Band testBand = new Band("Kendrick Lamar");
        testBand.Save();

        Band testBand2 = new Band("Moldy Peaches");
        testBand2.Save();

        //Act
        testVenue.AddBand(testBand);
        testVenue.AddBand(testBand2);

        List<Band> result = testVenue.GetBands();

        List<Band> testList = new List<Band>{testBand, testBand2};

        //Assert
        Assert.Equal(testList, result);
      }
      [Fact]
      public void GetBands_ReturnAllVenueBands_BandList()
      {
        //Arrange
        Venue testVenue = new Venue("Emporium");
        testVenue.Save();

        Band testBand1 = new Band("Kendrick Lamar");
        testBand1.Save();

        Band testBand2 = new Band("Moldy Peaches");
        testBand2.Save();

        //Act
        testVenue.AddBand(testBand1);
        List<Band> savedBands = testVenue.GetBands();
        List<Band> testList = new List<Band>{testBand1};

        //Assert
        Assert.Equal(testList, savedBands);
      }

      [Fact]
      public void Delete_DeletesVenueAssociationsFromDatabase_VenueList()
      {
        //Arrange
        Band testBand = new Band("Kendrick Lamar");
        testBand.Save();
        string testName = "Orange Peel";
        Venue testVenue = new Venue(testName);
        testVenue.Save();

        //Act
        testVenue.AddBand(testBand);
        testVenue.Delete();

        List<Venue> resultBandVenues = testBand.GetVenues();
        List<Venue> testBandVenues = new List<Venue> {};

        //Assert
        Assert.Equal(testBandVenues, resultBandVenues);
      }

      public void Dispose()
      {
        Band.DeleteAll();
        Venue.DeleteAll();
      }
  }
}
