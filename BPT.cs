using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IC_BPT
{
    class Node
    {
        public int Marca = 0;

        public Node dir;
        public Node esq;
        public Node pai;

        public bool e_folha;

        private int vertice;

        private int de;
        private int para;
        private int peso;
        private int cor;

        public Node(int vertice)
        {
            this.vertice = vertice;
            e_folha = true;
            cor = -1;
        }

        public Node(int de, int para, int peso)
        {
            this.de = de;
            this.para = para;
            this.peso = peso;
            e_folha = false;
            cor = -1;
        }

        public int GetVertice()
        {
            return vertice;
        }

        public int GetDe()
        {
            return de;
        }

        public int GetPara()
        {
            return para;
        }

        public int GetPeso()
        {
            return peso;
        }

        public int GetCor()
        {
            return cor;
        }

        public void SetVertice(int vertice)
        {
            this.vertice = vertice;
        }

        public void SetDe(int de)
        {
            this.de = de;
        }

        public void SetPara(int para)
        {
            this.para = para;
        }

        public void SetPeso(int peso)
        {
            this.peso = peso;
        }

        public void SetCor(int cor)
        {
            this.cor = cor;
        }

        public Node EncontrarPai()
        {
            Node tmp = this;

            while (tmp.pai != null)
            {
                tmp = tmp.pai;
            }

            return tmp;
        }
    }


    internal class BPT
    {

        public Node Raiz;
        public Node[] Folhas;
        private Dictionary<int, int> tamCores;
        private int numCores;

        public BPT(int numVertices)
        {
            Folhas = new Node[numVertices];
            numCores = 0;
            tamCores = new Dictionary<int, int>();
        }

        public List<MST_Edge> AdicionarSeeds(int[] seed, Grafo grafo)
        {

            List<MST_Edge> ws_cuts = new List<MST_Edge>();

            Node tmp;
            for (int i = 0; i < seed.Length; i++)
            {

                tmp = Folhas[seed[i]];

                tmp.SetCor(numCores);
                tamCores.Add(numCores, 0);
                tamCores[numCores]++;

                while (tmp != Raiz && tmp.Marca != 2)
                {

                    tmp = tmp.pai;
                    tmp.Marca++;

                    tmp.SetCor(numCores);

                    //if (tmp.e_folha)
                    //{
                    //    Console.WriteLine("Folha: " + tmp.GetVertice() + " visita - " + tmp.Marca);
                    //}
                    //else
                    //{
                    //    Console.WriteLine("Aresta: " + tmp.GetDe() + " -> " + tmp.GetPara() + " peso - " + tmp.GetPeso() + " visita - " + tmp.Marca);
                    //}



                    if (tmp.Marca == 2)
                    {
                        ws_cuts.Add(new MST_Edge(tmp.GetDe(), tmp.GetPara(), tmp.GetPeso()));
                    }
                }

                ColorirVertices(tmp, grafo, numCores);

                numCores++;
            }
            return ws_cuts;
        }

        public List<MST_Edge> RemoverSeeds(int[] seed, Grafo grafo)
        {
            List<MST_Edge> ws_cuts = new List<MST_Edge>();

            Node tmp;
            for (int i = 0; i < seed.Length; i++)
            {
                tmp = Folhas[seed[i]];
                while (tmp != Raiz && tmp.Marca != 1)
                {
                    tmp = tmp.pai;
                    tmp.Marca--;
                    if (tmp.Marca == 1)
                    {
                        ws_cuts.Add(new MST_Edge(tmp.GetDe(), tmp.GetPara(), tmp.GetPeso()));
                    }
                }

                RecolorirVertices(tmp, grafo);
            }

            return ws_cuts;
        }

        public void ColorirVertices(Node node, Grafo grafo, int cor)
        {
            if(node == null || (!node.e_folha && (node.GetCor() != -1 && node.GetCor() != cor))) return;

            ColorirVertices(node.esq, grafo, cor);

            if (node.e_folha)
            {
                node.SetCor(cor);
                tamCores[cor]++;
                grafo.grafo[node.GetVertice()].cor = cor;
                return;
            }
            

            ColorirVertices(node.dir, grafo, cor);

        }

        private void Recolorir(Node node, Grafo grafo, int cor, int sumir)
        {
            if(node == null || (node.GetCor() != sumir && node.GetCor() != -1)) return;

            Recolorir(node.esq, grafo, cor, sumir);

            if (node.e_folha)
            {
                node.SetCor(cor);
                tamCores[cor]++;
                grafo.grafo[node.GetVertice()].cor = cor;
                return;
            }

            node.SetCor(-1);
            
            Recolorir(node.dir, grafo, cor, sumir);
        }

        public void RecolorirVertices(Node node, Grafo grafo)
        {
            int cor1 = node.esq.GetCor();
            int cor2 = node.dir.GetCor();

            if (tamCores[cor1] > tamCores[cor2])
            {
                node.SetCor(cor1);
                tamCores.Remove(cor2);
                Recolorir(node.dir, grafo, cor1, cor2);
            }
            else
            {
                node.SetCor(cor2);
                tamCores.Remove(cor1);
                Recolorir(node.esq, grafo, cor2, cor1);
            }
        }

        public void PrintBPT()
        {
            List<List<Node>> ordenadaNivel = new List<List<Node>>();

            Console.WriteLine("BPT:");
            Queue<Node> fila = new Queue<Node>();
            fila.Enqueue(Raiz);
            int nivel = 0,len;

            while (fila.Count > 0)
            {
                len = fila.Count;
                ordenadaNivel.Add(new List<Node>());

                for (int i = 0; i < len; i++)
                {
                    Node node = fila.Dequeue();
                    ordenadaNivel[nivel].Add(node);

                    if (node.esq != null)
                    {
                        fila.Enqueue(node.esq);
                    }

                    if (node.dir != null)
                    {
                        fila.Enqueue(node.dir);
                    }
                }
                nivel++;

            }

            foreach (List<Node> level in ordenadaNivel)
            {
                Console.Write("[ ");
                for(int i = 0; i < level.Count; i++)
                {
                    if (level[i].e_folha)
                    {
                        Console.Write("Folha: " + level[i].GetVertice() + " ");
                    }
                    else
                    {
                        Console.Write("Aresta: " + level[i].GetDe() + " -> " + level[i].GetPara() + " peso - " + level[i].GetPeso() );
                    }

                    if (i < level.Count - 1)
                    {
                        Console.Write(", ");
                    }
                }
                Console.WriteLine(" ]");
            }
        }

    }
}
