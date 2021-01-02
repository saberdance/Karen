using DatavSimulator.DatavObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DatavSimulator
{
    public class DatavController
    {
        private readonly DatavContext _context;

        public DatavController(DatavContext context) => _context = context;

        #region Datav相关函数

        public Datav GetDatav(string name)
        {
            var datavs = _context.Datavs.Where(p => p.Name == name);
            if (!datavs.Any()) 
            {
                return Datav.Empty();
            } 
            else 
            {
                return datavs.First();
            }
        }

        public List<Datav> GetRunningDatavs()
        {
            return _context.Datavs.Where(p => p.Status == Constants.Status.running).ToList();
        }

        public List<Datav> GetAllDatavs()
        {
            return _context.Datavs.Where(p => p.Status != Constants.Status.deleted).ToList();
        }

        public List<Datav> GetPausedDatavs()
        {
            return _context.Datavs.Where(p => p.Status == Constants.Status.paused).ToList();
        }

        public List<Datav> GetStoppedDatavs()
        {
            return _context.Datavs.Where(p => p.Status == Constants.Status.stopped).ToList();
        }

        public bool NewDatav(string name)
        {
            if (!GetDatav(name).IsEmpty()) 
            {
                return false;
            }
            _context.Datavs.Add(new Datav(name));
            return true;
        }

        public bool RemoveDatav(string name)
        {
            if (GetDatav(name).IsEmpty()) 
            {
                return false;
            }
            //_context.Datavs.RemoveRange(_context.Datavs.Where(p => p.Name == name));
            _context.Datavs.Remove(_context.Datavs.First(p => p.Name == name));
            return true;
        }

        public bool StepAll()
        {
            var datavs = _context.Datavs.Where(p=>p.Status == Constants.Status.running);
            foreach (var datav in datavs) 
            {
                datav.Step();
            }
            return true;
        }

        public bool Stop(string name)
        {
            var datav = GetDatav(name);
            if (datav.IsEmpty())
            {
                return false;
            }
            return datav.Stop();
        }

        public bool StopAll()
        {
            var datavs = _context.Datavs.Where(p => (p.Status == Constants.Status.running)||p.Status == Constants.Status.paused);
            foreach (var datav in datavs)
            {
                datav.Stop();
            }
            return true;
        }

        #endregion

        #region Obj相关函数

        public bool NewObj(string datavName, IDatavObj datavObj)
        {
            var datav = GetDatav(datavName);
            if (datav.IsEmpty()) 
            {
                return false;
            }
            return datav.NewObj(datavObj);
        }

        public bool RemoveObj(string datavName, string datavObjName)
        {
            var datav = GetDatav(datavName);
            if (datav.IsEmpty())
            {
                return false;
            }
            return datav.RemoveObj(datavObjName);
        }

        public bool UpdateObj(string datavName, IDatavObj datavObj)
        {
            var datav = GetDatav(datavName);
            if (datav.IsEmpty())
            {
                return false;
            }
            return datav.UpdateObj(datavObj);
        }

        public Flop GetFlop(string datavName, string flopName)
        {
            var datav = GetDatav(datavName);
            if (datav.IsEmpty())
            {
                return null;
            }
            return datav.GetFlop(flopName);
        }

        #endregion

        //public void SaveChanges()
        //{
        //    _context.SaveChanges();
        //}
    }
}
