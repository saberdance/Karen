using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
//using Microsoft.EntityFrameworkCore;
using DatavSimulator.DatavObjects;
using tsubasa;

namespace DatavSimulator
{
    public class Datav
    {
        //id
        public int Id { get; set; }
        //名称
        //[Required]
        public string Name { get; set; }
        //运行状态
        public Constants.Status Status { get; set; }
        //是否激活
        public bool Enable { get; set; }
        //创建时间
        public DateTime CreateTime { get; set; }
        //最后修改时间（包括状态变化）
        //[Timestamp]
        public byte[] UpdateTime { get; set; }
        //持有的翻牌器控件
        public List<Flop> Flops = new List<Flop>();

        public Datav(string name)
        {
            Name = name;
            Status = Constants.Status.running;
            CreateTime = DateTime.Now;
            Enable = true;
        }

        public static Datav Empty()
        {
            return new Datav("empty_datav");
        }

        public bool IsEmpty()
        {
            return Name == "empty_datav";
        }

        public bool Start()
        {
            switch (Status) 
            {
                case Constants.Status.running:
                    return true;
                case Constants.Status.stopped:
                case Constants.Status.paused:
                    //DatavObjects.ForEach((s) => { s.Start(); });
                    Status = Constants.Status.running;
                    return true;
                case Constants.Status.deleted:
                default:
                    return false;
            }
        }

        public bool Pause()
        {
            switch (Status) 
            {
                case Constants.Status.running:
                    //Flops.ForEach((s) => { s.Pause(); });
                    Status = Constants.Status.paused;
                    return true;
                case Constants.Status.paused:
                    return true;
                case Constants.Status.stopped:
                case Constants.Status.deleted:
                default:
                    return false;
            }
        }

        public bool Stop()
        {
            switch (Status) 
            {
                case Constants.Status.running:
                case Constants.Status.paused:
                    Status = Constants.Status.stopped;
                    return true;
                case Constants.Status.stopped:
                    return true;
                case Constants.Status.deleted:
                default:
                    return false;
            }
        }

        public bool Step()
        {
            if (Status == Constants.Status.running)
            {
                Logger.Log($"[Datav:{Name}]步进");
                Flops.ForEach((s) => { s.Step(); });
            }
            return true;
        }

        public bool UpdateObj(IDatavObj obj)
        {
            switch (obj)
            {
                case Flop:
                    return UpdateFlop(obj);
                default:
                    return false;
            }
        }

        public Flop GetFlop(string flopName)
        {
            var existFlops = Flops.Where(p => p.Name() == flopName);
            if (existFlops.Any())
            {
                return existFlops.First();
            }
            else
            {
                return null;
            }
        }

        private bool UpdateFlop(IDatavObj obj)
        {
            Flop flop = obj as Flop;
            var existFlops = Flops.Where(p => p.Name() == flop.Name());
            if (existFlops.Any())
            {
                Flop existFlop = existFlops.First();
                if (existFlop.Same(flop))
                {
                    return false;
                }
                else
                {
                    Flops.Remove(existFlop);
                    Flops.Add(flop);
                    return true;
                }
            }
            else
            {
                Flops.Add(flop);
                return true;
            }
        }

        public bool NewObj(IDatavObj obj)
        {
            switch (obj)
            {
                case Flop:
                    return AddFlop(obj);
                default:
                    return false;
            }
        }

        public bool RemoveObj(string objName)
        {
            Flops.Remove(Flops.FirstOrDefault(p=>p.Name() == objName));
            return true;
        }


        private bool AddFlop(IDatavObj obj)
        {
            Flop flop = obj as Flop;
            var existFlops = Flops.Where(p => p.Name() == flop.Name());
            if (existFlops.Any())
            {
                return false;
            }
            else
            {
                Flops.Add(flop);
                return true;
            }
        }
    }
}