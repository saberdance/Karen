using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatavAPI.Models
{
    public class FlopReturnValue
    {
        public FlopReturnValue(string Name,int Value)
        {
            name = Name;
            value = Value;
        }

        public string name { get; set; }
        public int value { get; set; }

        public string toJsonString()
        {
            return $"[{{\"name\": \"{name}\",\"value\": {value}}}]";
        }
    }
}
