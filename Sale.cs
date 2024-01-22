using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazin
{
    public class Sale
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
        public Sale(string line)
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
            Img= items[8];
        }

        public static Flower FromLine(string line)
        {
            return new Flower(line);
        }
    }
    public class korzinaList
    {
        public List<Sale> list1 = new List<Sale>();
        public void LoadStr1(string str2)
        {
            list1.Add(new Sale(str2));
        }

        public void AdStr(String str)

        {
            Sale inp = new Sale(str);
            for (int i = 0; i < list1.Count; i++)
                if (list1[i].Id == inp.Id)
                {
                    list1[i].Quality += inp.Quality;
                    list1[i].Summa += inp.Summa;
                    return;
                }
            list1.Add(inp);
        }

        public void ClearList()

        {
            list1.Clear();
        }
        public void DelStr(String str)

        {
            Sale inp = new Sale(str);
            for (int i = 0; i < list1.Count; i++)
                if (list1[i].Id == inp.Id)
                {
                    list1[i].Quality -= inp.Quality;
                    list1[i].Summa -= inp.Summa;
                    if (list1[i].Quality == 0)
                    {
                        list1.Remove(list1[i]);
                    }
                    return;
                }
        }
        public long CalcQuality()
        {
            return (long)list1.Sum((c) => c.Quality);
        }
        public void RemoveStr(string str)

        {
            for (int i = 0; i < list1.Count; i++)
                if (list1[i].Id.ToString() == str)
                {
                    list1.Remove(list1[i]);
                    return;
                }
        }
    }
}
