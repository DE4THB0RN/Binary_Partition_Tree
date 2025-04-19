using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IC_BPT
{
    class MST_Edge
    {
        public int de;
        public int para;
        public int peso;

        public MST_Edge(int de, int para, int peso)
        {
            this.de = de;
            this.para = para;
            this.peso = peso;
        }
    }

    internal class MST
    {
        public List<MST_Edge> Mst;

        public MST()
        {
            Mst = new List<MST_Edge>();
        }

        public void AdicionarAresta(int de, int para, int peso)
        {
            Mst.Add(new MST_Edge(de, para, peso));
        }

        public void PrintMST()
        {
            foreach (MST_Edge aresta in Mst)
            {
                Console.WriteLine($"Aresta: {aresta.de} - {aresta.para} Peso: {aresta.peso}");
            }
        }

    }
}
