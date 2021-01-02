using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tsubasa;

namespace DatavSimulator
{
    public class SimulatorConfigure
    {
        public string Name { get; set; }
        public TimeSpan UpdateInterval { get; set; }
    }

    public class DatavSimulator
    {
        string Name { get; set; }
        TimeSpan UpdateInterval { get; set; }
        private DatavController _controller;
        Task StepTask = null;
        bool StopSignal = false;

        public DatavSimulator(string name, TimeSpan updateInterval,DatavController controller)
        {
            Init(name, updateInterval,controller);
        }
        public DatavSimulator(SimulatorConfigure configure,DatavController controller)
        {
            Init(configure,controller);
        }

        public DatavSimulator() { }

        public DatavController GetController()
        {
            return _controller;
        }

        public void Init(SimulatorConfigure configure, DatavController controller)
        {
            Name = configure.Name;
            UpdateInterval = configure.UpdateInterval;
            _controller = controller;
        }

        public void Init(string name, TimeSpan updateInterval, DatavController controller)
        {
            Name = name;
            UpdateInterval = updateInterval;
            _controller = controller;
        }

        public void Start()
        {
            if (Name==null||StepTask != null)
            {
                return;
            }
            Logger.Log("Simulator启动");
            StopSignal = false;
            StepTask = new Task(DoStep);
            StepTask.Start();
        }

        private void DoStep()
        {
            while (StopSignal == false)
            {
                Logger.Log("---------------------DataV步进---------------------");
                _controller.StepAll();
                Logger.Log("-------------------DataV步进结束-------------------");
                Thread.Sleep(UpdateInterval);
            }
            Logger.Log("Simulator停止");
            StepTask = null;
        }

        public void Stop()
        {
            StopSignal = true;
        }
    }
}
