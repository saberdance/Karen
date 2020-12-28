using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DatavSimulator;

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

        string Id { get; set; }
        string FlopName { get; set; }
        string DatavName { get; set; }
        int StartNumber { get; set; }
        int CurrentNumber { get; set; }
        int Variation { get; set; }
        int ChangeInterval { get; set; }
        DateTime LastUpdate { get; set; }
        bool Enabled { get; set; }

        public string Name()
        {
            return FlopName;
        }

        public void Enable(bool enable)
        {
            Enabled = enable;
        }

        public bool Same(int startNumber, int variation, int changeInterval)
        {
            return StartNumber == startNumber && Variation == variation && ChangeInterval == changeInterval;
        }

        public bool Update()
        {
            if (Enabled && (DateTime.Now - LastUpdate).TotalMilliseconds >=ChangeInterval) {
                CurrentNumber += ChangeInterval;
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
