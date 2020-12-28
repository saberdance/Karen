using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatavSimulator
{
    interface IDatavObj
    {
        public bool Reset();
        public void Enable(bool enable);
        public string Name();
        public bool Update();
    }
}
