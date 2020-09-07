using System;
using System.Collections.Generic;

namespace PromotionEngine
{
    internal class Program
    {
        private static Dictionary<string, int> UnitPrices = new Dictionary<string, int>()
        {
            { "A",50 },
            { "B",30 },
            { "C",20 },
            { "D",15 },
        };

        private static List<(List<(string SKU, int n)> SKUs, int fixedPrice)> ActivePromotions =
            new List<(List<(string SKU, int n)> SKUs, int fixedPrice)>();

        private static void Main()
        {
            var promotion = (new List<(string SKU, int n)>() { ("A", 3) }, 130);
            SetPromotion(promotion);
            promotion = (new List<(string SKU, int n)>() { ("B", 2) }, 45);
            SetPromotion(promotion);
            promotion = (new List<(string SKU, int n)>() { ("C", 1), ("D", 1) }, 30);
            SetPromotion(promotion);

            var order = new Dictionary<string, int> { { "A", 1 }, { "B", 1 }, { "C", 1 } };
            Console.WriteLine(ApplyPromotion(order));
            order = new Dictionary<string, int> { { "A", 5 }, { "B", 5 }, { "C", 1 } };
            Console.WriteLine(ApplyPromotion(order));
            order = new Dictionary<string, int> { { "A", 3 }, { "B", 5 }, { "C", 1 }, { "D", 1 } };
            Console.WriteLine(ApplyPromotion(order));
            Console.ReadKey();
        }

        private static int ApplyPromotion(Dictionary<string, int> order)
        {
            throw new NotImplementedException();
        }

        public static void SetPromotion((List<(string SKU, int n)> SKUs, int fixedPrice) promotion) => ActivePromotions.Add((promotion.SKUs, promotion.fixedPrice));
    }
}