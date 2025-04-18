using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IC_BPT
{
    class Edge
    {
        public int vertice;
        public int peso;

        public Edge(int vertice, int peso)
        {
            this.vertice = vertice;
            this.peso = peso;
        }
    }
    internal class Grafo
    {
        public List<List<Edge>> grafo;
        private MST mst;

        private int numVertices;

        public Grafo(int numVertices)
        {
            this.numVertices = numVertices;
            grafo = new List<List<Edge>>(numVertices);
            mst = new MST();
            for (int i = 0; i < numVertices; i++)
            {
                grafo.Add(new List<Edge>());
            }
        }

        public void AdicionarAresta(int origem, int destino, int peso)
        {
            grafo[origem].Add(new Edge(destino, peso));
            grafo[destino].Add(new Edge(origem, peso));
        }

        public List<MST_Edge> ListarArestas()
        {
            List<MST_Edge> arestas = new List<MST_Edge>();
            for (int i = 0; i < numVertices; i++)
            {
                foreach (Edge aresta in grafo[i])
                {
                    arestas.Add(new MST_Edge(i, aresta.vertice, aresta.peso));
                }
            }

            return arestas;
        }

        public int Tamanho()
        {
            return numVertices;
        }

        private int Find(int[] parent, int i)
        {
            if (parent[i] == -1)
                return i;
            return Find(parent, parent[i]);
        }

        private void Union(int[] parent, int x, int y)
        {
            int xset = Find(parent, x);
            int yset = Find(parent, y);

            parent[xset] = yset;
        }

        public BPT Kruskal()
        {
            BPT bpt = new BPT(numVertices);
            
            List<MST_Edge> arestas = ListarArestas();

            arestas.Sort((a, b) => a.peso.CompareTo(b.peso));

            int[] parent = new int[numVertices];

            for(int i = 0; i < numVertices; i++)
            {
                parent[i] = -1;
            }

            int edgeCount = 0;
            int index = 0;
            MST_Edge proxima;
            int x, y;
            Node nova_aresta = new Node(-1), pai1, pai2;

            while (edgeCount < numVertices - 1)
            {
                proxima = arestas[index++];

                x = Find(parent, proxima.de);
                y = Find(parent, proxima.para);

                if (x != y)
                {
                    if (bpt.Folhas[proxima.de] == null)
                    {
                        bpt.Folhas[proxima.de] = new Node(proxima.de);
                    }

                    if (bpt.Folhas[proxima.para] == null)
                    {
                        bpt.Folhas[proxima.para] = new Node(proxima.para);
                    }


                    nova_aresta = new Node(proxima.de, proxima.para, proxima.peso);
                    pai1 = bpt.Folhas[proxima.de].EncontrarPai();
                    pai2 = bpt.Folhas[proxima.para].EncontrarPai();

                    nova_aresta.esq = pai1;
                    nova_aresta.dir = pai2;
                    pai1.pai = nova_aresta;
                    pai2.pai = nova_aresta;


                    mst.AdicionarAresta(proxima.de, proxima.para, proxima.peso);
                    Union(parent, x, y);
                    edgeCount++;
                }

            }

            bpt.Raiz = nova_aresta;

            return bpt;
        }

    }
}
