using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionEngine;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var promotion = (new List<(string SKU, int n)>() { ("A", 3) }, 130);
            Program.SetPromotion(promotion);
            promotion = (new List<(string SKU, int n)>() { ("B", 2) }, 45);
            Program.SetPromotion(promotion);
            promotion = (new List<(string SKU, int n)>() { ("C", 1), ("D", 1) }, 30);
            Program.SetPromotion(promotion);

            var order = new Dictionary<string, int> { { "A", 1 }, { "B", 1 }, { "C", 1 } };
            Assert.AreEqual(Program.ApplyPromotion(order), 100);
            order = new Dictionary<string, int> { { "A", 5 }, { "B", 5 }, { "C", 1 } };
            Assert.AreEqual(Program.ApplyPromotion(order), 370);
            order = new Dictionary<string, int> { { "A", 3 }, { "B", 5 }, { "C", 1 }, { "D", 1 } };
            Assert.AreEqual(Program.ApplyPromotion(order), 280);
        }
    }
}