public class BiomeType : ITileType
{
    public const string None = "none";
    public const string Water = "water";
    public const string Plain = "plain";
    public const string Mud = "mud";
    public const string Mountain = "mountain";
}

public abstract class BiomeTile
{
    public string Type;
    public bool Passable = true;
}

public class WaterBiome : BiomeTile
{
    public new string Type = BiomeType.Water;
    public new bool Passable = false;
}

public class PlainBiome : BiomeTile
{
    public new string Type = BiomeType.Plain;
}

public class MudBiome : BiomeTile
{
    public new string Type = BiomeType.Mud;
}

public class MountainBiome : BiomeTile
{
    public new string Type = BiomeType.Mountain;
}
