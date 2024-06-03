using DegeneratorForMaps.ResourcePacks;
using DegeneratorForMaps.Structures;

namespace DegeneratorForMaps
{
    public class Map
    {
        public void DebugShowChunks()
        {
            foreach(var i in chunks)
            {
                Console.WriteLine(i);
            }
        }
        public static void DebugDrawMap(Map map)
        {
            DebugDrawMap(map.Field);
        }
        public static void DebugDrawMap(Char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }
        private List<(int y, int x)> chunks = new();
        public char[,] Field { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }

        private int chunkWidth;
        private int chunkHeight;

        public Map(int width, int height, int chunkWidth, int ChunkHeight)
        {
            this.Field = new char[height, width];
            this.Width = width;
            this.Height = height;

            this.chunkWidth = chunkWidth;
            this.chunkHeight = ChunkHeight;


            GenerateChunks();
            CalculateAndDrawChunks();
        }
        public static Map CreateMapWithRandomChunks(int width, int height) => new Map(width, height, Random.Shared.Next(height / 2), Random.Shared.Next(width / 2));
        public Map(int width, int height) : this(width, height, width / 4, height / 4) { }
        public Map():this(100,100) { }
        private void GenerateChunks()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        chunks.Add((i, j));
                    }
                    (int y, int x) lastChunk = chunks[chunks.Count - 1];
                    if (i - lastChunk.y >= chunkHeight | j - lastChunk.x >= chunkWidth)
                    {
                        chunks.Add((i, j));
                    }

                }
            }
        }
        private void CalculateAndDrawChunks()
        {
            List<AbstractStructure> structures = new();
            foreach(var i in chunks)
            {
                structures.Add(new Mountains(chunkWidth, chunkHeight, i.x, i.y, 1000));
            }
            foreach(var someStruct in structures)
            {
                for(int i = 0; i < someStruct.Height; i++)
                {
                    for(int j =0; j < someStruct.Width; j++)
                    {
                        Field[someStruct.YOffset + i, someStruct.XOffset + j] = someStruct.StructureChars[i, j];
                    }
                }
            }

        }

    }
}
