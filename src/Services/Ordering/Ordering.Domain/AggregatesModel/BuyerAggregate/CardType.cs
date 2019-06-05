using Ordering.Domain.SeedWork;

namespace Ordering.Domain.AggregatesModel.BuyerAggregate
{
    public class CardType : Enumeration
    {
        public static CardType Amex = new AmexCardType();
        public static CardType Visa = new VisaCardType();
        public static CardType MasterCard = new MasterCardType();

        public CardType(int id, string name)
            : base(id, name)
        {
        }

        private class AmexCardType : CardType
        {
            public AmexCardType() : base(1, "Amex")
            { }
        }

        private class VisaCardType : CardType
        {
            public VisaCardType() : base(2, "Visa")
            { }
        }

        private class MasterCardType : CardType
        {
            public MasterCardType() : base(3, "MasterCard")
            { }
        }
    }
}