using System;
using System.Collections.Generic;

namespace ITDevRisk.Domain
{
    public class TradePortfolio
    {
        private TradePortfolio() { }

        private TradePortfolio(DateTime referenceDate, int numberOfTrades, List<Trade> trades)
        {
            ReferenceDate = referenceDate;
            NumberOfTrades = numberOfTrades;
            Trades = trades;
        }

        public static TradePortfolio SetTradePortfolio(DateTime referenceDate, int numberOfTrades, List<Trade> trades)
        {
            return new TradePortfolio(referenceDate, numberOfTrades, trades);
        }
        
        public DateTime ReferenceDate { get; set; }

        public int NumberOfTrades { get; set; }

        public List<Trade> Trades { get; set; }
    }
}
