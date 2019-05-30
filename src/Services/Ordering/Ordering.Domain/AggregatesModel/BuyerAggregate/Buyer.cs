using System;

namespace eShopQ.Services.Ordering.Domain.AggregatesModel.BuyerAggregate
{
  public class Buyer
  {
    private string identity;
    private string name;

    public Buyer(string identity, string name)
    {
      this.identity = identity;
      this.name = name;
    }
  }
}