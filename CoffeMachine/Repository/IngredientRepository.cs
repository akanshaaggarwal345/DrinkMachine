using System;
using System.Collections.Generic;
using System.Text;
using CoffeeMachine.Models;
using System.Linq;

namespace CoffeeMachine.Repository
{
    public interface IIngredientRepository
    {
        List<Ingredient> GetAllIngredients();
        bool AllocateIngredient(string ingredientName, int quantity);
        int GetAvailableQuantity(string ingredientName);
        bool RefillIngredient(string ingredientName, int quantity);
    }
    public class IngredientRepository : IIngredientRepository
    {
        private readonly Dictionary<string, Ingredient> ingredientsMap ;

        public static IngredientRepository Create(List<Ingredient> ingredients)
        {
            return new IngredientRepository(ingredients);
        }

        public IngredientRepository(List<Ingredient> ingredients)
        {
            this.ingredientsMap = ingredients?.ToDictionary(x => x.Name, x => x) ?? new Dictionary<string, Ingredient>();
        }
        public bool AllocateIngredient(string ingredientName, int quantity)
        {
            if (!this.ingredientsMap.TryGetValue(ingredientName, out Ingredient ingredient))
            {
                Console.WriteLine($"Unknown Ingredient - {ingredientName}");
                return false;
            }
            return ingredient.Allocate(quantity);
        }

        public List<Ingredient> GetAllIngredients()
        {
            return this.ingredientsMap.Values.ToList();
        }

        public int GetAvailableQuantity(string ingredientName)
        {
            if (!this.ingredientsMap.TryGetValue(ingredientName, out Ingredient ingredient))
            {
                Console.WriteLine($"Unknown Ingredient - {ingredientName}");
                return -1;
            }
            return ingredient.GetQuantity();
        }

        public bool RefillIngredient(string ingredientName, int quantity)
        {
            if(!this.ingredientsMap.TryGetValue(ingredientName, out Ingredient ingredient))
            {
                Console.WriteLine($"Unknown Ingredient - {ingredientName}");
                return false;
            }

            return ingredient.Refill(quantity);
        }
    }
}
