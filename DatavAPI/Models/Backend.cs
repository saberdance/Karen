using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatavSimulator;

namespace DatavAPI.Models
{
    public class Backend
    {
        private static readonly Backend _instance = new Backend();
        DatavSimulator.DatavSimulator _simulator;
        DatavController _controller;
        DatavContext _context;
        private bool _init; 

        static Backend()
        {
        }

        private Backend()
        {
        }

        public static Backend Instance
        {
            get
            {
                return _instance;
            }
        }

        public DatavSimulator.DatavSimulator Simulator()
        {
            return _simulator;
        }

        public DatavController Controller()
        {
            return _controller;
        }

        public DatavContext Context()
        {
            return _context;
        }

        public bool Init(string name, TimeSpan stepInterval)
        {
            if (_init)
            {
                return false;
            }
            _context = new DatavContext();
            _controller = new DatavController(_context);
            _simulator = new DatavSimulator.DatavSimulator(name, stepInterval, _controller);
            _init = true;
            return true;
        }
    }
}
