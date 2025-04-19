
using IC_BPT;

Grafo grafo = new Grafo(8);

grafo.AdicionarAresta(0, 1, 95);
grafo.AdicionarAresta(0, 4, 0);
grafo.AdicionarAresta(1, 5, 80);
grafo.AdicionarAresta(1, 2, 120);
grafo.AdicionarAresta(2, 3, 9);
grafo.AdicionarAresta(2, 6, 125);
grafo.AdicionarAresta(3, 7, 116);
grafo.AdicionarAresta(4, 5, 15);
grafo.AdicionarAresta(5, 6, 75);
grafo.AdicionarAresta(6, 7, 0);


BPT bpt = grafo.Kruskal();


grafo.mst.PrintMST();


bpt.PrintBPT();

int control = 1;
List<int> seeds = new List<int>();
int seed;

//while (control > 0)
//{
//    Console.WriteLine("Inserir seeds: 1  -  Remover seeds: 2  -  Sair: 0");
//    control = Int32.Parse(Console.ReadLine());

//    if (control == 1)
//    {
//        Console.WriteLine("Vertices disponiveis:");
//        Console.Write("[ ");
//        for (int i = 0; i < bpt.Folhas.Length; i++)
//        {
//            if (!seeds.Contains(bpt.Folhas[i].GetVertice()))
//            {
//                Console.Write(bpt.Folhas[i].GetVertice());
//                if(i < bpt.Folhas.Length - 1) Console.Write(", ");
//            }
                
//        }
//        Console.Write(" ]");

//        Console.WriteLine("\nEscolha um vertice: ");

//        seed = Int32.Parse(Console.ReadLine());

//        if (seeds.Contains(seed))
//        {
//            Console.WriteLine("Vertice ja inserido");
//        }
//        else
//        {
//            seeds.Add(seed);
//            Console.WriteLine("Vertice " + seed + " adicionado");
//        }
//    }
//    else if (control == 2)
//    {
//        if (seeds.Count == 0)
//            Console.WriteLine("Nao ha seeds para remover");
//        else
//        {
//            Console.WriteLine("Seeds disponiveis:");
//            Console.Write("[ ");
//            for (int i = 0; i < seeds.Count; i++)
//            {
//                Console.Write(seeds[i]);
//                if (i < seeds.Count - 1) Console.Write(", ");
//            }
//            Console.Write(" ]");
//            Console.WriteLine("\nEscolha uma seed: ");

//            seed = Int32.Parse(Console.ReadLine());

//            if (seeds.Contains(seed))
//            {
//                seeds.Remove(seed);
//                Console.WriteLine("Seed " + seed + " removida");
//            }
//            else
//            {
//                Console.WriteLine("Seed nao encontrada");
//            }
//        }
            
//    }
//    else if(control > 2)
//    {
//        Console.WriteLine("Engraçadinho");
//        control = 0;
//    }
//}