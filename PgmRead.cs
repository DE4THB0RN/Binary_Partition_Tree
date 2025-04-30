namespace IC_BPT{

    internal class PgmRead{


        private static int height;
        private static int width;
        private static int max;

        static private int[][] MatrizImagem(string fileName){
            string[] linhasArq = File.ReadAllLines(fileName);
            if(linhasArq[0].Equals("P2")){
                string[] lenwidth = linhasArq[2].Split(" "); 
                height = Int32.Parse(lenwidth[0]);
                width = Int32.Parse(lenwidth[1]);

                max = Int32.Parse(linhasArq[3]);

                string[][] separado = new string[width][];
                int[][] resp = new int[width][];

                int fileLine = 4;

                for(int i = 0; i < width; i++){
                    resp[i] = new int[height];
                }

                for(int i = 0; i < width; i++){
                    for(int j = 0; j < height; j++){
                        
                        string tmp = linhasArq[fileLine].Replace('\n',' ').TrimEnd();
                        resp[i][j] = Int32.Parse(tmp);
                        fileLine++;
                    }
                }

                return resp;
            }

            Console.WriteLine("Arquivo invalido");
            return null;
        }

        public static Grafo CriarMatriz(string fileName){
            

            int[][] matriz = MatrizImagem(fileName);

            if(matriz == null) return null;

            Grafo resp = new Grafo(width * height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (i < width - 1)
                    {
                        resp.AdicionarAresta(i * height + j, (i + 1) * height + j, Math.Abs(matriz[i][j] - matriz[i + 1][j]));
                    }

                    if (j > 0)
                    {
                        resp.AdicionarAresta(i * height + j, i * height + j - 1, Math.Abs(matriz[i][j] - matriz[i][j - 1]));
                    }

                    if (j > 0 && i < width - 1)
                    {
                        resp.AdicionarAresta(i * height + j, (i + 1) * height + j - 1, Math.Abs(matriz[i][j] - matriz[i + 1][j - 1]));
                    }

                    if (j < height - 1 && i < width - 1)
                    {
                        resp.AdicionarAresta(i * height + j, (i + 1) * height + j + 1, Math.Abs(matriz[i][j] - matriz[i + 1][j + 1]));
                    }
                }
            }

            return resp;
        }


    }
}