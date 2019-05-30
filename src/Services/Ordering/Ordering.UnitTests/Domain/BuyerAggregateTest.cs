using System;
using Xunit;
using eShopQ.Services.Ordering.Domain.AggregatesModel.BuyerAggregate;

namespace eShopQ.Services.Ordering.UnitTests.Domain.AggregatesModel.BuyerAggregate
{
  public class BuyerAggregateTest
  {
    [Fact]
    public void Create_buyer_item_success()
    {
        //Arrange    
        var identity = new Guid().ToString();
        var name = "fakeUser";

        //Act 
        var fakeBuyerItem = new Buyer(identity, name);

        //Assert
        Assert.NotNull(fakeBuyerItem);
      }
  }
}