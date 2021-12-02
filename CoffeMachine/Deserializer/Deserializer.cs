using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using CoffeeMachine.Models;

namespace CoffeeMachine.Deserializer
{
    public static class Deserializer
    {
        public static MachineParams Deserialize(string filePath)
        {
            string json = File.ReadAllText(filePath);
            JObject obj = JObject.Parse(json);
            JObject machineObject = obj.Value<JObject>("machine");
            return GetParameters(machineObject);

        }

        private static MachineParams GetParameters(JObject machineObject)
        {
            int outlets = GetOutlets(machineObject);
            Dictionary<string, int> ingredients = GetIngredients(machineObject);
            List<Drink> drinks = GetDrinks(machineObject);

            return new MachineParams(outlets, drinks, ingredients);
        }

        private static List<Drink> GetDrinks(JObject machineObject)
        {
            var beveragesJObject = machineObject.Value<JObject>("beverages");
            var drinks = new List<Drink>();
            foreach (var prop in beveragesJObject.Properties())
            {
                Dictionary<string, int> recipie = beveragesJObject.Value<JObject>(prop.Name).ToObject<Dictionary<string, int>>();
                var drink = new Drink(prop.Name, recipie);
                drinks.Add(drink);
            }

            return drinks;
        }

        private static Dictionary<string, int> GetIngredients(JObject machineObject)
        {
            Dictionary<string, int> ingredients = machineObject.Value<JObject>("total_items_quantity").ToObject<Dictionary<string, int>>();
            return ingredients;
        }

        private static int GetOutlets(JObject machineObject)
        {
            JObject machineOutlets = machineObject.Value<JObject>("outlets");
            return machineOutlets.Value<int>("count_n");
        }
    }
}
