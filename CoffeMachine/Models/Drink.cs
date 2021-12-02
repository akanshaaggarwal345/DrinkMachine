using System;
using System.Collections.Generic;
using System.Linq;


namespace CoffeeMachine.Models
{
    public class Drink
    {
        public string DrinkName { get; set; }
        public Dictionary<string, int> Recipie { get; set; }

        public Drink(string name, Dictionary<string, int> recipie)
        {
            DrinkName = name;
            Recipie = recipie;
        }

    }
}