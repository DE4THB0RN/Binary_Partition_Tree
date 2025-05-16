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

        public void RemoverWSEdge(MST_Edge ws, Grafo MstGrafo)
        {
            Mst.Remove(ws);
            MstGrafo.RemoverAresta(ws.de, ws.para, ws.peso);
        }

        public void AdicionarWSEdge(MST_Edge ws, Grafo MstGrafo)
        {
            Mst.Add(ws);
            MstGrafo.AdicionarAresta(ws.de, ws.para, ws.peso);
        }

        public Grafo MstToGrafo(int numVertices)
        {
            Grafo MstGrafo = new Grafo(numVertices);

            foreach (MST_Edge aresta in Mst)
            {
                MstGrafo.AdicionarAresta(aresta.de, aresta.para, aresta.peso);
            }
            return MstGrafo;
        }


        public void Colorir(int vertice, int cor, bool[] visitados, Grafo grafo, Dictionary<int, int> tamCores, Grafo MstGrafo)
        {
            visitados[vertice] = true;
            grafo.grafo[vertice].cor = cor;
            tamCores[cor]++;

            for (int i = 0; i < MstGrafo.grafo[vertice].arestas.Count; i++)
            {
                if (!visitados[MstGrafo.grafo[vertice].arestas[i].vertice])
                {
                    Colorir(MstGrafo.grafo[vertice].arestas[i].vertice, cor, visitados, grafo, tamCores, MstGrafo);
                }
            }
        }

    }
}
