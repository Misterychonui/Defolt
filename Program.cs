using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections;

namespace Defolt
{
    class Program
    {
        static void Main(string[] args)
        {
            string text;
            using (var sr = new StreamReader("input.txt"))
            {
                text = sr.ReadToEnd();
            }
            var separators = new char[] { ' ', '\r', '\n' };
            var words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            int n = int.Parse(words[0]);
            int m = int.Parse(words[1]);
            int k = int.Parse(words[2]);
            var list = new List<int>();
            for (int i = 3; i < words.Length; i += 2)
            {
                list.Add(int.Parse(words[i]));
                list.Add(int.Parse(words[i + 1]));
            }
            Algoritm alg = new Algoritm();
            string s = alg.FindMin(list, n, m).ToString();
            
            using (var sw = new StreamWriter("output.txt"))
            {
                sw.WriteLine(s);
            }
        }
    }
    class Algoritm
    {
        public int result;
        public void FillTable(List<int> protectedCells, Queue<Coordenates> newPeak, Queue<Coordenates> oldPeak)
        {
            for (int i = 0; i < protectedCells.Count(); i += 2)
            {
                oldPeak.Enqueue(new Coordenates(protectedCells[i] - 1, protectedCells[i + 1] - 1));
                newPeak.Enqueue(new Coordenates(protectedCells[i] - 1, protectedCells[i + 1] - 1));
            }
        }
        public int FindMin(List<int> list, int n, int m)
        {
            var newPeak = new Queue<Coordenates>();
            var oldPeak = new Queue<Coordenates>();
            FillTable(list, newPeak, oldPeak);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    oldPeak.Enqueue(new Coordenates(i, j));
                    newPeak.Enqueue(new Coordenates(i, j));
                    
                }
            }
            while (oldPeak.Count != n * m)
            {
                int h = newPeak.Count();
                for (int i = 0; i < h; i++)
                {
                    var tekPeak = newPeak.Dequeue();
                    foreach (var peak in Find(tekPeak.x, tekPeak.y, n, m))
                    {
                        if (!oldPeak.Contains(peak) && !newPeak.Contains(peak))
                        {
                            newPeak.Enqueue(peak);
                            oldPeak.Enqueue(peak);
                        }
                    }
                }
                result++;
            }
            return result;
        }
        public List<Coordenates> Find(int x, int y, int n, int m)
        {
            List<Coordenates> list = new List<Coordenates>();
            if (x - 1 >= 0)
            {
                list.Add(new Coordenates(x - 1, y));
            }

            if (x + 1 < n)
            {
                list.Add(new Coordenates(x + 1, y));
            }

            if (y - 1 >= 0 )
            {
                list.Add(new Coordenates(x, y - 1));
            }

            if (y + 1 < m)
            {
                list.Add(new Coordenates(x, y + 1));
            }

            return list;
        }
    }
    struct Coordenates
    {
        public int x;
        public int y;

        public Coordenates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}