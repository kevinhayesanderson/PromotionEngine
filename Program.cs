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

        public static int ApplyPromotion(Dictionary<string, int> order)
        {
            int totalOrderValue = default;

            foreach (var (SKUs, fixedPrice) in ActivePromotions)
            {
                bool apply = false;
                SKUs.ForEach(sku =>
                {
                    apply = order.ContainsKey(sku.SKU) && order.TryGetValue(sku.SKU, out int n) && (n >= sku.n);
                });

                if (apply)
                {
                    int no = 1;
                    SKUs.ForEach(sku =>
                    {
                        order.TryGetValue(sku.SKU, out int n);
                        if (SKUs.Count > 1)
                        {
                            int times = order[sku.SKU] / sku.n;
                            no = no == 0 ? times : (times < no) ? times : no;
                            order[sku.SKU] -= times * sku.n;
                        }
                        else
                        {
                            no = order[sku.SKU] / sku.n;
                            order[sku.SKU] -= no * sku.n;
                        }
                    });
                    totalOrderValue += no * fixedPrice;
                }
            }

            foreach (var orderItem in order)
            {
                totalOrderValue += UnitPrices[orderItem.Key] * orderItem.Value;
            }

            return totalOrderValue;
        }

        public static void SetPromotion((List<(string SKU, int n)> SKUs, int fixedPrice) promotion) => ActivePromotions.Add((promotion.SKUs, promotion.fixedPrice));
    }
}