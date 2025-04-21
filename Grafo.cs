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

    class Vertex
    {
        public int id;
        public int cor;
        public List<Edge> arestas;

        public Vertex(int id)
        {
            this.id = id;
            arestas = new List<Edge>();
            cor = -1;
        }

        public void AdicionarAresta(int vertice, int peso)
        {
            arestas.Add(new Edge(vertice, peso));
        }
    }

    internal class Grafo
    {
        public List<Vertex> grafo;
        public MST mst;

        private int numVertices;

        public Grafo(int numVertices)
        {
            this.numVertices = numVertices;
            grafo = new List<Vertex>(numVertices);
            mst = new MST();
            for (int i = 0; i < numVertices; i++)
            {
                grafo.Add(new Vertex(i));
            }
        }

        public void AdicionarAresta(int origem, int destino, int peso)
        {
            grafo[origem].AdicionarAresta(destino,peso);
            grafo[destino].AdicionarAresta(origem, peso);
        }

        public void PrintCores()
        {
            Console.WriteLine("Cores: ");
            foreach (Vertex vertex in grafo)
            {
                Console.WriteLine("Vertice " + vertex.id + " de cor " + vertex.cor);
            }
        }

        public List<MST_Edge> ListarArestas()
        {
            List<MST_Edge> arestas = new List<MST_Edge>();
            for (int i = 0; i < numVertices; i++)
            {
                foreach (Edge aresta in grafo[i].arestas)
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

        private int Find(int[] parent, int q)
        {
            int r = q,tmp;
            while (parent[r] >= 0) r = parent[r];
            while (parent[q] >= 0)
            {
                tmp = q;
                q = parent[q];
                parent[tmp] = r;
            }
            return r;
        }

        private void swap(int[] rank, int x, int y)
        {
            int temp = rank[x];
            rank[x] = rank[y];
            rank[y] = temp;
        }

        private void Union(int[] parent,int[] rank, int x, int y)
        {
            if (rank[x] > rank[y])
                swap(rank, x, y);
            if (rank[x] == rank[y])
                rank[y]++;

            parent[x] = y;
        }

        public BPT Kruskal()
        {
            BPT bpt = new BPT(numVertices);
            
            List<MST_Edge> arestas = ListarArestas();
            Node[] raizes = new Node[numVertices];

            arestas.Sort((a, b) => a.peso.CompareTo(b.peso));

            int[] parent = new int[numVertices];
            int[] rank = new int[numVertices];

            for (int i = 0; i < numVertices; i++)
            {
                parent[i] = -1;
                rank[i] = 0;
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
                        raizes[proxima.de] = bpt.Folhas[proxima.de];
                    }

                    if (bpt.Folhas[proxima.para] == null)
                    {
                        bpt.Folhas[proxima.para] = new Node(proxima.para);
                        raizes[proxima.para] = bpt.Folhas[proxima.para];
                    }

                    nova_aresta = new Node(proxima.de, proxima.para, proxima.peso);

                    pai1 = raizes[x];
                    pai2 = raizes[y];

                    raizes[x] = nova_aresta;
                    raizes[y] = nova_aresta;

                    nova_aresta.esq = pai1;
                    nova_aresta.dir = pai2;
                    pai1.pai = nova_aresta;
                    pai2.pai = nova_aresta;

                    mst.AdicionarAresta(proxima.de, proxima.para, proxima.peso);
                    Union(parent,rank, x, y);
                    edgeCount++;
                }

            }

            bpt.Raiz = nova_aresta;

            return bpt;
        }

    }
}
