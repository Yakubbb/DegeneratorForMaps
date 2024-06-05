using DegeneratorForMaps.ResourcePacks;

namespace DegeneratorForMaps.Structures
{
    public abstract class Structure
    {
        public static  Char DefaultBlock { get; set; } = '.';
        public char[,] StructureField { get; init; }
        public int XOffset { get; init; }
        public int YOffset { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }
        public int Depth {get;set;}
        public abstract IResourcePack textures { get; init; }
        public Structure(int width, int height,int xOffset,int yOffset) : this(width,height,xOffset,yOffset,10) { }
        public Structure(int width, int height, int depth) : this(width, height, 0, 0, depth) { }
        public Structure(int width, int height, int xOffset, int yOffset,int depth)
        {
            XOffset = xOffset;
            YOffset = yOffset;
            Width = width;
            Height = height;
            Depth = depth;
            StructureField = new char[height, width];
            randomStructureSpot.x = Random.Shared.Next(0, width);
            randomStructureSpot.y = Random.Shared.Next(0, height);
        }
        protected (int x, int y) randomStructureSpot;
        protected int DistanceValue(int i, int j)
        {
            var height = Math.Abs(randomStructureSpot.y - i);
            var width = Math.Abs(randomStructureSpot.x - j);
            return height + width;
        }
        protected bool BlockIsNull(char b )=>b=='\0';
        protected bool IsCharClear(char c) => !(textures.CantGoThrough.Contains(c) || textures.CanGoThrough.Contains(c));
        protected bool IsCharClearFromWalls(char c) => !textures.CantGoThrough.Contains(c);
        protected bool ISCharCLearFromFloor(char c) => !textures.CanGoThrough.Contains(c);
        protected bool IsSurroundedWithFloor(int i, int j)
        {
            var up = i != 0 && !ISCharCLearFromFloor(StructureField[i - 1, j]);
            var down = i != Height - 1 && !ISCharCLearFromFloor(StructureField[i + 1, j]);
            var left = j != 0 && !ISCharCLearFromFloor(StructureField[i, j - 1]);
            var right = j != Width - 1 && !ISCharCLearFromFloor(StructureField[i, j + 1]);

            return up && down && left && right;
        }
        protected bool IsSurroundedWithWalls(int i, int j)
        {

            var up = i != 0 && !IsCharClearFromWalls(StructureField[i - 1, j]);
            var down = i != Height - 1 && !IsCharClearFromWalls(StructureField[i + 1, j]);
            var left = j != 0 && !IsCharClearFromWalls(StructureField[i, j - 1]);
            var right = j != Width - 1 && !IsCharClearFromWalls(StructureField[i, j + 1]);

            return up && down && left && right;
        }
        protected bool IsSurrounded(int i, int j)
        {

            var up = i != 0 && !IsCharClear(StructureField[i - 1, j]);
            var down = i != Height - 1 && !IsCharClear(StructureField[i + 1, j]);
            var left = j != 0 && !IsCharClear(StructureField[i, j - 1]);
            var right = j != Width - 1 && !IsCharClear(StructureField[i, j + 1]);

            return up && down && left && right;
        }
        protected bool IsSemiSurrounded(int i, int j)
        {
            List<bool> list = new();
            list.Add(i != 0 && !IsCharClear(StructureField[i - 1, j]));
            list.Add(i != Height - 1 && !IsCharClear(StructureField[i + 1, j]));
            list.Add(j != 0 && !IsCharClear(StructureField[i, j - 1]));
            list.Add(j != Width - 1 && !IsCharClear(StructureField[i, j + 1]));

            return list.Where(x => x == true).Count() >= 3;
        }
        protected bool AreBlocksNearby(int i, int j)
        {
            List<bool> list = new();
            list.Add(i != 0 && !IsCharClear(StructureField[i - 1, j]));
            list.Add(i != Height - 1 && !IsCharClear(StructureField[i + 1, j]));
            list.Add(j != 0 && !IsCharClear(StructureField[i, j - 1]));
            list.Add(j != Width - 1 && !IsCharClear(StructureField[i, j + 1]));

            return list.Where(x => x == true).Count() >= 1;
        }
        protected abstract void GenerateStructure();
    }
}
