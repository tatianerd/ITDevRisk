using ITDevRisk.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ITDevRisk
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> teste =
                new List<string>()
                {
                    "12/11/20200",
                    "4",
                    "2000000 Private 12/29/2025",
                    "400000 Public 07/01/2020",
                    "5000000 Public 01/02/2024",
                    "3000000 Public 10/26/2023"
                };

            List<string> tradePortfolio = new List<string>();

            Console.WriteLine("Please input the reference date (MM/dd/yyyy): ");
            tradePortfolio.Add(Console.ReadLine());

            Console.WriteLine("Please inform the amount of trades of the portfolio: ");
            tradePortfolio.Add(Console.ReadLine());

            Console.WriteLine("Now please inform the trade operations information (Value Sector NextPaymentDate). Press enter once to inform the next trade, press enter twice to calculate the risks of each informed operation: ");
            string tradeInfo = null;

            while (tradeInfo != ""){
                tradeInfo = Console.ReadLine();
                if(tradeInfo != "")
                    tradePortfolio.Add(tradeInfo);
            }

            var serviceProvider = new ServiceCollection()
            .AddSingleton<ITradeCategorizationService, TradeCategorizationService>()
            .BuildServiceProvider();

            var tradeCategorizationService = serviceProvider.GetService<ITradeCategorizationService>();

            try
            {
                var tradeCategorization = tradeCategorizationService.Categorize(tradePortfolio);

                foreach (var trade in tradeCategorization.Result)
                {
                    Console.WriteLine(trade);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
                
        }
    }
}
