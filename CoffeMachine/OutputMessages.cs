using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine
{
    public static class OutputMessages
    {

        public static string Success(string drinkName)
        {
            return $"{drinkName} is prepared.";
        }

        public static string Failure(string drinkName, string ingredientName)
        {
            return $"{drinkName} cannot be prepared because {ingredientName} is not available.";
        }

        public static string Preparing(string drinkName)
        {
            return $"Preparing {drinkName}";
        }
    }
}
