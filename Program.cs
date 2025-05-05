
using IC_BPT;

// Grafo grafo = new Grafo(8);

// grafo.AdicionarAresta(0, 1, 2);
// grafo.AdicionarAresta(0, 4, 0);
// grafo.AdicionarAresta(1, 5, 5);
// grafo.AdicionarAresta(1, 2, 4);
// grafo.AdicionarAresta(2, 3, 0);
// grafo.AdicionarAresta(2, 6, 6);
// grafo.AdicionarAresta(3, 7, 1);
// grafo.AdicionarAresta(4, 5, 1);
// grafo.AdicionarAresta(5, 6, 6);
// grafo.AdicionarAresta(6, 7, 0);

Grafo grafo = PgmRead.CriarMatriz("../../../teste1.pgm");
MST mst = new MST();
BPT bpt = grafo.Kruskal(mst);
Grafo MstGrafo = mst.MstToGrafo(grafo.Tamanho());


//grafo.mst.PrintMST();

//bpt.PrintBPT();

int control = 1;
List<int> seeds = new List<int>();
List<int> seedsAtuais = new List<int>();
List<int> seedsRemovidas = new List<int>();
int seed;

while (control > 0)
{
    Console.WriteLine("Inserir seeds: 1  -  Remover seeds: 2  - Processar WS(adição): 3 - Processar WS(remoção): 4 - Printar cores: 5 - Sair: 0");
    control = Int32.Parse(Console.ReadLine());

    if (control == 1)
    {
        Console.WriteLine("Vertices disponiveis:");
        Console.Write("[ ");
        for (int i = 0; i < bpt.Folhas.Length; i++)
        {
            if (!seeds.Contains(bpt.Folhas[i].GetVertice()) && !seedsAtuais.Contains(bpt.Folhas[i].GetVertice()))
            {
                Console.Write(bpt.Folhas[i].GetVertice());
                if (i < bpt.Folhas.Length - 1) Console.Write(", ");
            }

        }
        Console.Write(" ]");

        Console.WriteLine("\nEscolha um vertice: ");

        seed = Int32.Parse(Console.ReadLine());

        if (seeds.Contains(seed) || seedsAtuais.Contains(seed))
        {
            Console.WriteLine("Vertice ja inserido");
        }
        else
        {
            seedsAtuais.Add(seed);
            Console.WriteLine("Vertice " + seed + " adicionado");
        }
    }
    else if (control == 2)
    {
        if (seeds.Count == 0)
            Console.WriteLine("Nao ha seeds para remover");
        else
        {
            Console.WriteLine("Seeds disponiveis:");
            Console.Write("[ ");
            for (int i = 0; i < seeds.Count; i++)
            {
                Console.Write(seeds[i]);
                if (i < seeds.Count - 1) Console.Write(", ");
            }
            Console.Write(" ]");
            Console.WriteLine("\nEscolha uma seed: ");

            seed = Int32.Parse(Console.ReadLine());

            if (seeds.Contains(seed))
            {
                seeds.Remove(seed);
                seedsRemovidas.Add(seed);
                Console.WriteLine("Seed " + seed + " removida");
            }
            else
            {
                Console.WriteLine("Seed nao encontrada");
            }
        }

    }
    else if (control == 3)
    {
        MST_Edge[] ws_cuts = bpt.AdicionarSeeds(seedsAtuais.ToArray(), grafo, mst, MstGrafo
    ).ToArray();
        foreach (int i in seedsAtuais)
        {
            seeds.Add(i);
        }
        for (int i = 0; i < ws_cuts.Length; i++)
        {
            Console.WriteLine("Aresta: " + ws_cuts[i].para + " - " + ws_cuts[i].de + " peso - " + ws_cuts[i].peso);
        }
        seedsAtuais.Clear();
    }
    else if (control == 4)
    {
        MST_Edge[] ws_cuts = bpt.RemoverSeeds(seedsRemovidas.ToArray(), grafo, mst, MstGrafo
    ).ToArray();
        for (int i = 0; i < ws_cuts.Length; i++)
        {
            Console.WriteLine("Aresta: " + ws_cuts[i].para + " - " + ws_cuts[i].de + " peso - " + ws_cuts[i].peso);
        }
        seedsRemovidas.Clear();
    }
    else if (control == 5)
    {
        grafo.PrintCores();
    }
    else if (control == 0)
    {
        Console.WriteLine("Saindo...");
    }
    else
    {
        control = -1;
        Console.WriteLine("Engraçadinho");
    }
}
