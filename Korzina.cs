using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazin
{
    public class Korzina
    {
        public int Art { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Vid { get; set; }
        public int prince { get; set; }
        public int quality { get; set; }
        public int amount { get; set; }

        public Korzina(string line)
        {
            string newline = line.Replace(" ", "");
            string[] items = newline.Split(';');
            Art = int.Parse(items[0]);
            Name = items[1];
            Color = items[2];
            Vid = items[3];
            prince = int.Parse(items[4]);
            quality = int.Parse(items[5]);
            amount = int.Parse(items[6]);
        }

        public static Flower FromLine(string line)
        {
            return new Flower(line);
        }
    }
    public class korzinaList
    {
        public List<Korzina> list1 = new List<Korzina>();

        public void LoadStr1(string str2)

        {
            list1.Add(new Korzina(str2));
        }
      
    }
}
