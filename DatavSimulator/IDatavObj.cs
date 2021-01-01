using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatavSimulator
{
    public interface IDatavObj
    {
        public bool Reset();
        public void Enable(bool enable);
        public string Name();
        public bool Step();
        public bool Same(IDatavObj obj);
    }
}
