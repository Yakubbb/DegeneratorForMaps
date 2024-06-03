using DegeneratorForMaps.ResourcePacks;

namespace DegeneratorForMaps.Structures
{
    public abstract class AbstractStructure
    {
        public Char DefaultBlock { get; set; } = '.';
        public char[,] StructureChars { get; init; }
        public int XOffset { get; init; }
        public int YOffset { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }
        public int Depth {get;set;}
        public abstract IResourcePack textures { get; init; }
        public AbstractStructure(int width, int height, int xOffset, int yOffset,int depth)
        {
            XOffset = xOffset;
            YOffset = yOffset;
            Width = width;
            Height = height;
            Depth = depth;
            StructureChars = new char[height, width];
        }
        protected abstract void GenerateStructure();
    }
}
