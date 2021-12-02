
using CoffeeMachine.Strategies;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CoffeeMachine.Models
{
    public class Ingredient
    {
        private readonly SemaphoreSlim quantityLock;
        private readonly IRefillStrategy refillStrategy;
        public string Name { get; }

        private int quantity;
        //public Indicator Indicator { get; set; }
        public Ingredient(string name, int quantity, IRefillStrategy refillStrategy)
        {
            this.Name = name;
            this.quantity = quantity;
            this.quantityLock = new SemaphoreSlim(1, 1);
            this.refillStrategy = refillStrategy;
        }

        public static Ingredient Create(string name, int quantity, IRefillStrategy refillStrategy)
        {
            return new Ingredient(name, quantity, refillStrategy);
        }

        public int GetQuantity()
        {
            int currentQuantity = -1;
            try
            {
                quantityLock.Wait();
                currentQuantity = quantity;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while getting {0} in Ingredient class : {1}", Name, ex);

            }
            finally
            {
                quantityLock.Release();
            }

            return currentQuantity;

        }

        public bool Refill(int refillQuantity)
        {
            bool isRefilled = false;
            try
            {
                quantityLock.Wait();
                quantity = refillStrategy.Refill(quantity, refillQuantity);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while refilling {0} in Ingredient class : {1}", Name, ex);

            }
            finally
            {
                quantityLock.Release();
            }

            return isRefilled;
        }

        public bool Allocate(int quantityRequired)
        {
            bool isAllocated = false;
            try
            {
                quantityLock.Wait();
                if(quantity >= quantityRequired)
                {
                    quantity -= quantityRequired;
                    isAllocated = true;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error while allocating {0} in Ingredient class : {1}",Name,ex);

            }
            finally
            {
                quantityLock.Release();
            }

            return isAllocated;
        }

        public bool IsRefillRequired()
        {
            bool isRefillRequired = false ;
            try
            {
                quantityLock.Wait();
                isRefillRequired = refillStrategy.IsRefillRequired(quantity);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error while allocating {0} in Ingredient class : {1}", Name, ex);
            }
            finally
            {
                quantityLock.Release();
            }
            return isRefillRequired;
        }
    }
}