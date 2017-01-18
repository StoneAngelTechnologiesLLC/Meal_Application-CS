//John Pietrangelo Tues & Thurs 9am
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohnPietrangeloFinalProject
{
    class Customer
    {
        // Class Variables
        private string stuFirstName;
        private string stuLastName;
        private string stuCollegeName;
        private string stuState;
        private double stuGpa;
        private uint stuGpaMealCredit;
        private uint stuCode;
        private uint stuStandardMealCredit;
        private double stuCardBalance;

        //Overloaded Constructor
        public Customer(string firstName, string lastName, string collegeName, string state, double gpa,uint mealCredit, uint code, uint credit, double balance)
        {
        stuFirstName = firstName;
        stuLastName = lastName;
        stuCollegeName = collegeName;
        stuState = state;
        stuGpa = gpa;
        stuGpaMealCredit = mealCredit;
        stuCode = code;
        stuStandardMealCredit = credit;
        stuCardBalance = balance;
        }
        
       //Class Methods to get and set class variables
        public string GetStuFirstName()
        {
            return stuFirstName;
        }
        public string GetStuLastName()
        {
            return stuLastName;
        }

        public void SetStuGpaMealCredit(uint mealCredit)
        {
            stuGpaMealCredit = mealCredit;
        }
        public uint GetStuGpaMealCredit()
        {
            return stuGpaMealCredit;
        }
        
        public void SetStuNonGpaMealCredit(uint credit)
        {
            stuStandardMealCredit = credit;
        }
        public uint GetStuNonGpaMealCredit()
        {
            return stuStandardMealCredit;
        }
        
        public void SetStuCardBalance(double balance)
        {
            stuCardBalance = balance;
        }
        public double GetStuCardBalance()
        {
            return stuCardBalance;
        }

        // Class Methods to Create, Edit and Save Student Data
        public static uint ValidateStudent()
        {
            // Local Variables.
            bool repeat = true;
            string studentInitals = "";
            uint code;
            string temp;

            // Requests input from Customer (actor).
            Console.WriteLine("\n{0,60}", "Press 'Enter' to continue If you are a guest.");
            Console.Write("\n{0,65}", "--> If you have a student account, Please Enter your initials: ");
            
            // Converts input to Upper case.
            studentInitals = Console.ReadLine().ToUpper();

            // Converts input string to char array.
            char[] tempInitals = studentInitals.ToArray();
           
            // If array lenght is more or less than 2.
            if (tempInitals.Length < 2 || tempInitals.Length > 2)
            {
                Console.WriteLine("-------------------------------------------------------------------------------");
                Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                Console.WriteLine("{0,50}","** No Match Found **");
                Console.WriteLine("{0,60}", "*** Routing to Guest Payment Options ***\n");
                Console.WriteLine("{0,55}", "--> Press Any Key To Continue ");
                Console.ReadKey();

                // Returns guest object.
                return 0;
            }
            // If initals are JP.
            else if (tempInitals[0] == 'J' && tempInitals[1] == 'P')
            {
                // Requests input from Customer (actor).
                Console.Write("\n{0,65}", "--> Please Enter Student Code Number: ");
                
                // Variable stores input. 
                temp = Console.ReadLine();

                // Convert string to uint.
                repeat = uint.TryParse(temp, out code);

                if (code == 33)
                {
                    // Returns John Pietrangelo object.
                    return 1;
                }
            }
            else if (tempInitals[0] == 'C' && tempInitals[1] == 'K')
            {
                // Requests input from Customer (actor).
                Console.Write("\n{0,65}", "--> Please Enter Student Code Number: ");

                // Variable stores input. 
                temp = Console.ReadLine();

                // Convert string to uint.
                repeat = uint.TryParse(temp, out code);

                if (code == 44)
                {
                    // Returns Clark Kent object.
                    return 2;
                }
            }
            else if (tempInitals[0] == 'B' && tempInitals[1] == 'P')
            {
                // Requests input from Customer (actor).
                Console.Write("\n{0,65}", "--> Please Enter Student Code Number: ");

                // Variable stores input. 
                temp = Console.ReadLine();

                // Convert string to uint.
                repeat = uint.TryParse(temp, out code);

                if (code == 55)
                {
                    // Returns Brad Pitt customer object.
                    return 3;
                }
            }
            else
            
                Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                Console.WriteLine("{0,50}", "** No Match Found **");
                Console.WriteLine("{0,60}", "*** Routing to Guest Payment Options ***\n");
                Console.WriteLine("{0,55}", "--> Press Any Key To Continue ");
                Console.ReadKey();

                // Returns guest object.
                return 0;
        }
        public static uint DisplayPaymentOptions(string customerFirstName, string customerLastName)
        {  
            // Used to validate selection input (string) is a number.
            bool repeat = true;
            
            // Variable to hold menu selection.
            uint payMethod;
            {
                // Displays Menu.
                Console.Clear();
                Console.WriteLine("\n{0,55}", "*** Student Payment Options ***\n");
                Console.WriteLine("{2,36} {0} {1}", customerFirstName, customerLastName, "Student:");
                Console.WriteLine("-------------------------------------------------------------------------------\n");
                Console.WriteLine("\n{0,50}", "1. Use Meal Plan Credits");
                Console.WriteLine("\n{0,45}", "2. Use Card Payment");
                Console.WriteLine("\n{0,63}", "3. View Plan Credits and Card Balance");
                Console.WriteLine("\n{0,35}", "4. Cancel");
                Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                Console.Write("\n{0,59}", "--> Please Select Payment Option: ");

                // Customer input.
                string temp = Console.ReadLine();

                // Convert string to uint.
                repeat = uint.TryParse(temp, out payMethod);

                // Valid selection options.
                if (payMethod == 1 || payMethod == 2 || payMethod == 3 || payMethod == 4)
                {
                    // returns selection.
                    return payMethod;
                }
                // If string is not a number.
                else if (repeat = !repeat)
                {
                    Console.WriteLine("\n{0,58}", "*** Invalid Entery. Please Try Again. ***");
                    Console.ReadKey();
                    repeat = !repeat;
                    return 0;
                }
                // If input is a number that is not 1 through 4.
                else
                {
                    Console.WriteLine("\n{0,58}", "*** Invalid Entery. Please Try Again. ***");
                    Console.ReadKey();
                    return 0;
                }
            }
        }
        public static uint VarifyMealCredits(string customerFirstName, string customerLastName, uint mealCredit, uint gpaMealCredit, uint mealQuantitySelected, double purPrice, double purTax, double totalPrice, double cashCredits, string mealType, uint curMealCount)
        {
            //  Local Variables
            uint curMealCredits;
            uint remainingMeals;
            uint mealsOver;

            // Display screen if customer (object) has insufficient meal credits.
            if (mealQuantitySelected > (mealCredit + gpaMealCredit))
            {
                mealsOver = mealQuantitySelected - (mealCredit + gpaMealCredit);
                Console.Clear();
                Console.WriteLine("\n{0,55}", "*** Student Meal Credits ***\n");
                Console.WriteLine("{2,38} {0} {1}", customerFirstName, customerLastName, "Student:");
                Console.WriteLine("-------------------------------------------------------------------------------\n");
                Console.WriteLine("\n{0,54}", "Insufficient meal Credits! ");
                Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                Console.WriteLine("\n{0}", "Your Current Credits:");
                Console.WriteLine("{0}", "--------------------");
                Console.WriteLine("\n{0,65}{1,8}", "Student Academic Achievement Meal Credit: ", gpaMealCredit);
                Console.WriteLine("\n{0,45}{1,28}", "Standard Meal Credit: ", mealCredit);
                Console.WriteLine("\n{0,71}{1,2}", "Number of meals over what you have credits for: ", mealsOver);
                Console.Write("\n{0,55}", "--> Press any key to continue.");
                Console.ReadKey();

                // returns no change in mealCredit value.
                return mealCredit;
            }
                // Display screen if customer (object) has sufficient credits for purchase. 
            else if ((gpaMealCredit + mealCredit) >= mealQuantitySelected)
            {   
                // Deducts meal quantity selected from current meals available for display screen.
                remainingMeals = curMealCount - mealQuantitySelected;

                // Deducts meal quantity selected from customer meal credits. 
                curMealCredits = (mealCredit + gpaMealCredit) - mealQuantitySelected;
                Console.Clear();
                Console.WriteLine("\n{0,55}", "*** Purchase Reciept ***");
               
                Console.WriteLine("--------------------------------------------------------------------------------");
                Console.WriteLine("{2,40} {0} {1}", customerFirstName, customerLastName, "Student:");

                // Message given if customer (object) has GPA meal credit.
                if (gpaMealCredit > 0)
                {
                    Console.WriteLine("\n * Your SAA Meal Credit Was Used To Cover The Cost Of One Meal In This Purchase");
                }

                Console.WriteLine("--------------------------------------------------------------------------------");
                Console.WriteLine("{0,44}{1} ***","*** Mealtype: ",mealType);
                Console.WriteLine("\n{0,28}{1,5}{2,26}{3,9:C}", "Quantity: ", mealQuantitySelected, "\t\tPrevious Card Credit: ", cashCredits);
                Console.WriteLine("\n{0,28}{1,5}{2,17}{3,20:C}", "Your Previous Meal Credits: ", mealCredit, "\t\tSub Total: ", purPrice);
                Console.WriteLine("\n{0,29}{1,4}{2,17}{3,18:C}", mealType + " Meals Availabile:  ", remainingMeals, "\t\tGrand Total: ", purPrice);
                Console.WriteLine("\n{0,27}{1,6}{2,23}{3,10:C}", "Your Current Meal Credits:", curMealCredits, "\t\tCurrent Card Credit: ", cashCredits);
                Console.WriteLine("\n--------------------------------------------------------------------------------");
                Console.WriteLine("--------------------------------------------------------------------------------");
                Console.WriteLine("\n{0,55}", "--> Press any Key To Continue");
                Console.ReadKey();

                // To adjust meal credit return if Customer (object) has GPA meal credit.
                if (gpaMealCredit > 0)
                {
                    mealQuantitySelected--;
                }

                mealCredit = mealCredit - mealQuantitySelected;

                // Returns revised customer (object) standard mealcredits.
                return mealCredit;
            }
            return mealCredit;
        }
        public static double VarifyCardPayment(string customerFirstName, string customerLastName, uint mealCredit, uint gpaMealCredit, uint mealQuantitySelected, double purPrice, double purTax, double totalPrice, double cashCredits, string mealType, uint curMealCount, double singleMealPrice)
        {
            double curBalance;
            uint remainingMeals;
            double lowBalance;
            
            // Display screen if customer (object) has insufficient cash credit on card.
            if (totalPrice > (cashCredits + (gpaMealCredit * singleMealPrice)))
            {
                lowBalance = (mealQuantitySelected * ((singleMealPrice * .1) + singleMealPrice)) - cashCredits;
                Console.Clear();
                Console.WriteLine("\n{0,55}", "*** Student Meal Credits ***\n");
                Console.WriteLine("{2,38} {0} {1}", customerFirstName, customerLastName, "Student:");
                Console.WriteLine("-------------------------------------------------------------------------------\n");
                Console.WriteLine("{0,54}", " *** Insufficient meal Credits! *** ");
                Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                Console.WriteLine("\n{0}", "Your Current Credits:");
                Console.WriteLine("{0}", "--------------------");
                Console.WriteLine("\n{0,65}{1,8}", "Student Academic Achievement Meal Credit: ", gpaMealCredit);
                Console.WriteLine("\n{0,65}{1,8}", "Standard Meal Credit: ", mealCredit);
                Console.WriteLine("\n{0,65}{1,8:C}", "Card Balance: ", cashCredits);
                Console.WriteLine("\n{0,65}{1,8:C}", "Purchase Grand Total: ", totalPrice);
                Console.WriteLine("\n-------------------------------------------------------------------------------");
                Console.WriteLine("\t>>> Your Card's Balance is {0:C} credits under Purchase Price <<<", lowBalance);
                Console.WriteLine("-------------------------------------------------------------------------------");
                Console.Write("\n{0,55}", "--> Press any key to continue.");
                Console.ReadKey();

                // returns no change in card credit.
                return cashCredits;
            }
            // Display screen if customer (object) has sufficient credits for purchase.
            else
            {
                // Deducts meal quantity selected from current meals available for display screen.
                remainingMeals = curMealCount - mealQuantitySelected;
                Console.Clear();
                Console.WriteLine("\n{0,55}", "*** Purchase Reciept ***");
                Console.WriteLine("--------------------------------------------------------------------------------");
                Console.WriteLine("{2,40} {0} {1}", customerFirstName, customerLastName, "Student:");
                
                // Message given if customer (object) has GPA meal credit.
                if (gpaMealCredit > 0)
                {
                    Console.WriteLine("\n* Your SAA Meal Credit Was Used To Cover The Cost Of One Meal In This Purchase");
                }

                Console.WriteLine("--------------------------------------------------------------------------------");
                Console.WriteLine("{0,45}{1} ***", "*** Mealtype: ", mealType);
                Console.WriteLine("\n{0,28}{1,5}{2,10}{3,9:C}", "Quantity: ", mealQuantitySelected, "\t\tPrevious Card Credit: ", cashCredits);

                // To adjust variables for display purposes.
                if (gpaMealCredit > 0)
                {
                    mealQuantitySelected--;
                    mealCredit++;  
                }

                Console.WriteLine("\n{0,28}{1,5}{2,5}{3,20:C}", "Your Previous Meal Credits: ", mealCredit, "\t\tSub Total: ", purPrice);
                Console.WriteLine("\n{0,29}{1,4}{2,10}{3,21:C}", mealType + " Meals Availabile:  ", remainingMeals, "\t\tSales-tax:", purTax);
                
                // To re adjust variable for display purposes.
                if (gpaMealCredit > 0)
                {
                    mealCredit--;
                }
                
                Console.WriteLine("\n{0,27}{1,6}{2,5}{3,18:C}", "Your Current Meal Credits:", mealCredit, "\t\tGrand Total: ", totalPrice);
                curBalance = cashCredits - (mealQuantitySelected * ((singleMealPrice * .10) + singleMealPrice));
                Console.WriteLine("\n{0,60}{1,10:C}", "\t\tCurrent Card Credit: ", curBalance);
                Console.WriteLine("--------------------------------------------------------------------------------");
                
                // Message given if customer (object) has GPA meal credit.
                if (gpaMealCredit > 0)
                {
                    Console.WriteLine("* Current Card Balance Reflects the {0:C} SAA Meal Credit Used In This Purchase", ((singleMealPrice * .10) + singleMealPrice));
                }

                Console.WriteLine("\n--------------------------------------------------------------------------------");
                Console.WriteLine("\n{0,55}", "--> Press any Key To Continue");
                Console.ReadKey();
                // Returns revised customer (object) Card Credits.
                return curBalance;
            }

        }
        public static void DisplayPlanCreditsAndCardBalance(string customerFirstName, string customerLastName, uint mealCredit,uint gpaMealCredit, double balanceCash)
        {  // Simplistic dispaly of current Customer (object) credit Balances.
            Console.Clear();
            Console.WriteLine("\n{0,55}", "*** Student Balances ***\n");
            Console.WriteLine("{2,40} {0} {1}", customerFirstName, customerLastName, "Student:");
            Console.WriteLine("-------------------------------------------------------------------------------\n");
            Console.WriteLine("\n{0,54}{1,9}", "Student Academic Achievement Meal Credit: ",gpaMealCredit);
            Console.WriteLine("\n{0,54}{1,9}", "Standard Meal Credit: ",mealCredit );
            Console.WriteLine("\n{0,54}{1,9:C}", "Cash Credit Balance: ", balanceCash);
            Console.WriteLine("\n-------------------------------------------------------------------------------");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("\n{0,70}", "* Each 'Meal Credit' covers the full cost of any ONE meal.");
            Console.WriteLine("\n{0,71}","* 'Meal Credit' accepted for ANY of the three meal-catagories.");
            Console.WriteLine("\n{0,65}", "--> Press any Key to return to Payment Option menu.");
            Console.WriteLine("\n-------------------------------------------------------------------------------");
            Console.ReadKey();
        }
    }
}
    

