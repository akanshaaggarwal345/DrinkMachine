# DrinkMachine
#It will Read all the test cases in The Folder TestCases and log the output in the form if Drink was prepared or not.
#StartMachine is the EntryPoint 
#IngredientRepository takes care of all updations and allocation of Ingredients
#RefillStrategy is used to understand when a refill is required
#Deserializer helps in converting the input json to required machine parameters
#Semaphores are used in prepareDrink method to ensure only n(outlets) drinks are prepared.
#Semaphores are also used on the ingredient level to ensure currect updations while parallel execution
#In main method parallel for loop is used to prepare all n beverages at the same time. 
