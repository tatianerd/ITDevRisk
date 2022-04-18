using System;
using System.Linq;

namespace ITDevRisk.Domain
{
    public  class Trade : ITrade
    {
        private Trade() { }

        private Trade(double value, string clientSector, DateTime nextPaymentDate)
        {
            Value = value;
            ClientSector = clientSector;
            NextPaymentDate = nextPaymentDate;
        }

        public static Trade SetTrade(double value, string clientSector, DateTime nextPaymentDate)
        {
            return new Trade(value, clientSector, nextPaymentDate);
        }

        public static string CalculateRisk(Trade trade, DateTime referenceDate)
        {
            return TradeCategories.ListAll().FirstOrDefault(x => x.SectorValidation == trade.ClientSector && x.ValueValidation > 0 ? trade.Value > x.ValueValidation : false
            ||
            x.DateValidation > 0 &&
            DateTime.Compare(trade.NextPaymentDate.AddDays(x.DateValidation), referenceDate) < 0).CategoryName;
        }

        public double Value { get; set; }

        public string ClientSector { get; set; }

        public DateTime NextPaymentDate { get; set; }
    }
}
