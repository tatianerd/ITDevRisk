using System.Collections.Generic;

namespace ITDevRisk.Domain
{
    public class TradeCategories
    {
        public static TradeCategories Expired = new TradeCategories(null, 0, false, 30, "EXPIRED");

        public static TradeCategories HighRisk = new TradeCategories("Private", 1000000, false, 0, "HIGHRISK");

        public static TradeCategories MediumRisk = new TradeCategories("Public", 1000000, false, 0, "MEDIUMRISK");

        private TradeCategories(string sectorValidation, double valueValidation, bool conditionValidation, double dateValidation, string categoryName)
        {
            SectorValidation = sectorValidation;
            ValueValidation = valueValidation;
            ConditionValidation = conditionValidation;
            DateValidation = dateValidation;
            CategoryName = categoryName;
        }

        public string SectorValidation { get; set; }

        public double ValueValidation { get; set; }

        public bool ConditionValidation { get; set; }

        public double DateValidation { get; set; }

        public string CategoryName { get; set; }

        public static IEnumerable<TradeCategories> ListAll()
        {
            return new[] { Expired, HighRisk, MediumRisk, };
        }
    }
}
