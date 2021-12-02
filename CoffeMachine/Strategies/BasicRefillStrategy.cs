using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Strategies
{
    public class BasicRefillStrategy : IRefillStrategy
    {

        private readonly int maxQuantity;
        private readonly int minQuantity;       
        internal BasicRefillStrategy(int maxQuantity, int minQuantity)
        {

            this.maxQuantity = maxQuantity;
            this.minQuantity = minQuantity;

        }

        public static IRefillStrategy Create(int maxQuantity, int minQuantity)
        {
            return new BasicRefillStrategy(maxQuantity, minQuantity);
        }
        public int Refill(int initialQuantity, int refillAmount)
        {
            int updatedQuantity = initialQuantity + refillAmount;
            if(updatedQuantity <= maxQuantity)
            {
                return updatedQuantity;
            }

            Console.WriteLine("Some ingredient overflown while refilling");
            return maxQuantity;
        } 
        
        public bool IsRefillRequired(int quantity)
        {
            return quantity < minQuantity;
        }
    }
}
