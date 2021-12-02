using System;
using System.Linq;
using System.Collections.Generic;
using CoffeeMachine.Repository;
using System.Threading;

namespace CoffeeMachine.Models
{
    public interface IDrinkMachine
    {
        bool PrepareDrink(Drink drink);
        void RefillIngredient(Ingredient ingredient, int quantity);
    }

    public class BeveragesMachine : IDrinkMachine
    {
        public int Outlets { get; set; }
        public  List<Drink> Beverages { get; }
        private Dictionary<string, int> ingredients { get; set; }
        private readonly SemaphoreSlim outletLock;

        private readonly IIngredientRepository ingredientRepository;

        public BeveragesMachine(int outlets, IIngredientRepository ingredientRepository)
        {
            Outlets = outlets;
            this.ingredientRepository = ingredientRepository;
            this.outletLock = new SemaphoreSlim(outlets, outlets);
        }

        public static IDrinkMachine Create(int outlets, IIngredientRepository ingredientRepository)
        {
            return new BeveragesMachine(outlets, ingredientRepository);
        }

        public bool PrepareDrink(Drink drink)
        {
            Console.WriteLine(OutputMessages.Preparing(drink.DrinkName));

            bool canPrepare = false;
            try
            {
                outletLock.Wait();
                canPrepare = true;
                
                foreach (var ingredient in drink.Recipie)
                {
                    if(!ingredientRepository.AllocateIngredient(ingredient.Key, ingredient.Value))
                    {
                        canPrepare = false;
                        Console.WriteLine(OutputMessages.Failure(drink.DrinkName, ingredient.Key));
                        break;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error while preparing drink {drink.DrinkName} : {ex}");
                canPrepare = false;
            }

            finally
            {
                outletLock.Release();
            }

            if (canPrepare)
            {
                Console.WriteLine(OutputMessages.Success(drink.DrinkName));
            }
            else
            {
                Console.WriteLine($"Failed to prepare Drink - {drink.DrinkName}");
            }

            return canPrepare;
        }

        public void RefillIngredient(Ingredient ingredient, int quantity)
        {
            return;
        }
    }

}
