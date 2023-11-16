public enum TileType
{
    NONE,
    ICE,
    DIRT_DRY,
    DIRT_WET,
    GRASS,
    BUSH
}

public static class TileTypeExtensions
{
    public static bool IsPassable(this TileType type)
    {
        return type != TileType.ICE;
    }
}