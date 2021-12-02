using CoffeeMachine.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Models
{
    public class MachineParams
    {
        public int Outlets { get; set; }
        public List<Drink> Beverages { get; }
        public Dictionary<string, int> Ingredients { get; set; }

        public MachineParams(int outlets, List<Drink> beverages, Dictionary<string, int> ingredients)
        {
            Outlets = outlets;
            Beverages = beverages;
            Ingredients = ingredients;           
        }

    }
}
