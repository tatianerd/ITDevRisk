using ITDevRisk.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Globalization;

namespace ITDevRisk.Application.Services
{
    public class TradeCategorizationService : ITradeCategorizationService
    {
        public async Task<List<string>> Categorize(List<string> tradeInput)
        {
            try
            {
                if (tradeInput != null)
                {
                    var tradePortfolio = await CreateTradePortfolio(tradeInput).ConfigureAwait(false);

                    if (tradePortfolio.NumberOfTrades != tradePortfolio.Trades.Count)
                    {
                        throw new Exception("Number of trades has to match");
                    }

                    return await ClassifyPortfolio(tradePortfolio).ConfigureAwait(false);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        private async Task<TradePortfolio> CreateTradePortfolio(List<string> tradeInput)
        {
            try
            {
                List<Trade> trades = new List<Trade>();

                for (var i = 2; i<tradeInput.Count(); i++)
                {
                    var trade = tradeInput[i].Split(" ");

                    trades.Add(Trade.SetTrade(double.Parse(trade[0].Trim()), trade[1].Trim(), DateTime.ParseExact(trade[2].Trim(), "MM/dd/yyyy", CultureInfo.CreateSpecificCulture("en-US"))));
                }

                return TradePortfolio.SetTradePortfolio(DateTime.Parse(tradeInput[0].Trim()), Convert.ToInt32(tradeInput[1].Trim()), trades);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<List<string>> ClassifyPortfolio(TradePortfolio portfolio)
        {
            try
            {
                List<string> classifications = new List<string>();

                foreach (var trade in portfolio.Trades)
                {
                    classifications.Add(Trade.CalculateRisk(trade, portfolio.ReferenceDate));
                }

                return classifications;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }      
}
