using System;

namespace TanklagerLibraryv2
{
   public class OilTank
    {
        // Variablen
        public int id { get; set; }
        public string name { get; set; }
      
        private  DateTime creationdate;
        public DateTime creationDate
        {
            get
            {
                return this.creationdate;
            }
            set
            {
                this.creationdate = DateTime.Now;
            }
        }
        public  int capacity { get; set; }
        public int oilAmount { get; private set; }
        public bool isInMaintenance { get; private set; }

        // Konstruktor
        public OilTank(int id, string name, DateTime creationDate, int capacity, int oilAmount, bool isInMaintenance)
        {
            if (capacity < 0) {
                throw new Exception("negative Kapazität");
            }
            
            this.id = id;
            this.name = name;
            this.creationDate = creationDate;
            this.capacity = capacity;
            this.oilAmount = oilAmount;
            this.isInMaintenance = isInMaintenance;
        }

        public OilTank() 
        {
            
        }


        /// <summary>
        /// Berechnung in Prozent wie gefüllt ein Tank ist
        /// </summary>
        /// <returns>Prozent vom Füllstand</returns>
        public int GetVolume()
        {
            return (int)Math.Round((double)((100 * oilAmount) / capacity));
        }
        
        /// <summary>
        /// Berechnet noch den verfügbaren Platz
        /// </summary>
        /// <returns>space</returns>
        public int GetSpace()
        {
            int space = capacity - oilAmount;
            return space; 
        }

        /// <summary>
        /// Todo: dieses Konzept übernehmen, bisheriges verwerfen
        /// Verusucht, die übergebene Menge öl aufzunehmen.
        /// </summary>
        /// <param name="amount">aufzunehmende Menge öl</param>
        /// <returns>tatsächlich aufgenommene Menge öl</returns>
        /// <exception cref="InvalidOperationException">falls negativer Amount übergeben wird</exception>
        public int OptimizedFill(int amount)
        {
            if (amount < 0)
            {
                throw new InvalidOperationException("No negativ fill amount!");
            }

            int spaceBeforeFill = GetSpace();
            if (amount <= spaceBeforeFill)
            {//Anlieferung ist kleiner oder gleich freiem Platz
                oilAmount += amount;
                return amount;
            } else
            {
                //Anlieferung ist kleiner oder gleich freiem Platz
                //oilAmount = capacity;
                oilAmount += spaceBeforeFill;
                return spaceBeforeFill;
            }
            
        }

        /// <summary>
        /// Füllt den Tank und gibt den overflow zurück
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>overflow</returns>
        public int FillTank(int amount)
        {
            oilAmount = oilAmount + amount;
            int overflow = oilAmount - capacity;
            
            if (overflow > 0)
            {
                oilAmount = capacity; 
            }
           
            return overflow; 
        }
        
        /// <summary>
        /// Leert den Tank und gibt den overflow zurück
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>overflow</returns>
        public int EmptyTank(int amount)
        {
         
            oilAmount = oilAmount - amount;
            int overflow = oilAmount * (-1);

            if (overflow > 0)
            {
                
                oilAmount = 0; 
                
            }

            return overflow; 
        }
        
        /// <summary>
        /// Setzt die Wartung 
        /// </summary>
        /// <param name="input"></param>
        public void SetMaintenance(bool input)
        {
            if (input == true)
            {   
                isInMaintenance = true;
                oilAmount = 0;
            }
            else if (input == false)
            {
                isInMaintenance = false;
            }
        } 
        
        
    }
}