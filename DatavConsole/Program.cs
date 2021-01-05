using System;
using System.IO;
using DatavSimulator;

namespace DatavConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length<1)
            {
                Console.WriteLine("请输入正确的参数");
                return;
            }
            switch (args[0])
            {
                case "-c":
                    if (args.Length < 3)
                    {
                        Console.WriteLine("请输入正确的参数");
                        return;
                    }
                    Convert(args[1],args[2]);
                    break;
                default:
                    break;
            }
        }

        private static void Convert(string convertType, string filePath)
        {
            MapPloyArray array;
            string longlatString = LoadStringFromFile(filePath);
            switch (convertType)
            {
                case "datav":
                    array = new DatavPolyArray(longlatString);
                    break;
                case "autonavi":
                    array = new AutoNaviPolyArray(longlatString);
                    break;
                default:
                    return;
            }
            DoConvert(filePath, array);
        }

        private static void DoConvert(string orgFilePath,MapPloyArray array)
        {
            string ret = array.ToString();
            if (string.IsNullOrEmpty(ret))
            {
                Console.WriteLine("转换坐标失败");
                return;
            } 
            else
            {
                using (StreamWriter sw = new StreamWriter(orgFilePath + array.Suffix()))
                {
                    sw.WriteLine(ret);
                    sw.Flush();
                }
            }
        }

        private static string LoadStringFromFile(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
