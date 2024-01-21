using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Magazin
{
    public class Flower
    {

        public int Id { get; set; }
        public double Art { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EdIzn { get; set; }
        public double Prince { get; set; }
        public double Quality { get; set; }
        public double Summa { get; set; }
        public string Img { get; set; }
        public string ImgName { get; set; }
        public Flower(string line)
        {
            string newline = line.Replace(" ", "");
            string[] items = newline.Split(';');
            Id = int.Parse(items[0]);
            Art = double.Parse(items[1]);
            Name = items[2];
            Description = items[3];
            EdIzn = items[4];
            Prince = double.Parse(items[5]);
            Quality = double.Parse(items[6]);
            Summa = double.Parse(items[7]);
            Img = items[8];

        }

        public static Flower FromLine(string line)
        {
            return new Flower(line);
        }
    }
    public class flowerList
    {
        public List<Flower> list = new List<Flower>();

        public void LoadFromFile(string filename)

        {
            list = File.ReadAllLines(filename, Encoding.Default)
                     .Skip(0)
                     .Select(Flower.FromLine)
                     .ToList();
        }
        public void LoadStr(string str)

        {
            list.Add(new Flower(str));
        }
        public void RemoveStr(string str)

        {
            //list.Remove(???);// как удалить 
           


        }

    }
}
