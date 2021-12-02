using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Strategies
{
    public interface IRefillStrategy
    {
        int Refill(int initialQuantity, int refillAmount);

        bool IsRefillRequired(int quantity);
    }
    
}
