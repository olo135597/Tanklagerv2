using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace TanklagerLibrary;

public class TankStock
    {
        
        //Variablen vom Tank Lager  todo: private machen, entsprechende Code-Stellen anpassen..
        public List<OilTank> tanks { get; private set; }  = new List<OilTank>();
        

        /// <summary>
        /// Gibt die Kapazität von allen Tanks aus
        /// </summary>
        /// <returns>Kapazität von allen Tanks</returns>
        public int GetAllCapacity()
        {
            return tanks.Sum(t => t.capacity);
        }
        
        /// <summary>
        /// Gibt das gesamte Öl von allen Tanks
        /// </summary>
        /// <returns>Gesamte Öl von allen Tanks</returns>
        public int GetAllOilAmount()
        {
            return tanks.Sum(t => t.oilAmount);
        }
        
        /// <summary>
        /// Gibt den gesamten zu verfügbaren Platz
        /// </summary>
        /// <returns>Gesamten verfügbaren Platz</returns>
        public int GetAllSpace()
        {
            return tanks
                .Where(t => t.isInMaintenance == false)
                .Sum(t => t.GetSpace());
        }
        /// <summary>
        /// Zählt wie viele Tanks es gibt
        /// </summary>
        /// <returns></returns>
        public int CountTanks()
        {
            return tanks.Count;
        }
        
        /// <summary>
        /// Testet ob der Name schon existiert
        /// </summary>
        /// <param name="name"></param>
        /// <returns>true wenn der name noch nicht existiert</returns>
        public bool CheckName(string name)
        { 
            return tanks.Count(tank => tank.name == name) == 0;
        }
        
        /// <summary>
        /// Testet ob die Id schon existiert
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true wenn die id noch nicht existiert</returns>
        public bool CheckId(int id)
        {
            var findIdenticalId = tanks.Where(tank => tank.id == id);
            if (findIdenticalId.Count() != 0)
            {
                return false;
            }
            else
            {
                return true; 
            }
        }
        
        /// <summary>
        /// Erstellt einen Tank
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="capacity"></param>
        /// <param name="isInMaintenance"></param>
        public void CreateTank(int id, string name, int capacity, bool isInMaintenance)
        {
            
            OilTank newTank = new OilTank(id, name, DateTime.Now, capacity, 0, isInMaintenance);
            tanks.Add(newTank);
        }

  
        
        /// <summary>
        /// Löscht einen Tank und prüft ob das Öl verteilt werden kann
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true wenn der Tank gelöscht werden kann</returns>
        public bool DeleteTank(int id)
        {
            var tankToDelete = GetTankById(id);
            int deletedAmount = tankToDelete.oilAmount;
            int deletedSpace = tankToDelete.GetSpace();

            if (CheckOilFill(deletedAmount, deletedSpace))
            {

                tanks.Remove(tankToDelete);
                FillTanks(deletedAmount);
                return true;
            }
            else
            {
                return false;
            }
         
        }
        
        /// <summary>
        /// Ändert die Wartung eines Tankes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>true wenn der Tank in Wartung getan werden kann</returns>
        public bool ChangeMaintenance(int id, bool input)
        {
            OilTank tankInMaintenance = GetTankById(id); 
            
            
            int maintenancedAmount = tankInMaintenance.oilAmount;
            int maintenancedSpace = tankInMaintenance.GetSpace();
            bool isMaintenanceOk = CheckOilFill(maintenancedAmount, maintenancedSpace);
            if (isMaintenanceOk)
            {
                tankInMaintenance.SetMaintenance(input);
                FillTanks(maintenancedAmount);
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// Gibt den Tank von der gesuchten Id zurück
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Tank mit der gegebenen Id</returns>
        private OilTank GetTankById(int id)
        {
            return tanks.FirstOrDefault(t => t.id == id);
        }

        /// <summary>
        /// Füllt die Tanks
        /// </summary>
        /// <param name="amountFill"></param>
        /// <returns>overflow</returns>
        public int FillTanks(int amountFill)
        {
            
            //todo: umbauen wie gezeigt. dh: Tanks ermitteln (welche nicht in Wartung), sortieren, befüllen
            int overflow = amountFill;
           
            while (overflow > 0)
            {
                bool checkOneWithoutMaintenance = tanks
                    .Where(t => t.GetSpace() > 0)
                    .Any(t => t.isInMaintenance == false);
                
                    
                if (GetAllSpace() > 0 && checkOneWithoutMaintenance)
                { 
                    var lowestSpaceTank = tanks
                        .Where(t => t.GetSpace() > 0)
                        .Where(t => t.isInMaintenance == false)
                        .OrderBy(t => t.GetSpace())
                        .FirstOrDefault();
                
                     overflow = lowestSpaceTank.FillTank(overflow);
                     
                }
                else
                {
                    break;
                }
            }

            return overflow;


        }
        
        /// <summary>
        /// Leert die Tanks
        /// </summary>
        /// <param name="amountEmpty"></param>
        /// <returns>overflow</returns>
        public int EmptyTanks(int amountEmpty)
        {
            //todo: umbauen wie FillTank, invers. dh: Tanks ermitteln (welche nicht in Wartung), sortieren, befüllen

            int overflow = amountEmpty;
            while (overflow > 0)
            {
                bool checkOneWithoutMaintenance = tanks
                    .Where(t => t.oilAmount > 0)
                    .Any(t => t.isInMaintenance == false);
                if (GetAllSpace() < GetAllCapacity() && checkOneWithoutMaintenance == true)
                {
                    var lowestAmountOil = tanks
                        .Where(t => t.oilAmount > 0)
                        .Where(t => t.isInMaintenance == false)
                        .OrderBy(t => t.oilAmount)
                        .FirstOrDefault();
                    overflow = lowestAmountOil.EmptyTank(overflow);
                }
                else
                {
                    break;
                }
                    
                
            }

            return overflow;
        }
        
        /// <summary>
        /// Prüft ob das Öl verteilt werden kann
        /// </summary>
        /// <param name="amountFill"></param>
        /// <param name="deletedSpace"></param>
        /// <returns>true wenn es verteilt werden kann</returns>
        public bool CheckOilFill(int amountFill, int deletedSpace)
        {
            int wholeSpace = GetAllSpace() - deletedSpace;
            if (wholeSpace < amountFill)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        /// <summary>
        /// Gibt den Platz zurück von Tanks in Wartung
        /// </summary>
        /// <returns>Anzahl Platz</returns>
        public int AmountSpaceMaintenance()
        {
            return tanks
                .Where(t => t.isInMaintenance)
                .Sum(t => t.GetSpace());
        }
        
        /// <summary>
        /// Gibt die Kapazität zurück on Tanks in Wartung
        /// </summary>
        /// <returns>Anzahl Kapazität</returns>
        public int AmountCapacityMaintenance()
        {
            return tanks
                .Where(t => t.isInMaintenance)
                .Sum(t => t.capacity);
        }
        
        
     
    } 