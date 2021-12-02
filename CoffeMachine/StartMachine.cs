using System;
using System.Xml.Linq;
using CoffeeMachine.Repository;
using CoffeeMachine.Deserializer;
using CoffeeMachine.Models;
using CoffeeMachine.Strategies;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace CoffeMachine
{
    public class StartMachine
    {
        
        static void Main(string[] args)
        {
            string[] testCases = Directory.GetFiles(@"TestCases");

            foreach(var testCase in testCases)
            {
                Console.WriteLine($"Started Executing TestCase - {testCase}\n");

                var machineParams = Deserializer.Deserialize(testCase);

                List<Ingredient> ingredients = new List<Ingredient>();

                foreach (var ingredient in machineParams.Ingredients)
                {
                    ingredients.Add(Ingredient.Create(ingredient.Key, ingredient.Value, BasicRefillStrategy.Create(maxQuantity: 500, minQuantity: 10)));
                }

                IIngredientRepository ingredientRepository = IngredientRepository.Create(ingredients);

                IDrinkMachine coffeeMachine = BeveragesMachine.Create(machineParams.Outlets, ingredientRepository);

                Parallel.ForEach(machineParams.Beverages,
                                drink =>
                                {
                                    coffeeMachine.PrepareDrink(drink);
                                });

                Console.WriteLine($"Finished Executing TestCase - {testCase}\n");
            }
            

            

            
        }
    }
}
