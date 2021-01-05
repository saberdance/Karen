using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatavSimulator
{
    public abstract class MapPloyArray
    {
        public List<DatavPoint> Points = null;

        public MapPloyArray() => Points ??= new List<DatavPoint>();
        public MapPloyArray(string lonlatString)
        {
            Points ??= new List<DatavPoint>();
            var lonlatList = lonlatString.Split(",");
            if (lonlatList.Length % 2 != 0)
            {
                return;
            }
            for (int i = 0; i < lonlatList.Length; i += 2)
            {
                DatavPoint point = new DatavPoint(lonlatList[i], lonlatList[i + 1]);
                Points.Add(point);
            }
        }
        public override abstract string ToString();
        public abstract string Suffix();

    }
    public class DatavPolyArray : MapPloyArray
    {
        public DatavPolyArray(string lonlatString) : base(lonlatString) { }
        public override string ToString()
        {
            string ret = "";
            for (int i = 0; i < Points.Count; i++)
            {
                string onePointStr = $"[\n{Points[i].Lon},\n{Points[i].Lat}\n],\n";
                ret += onePointStr;
            }
            return ret;
        }
        public override string Suffix() => "_datav";
    }

    public class AutoNaviPolyArray : MapPloyArray
    {
        public AutoNaviPolyArray(string lonlatString) : base(lonlatString) { }
        public override string ToString()
        {
            string ret = "";
            for (int i = 0; i < Points.Count; i++)
            {
                string onePointStr = $"[{Points[i].Lon},{Points[i].Lat}],\n";
                ret += onePointStr;
            }
            return ret;
        }
        public override string Suffix() => "_autonavi";
    }

    public class DatavPoint
    {
        public string Lon;
        public string Lat;

        public DatavPoint(string lon, string lat)
        {
            Lon = lon;
            Lat = lat;
        }
    }

    public class MapPoly
    {
    }
}
