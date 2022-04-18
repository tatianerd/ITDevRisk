using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITDevRisk.Application.Services
{
    public  interface ITradeCategorizationService
    {
        /// <summary>
        /// Categorize the trades according to input data
        /// </summary>
        /// <param name="tradeInput">Trade data</param>
        /// <returns>List of string with the risks</returns>
        public Task<List<string>> Categorize(List<string> tradeInput);
    }
}
