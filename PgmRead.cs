using System.Text;

namespace IC_BPT
{

    internal class PgmRead
    {


        private static int height;
        private static int width;
        private static int max;

        static private int[][] MatrizImagem(string fileName)
        {

            const Int32 BufferSize = 128;
            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.ASCII, true, BufferSize))
            {
                Console.WriteLine("Abrindo stream");
                String line;
                line = streamReader.ReadLine();
                if (!line.Equals("P2"))
                {
                    Console.WriteLine("Arquivo invalido");
                    return null;
                }
                line = streamReader.ReadLine();
                line = streamReader.ReadLine();
                string[] lenwidth = line.Split(" ");

                height = Int32.Parse(lenwidth[0]);
                width = Int32.Parse(lenwidth[1]);

                line = streamReader.ReadLine();

                max = Int32.Parse(line);

                int[][] resp = new int[width][];

                for (int i = 0; i < width; i++)
                {
                    resp[i] = new int[height];
                }

                int lin = 0, alt = 0;
                Console.WriteLine("Começando leitura");
                while ((line = streamReader.ReadLine()) != null)
                {
                    resp[lin][alt] = Int32.Parse(line);
                    alt++;
                    if (alt == height)
                    {
                        alt = 0;
                        lin++;
                    }
                }

                Console.WriteLine("Criei a matriz");

                return resp;
            }

        }

        public static Grafo CriarMatriz(string fileName)
        {

            Console.WriteLine("Trace On");

            int[][] matriz = MatrizImagem(fileName);
            Console.WriteLine("Peguei a matriz");

            Grafo resp = new(width * height);
            int alt = 0, lin = 0;

            Console.WriteLine("Hora de criar o grafo");


            for (int caminhamento = 0; caminhamento < width * height; caminhamento++)
            {
                if (lin < width - 1)
                {
                    resp.AdicionarAresta(lin * height + alt, (lin + 1) * height + alt, Math.Abs(matriz[lin][alt] - matriz[lin + 1][alt]));
                }

                if (alt > 0)
                {
                    resp.AdicionarAresta(lin * height + alt, lin * height + alt - 1, Math.Abs(matriz[lin][alt] - matriz[lin][alt - 1]));
                }

                if (alt > 0 && lin < width - 1)
                {
                    resp.AdicionarAresta(lin * height + alt, (lin + 1) * height + alt - 1, Math.Abs(matriz[lin][alt] - matriz[lin + 1][alt - 1]));
                }

                if (alt < height - 1 && lin < width - 1)
                {
                    resp.AdicionarAresta(lin * height + alt, (lin + 1) * height + alt + 1, Math.Abs(matriz[lin][alt] - matriz[lin + 1][alt + 1]));
                }

                alt++;
                if (alt == height)
                {
                    alt = 0;
                    lin++;
                }
            }

            Console.WriteLine("Grafo criado");

            return resp;
        }


    }
}