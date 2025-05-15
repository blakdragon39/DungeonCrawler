using Godot;

namespace DungeonCrawler.scripts;

public enum FacingDirection {
    North, South, East, West
}

public class ForwardTileConsts {
    public static Vector3 North = new (-1, 0, 0);
    public static Vector3 South = new (1, 0, 0);
    public static Vector3 East = new (0, 0, -1);
    public static Vector3 West = new (0, 0, 1);
}

public class DirectionUtilities {
    
    public const float NorthRads = 0;
    public const float EastRads = 3 * Mathf.Pi / 2;
    public const float SouthRads = Mathf.Pi;
    public const float WestRads = Mathf.Pi / 2;

    public static float GetRadsFor(FacingDirection dir) {
        return dir switch {
            FacingDirection.North => NorthRads,
            FacingDirection.East => EastRads,
            FacingDirection.South => SouthRads,
            FacingDirection.West => WestRads
        };
    }
    
    public static FacingDirection GetRight(FacingDirection direction) {
        return direction switch {
            FacingDirection.North => FacingDirection.East,
            FacingDirection.East => FacingDirection.South,
            FacingDirection.South => FacingDirection.West,
            FacingDirection.West => FacingDirection.North
        };
    }
    
    public static FacingDirection GetLeft(FacingDirection direction) {
        return direction switch {
            FacingDirection.North => FacingDirection.West,
            FacingDirection.East => FacingDirection.North,
            FacingDirection.South => FacingDirection.East,
            FacingDirection.West => FacingDirection.South
        };
    }
    
    public static Vector3 GetForwardTile(FacingDirection direction) {
        return direction switch {
            FacingDirection.North => ForwardTileConsts.North,
            FacingDirection.East => ForwardTileConsts.East,
            FacingDirection.South => ForwardTileConsts.South,
            FacingDirection.West => ForwardTileConsts.West
        };
    }
}