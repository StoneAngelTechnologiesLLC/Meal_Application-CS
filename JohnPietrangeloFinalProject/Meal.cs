//John Pietrangelo Tues & Thurs 9am
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohnPietrangeloFinalProject
{
    class Meal
    { 
        // Class Variables
        private string mealTypeBreakfast;
        private string mealTypeLunch;
        private string mealTypeDinner;
        private double mealPriceBreakfast;
        private double mealPriceLunch;
        private double mealPriceDinner;
        private uint mealQuantityBreakfast;
        private uint mealQuantityLunch;
        private uint mealQuantityDinner;
        private double subTotal;
        private double totalPrice;

        // Defulat Constructor for Meal objects.
        public Meal(){}

        //Class Methods to get and set class variables
        public void SetMealTypeBreakfast (string mealType)
        {
            mealTypeBreakfast = mealType;
        }
        public string GetMealTypeBreakfast()
        {
            return mealTypeBreakfast;
        }

        public void SetMealPriceBreakfast(double mealPrice)
        {
            mealPriceBreakfast = mealPrice;
        }
        public double GetMealPriceBreakfast()
        {
            return mealPriceBreakfast;
        }

        public void SetMealQuantityBreakfast(uint mealQuantity)
        {
            mealQuantityBreakfast = mealQuantity;
        }
        public uint GetMealQuantityBreakfast()
        {
            return mealQuantityBreakfast;
        }

        public void SetMealTypeLunch(string mealType)
        {
            mealTypeLunch = mealType;
        }
        public string GetMealTypeLunch()
        {
            return mealTypeLunch;
        }

        public void SetMealPriceLunch(double mealPrice)
        {
            mealPriceLunch = mealPrice;
        }
        public double GetMealPriceLunch()
        {
            return mealPriceLunch;
        }

        public void SetMealQuantityLunch(uint mealQuantity)
        {
            mealQuantityLunch = mealQuantity;
        }
        public uint GetMealQuantityLunch()
        {
            return mealQuantityLunch;
        }

        public void SetMealTypeDinner(string mealType)
        {
            mealTypeDinner = mealType;
        }
        public string GetMealTypeDinner()
        {
            return mealTypeDinner;
        }

        public void SetMealPriceDinner(double mealPrice)
        {
            mealPriceDinner = mealPrice;
        }
        public double GetMealPriceDinner()
        {
            return mealPriceDinner;
        }

        public void SetMealQuantityDinner(uint mealQuantity)
        {
            mealQuantityDinner = mealQuantity;
        }
        public uint GetMealQuantityDinner()
        {
            return mealQuantityDinner;
        }
        
        // Class Methods to validate input, and Display meal data.
        public double SetTotalPrice(double subTotal)
        {
            double tax;
            const double rate = .10;
            tax = subTotal * rate;
            totalPrice = subTotal + tax;
            return tax;
        }
        public double GetTotalPrice()
        {
            return totalPrice;
        }
        public static void DisplayMainMenu(uint breakfast, uint lunch, uint dinner)
        {  
            // Simple display of Main Menu.
            Console.Clear();
            Console.WriteLine("\n\t\t\t      <<<<<Main Menu>>>>>\n\n" +
                              "--------------------------------------------------------------------------------");
            Console.WriteLine("\t       Please select meal type & number of meal(s) desired.\n\n\n");
            Console.WriteLine("{0,20}{1,22}{2,28}", "Meal Type", "Price", "Meals Remaining");
            Console.WriteLine("{0,20}{1,22}{2,29}", "---------", "-----", "---------------\n");
            Console.WriteLine("{0,12}{1}{2,21:c}{3,20}\n", "1. ", "Breakfast", 6.25, breakfast);
            Console.WriteLine("{0,12}{1}{2,25:c}{3,20}\n", "2. ", "Lunch", 7.95, lunch);
            Console.WriteLine("{0,12}{1}{2,24:c}{3,20}\n", "3. ", "Dinner", 9.80, dinner);
            Console.WriteLine("{0,12}{1}{2,25}{3,24}", "4. ", "Exit", "N/A", "N/A\n\n");
        }     
        public static uint ValidateQuantiyChosen(uint availableQuantity)
        {
            uint chosenQuantity;
            bool repeat = true;
            int i = 0;
            do
            {
               
                while (i == 0)
                {
                    Console.Write("\n{0,48}", "--> Enter meal quantity: ");
                    i++;
                }

                if (i > 1)
                {
                    Console.Write("\n{0,51}", "--> Re-enter meal quantity: ");   
                }
                
                string temp = Console.ReadLine();

                // Bool statement to validate selection. Statement returns false & 0, if input is not a number.    
                repeat = uint.TryParse(temp, out chosenQuantity);

                    // To validate input is a number.
                    if (repeat == false)
                    {
                        Console.WriteLine("-------------------------------------------------------------------------------");
                        Console.WriteLine("\n{0,68}", "*** Invalid Entery. Please Enter Quantity In Integer Form. ***");
                        repeat = !repeat;
                    }

                    // If bool is true(input was a number) but input is actually a hard 0.
                    else if (chosenQuantity == 0)
                    {
                        Console.WriteLine("-------------------------------------------------------------------------------");
                        Console.WriteLine("\n{0,68}", "  *** Invalid Entery. Please Enter Number Greater Than Zero. ***");
                    }

                    // if input amount is graeter than meals available.
                    else if (chosenQuantity > availableQuantity)
                    {
                        Console.WriteLine("-------------------------------------------------------------------------------");
                        Console.WriteLine("\n{0,68}", "*** Insufficant number of meals available for request. ***");
                        Console.WriteLine("\n\t\t\t** There are {0} meals available. **", availableQuantity);
                    }

                    // Retuns input amount if input is a valid quantity.
                    else
                    {
                        repeat = !repeat;
                        return chosenQuantity;
                    }
                i++;
            } while (true);
        }
        public double DisplaySubTotal(uint quantity, double price)
        {
            // Simple display of purchase sub-total.
            subTotal = price * quantity;
            Console.WriteLine("\n<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            Console.WriteLine("\n\t*** You have selected {0} meal(s). Your subtotal is: {1:c} ***\n", quantity, subTotal);
            Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            return subTotal;
        }
    }
}

