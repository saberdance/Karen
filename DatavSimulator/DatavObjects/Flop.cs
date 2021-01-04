using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DatavSimulator;
using tsubasa;

namespace DatavSimulator.DatavObjects
{
    public class Flop : IDatavObj
    {
        public Flop(string flopName, string datavName, int startNumber, int currentNumber, int variation, int changeInterval)
        {
            FlopName = flopName;
            DatavName = datavName;
            StartNumber = startNumber;
            CurrentNumber = currentNumber;
            Variation = variation;
            ChangeInterval = changeInterval;
            LastUpdate = DateTime.Now;
            Enabled = true;
        }

        public string Id { get; set; }
        public string FlopName { get; set; }
        public string DatavName { get; set; }
        public int StartNumber { get; set; }
        public int CurrentNumber { get; set; }
        public int Variation { get; set; }
        public int ChangeInterval { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Enabled { get; set; }

        public string Name()
        {
            return FlopName;
        }

        public override string ToString()
        {
            return $"Id:{Id}\nFlopName:{FlopName}\nDatavName:{DatavName}\nStartNumber:{StartNumber}\nCurrentNumber:{CurrentNumber}\nVariation:{Variation}\nChangeInterval:{ChangeInterval}\nEnabled:{Enabled}\n";
        }

        public void Enable(bool enable)
        {
            Enabled = enable;
        }

        public bool Same(int startNumber, int variation, int changeInterval)
        {
            return StartNumber == startNumber && Variation == variation && ChangeInterval == changeInterval;
        }

        public bool Same(IDatavObj obj)
        {
            if (obj is not Flop)
            {
                return false;
            }
            Flop flop = obj as Flop;
            return StartNumber == flop.StartNumber && Variation == flop.Variation && ChangeInterval == flop.ChangeInterval;
        }

        public bool Step()
        {
            if (Enabled && (DateTime.Now - LastUpdate).TotalMilliseconds >=ChangeInterval) 
            {
                Logger.Log($"[Flop:{FlopName}]步进");
                CurrentNumber += Variation;
                LastUpdate = DateTime.Now;
            }
            return true;
        }

        public bool Reset()
        {
            CurrentNumber = StartNumber;
            LastUpdate = DateTime.Now;
            return true;
        }
        
    }
}
