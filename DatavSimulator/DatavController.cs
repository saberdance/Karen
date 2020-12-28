using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatavSimulator
{
    class DatavController
    {
        private readonly DatavContext _context;

        public DatavController(DatavContext context) => _context = context;

        public Datav GetDatav(string name)
        {
            var datavs = _context.Datavs.Where(p => p.Name == name);
            if (!datavs.Any()) {
                return Datav.Empty();
            } else {
                return datavs.First();
            }
        }

        public bool NewDatav(string name)
        {
            if (!GetDatav(name).IsEmpty()) {
                return false;
            }
            _context.Datavs.Add(new Datav(name));
            return true;
        }

        public bool RemoveDatav(string name)
        {
            if (GetDatav(name).IsEmpty()) {
                return false;
            }
            _context.Datavs.RemoveRange(_context.Datavs.Where(p => p.Name == name));
            return true;
        }

        public bool UpdateAll()
        {
            var datavs = _context.Datavs.Where(p=>p.Status == Constants.Status.running);
            foreach (var datav in datavs) {
                datav.Update();
            }
            return true;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
