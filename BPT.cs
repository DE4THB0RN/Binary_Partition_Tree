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

        public Node(int vertice)
        {
            this.vertice = vertice;
            e_folha = true;
        }

        public Node(int de, int para, int peso)
        {
            this.de = de;
            this.para = para;
            this.peso = peso;
            e_folha = false;
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
        public int[] visitCount;

        public BPT(int numVertices)
        {
            Folhas = new Node[numVertices];
        }

        public List<MST_Edge> AdicionarSeeds(int[] seed)
        {
            List<MST_Edge> ws_cuts = new List<MST_Edge>();

            Node tmp;
            for (int i = 0; i < seed.Length; i++)
            {
                tmp = Folhas[seed[i]];
                while (tmp != Raiz && tmp.Marca != 2)
                {
                    
                    tmp = tmp.pai;
                    tmp.Marca++;

                    if (tmp.e_folha)
                    {
                        Console.WriteLine("Folha: " + tmp.GetVertice() + " visita - " + tmp.Marca);
                    }
                    else
                    {
                        Console.WriteLine("Aresta: " + tmp.GetDe() + " -> " + tmp.GetPara() + " peso - " + tmp.GetPeso() + " visita - " + tmp.Marca);
                    }

                    if (tmp.Marca == 2)
                    {
                        ws_cuts.Add(new MST_Edge(tmp.GetDe(), tmp.GetPara(), tmp.GetPeso()));
                    }
                }
            }
            
            return ws_cuts;
        }

        public List<MST_Edge> RemoverSeeds(int[] seed)
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
            }

            return ws_cuts;
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
