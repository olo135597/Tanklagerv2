using System;
using System.Collections.Generic;
using TanklagerLibraryv2;
namespace Tanklager_1
{
   
    internal class Program
    { 
        
        public static void Main(string[] args)
        {
            //Tanklager erstellen
            TankStock tanks = new TankStock();
            MainFunction(tanks);
            
        }

        /// <summary>
        /// Hauptmethode
        /// </summary>
        /// <param name="tanks"></param>
        static void MainFunction(TankStock tanks)
        {
            string input = "";
            Console.WriteLine(
                "Welcome to the Tankstock program, where you can Create, Fill, Empty and delete tanks whenever you want.");
            while (true)
            {
                try
                {


                    Console.WriteLine(
                        "Choose a number from the list, what you want to do (if you want to exit write: 'exit'): ");
                    Console.WriteLine(
                        "- Create Tank(1) \n- Delete Tank(2) \n- Get Information(3) \n- Status report(4) \n- Fill tanks(5) \n- Empty tanks(6) \n- Change Maintenance(7)");
                    input = Console.ReadLine();

                    if (input == "exit")
                    {
                        Console.WriteLine("Exiting program...");
                        break;
                    }




                    //Kontrolliert das es eine Nummer ist
                    if (int.TryParse(input, out int answer))
                    {
                        //Switch sodass der Nutzer eine Funktion auswählen kann
                        switch (answer)
                        {
                            case 1:
                                CreateTankPrint(tanks);
                                break;
                            case 2:
                                DeleteTankPrint(tanks);
                                break;
                            case 3:
                                ListTanksPrint(tanks);
                                break;
                            case 4:
                                StatusReportPrint(tanks);
                                break;
                            case 5:
                                FillTanksPrint(tanks);
                                break;
                            case 6:
                                EmptyTanks(tanks);
                                break;
                            case 7:
                                ChangeMaintenancePrint(tanks);
                                break;
                            default:
                                Console.WriteLine(
                                    "It seems like you haven't chosen anything or your number is not on this list.");
                                break;

                        }
                    }
                    else if (input != "exit")
                    {
                        Console.WriteLine("Invalid input. Please enter an integer");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    
                }

                Console.WriteLine("Thank you for your work");

            }
        }
       
        
        
        /// <summary>
        /// Füllt einzelner Tank = Aufgabe 1
        /// </summary>
        /// <param name="Tank"></param>
        static void FillTankPrint(OilTank Tank)
        {
            Console.WriteLine("You want to fill a Tank");
            
            Console.WriteLine("By how much should it be filled?");
            int amountFill = Convert.ToInt32(Console.ReadLine());  
            
            Console.WriteLine("Filling Tank: ");
            int overflow = Tank.FillTank(amountFill);
            Console.WriteLine($"The overflow is: {overflow}, OilAmount: {Tank.oilAmount}, Volume: {Tank.GetVolume()}");
        }

        /// <summary>
        /// Listet nur Id und Namen von allen Tanks
        /// </summary>
        /// <param name="tanks"></param>
        static void ShortListTanksPrint(TankStock tanks)
        {
            foreach (OilTank tank in tanks.tanks)
            {
                Console.WriteLine($"- Id: {tank.id}, Name: {tank.name}");
            }
        }
        /// <summary>
        /// Liste alle Tanks mit ihren Informationen
        /// </summary>
        /// <param name="tanks"></param>
        static void ListTanksPrint(TankStock tanks)
        {
            Console.WriteLine("\n  _      _     _     _______          _    \n | |    (_)   | |   |__   __|        | |   \n | |     _ ___| |_     | | __ _ _ __ | | __\n | |    | / __| __|    | |/ _` | '_ \\| |/ /\n | |____| \\__ \\ |_     | | (_| | | | |   < \n |______|_|___/\\__|    |_|\\__,_|_| |_|_|\\_\\\n                                           \n                                           \n");
            foreach (OilTank tank in tanks.tanks)
            {
                Console.WriteLine("\n ");
                StatusTankPrint(tank);
            }
        }
       
        /// <summary>
        /// Zusammenfasst alle Informationen von allen Tanks
        /// </summary>
        /// <param name="tanks"></param>
        static void StatusReportPrint(TankStock tanks)
        {
            Console.WriteLine("\n   _____ _        _                                         _   \n  / ____| |      | |                                       | |  \n | (___ | |_ __ _| |_ _   _ ___   _ __ ___ _ __   ___  _ __| |_ \n  \\___ \\| __/ _` | __| | | / __| | '__/ _ \\ '_ \\ / _ \\| '__| __|\n  ____) | || (_| | |_| |_| \\__ \\ | | |  __/ |_) | (_) | |  | |_ \n |_____/ \\__\\__,_|\\__|\\__,_|___/ |_|  \\___| .__/ \\___/|_|   \\__|\n                                          | |                   \n                                          |_|                   \n");

            Console.WriteLine("Here is the Status Report of all Tanks combined");
            Console.WriteLine($"- Number of Tanks {tanks.tanks.Count} \n- Whole Capacity: {tanks.GetAllCapacity() - tanks.AmountCapacityMaintenance()}\n- All Oil amount: {tanks.GetAllOilAmount()} \n - Place Left: {tanks.GetAllSpace() - tanks.AmountSpaceMaintenance()}");
        }
        
       /// <summary>
       /// Leere einzelner Tank = Aufgabe 1
       /// </summary>
       /// <param name="Tank"></param>
        static void EmptyTankPrint(OilTank Tank)
        {
            Console.WriteLine("You want to empty a Tank"); 
            Console.WriteLine("By how much should it be emptied?");
            int amountEmpty = Convert.ToInt32(Console.ReadLine()); 
            
            Console.WriteLine("Empty Tank: ");
            int overflow = Tank.EmptyTank(amountEmpty); 
            Console.WriteLine($"The overflow is: {overflow}, OilAmount: {Tank.oilAmount}, Volume: {Tank.GetVolume()}");

        }
        
        /// <summary>
        /// Gibt die Informationen von einem Tank aus
        /// </summary>
        /// <param name="Tank"></param>
        static void StatusTankPrint(OilTank Tank)
        {
            Console.WriteLine($"Information about the Tank:\n-Id: {Tank.id} \n- Name: {Tank.name} \n- Date of creation: {Tank.creationDate} \n- Capacity: {Tank.capacity} \n- Amount of Oil: {Tank.oilAmount} \n- Is in maintenance: {Tank.isInMaintenance} ");
        }
        
        /// <summary>
        /// Erstellt einen Tank 
        /// </summary>
        /// <param name="tanks"></param>
        static void CreateTankPrint(TankStock tanks)
        {
            Console.WriteLine("\n   _____                _         _______          _    \n  / ____|              | |       |__   __|        | |   \n | |     _ __ ___  __ _| |_ ___     | | __ _ _ __ | | __\n | |    | '__/ _ \\/ _` | __/ _ \\    | |/ _` | '_ \\| |/ /\n | |____| | |  __/ (_| | ||  __/    | | (_| | | | |   < \n  \\_____|_|  \\___|\\__,_|\\__\\___|    |_|\\__,_|_| |_|_|\\_\\\n                                                        \n                                                        \n");

            Console.WriteLine("Here you can create your Tank. Please write the credentials for the Tank:");
            Console.WriteLine("Identification number: ");
            int id = Convert.ToInt32(Console.ReadLine());
            if (tanks.CheckId(id))
            {
                Console.WriteLine("Name: ");
                string name = Convert.ToString(Console.ReadLine());
                if (tanks.CheckName(name))
                {
                    Console.WriteLine("Capacity: ");
                    int capacity = Convert.ToInt32(Console.ReadLine());
                    if (capacity > 0)
                    {
                        Console.WriteLine("Is the new Tank in maintenance? y/n");
                        bool isInMaintenance = false; 
                        string answerMaintenance = Convert.ToString(Console.ReadLine());
                        if (answerMaintenance == "y")
                        {
                            isInMaintenance = true;  
                            tanks.CreateTank(id, name, capacity, isInMaintenance);
                        }
                        else if (answerMaintenance == "n")
                        {
                            isInMaintenance = false; 
                            tanks.CreateTank(id, name, capacity, isInMaintenance);
                        }
                        else
                        {
                            Console.WriteLine("Your Input is sadly wrong please use y or n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("The capacity can't be negative or null");
                    }
            
                   
                
                  
                }
                else
                {
                    Console.WriteLine("Sadly the name is already taken, you can't create this Tank.");
                }
            } 
            else
            {
                Console.WriteLine("Sadly the id is already taken, you can't create this Tank.");
            }
           
            
            
        }
        
        /// <summary>
        /// Löscht einen Tank
        /// </summary>
        /// <param name="tanks"></param>
        static void DeleteTankPrint(TankStock tanks)
        {
            Console.WriteLine("\n  _____       _      _         _______          _    \n |  __ \\     | |    | |       |__   __|        | |   \n | |  | | ___| | ___| |_ ___     | | __ _ _ __ | | __\n | |  | |/ _ \\ |/ _ \\ __/ _ \\    | |/ _` | '_ \\| |/ /\n | |__| |  __/ |  __/ ||  __/    | | (_| | | | |   < \n |_____/ \\___|_|\\___|\\__\\___|    |_|\\__,_|_| |_|_|\\_\\\n                                                     \n                                                     \n");

            Console.WriteLine("Choose the Id from which you want to delete: ");
            ShortListTanksPrint(tanks); 
            Console.WriteLine("Choose the Id number from the tank you want to delete: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            if (tanks.CheckId(choice))
            {
                Console.WriteLine("Sadly, the id you chose doesn't exist.");
            }
            else
            {
                bool checkDeleteOk = tanks.DeleteTank(choice);
                if (checkDeleteOk)
                {
                    Console.WriteLine("Tank is deleted");
                }
                else
                {
                    Console.WriteLine("Sadly the tank can't be deleted, because the amount of Oil is to much for all of the tanks");
                }
                
            }
           
        }
        
        /// <summary>
        /// Füllt die Tanks
        /// </summary>
        /// <param name="tanks"></param>
        static void FillTanksPrint(TankStock tanks)
        {
            Console.WriteLine("\n  ______ _ _ _   _              _        \n |  ____(_) | | | |            | |       \n | |__   _| | | | |_ __ _ _ __ | | _____ \n |  __| | | | | | __/ _` | '_ \\| |/ / __|\n | |    | | | | | || (_| | | | |   <\\__ \\\n |_|    |_|_|_|  \\__\\__,_|_| |_|_|\\_\\___/\n                                         \n                                         \n");

            Console.WriteLine("Here you can fill up the tanks.");
            if (tanks.tanks.Count == 0)
            {
                Console.WriteLine("There are no Tanks to be filled, please create them first");
            }
            else
            {
                Console.WriteLine("How much do you want it to be filled? Please give amount:");
                int amountFill = Convert.ToInt32(Console.ReadLine());
                if (amountFill > 0)
                {
         
                    int overflow = tanks.FillTanks(amountFill);
                    if (overflow > 0)
                    {
                        Console.WriteLine($"Sadly {overflow} are too much for the Space in the tanks that can't be filled ");
                    }
                    else
                    {
                        Console.WriteLine("Fill complete"); 
                    }
          
                   
                }
                else
                {
                    Console.WriteLine("The amount you want to fill can't be negative or 0");
                }
            }
           
        }
        
        /// <summary>
        /// Leert die Tanks
        /// </summary>
        /// <param name="tanks"></param>
        static void EmptyTanks(TankStock tanks)
        {
            Console.WriteLine("\n  ______                 _           _              _        \n |  ____|               | |         | |            | |       \n | |__   _ __ ___  _ __ | |_ _   _  | |_ __ _ _ __ | | _____ \n |  __| | '_ ` _ \\| '_ \\| __| | | | | __/ _` | '_ \\| |/ / __|\n | |____| | | | | | |_) | |_| |_| | | || (_| | | | |   <\\__ \\\n |______|_| |_| |_| .__/ \\__|\\__, |  \\__\\__,_|_| |_|_|\\_\\___/\n                  | |         __/ |                          \n                  |_|        |___/                           \n");

            Console.WriteLine("Here you can empty the tanks");
            if (tanks.tanks.Count == 0 && tanks.GetAllOilAmount() == 0)
            {
                Console.WriteLine("There are no Tanks to be emptied, please create them first and fill them");
            }
            else
            {
        
            Console.WriteLine("How much do you want it to be emptied? Please give amount:");
            int amountEmpty = Convert.ToInt32(Console.ReadLine());
            if (amountEmpty > 0)
            {
                
            
                int overflow = tanks.EmptyTanks(amountEmpty);
                if (overflow > 0)
                {
                    Console.WriteLine($"Sadly {overflow} are too much for the Space in the tanks that can't be emptied ");

                }
                else
                {
                    Console.WriteLine("Empty complete");
                }
         
            }
            else
            {
                Console.WriteLine("The amount you want to empty can't be negative or 0");
            }
                    
            }
        }
        
        /// <summary>
        /// Ändert die Wartung
        /// </summary>
        /// <param name="tanks"></param>
        static void ChangeMaintenancePrint(TankStock tanks)
        {
            bool checkMaintenanceOk = true;
            Console.WriteLine("\n   _____ _                                              _       _                                  \n  / ____| |                                            (_)     | |                                 \n | |    | |__   __ _ _ __   __ _  ___   _ __ ___   __ _ _ _ __ | |_ ___ _ __   __ _ _ __   ___ ___ \n | |    | '_ \\ / _` | '_ \\ / _` |/ _ \\ | '_ ` _ \\ / _` | | '_ \\| __/ _ \\ '_ \\ / _` | '_ \\ / __/ _ \\\n | |____| | | | (_| | | | | (_| |  __/ | | | | | | (_| | | | | | ||  __/ | | | (_| | | | | (_|  __/\n  \\_____|_| |_|\\__,_|_| |_|\\__, |\\___| |_| |_| |_|\\__,_|_|_| |_|\\__\\___|_| |_|\\__,_|_| |_|\\___\\___|\n                            __/ |                                                                  \n                           |___/                                                                   \n");

            Console.WriteLine("Here you can set a tank into maintenance");
            Console.WriteLine("Here are all the tanks: ");
            ShortListTanksPrint(tanks);
            Console.WriteLine("Which tank do you want to set into maintenance? give id");
            int choice = Convert.ToInt32(Console.ReadLine());
            if (tanks.CheckId(choice))
            {
                Console.WriteLine("Sadly, the id you chose doesn't exist.");
            }
            else
            {
                Console.WriteLine("Do you want to put the tank into maintenance or release it from it? \n " +
                                  "Input: \n - m = set into maintenance \n - r = release from maintenance ");
                string input = Convert.ToString(Console.ReadLine());
                if (input == "m")
                {
                    Console.WriteLine("The tank is going into maintenance");
                 
                    checkMaintenanceOk = tanks.ChangeMaintenance(choice, true);
                }
                else if (input == "r")
                {
                    Console.WriteLine("The tank released from maintenance");
                  
                    checkMaintenanceOk = tanks.ChangeMaintenance(choice, false);
                }
                else
                {
                    Console.WriteLine("Sadly the input is wrong, it should be m or r");
                }
                
               

                if (checkMaintenanceOk)
                {
                    if (input == "m")
                    {
                        Console.WriteLine("Tanks maintenance is changed");
                        Console.WriteLine("It is now in maintenance");
                    }
                    else if(input == "r")
                    {
                        Console.WriteLine("The Tank is now not in maintenance and can be filled");
                    }
                   
                }
                else
                {
                    Console.WriteLine("Sadly the maintenance can't be changed, because there is no space for the oil");
                }
                
            
            }
        }
      

 
    }
}
