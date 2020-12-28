﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatavSimulator.DatavObjects;

namespace DatavSimulator
{
    public class Datav
    {
        //id
        public int Id { get; set; }
        //名称
        [Required]
        public string Name { get; set; }
        //运行状态
        public Constants.Status Status { get; set; }
        //是否激活
        public bool Enable { get; set; }
        //创建时间
        public DateTime CreateTime { get; set; }
        //最后修改时间（包括状态变化）
        [Timestamp]
        public byte[] UpdateTime { get; set; }
        //持有的翻牌器控件
        List<Flop> Flops { get; set; }

        public Datav(string name)
        {
            Name = name;
            Status = Constants.Status.stopped;
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
            switch (Status) {
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
            switch (Status) {
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
            switch (Status) {
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

        public bool Update()
        {
            if (Status == Constants.Status.running)
                Flops.ForEach((s) => { s.Update(); });
            return true;
        }
    }
}