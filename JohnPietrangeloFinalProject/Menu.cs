//John Pietrangelo Tues & Thurs 9am
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohnPietrangeloFinalProject
{
    class Menu
    {
        public static void Main(string[] args)
        {  // Local Variables
            bool repeat = true;
            string[] mealTypes = { "Breakfast", "Lunch", "Dinner" };
            double[] mealPrices = { 6.25, 7.95, 9.80 };
            uint[] mealQuantities = { 10, 20, 35 };
            string input;
            uint chosenQuantity;
            double purchaseSubTotal;
            uint varifiedStudent;
            uint payMethod;
            uint revisedMealCredit;
            double revisedCardAmount;
            double tax;
            double quantityPurchased;
            uint quantityPurchasedWholeNum;

            // To create an array of 4 Customer objects(1 guest, 3 students) with overLoaded constructors.
            Customer[] students = new Customer[4];
            students[0] = new Customer("System", "Guest", "", "", 0, 0, 0, 0, 0);
            students[1] = new Customer("John","Pietrangelo","ASU","Arizona", 3.5, 1, 33, 5, 30.99);
            students[2] = new Customer("Clark", "Kent", "OSU", "Oregan", 3.0, 0, 44, 1, 1289.62);
            students[3] = new Customer("Brad", "Pitt", "SDSU", "California", 3.49, 0, 55, 100, 4.25);

            // To create Meal object with defualt constructor.
            Meal meal = new Meal();
            
            /* To set each meal type with "meal quantity variable" for meal count display of Main menu.
                  In anticipation for accurate count for future customer. */
            meal.SetMealQuantityBreakfast(mealQuantities[0]);
            meal.SetMealQuantityLunch(mealQuantities[1]);
            meal.SetMealQuantityDinner(mealQuantities[2]);

            do 
            {
                // To display Main menu
                Meal.DisplayMainMenu(meal.GetMealQuantityBreakfast(), meal.GetMealQuantityLunch(),meal.GetMealQuantityDinner());
                
                // To request meal type from customer(actor).
                Console.Write("{0,48}", "--> Enter meal type: ");
                
                // To retrive input from customer(actor).
                input = Console.ReadLine();

                // Code path if "Breakfast" is selected by customer(actor).
                if (input == "1")
                {   
                    // To set the name and price of Meal object.
                    meal.SetMealTypeBreakfast(mealTypes[0]);
                    meal.SetMealPriceBreakfast(mealPrices[0]);
                    
                    /* To call a method that will guarantee the system will only accept a selected meal quantity 
                       of at least 1 meal and no more than the available amount set in the system memory. */
                    chosenQuantity = Meal.ValidateQuantiyChosen(meal.GetMealQuantityBreakfast());

                    // To call a method that calculates and displays the chosen Meal object's quantity and corrosponding subtotal sale price.
                    purchaseSubTotal = meal.DisplaySubTotal(chosenQuantity, meal.GetMealPriceBreakfast());
                    
                    // To call a method that calculates the sale price of Meal object's, including tax.
                    tax = meal.SetTotalPrice(purchaseSubTotal);
                    
                    // To call a method that assigns customer(actor) with Customer object.
                    varifiedStudent = Customer.ValidateStudent();
                    
                    // To loop Display Menu until turned off.
                    bool repeat1 = true;
                    while (repeat1)
                    {   
                        // Selected Customer Object
                        switch (varifiedStudent)
                        {
                            // Selected Customer Object: John Pietrangelo.
                            case 1:
                                {
                                    //Method to display Payment Options, returns seletion.
                                    payMethod = Customer.DisplayPaymentOptions(students[1].GetStuFirstName(), students[1].GetStuLastName());
                                    
                                    // Returned selection.
                                    switch (payMethod)
                                    {
                                        //case 0 = Invaled input.
                                        case 0:
                                            {
                                                //Repeats Payment Method menu.
                                                break;
                                            }
                                        // case 1 = meal program selected.
                                        case 1:
                                            
                                            {
                                                // Method returns the remaining meal credits of Customer (object).
                                                revisedMealCredit = Customer.VarifyMealCredits(students[1].GetStuFirstName(), students[1].GetStuLastName(), students[1].GetStuNonGpaMealCredit(), students[1].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[1].GetStuCardBalance(), meal.GetMealTypeBreakfast(), meal.GetMealQuantityBreakfast());
                                                
                                                // If no change takes place. (Insufficent credits).
                                                if (revisedMealCredit == (students[1].GetStuNonGpaMealCredit() + students[1].GetStuGpaMealCredit()))
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If student has Adequate credits.
                                                if (students[1].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[1].GetStuNonGpaMealCredit() + students[1].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[1].SetStuGpaMealCredit(0);
                                                    
                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityBreakfast(meal.GetMealQuantityBreakfast()-1);
                                                }
                                                // Saves the reduction of meal count available to account for purchased meal(s). 
                                                meal.SetMealQuantityBreakfast(meal.GetMealQuantityBreakfast() - (students[1].GetStuNonGpaMealCredit() - revisedMealCredit));
                                                
                                                // Saves the reduction of credits to the Customer's "standard meal" credits.
                                                students[1].SetStuNonGpaMealCredit(revisedMealCredit);
                                                
                                                //Returns focus to Main Menu
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        //case 2 = credit card selected.
                                        case 2:
                                            {
                                                // Method returns remaining balance of Customer (object) card balance.
                                                revisedCardAmount = Customer.VarifyCardPayment(students[1].GetStuFirstName(), students[1].GetStuLastName(), students[1].GetStuNonGpaMealCredit(), students[1].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[1].GetStuCardBalance(), meal.GetMealTypeBreakfast(), meal.GetMealQuantityBreakfast(),mealPrices[0]);
                                                
                                                // If no change takes place. (Insufficent funds).
                                                if (revisedCardAmount == students[1].GetStuCardBalance() && students[1].GetStuGpaMealCredit() < 1)
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If customer (object) has sufficient funds.
                                                if (students[1].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[1].GetStuNonGpaMealCredit() + students[1].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit. 
                                                    students[1].SetStuGpaMealCredit(0);
                                                    
                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityBreakfast(meal.GetMealQuantityBreakfast() - 1);
                                                }
                                                // Variable to hold price of purchase divided by unit cost to store quantity of units purchased.
                                                quantityPurchased = ((students[1].GetStuCardBalance() - revisedCardAmount)/(mealPrices[0] + (mealPrices[0] * .1)));
                                                
                                                // Variable to hold units purchase in integer (whole number) form.
                                                quantityPurchasedWholeNum = (uint)Math.Ceiling(quantityPurchased);
                                                
                                                // Saves the reduction of meal count available, to account for purchased meal(s).
                                                meal.SetMealQuantityBreakfast(meal.GetMealQuantityBreakfast() - quantityPurchasedWholeNum);

                                                // Saves the reduction of cash credits to the Customer (object) cardBalance.
                                                students[1].SetStuCardBalance(revisedCardAmount);

                                                // Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        // case 3 = Display Balances.
                                        case 3:
                                            {
                                                // Method to display GPA meal credit, Standard meal credit & Card balance of Customer (object).
                                                Customer.DisplayPlanCreditsAndCardBalance(students[1].GetStuFirstName(), students[1].GetStuLastName(), students[1].GetStuNonGpaMealCredit(), students[1].GetStuGpaMealCredit(), students[1].GetStuCardBalance());
                                                break;
                                            }
                                        // default = Exit selected.
                                        default:
                                            {
                                                // Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                    }
                                    break;
                                }
                            // Selected Customer Object: Clark Kent.
                            case 2:
                                {
                                    //Method to display Payment Options, returns seletion.
                                    payMethod = Customer.DisplayPaymentOptions(students[2].GetStuFirstName(), students[2].GetStuLastName());
                                    
                                    // Returned selection.
                                    switch (payMethod)
                                    {
                                        // case 0 = Invalid input.
                                        case 0:
                                            {
                                                //Repeats Payment Method menu.
                                                break;
                                            }
                                        // case 1 = meal program selected.
                                        case 1:
                                            {
                                                // Method returns the remaining meal credits.
                                                revisedMealCredit = Customer.VarifyMealCredits(students[2].GetStuFirstName(), students[2].GetStuLastName(), students[2].GetStuNonGpaMealCredit(), students[2].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[2].GetStuCardBalance(), meal.GetMealTypeBreakfast(), meal.GetMealQuantityBreakfast());
                                                
                                                // If no change takes place. (Insufficent credits).
                                                if (revisedMealCredit == students[2].GetStuNonGpaMealCredit())
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If student has Adequate credits.
                                                if (students[2].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[2].GetStuNonGpaMealCredit() + students[2].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[2].SetStuGpaMealCredit(0);
                                                    
                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityBreakfast(meal.GetMealQuantityBreakfast() - 1);
                                                }
                                                // Saves the reduction of meal count available to account for purchased meal(s). 
                                                meal.SetMealQuantityBreakfast(meal.GetMealQuantityBreakfast() - (students[2].GetStuNonGpaMealCredit() - revisedMealCredit));
                                                
                                                // Saves the reduction of credits from the standard meal credits.
                                                students[2].SetStuNonGpaMealCredit(revisedMealCredit);
                                                
                                                // Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        //case 2 = credit card selected.
                                        case 2:
                                            {
                                                // Method returns remaining balance of Customer (object) card balance.
                                                revisedCardAmount = Customer.VarifyCardPayment(students[2].GetStuFirstName(), students[2].GetStuLastName(), students[2].GetStuNonGpaMealCredit(), students[2].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[2].GetStuCardBalance(), meal.GetMealTypeBreakfast(), meal.GetMealQuantityBreakfast(),mealPrices[0]);
                                                
                                                // If no change takes place. (Insufficent funds).
                                                if (revisedCardAmount == students[2].GetStuCardBalance())
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If customer (object) has sufficient funds.
                                                if (students[2].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[2].GetStuNonGpaMealCredit() + students[2].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[2].SetStuGpaMealCredit(0);
                                                    
                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityBreakfast(meal.GetMealQuantityBreakfast() - 1);
                                                }
                                                // Variable to hold price of purchase divided by unit cost to store quantity of units purchased.
                                                quantityPurchased = ((students[2].GetStuCardBalance() - revisedCardAmount) / (mealPrices[0] + (mealPrices[0] * .1)));

                                                // Variable to hold units purchase in integer (whole number) form.
                                                quantityPurchasedWholeNum = (uint)Math.Ceiling(quantityPurchased);
                                                
                                                // Saves the reduction of meal count available, to account for purchased meal(s).
                                                meal.SetMealQuantityBreakfast(meal.GetMealQuantityBreakfast() - quantityPurchasedWholeNum);

                                                // Saves the reduction of cash credits to the Customer (object) cardBalance.
                                                students[2].SetStuCardBalance(revisedCardAmount);
                                                
                                                // Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        // case 3 = Display Balances.
                                        case 3:
                                            {
                                                // Method to display GPA meal credit, Standard meal credit & Card balance of Customer (object).
                                                Customer.DisplayPlanCreditsAndCardBalance(students[2].GetStuFirstName(), students[2].GetStuLastName(), students[2].GetStuNonGpaMealCredit(), students[2].GetStuGpaMealCredit(), students[2].GetStuCardBalance());
                                                break;
                                            }
                                        // default = Exit selected.
                                        default:
                                            {
                                                // Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                    }
                                    break;
                                }
                            // Selected Customer Object: Brad Pitt.
                            case 3:
                                {
                                    //Method to display Payment Options, returns seletion.
                                    payMethod = Customer.DisplayPaymentOptions(students[3].GetStuFirstName(), students[3].GetStuLastName());
                                    
                                    // Returned selection.
                                    switch (payMethod)
                                    {
                                        //case 0 = Invaled input.
                                        case 0:
                                            {
                                                //Repeats Payment Method menu.
                                                break;
                                            }
                                        // case 1 = meal program selected.
                                        case 1:
                                            {
                                                // Method returns the remaining meal credits.
                                                revisedMealCredit = Customer.VarifyMealCredits(students[3].GetStuFirstName(), students[3].GetStuLastName(), students[3].GetStuNonGpaMealCredit(), students[3].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[3].GetStuCardBalance(), meal.GetMealTypeBreakfast(), meal.GetMealQuantityBreakfast());
                                                
                                                // If no change takes place. (Insufficent credits).
                                                if (revisedMealCredit == students[3].GetStuNonGpaMealCredit())
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If student has Adequate credits.
                                                if (students[3].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[3].GetStuNonGpaMealCredit() + students[3].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[3].SetStuGpaMealCredit(0);
                                                    
                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityBreakfast(meal.GetMealQuantityBreakfast() - 1);
                                                }
                                                // Saves the reduction of meal count available to account for purchased meal(s). 
                                                meal.SetMealQuantityBreakfast(meal.GetMealQuantityBreakfast() - (students[3].GetStuNonGpaMealCredit() - revisedMealCredit));
                                                
                                                // Saves the reduction of credits from the standard meal credits.
                                                students[3].SetStuNonGpaMealCredit(revisedMealCredit);
                                                
                                                // Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        //case 2 = credit card selected.
                                        case 2:
                                            {
                                                // Method returns remaining balance of Customer (object) card balance.
                                                revisedCardAmount = Customer.VarifyCardPayment(students[3].GetStuFirstName(), students[3].GetStuLastName(), students[3].GetStuNonGpaMealCredit(), students[3].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[3].GetStuCardBalance(), meal.GetMealTypeBreakfast(), meal.GetMealQuantityBreakfast(),mealPrices[0]);
                                                
                                                // If no change takes place. (Insufficent funds).
                                                if (revisedCardAmount == students[3].GetStuCardBalance())
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If customer (object) has sufficient funds.
                                                if (students[3].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[3].GetStuNonGpaMealCredit() + students[3].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[3].SetStuGpaMealCredit(0);
                                                    
                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityBreakfast(meal.GetMealQuantityBreakfast() - 1);
                                                }
                                                // Variable to hold price of purchase divided by unit cost to store quantity of units purchased.
                                                quantityPurchased = ((students[3].GetStuCardBalance() - revisedCardAmount) / (mealPrices[0] + (mealPrices[0] * .1)));

                                                // Variable to hold units purchase in integer (whole number) form.
                                                quantityPurchasedWholeNum = (uint)Math.Ceiling(quantityPurchased);

                                                // Saves the reduction of meal count available, to account for purchased meal(s).
                                                meal.SetMealQuantityBreakfast(meal.GetMealQuantityBreakfast() - quantityPurchasedWholeNum);

                                                // Saves the reduction of cash credits to the Customer (object) cardBalance.
                                                students[3].SetStuCardBalance(revisedCardAmount);

                                                // Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        // case 3 = Display Balances.
                                        case 3:
                                            {
                                                // Method to display GPA meal credit, Standard meal credit & Card balance of Customer (object).
                                                Customer.DisplayPlanCreditsAndCardBalance(students[3].GetStuFirstName(), students[3].GetStuLastName(), students[3].GetStuNonGpaMealCredit(), students[3].GetStuGpaMealCredit(), students[3].GetStuCardBalance());
                                                break;
                                            }
                                        // default = Exit selected.
                                        default:
                                            {
                                                // Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                    }
                                    break;
                                }
                            // Selected Customer Object: System Guest.
                            default:
                                {
                                    //Method to display Payment Options, returns seletion.
                                    payMethod = Customer.DisplayPaymentOptions(students[0].GetStuFirstName(), students[0].GetStuLastName());
                                    
                                    // Returned selection.
                                    switch (payMethod)
                                    {
                                        //case 0 = Invaled input.
                                        case 0:
                                            {
                                                //Repeats Payment Method menu.
                                                break;
                                            }
                                        // case 1 = meal program selected.
                                        case 1:
                                            {
                                                // Method returns the remaining meal credits.
                                                revisedMealCredit = Customer.VarifyMealCredits(students[0].GetStuFirstName(), students[0].GetStuLastName(), students[0].GetStuNonGpaMealCredit(), students[0].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[0].GetStuCardBalance(), meal.GetMealTypeBreakfast(), meal.GetMealQuantityBreakfast());
                                                
                                                // If no change takes place. (Insufficent credits).
                                                if (revisedMealCredit == students[0].GetStuNonGpaMealCredit())
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If student has Adequate credits.
                                                if (students[0].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[0].GetStuNonGpaMealCredit() + students[0].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[0].SetStuGpaMealCredit(0);
                                                    
                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityBreakfast(meal.GetMealQuantityBreakfast() - 1);
                                                }
                                                // Saves the reduction of meal count available to account for purchased meal(s). 
                                                meal.SetMealQuantityBreakfast(meal.GetMealQuantityBreakfast() - (students[0].GetStuNonGpaMealCredit() - revisedMealCredit));
                                                
                                                // Saves the reduction of credits from the standard meal credits.
                                                students[0].SetStuNonGpaMealCredit(revisedMealCredit);
                                                
                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        //case 2 = credit card selected.
                                        case 2:
                                            {
                                                // Method returns remaining balance of Customer (object) card balance.
                                                revisedCardAmount = Customer.VarifyCardPayment(students[0].GetStuFirstName(), students[0].GetStuLastName(), students[0].GetStuNonGpaMealCredit(), students[0].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[0].GetStuCardBalance(), meal.GetMealTypeBreakfast(), meal.GetMealQuantityBreakfast(),mealPrices[0]);
                                                
                                                // If no change takes place. (Insufficent funds).
                                                if (revisedCardAmount == students[0].GetStuCardBalance())
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If customer (object) has sufficient funds.
                                                if (students[0].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[0].GetStuNonGpaMealCredit() + students[0].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[0].SetStuGpaMealCredit(0);
                                                    
                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityBreakfast(meal.GetMealQuantityBreakfast() - 1);
                                                }
                                                // Variable to hold price of purchase divided by unit cost to store quantity of units purchased.
                                                quantityPurchased = ((students[0].GetStuCardBalance() - revisedCardAmount) / (mealPrices[0] + (mealPrices[0] * .1)));
                                                
                                                // Variable to hold units purchase in integer (whole number) form.
                                                quantityPurchasedWholeNum = (uint)Math.Ceiling(quantityPurchased);

                                                // Saves the reduction of meal count available, to account for purchased meal(s).
                                                meal.SetMealQuantityBreakfast(meal.GetMealQuantityBreakfast() - quantityPurchasedWholeNum);

                                                // Saves the reduction of cash credits to the Customer (object) cardBalance.
                                                students[0].SetStuCardBalance(revisedCardAmount);
                                                
                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        // case 3 = Display Balances.
                                        case 3:
                                            {
                                                // Method to display GPA meal credit, Standard meal credit & Card balance of Customer (object).
                                                Customer.DisplayPlanCreditsAndCardBalance(students[0].GetStuFirstName(), students[0].GetStuLastName(), students[0].GetStuNonGpaMealCredit(), students[0].GetStuGpaMealCredit(), students[0].GetStuCardBalance());
                                                break;
                                            }
                                        // default = Exit selected.
                                        default:
                                            {//Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                    }
                                    break;
                                }
                        }
                    }
                }
                
                // Code path if "Lunch" is selected by customer(actor).
                else if (input == "2")
                {   
                    // To set the name and price of Meal object.
                    meal.SetMealTypeLunch(mealTypes[1]);
                    meal.SetMealPriceLunch(mealPrices[1]);

                    /* To call a method that will guarantee the system will only accept a selected meal quantity 
                       of at least 1 meal and no more than the available amount set in the system memory. */
                    chosenQuantity = Meal.ValidateQuantiyChosen(meal.GetMealQuantityLunch());

                    // To call a method that calculates the chosen Meal object's quantity and corrosponding subtotal sale price.
                    purchaseSubTotal = meal.DisplaySubTotal(chosenQuantity, meal.GetMealPriceLunch());
                    
                    // To call a method that calculates the sale price of Meal object's, including tax.
                    tax = meal.SetTotalPrice(purchaseSubTotal);
                    
                    // To call a method that assigns customer(actor) with Customer object.
                    varifiedStudent = Customer.ValidateStudent();

                    // To loop Display Menu until turned off.
                    bool repeat1 = true;
                    while (repeat1)
                    {
                        // Selected Customer Object
                        switch (varifiedStudent)
                        {
                            // Selected Customer Object: John Pietrangelo.
                            case 1:
                                {
                                    //Method to display Payment Options, returns seletion.
                                    payMethod = Customer.DisplayPaymentOptions(students[1].GetStuFirstName(), students[1].GetStuLastName());
                                 
                                    // Returned selection.
                                    switch (payMethod)
                                    {
                                        //case 0 = Invaled input.
                                        case 0:
                                            {
                                                //Repeats Payment Method menu.
                                                break;
                                            }
                                        // case 1 = meal program selected.
                                        case 1:
                                            {
                                                // Method returns the remaining meal credits.
                                                revisedMealCredit = Customer.VarifyMealCredits(students[1].GetStuFirstName(), students[1].GetStuLastName(), students[1].GetStuNonGpaMealCredit(), students[1].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[1].GetStuCardBalance(), meal.GetMealTypeLunch(), meal.GetMealQuantityLunch());
                                                
                                                // If no change takes place. (Insufficent credits).
                                                if (revisedMealCredit == (students[1].GetStuNonGpaMealCredit()+ students[1].GetStuGpaMealCredit()))
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If student has Adequate credits.
                                                if (students[1].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[1].GetStuNonGpaMealCredit() + students[1].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[1].SetStuGpaMealCredit(0);
                                                    
                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityLunch(meal.GetMealQuantityLunch() - 1);
                                                }
                                                // Saves the reduction of meal count available to account for purchased meal(s). 
                                                meal.SetMealQuantityLunch(meal.GetMealQuantityLunch() - (students[1].GetStuNonGpaMealCredit() - revisedMealCredit));
                                                
                                                // Saves the reduction of credits from the standard meal credits.
                                                students[1].SetStuNonGpaMealCredit(revisedMealCredit);
                                                
                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        //case 2 = credit card selected.
                                        case 2:
                                            {
                                                // Method returns remaining balance of Customer (object) card balance.
                                                revisedCardAmount = Customer.VarifyCardPayment(students[1].GetStuFirstName(), students[1].GetStuLastName(), students[1].GetStuNonGpaMealCredit(), students[1].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[1].GetStuCardBalance(), meal.GetMealTypeLunch(), meal.GetMealQuantityLunch(),mealPrices[1]);
                                                
                                                // If no change takes place. (Insufficent funds).
                                                if (revisedCardAmount == students[1].GetStuCardBalance() && students[1].GetStuGpaMealCredit() < 1)
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If customer (object) has sufficent funds.
                                                if (students[1].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[1].GetStuNonGpaMealCredit() + students[1].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[1].SetStuGpaMealCredit(0);

                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityLunch(meal.GetMealQuantityLunch() - 1);
                                                }
                                                // Variable to hold price of purchase divided by unit cost to store quantity of units purchased.
                                                quantityPurchased = ((students[1].GetStuCardBalance() - revisedCardAmount) / (mealPrices[1] + (mealPrices[1] * .1)));
                                                
                                                // Variable to hold units purchase in integer (whole number) form.
                                                quantityPurchasedWholeNum = (uint)Math.Ceiling(quantityPurchased);

                                                // Saves the reduction of meal count available, to account for purchased meal(s).
                                                meal.SetMealQuantityLunch(meal.GetMealQuantityLunch() - quantityPurchasedWholeNum);

                                                // Saves the reduction of cash credits to the Customer (object) cardBalance.
                                                students[1].SetStuCardBalance(revisedCardAmount);

                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        // case 3 = Display Balances.
                                        case 3:
                                            {
                                                // Method to display GPA meal credit, Standard meal credit & Card balance of Customer (object).
                                                Customer.DisplayPlanCreditsAndCardBalance(students[1].GetStuFirstName(), students[1].GetStuLastName(), students[1].GetStuNonGpaMealCredit(), students[1].GetStuGpaMealCredit(), students[1].GetStuCardBalance());
                                                break;
                                            }
                                        // default = Exit selected.
                                        default:
                                            {
                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                    }
                                    break;
                                }
                            // Selected Customer Object: Clark Kent.
                            case 2:
                                {
                                    //Method to display Payment Options, returns seletion.
                                    payMethod = Customer.DisplayPaymentOptions(students[2].GetStuFirstName(), students[2].GetStuLastName());
                                 
                                    // Returned selection.
                                    switch (payMethod)
                                    {
                                        //case 0 = Invaled input.
                                        case 0:
                                            {
                                                //Repeats Payment Method menu.
                                                break;
                                            }
                                        // case 1 = meal program selected.
                                        case 1: 
                                            {
                                                // Method returns the remaining meal credits.
                                                revisedMealCredit = Customer.VarifyMealCredits(students[2].GetStuFirstName(), students[2].GetStuLastName(), students[2].GetStuNonGpaMealCredit(), students[2].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[2].GetStuCardBalance(), meal.GetMealTypeLunch(), meal.GetMealQuantityLunch());
                                                
                                                // If no change takes place. (Insufficent credits).
                                                if (revisedMealCredit == students[2].GetStuNonGpaMealCredit())
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If student has Adequate credits.
                                                if (students[2].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[2].GetStuNonGpaMealCredit() + students[2].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[2].SetStuGpaMealCredit(0);
                                                    
                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityLunch(meal.GetMealQuantityLunch() - 1);
                                                }
                                                // Saves the reduction of meal count available to account for purchased meal(s). 
                                                meal.SetMealQuantityLunch(meal.GetMealQuantityLunch() - (students[2].GetStuNonGpaMealCredit() - revisedMealCredit));
                                                
                                                // Saves the reduction of credits from the standard meal credits.
                                                students[2].SetStuNonGpaMealCredit(revisedMealCredit);
                                                
                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        //case 2 = credit card selected.
                                        case 2:
                                            {
                                                // Method returns remaining balance of Customer (object) card balance.
                                                revisedCardAmount = Customer.VarifyCardPayment(students[2].GetStuFirstName(), students[2].GetStuLastName(), students[2].GetStuNonGpaMealCredit(), students[2].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[2].GetStuCardBalance(), meal.GetMealTypeLunch(), meal.GetMealQuantityLunch(),mealPrices[1]);
                                                
                                                // If no change takes place. (Insufficent funds).
                                                if (revisedCardAmount == students[2].GetStuCardBalance())
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If customer (object) has sufficient funds.
                                                if (students[2].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[2].GetStuNonGpaMealCredit() + students[2].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[2].SetStuGpaMealCredit(0);

                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityLunch(meal.GetMealQuantityLunch() - 1);
                                                }
                                                // Variable to hold price of purchase divided by unit cost to store quantity of units purchased.
                                                quantityPurchased = ((students[2].GetStuCardBalance() - revisedCardAmount) / (mealPrices[1] + (mealPrices[1] * .1)));

                                                // Variable to hold units purchase in integer (whole number) form.
                                                quantityPurchasedWholeNum = (uint)Math.Ceiling(quantityPurchased);

                                                // Saves the reduction of meal count available, to account for purchased meal(s).
                                                meal.SetMealQuantityLunch(meal.GetMealQuantityLunch() - quantityPurchasedWholeNum);

                                                // Saves the reduction of cash credits to the Customer (object) cardBalance.
                                                students[2].SetStuCardBalance(revisedCardAmount);
                                                
                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        // case 3 = Display Balances.
                                        case 3:
                                            {
                                                // Method to display GPA meal credit, Standard meal credit & Card balance of Customer (object).
                                                Customer.DisplayPlanCreditsAndCardBalance(students[2].GetStuFirstName(), students[2].GetStuLastName(), students[2].GetStuNonGpaMealCredit(), students[2].GetStuGpaMealCredit(), students[2].GetStuCardBalance());
                                                break;
                                            }
                                        // default = Exit selected.
                                        default:
                                            {
                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                    }
                                    break;
                                }
                            // Selected Customer Object: Brad Pitt.
                            case 3:
                                {
                                    //Method to display Payment Options, returns seletion.
                                    payMethod = Customer.DisplayPaymentOptions(students[3].GetStuFirstName(), students[3].GetStuLastName());
                                 
                                    // Returned selection.
                                    switch (payMethod)
                                    {
                                        //case 0 = Invaled input.
                                        case 0:
                                            {
                                                //Repeats Payment Method menu.
                                                break;
                                            }
                                        // case 1 = meal program selected.
                                        case 1:
                                            {
                                                // Method returns the remaining meal credits.
                                                revisedMealCredit = Customer.VarifyMealCredits(students[3].GetStuFirstName(), students[3].GetStuLastName(), students[3].GetStuNonGpaMealCredit(), students[3].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[3].GetStuCardBalance(), meal.GetMealTypeLunch(),meal.GetMealQuantityLunch());
                                                
                                                // If no change takes place. (Insufficent credits).
                                                if (revisedMealCredit == students[3].GetStuNonGpaMealCredit())
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If student has Adequate credits.
                                                if (students[3].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[3].GetStuNonGpaMealCredit() + students[3].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[3].SetStuGpaMealCredit(0);
                                                    
                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityLunch(meal.GetMealQuantityLunch() - 1);
                                                }
                                                // Saves the reduction of meal count available to account for purchased meal(s). 
                                                meal.SetMealQuantityLunch(meal.GetMealQuantityLunch() - (students[3].GetStuNonGpaMealCredit() - revisedMealCredit));
                                                
                                                // Saves the reduction of credits from the standard meal credits.
                                                students[3].SetStuNonGpaMealCredit(revisedMealCredit);
                                                
                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        //case 2 = credit card selected.
                                        case 2:
                                            {
                                                // Method returns remaining balance of Customer (object) card balance.
                                                revisedCardAmount = Customer.VarifyCardPayment(students[3].GetStuFirstName(), students[3].GetStuLastName(), students[3].GetStuNonGpaMealCredit(), students[3].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[3].GetStuCardBalance(), meal.GetMealTypeLunch(), meal.GetMealQuantityLunch(),mealPrices[1]);
                                                
                                                // If no change takes place. (Insufficent funds).
                                                if (revisedCardAmount == students[3].GetStuCardBalance())
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If customer (object) has sufficient funds.
                                                if (students[3].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[3].GetStuNonGpaMealCredit() + students[3].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[3].SetStuGpaMealCredit(0);

                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityLunch(meal.GetMealQuantityLunch() - 1);
                                                }
                                                // Variable to hold price of purchase divided by unit cost to store quantity of units purchased.
                                                quantityPurchased = ((students[3].GetStuCardBalance() - revisedCardAmount) / (mealPrices[1] + (mealPrices[1] * .1)));

                                                // Variable to hold units purchase in integer (whole number) form.
                                                quantityPurchasedWholeNum = (uint)Math.Ceiling(quantityPurchased);

                                                // Saves the reduction of meal count available, to account for purchased meal(s).
                                                meal.SetMealQuantityLunch(meal.GetMealQuantityLunch() - quantityPurchasedWholeNum);

                                                // Saves the reduction of cash credits to the Customer (object) cardBalance.
                                                students[3].SetStuCardBalance(revisedCardAmount);
                                                
                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        // case 3 = Display Balances.
                                        case 3:
                                            {
                                                // Method to display GPA meal credit, Standard meal credit & Card balance of Customer (object).
                                                Customer.DisplayPlanCreditsAndCardBalance(students[3].GetStuFirstName(), students[3].GetStuLastName(), students[3].GetStuNonGpaMealCredit(), students[3].GetStuGpaMealCredit(), students[3].GetStuCardBalance());
                                                break;
                                            }
                                        // default = Exit selected.
                                        default:
                                            {
                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                    }
                                    break;
                                }
                            // Selected Customer Object: System Guest.
                            default:
                                {
                                    //Method to display Payment Options, returns seletion.
                                    payMethod = Customer.DisplayPaymentOptions(students[0].GetStuFirstName(), students[0].GetStuLastName());
                                 
                                    // Returned selection.
                                    switch (payMethod)
                                    {
                                        //case 0 = Invaled input.
                                        case 0:
                                            {
                                                //Repeats Payment Method menu.
                                                break;
                                            }
                                        // case 1 = meal program selected.
                                        case 1:
                                            {
                                                // Method returns the remaining meal credits.
                                                revisedMealCredit = Customer.VarifyMealCredits(students[0].GetStuFirstName(), students[0].GetStuLastName(), students[0].GetStuNonGpaMealCredit(), students[0].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[0].GetStuCardBalance(), meal.GetMealTypeLunch(),meal.GetMealQuantityLunch());
                                                
                                                // If no change takes place. (Insufficent credits).
                                                if (revisedMealCredit == students[0].GetStuNonGpaMealCredit())
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If student has Adequate credits.
                                                if (students[0].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[0].GetStuNonGpaMealCredit() + students[0].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[0].SetStuGpaMealCredit(0);
                                                    
                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityLunch(meal.GetMealQuantityLunch() - 1);
                                                }
                                                // Saves the reduction of meal count available to account for purchased meal(s). 
                                                meal.SetMealQuantityLunch(meal.GetMealQuantityLunch() - (students[0].GetStuNonGpaMealCredit() - revisedMealCredit));
                                                
                                                // Saves the reduction of credits from the standard meal credits.
                                                students[0].SetStuNonGpaMealCredit(revisedMealCredit);
                                                
                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        //case 2 = credit card selected.
                                        case 2:
                                            {
                                                // Method returns remaining balance of Customer (object) card balance.
                                                revisedCardAmount = Customer.VarifyCardPayment(students[0].GetStuFirstName(), students[0].GetStuLastName(), students[0].GetStuNonGpaMealCredit(), students[0].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[0].GetStuCardBalance(), meal.GetMealTypeLunch(), meal.GetMealQuantityLunch(),mealPrices[1]);
                                                
                                                // If no change takes place. (Insufficent funds).
                                                if (revisedCardAmount == students[0].GetStuCardBalance())
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If customer (object) has sufficient funds.
                                                if (students[0].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[0].GetStuNonGpaMealCredit() + students[0].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[0].SetStuGpaMealCredit(0);

                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityLunch(meal.GetMealQuantityLunch() - 1);
                                                }
                                                // Variable to hold price of purchase divided by unit cost to store quantity of units purchased.
                                                quantityPurchased = ((students[0].GetStuCardBalance() - revisedCardAmount) / (mealPrices[1] + (mealPrices[1] * .1)));

                                                // Variable to hold units purchase in integer (whole number) form.
                                                quantityPurchasedWholeNum = (uint)Math.Ceiling(quantityPurchased);

                                                // Saves the reduction of meal count available, to account for purchased meal(s).
                                                meal.SetMealQuantityLunch(meal.GetMealQuantityLunch() - quantityPurchasedWholeNum);

                                                // Saves the reduction of cash credits to the Customer (object) cardBalance.
                                                students[0].SetStuCardBalance(revisedCardAmount);
                                                
                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        // case 3 = Display Balances.
                                        case 3:
                                            {
                                                // Method to display GPA meal credit, Standard meal credit & Card balance of Customer (object).
                                                Customer.DisplayPlanCreditsAndCardBalance(students[0].GetStuFirstName(), students[0].GetStuLastName(), students[0].GetStuNonGpaMealCredit(), students[0].GetStuGpaMealCredit(), students[0].GetStuCardBalance());
                                                break;
                                            }
                                        // default = Exit selected.
                                        default:
                                            {
                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                    }
                                    break;
                                }
                        }
                    }
                }
                // Code path if "Dinner" is selected by customer(actor).
                else if (input == "3")
                {   
                    // To set the name and price of Meal object.
                    meal.SetMealTypeDinner(mealTypes[2]);
                    meal.SetMealPriceDinner(mealPrices[2]);

                    /* To call a method that will guarantee the system will only accept a selected meal quantity 
                       of at least 1 meal and no more than the available amount set in the system memory. */
                    chosenQuantity = Meal.ValidateQuantiyChosen(meal.GetMealQuantityDinner());

                    // To call a method that calculates the chosen Meal object's quantity and corrosponding subtotal sale price.
                    purchaseSubTotal = meal.DisplaySubTotal(chosenQuantity, meal.GetMealPriceDinner());
                    
                    // To call a method that calculates the sale price of Meal object's, including tax.
                    tax = meal.SetTotalPrice(purchaseSubTotal);

                    // To call a method that assigns customer(actor) with Customer object.
                    varifiedStudent = Customer.ValidateStudent();

                    // To loop Display Menu until turned off.
                    bool repeat1 = true;
                    while (repeat1)
                    {
                        switch (varifiedStudent)
                        {
                            // Selected Customer Object: John Pietrangelo.
                            case 1:
                                {
                                    //Method to display Payment Options, returns seletion.
                                    payMethod = Customer.DisplayPaymentOptions(students[1].GetStuFirstName(), students[1].GetStuLastName());
                                    
                                    //Returned selection.
                                    switch (payMethod)
                                    {
                                        //case 0 = Invaled input.
                                        case 0:
                                            {
                                                //Repeats Payment Method menu.
                                                break;
                                            }
                                     // case 1 = meal program selected.
                                        case 1:
                                            {
                                                // Method returns the remaining meal credits.
                                                revisedMealCredit = Customer.VarifyMealCredits(students[1].GetStuFirstName(), students[1].GetStuLastName(), students[1].GetStuNonGpaMealCredit(), students[1].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[1].GetStuCardBalance(), meal.GetMealTypeDinner(), meal.GetMealQuantityDinner());
                                                
                                                // If no change takes place. (Insufficent credits).
                                                if (revisedMealCredit == (students[1].GetStuNonGpaMealCredit()+ students[1].GetStuGpaMealCredit()))
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If student has Adequate credits.
                                                if (students[1].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[1].GetStuNonGpaMealCredit() + students[1].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[1].SetStuGpaMealCredit(0);
                                                    
                                                    // adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityDinner(meal.GetMealQuantityDinner() - 1);
                                                }
                                                // Saves the reduction of meal count available to account for purchased meal(s). 
                                                meal.SetMealQuantityDinner(meal.GetMealQuantityDinner() - (students[1].GetStuNonGpaMealCredit() - revisedMealCredit));
                                                
                                                // Saves the reduction of credits from the standard meal credits.
                                                students[1].SetStuNonGpaMealCredit(revisedMealCredit);
                                                
                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        //case 2 = credit card selected.
                                        case 2:
                                            {
                                                // Method returns remaining balance of Customer (object) card balance.
                                                revisedCardAmount = Customer.VarifyCardPayment(students[1].GetStuFirstName(), students[1].GetStuLastName(), students[1].GetStuNonGpaMealCredit(), students[1].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[1].GetStuCardBalance(), meal.GetMealTypeDinner(), meal.GetMealQuantityDinner(),mealPrices[2]);
                                                
                                                // If no change takes place. (Insufficent funds).
                                                if (revisedCardAmount == students[1].GetStuCardBalance() && students[1].GetStuGpaMealCredit() < 1)
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If customer (object) has sufficient funds.
                                                if (students[1].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[1].GetStuNonGpaMealCredit() + students[1].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[1].SetStuGpaMealCredit(0);

                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityDinner(meal.GetMealQuantityDinner() - 1);
                                                }
                                                // Variable to hold price of purchase divided by unit cost to store quantity of units purchased.
                                                quantityPurchased = ((students[1].GetStuCardBalance() - revisedCardAmount) / (mealPrices[2] + (mealPrices[2] * .1)));

                                                // Variable to hold units purchase in integer (whole number) form.
                                                quantityPurchasedWholeNum = (uint)Math.Ceiling(quantityPurchased);

                                                // Saves the reduction of meal count available, to account for purchased meal(s).
                                                meal.SetMealQuantityDinner(meal.GetMealQuantityDinner() - quantityPurchasedWholeNum);

                                                // Saves the reduction of cash credits to the Customer (object) cardBalance.
                                                students[1].SetStuCardBalance(revisedCardAmount);

                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        // case 3 = Display Balances.
                                        case 3:
                                            {
                                                // Method to display GPA meal credit, Standard meal credit & Card balance of Customer (object).
                                                Customer.DisplayPlanCreditsAndCardBalance(students[1].GetStuFirstName(), students[1].GetStuLastName(), students[1].GetStuNonGpaMealCredit(), students[1].GetStuGpaMealCredit(), students[1].GetStuCardBalance());
                                                break;
                                            }
                                        // default = Exit selected.
                                        default:
                                            {
                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                    }
                                    break;
                                }
                            // Selected Customer Object: Clark Kent.
                            case 2:
                                {
                                    //Method to display Payment Options, returns seletion.
                                    payMethod = Customer.DisplayPaymentOptions(students[2].GetStuFirstName(), students[2].GetStuLastName());
                                 
                                    // Returned selection.
                                    switch (payMethod)
                                    {
                                        //case 0 = Invaled input.
                                        case 0:
                                            {
                                                //Repeats Payment Method menu.
                                                break;
                                            }
                                        // case 1 = meal program selected.
                                        case 1:
                                            {
                                                // Method returns the remaining meal credits.
                                                revisedMealCredit = Customer.VarifyMealCredits(students[2].GetStuFirstName(), students[2].GetStuLastName(), students[2].GetStuNonGpaMealCredit(), students[2].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[2].GetStuCardBalance(), meal.GetMealTypeDinner(), meal.GetMealQuantityDinner());
                                                
                                                // If no change takes place. (Insufficent credits).
                                                if (revisedMealCredit == students[2].GetStuNonGpaMealCredit())
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If student has Adequate credits.
                                                if (students[2].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[2].GetStuNonGpaMealCredit() + students[2].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[2].SetStuGpaMealCredit(0);
                                                    
                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityDinner(meal.GetMealQuantityDinner() - 1);
                                                }
                                                // Saves the reduction of meal count available to account for purchased meal(s). 
                                                meal.SetMealQuantityDinner(meal.GetMealQuantityDinner() - (students[2].GetStuNonGpaMealCredit() - revisedMealCredit));
                                                
                                                // Saves the reduction of credits from the standard meal credits.
                                                students[2].SetStuNonGpaMealCredit(revisedMealCredit);
                                                
                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        //case 2 = credit card selected.
                                        case 2:
                                            {
                                                // Method returns remaining balance of Customer (object) card balance.
                                                revisedCardAmount = Customer.VarifyCardPayment(students[2].GetStuFirstName(), students[2].GetStuLastName(), students[2].GetStuNonGpaMealCredit(), students[2].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[2].GetStuCardBalance(), meal.GetMealTypeDinner(), meal.GetMealQuantityDinner(),mealPrices[2]);
                                                
                                                // If no change takes place. (Insufficent funds).
                                                if (revisedCardAmount == students[2].GetStuCardBalance())
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If customer (object) has sufficient funds.
                                                if (students[2].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[2].GetStuNonGpaMealCredit() + students[2].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[2].SetStuGpaMealCredit(0);

                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityDinner(meal.GetMealQuantityDinner() - 1);
                                                }
                                                // Variable to hold price of purchase divided by unit cost to store quantity of units purchased.
                                                quantityPurchased = ((students[2].GetStuCardBalance() - revisedCardAmount) / (mealPrices[2] + (mealPrices[2] * .1)));

                                                // Variable to hold units purchase in integer (whole number) form.
                                                quantityPurchasedWholeNum = (uint)Math.Ceiling(quantityPurchased);

                                                // Saves the reduction of meal count available, to account for purchased meal(s).
                                                meal.SetMealQuantityDinner(meal.GetMealQuantityDinner() - quantityPurchasedWholeNum);

                                                // Saves the reduction of cash credits to the Customer (object) cardBalance.
                                                students[2].SetStuCardBalance(revisedCardAmount);

                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        // case 3 = Display Balances.
                                        case 3:
                                            {
                                                // Method to display GPA meal credit, Standard meal credit & Card balance of Customer (object).
                                                Customer.DisplayPlanCreditsAndCardBalance(students[2].GetStuFirstName(), students[2].GetStuLastName(), students[2].GetStuNonGpaMealCredit(), students[2].GetStuGpaMealCredit(), students[2].GetStuCardBalance());
                                                break;
                                            }
                                        // default = Exit selected.
                                        default:
                                            {
                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                    }
                                    break;
                                }
                            // Selected Customer Object: Brad Pitt.
                            case 3:
                                {
                                    //Method to display Payment Options, returns seletion.
                                    payMethod = Customer.DisplayPaymentOptions(students[3].GetStuFirstName(), students[3].GetStuLastName());
                                 
                                    // Returned selection.
                                    switch (payMethod)
                                    {
                                        //case 0 = Invaled input.
                                        case 0:
                                            {
                                                //Repeats Payment Method menu.
                                                break;
                                            }
                                        // case 1 = meal program selected.
                                        case 1:
                                            {
                                                // Method returns the remaining meal credits.
                                                revisedMealCredit = Customer.VarifyMealCredits(students[3].GetStuFirstName(), students[3].GetStuLastName(), students[3].GetStuNonGpaMealCredit(), students[3].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[3].GetStuCardBalance(), meal.GetMealTypeDinner(),meal.GetMealQuantityDinner());
                                                
                                                // If no change takes place. (Insufficent credits).
                                                if (revisedMealCredit == students[3].GetStuNonGpaMealCredit())
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If student has Adequate credits.
                                                if (students[3].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[3].GetStuNonGpaMealCredit() + students[3].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[3].SetStuGpaMealCredit(0);
                                                    
                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityDinner(meal.GetMealQuantityDinner() - 1);
                                                }
                                                // Saves the reduction of meal count available to account for purchased meal(s). 
                                                meal.SetMealQuantityDinner(meal.GetMealQuantityDinner() - (students[3].GetStuNonGpaMealCredit() - revisedMealCredit));
                                                
                                                // Saves the reduction of credits from the standard meal credits.
                                                students[3].SetStuNonGpaMealCredit(revisedMealCredit);
                                                
                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        //case 2 = credit card selected.
                                        case 2:
                                            {
                                                // Method returns remaining balance of Customer (object) card balance.
                                                revisedCardAmount = Customer.VarifyCardPayment(students[3].GetStuFirstName(), students[3].GetStuLastName(), students[3].GetStuNonGpaMealCredit(), students[3].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[3].GetStuCardBalance(), meal.GetMealTypeDinner(), meal.GetMealQuantityDinner(),mealPrices[2]);
                                                
                                                // If no change takes place. (Insufficent funds).
                                                if (revisedCardAmount == students[2].GetStuCardBalance())
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If customer (object) has sufficient funds. 
                                                if (students[3].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[3].GetStuNonGpaMealCredit() + students[3].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[3].SetStuGpaMealCredit(0);

                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityDinner(meal.GetMealQuantityDinner() - 1);
                                                }
                                                // Variable to hold price of purchase divided by unit cost to store quantity of units purchased.
                                                quantityPurchased = ((students[3].GetStuCardBalance() - revisedCardAmount) / (mealPrices[2] + (mealPrices[2] * .1)));

                                                // Variable to hold units purchase in integer (whole number) form.
                                                quantityPurchasedWholeNum = (uint)Math.Ceiling(quantityPurchased);

                                                // Saves the reduction of meal count available, to account for purchased meal(s).
                                                meal.SetMealQuantityDinner(meal.GetMealQuantityDinner() - quantityPurchasedWholeNum);

                                                // Saves the reduction of cash credits to the Customer (object) cardBalance.
                                                students[3].SetStuCardBalance(revisedCardAmount);
                                                
                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        // case 3 = Display Balances.
                                        case 3:
                                            {
                                                // Method to display GPA meal credit, Standard meal credit & Card balance of Customer (object).
                                                Customer.DisplayPlanCreditsAndCardBalance(students[3].GetStuFirstName(), students[3].GetStuLastName(), students[3].GetStuNonGpaMealCredit(), students[3].GetStuGpaMealCredit(), students[3].GetStuCardBalance());
                                                break;
                                            }
                                        // default = Exit selected.
                                        default:
                                            {
                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                    }
                                    break;
                                }
                            // Selected Customer Object: System Guest.
                            default:
                                {
                                    //Method to display Payment Options, returns seletion.
                                    payMethod = Customer.DisplayPaymentOptions(students[0].GetStuFirstName(), students[0].GetStuLastName());
                                 
                                    // Returned selection.
                                    switch (payMethod)
                                    {
                                        //case 0 = Invaled input.
                                        case 0:
                                            {
                                                //Repeats Payment Method menu.
                                                break;
                                            }
                                        // case 1 = meal program selected.
                                        case 1:
                                            {
                                                // Method returns the remaining meal credits.
                                                revisedMealCredit = Customer.VarifyMealCredits(students[0].GetStuFirstName(), students[0].GetStuLastName(), students[0].GetStuNonGpaMealCredit(), students[0].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[0].GetStuCardBalance(), meal.GetMealTypeDinner(),meal.GetMealQuantityDinner());
                                                
                                                // If no change takes place. (Insufficent credits).
                                                if (revisedMealCredit == students[0].GetStuNonGpaMealCredit())
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If student has Adequate credits.
                                                if (students[0].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[0].GetStuNonGpaMealCredit() + students[0].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[0].SetStuGpaMealCredit(0);
                                                    
                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityDinner(meal.GetMealQuantityDinner() - 1);
                                                }
                                                // Saves the reduction of meal count available to account for purchased meal(s). 
                                                meal.SetMealQuantityDinner(meal.GetMealQuantityDinner() - (students[0].GetStuNonGpaMealCredit() - revisedMealCredit));
                                                
                                                // Saves the reduction of credits from the standard meal credits.
                                                students[0].SetStuNonGpaMealCredit(revisedMealCredit);
                                                
                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        //case 2 = credit card selected.
                                        case 2:
                                            {
                                                // Method returns remaining balance of Customer (object) card balance.
                                                revisedCardAmount = Customer.VarifyCardPayment(students[0].GetStuFirstName(), students[0].GetStuLastName(), students[0].GetStuNonGpaMealCredit(), students[0].GetStuGpaMealCredit(), chosenQuantity, purchaseSubTotal, tax, meal.GetTotalPrice(), students[0].GetStuCardBalance(), meal.GetMealTypeDinner(), meal.GetMealQuantityDinner(),mealPrices[2]);
                                                
                                                // If no change takes place. (Insufficent funds).
                                                if (revisedCardAmount == students[0].GetStuCardBalance())
                                                {
                                                    //Counter-acts the "Returns focus to Main Menu" at end of code block.
                                                    repeat1 = !repeat1;
                                                }
                                                // If customer (object) has sufficient funds.
                                                if (students[0].GetStuGpaMealCredit() > 0 && chosenQuantity < (students[0].GetStuNonGpaMealCredit() + students[0].GetStuGpaMealCredit()))
                                                {
                                                    // Removes free meal credit from student if they have a free meal credit.
                                                    students[2].SetStuGpaMealCredit(0);

                                                    // Adjusts meal quantity to account for non-standard meal credit.
                                                    meal.SetMealQuantityDinner(meal.GetMealQuantityDinner() - 1);
                                                }
                                                // Variable to hold price of purchase divided by unit cost to store quantity of units purchased.
                                                quantityPurchased = ((students[0].GetStuCardBalance() - revisedCardAmount) / (mealPrices[2] + (mealPrices[2] * .1)));

                                                // Variable to hold units purchase in integer (whole number) form.
                                                quantityPurchasedWholeNum = (uint)Math.Ceiling(quantityPurchased);

                                                // Saves the reduction of meal count available, to account for purchased meal(s).
                                                meal.SetMealQuantityDinner(meal.GetMealQuantityDinner() - quantityPurchasedWholeNum);

                                                // Saves the reduction of cash credits to the Customer (object) cardBalance.
                                                students[0].SetStuCardBalance(revisedCardAmount);

                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                        // case 3 = Display Balances.
                                        case 3:
                                            {
                                                // Method to display GPA meal credit, Standard meal credit & Card balance of Customer (object).
                                                Customer.DisplayPlanCreditsAndCardBalance(students[0].GetStuFirstName(), students[0].GetStuLastName(), students[0].GetStuNonGpaMealCredit(), students[0].GetStuGpaMealCredit(), students[0].GetStuCardBalance());
                                                break;
                                            }
                                        // default = Exit selected.
                                        default:
                                            {
                                                //Returns focus to Main Menu.
                                                repeat1 = !repeat1;
                                                break;
                                            }
                                    }
                                    break;
                               }
                        }
                    }
                }
                // Code path if "Exit" is selected by customer(actor).
                else if (input == "4")
                {
                    // Closed Program.
                    repeat = !repeat;
                }
                // If Invalid entry.
                else
                {
                    Console.WriteLine("\n{0,62}", "Invalid Entery! Enter a selection from 1 to 4.");
                    Console.ReadKey();
                }
            } while (repeat);
        }
    }
}

