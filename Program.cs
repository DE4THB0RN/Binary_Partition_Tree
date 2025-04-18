
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

Console.WriteLine("Arestas da BPT:");

bpt.PrintBPT();
