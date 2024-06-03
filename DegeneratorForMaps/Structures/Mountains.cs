using DegeneratorForMaps.ResourcePacks;

namespace DegeneratorForMaps.Structures
{
    public class Mountains : AbstractStructure
    {
        public Mountains(int width, int height, int xOffset, int yOffset, int depth) : base(width, height, xOffset, yOffset, depth)
        {
            randomMountainSpot.x = Random.Shared.Next(0, width);
            randomMountainSpot.y = Random.Shared.Next(0, height);
            GenerateStructure();
        }
        private int DistanceValue(int i, int j)
        {

            return Math.Abs(i - randomMountainSpot.y + j - randomMountainSpot.x);
        }
        private bool IsCharClear(char c) => !textures.CantGoThrough.Contains(c);
        private bool IsSurrounded(int i, int j)
        {

            var up = i != 0 && !IsCharClear(StructureChars[i - 1, j]);
            var down = i != Height - 1 && !IsCharClear(StructureChars[i + 1, j]);
            var left = j != 0 && !IsCharClear(StructureChars[i, j - 1]);
            var right = j != Width - 1 && !IsCharClear(StructureChars[i, j + 1]);

            return up && down && left && right;
        }
        private bool IsSemiSurrounded(int i, int j)
        {
            List<bool> list = new();
            list.Add(i != 0 && !IsCharClear(StructureChars[i - 1, j]));
            list.Add(i != Height - 1 && !IsCharClear(StructureChars[i + 1, j]));
            list.Add(j != 0 && !IsCharClear(StructureChars[i, j - 1]));
            list.Add(j != Width - 1 && !IsCharClear(StructureChars[i, j + 1]));

            return list.Where(x => x == true).Count() >= 3;
        }
        private (int x, int y) randomMountainSpot;
        public override IResourcePack textures { get; init; } = new StoneWallsPack();

        protected override void GenerateStructure()
        {
            for (int depthRun = 0; depthRun < Depth; depthRun++)
            {
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        ref char block = ref StructureChars[i, j];

                        block = DefaultBlock;

                        if (IsCharClear(block))
                        {

                            var surrounded = IsSurrounded(i, j);
                            var semi = IsSemiSurrounded(i, j);

                            if (Enumerable.Range(0, 5).Contains(Random.Shared.Next(0, DistanceValue(i, j))) || surrounded || semi)
                            {

                                if (textures.CantGoThrough.Count() < 3)
                                {
                                    block = textures.GetRandomCantGoThroughBlock();
                                }
                                if (surrounded)
                                {
                                    block = textures.CantGoThrough.ElementAt(textures.CantGoThrough.Count() - 1);
                                }
                                else if (semi)
                                {
                                    block = textures.CantGoThrough.ElementAt(textures.CantGoThrough.Count() - 2);
                                }
                                else
                                {
                                    block = textures.CantGoThrough.ElementAt(textures.CantGoThrough.Count() - 3);
                                }
                                if(i == randomMountainSpot.y && j == randomMountainSpot.x)
                                {
                                    block = 'A';
                                }
                                
                            }
                        }
                    }
                }
            }
        }
    }
}
