using Godot;

namespace DungeonCrawler.scripts.utils;

public enum Direction {
    North, South, East, West
}

public class ForwardTileConsts {
    public static Vector3 North = new (0, 0, -1);
    public static Vector3 South = new (0, 0, 1);
    public static Vector3 East = new (1, 0, 0);
    public static Vector3 West = new (-1, 0, 0);
}

public class DirectionUtils {
    
    public const float NorthRads = 0;
    public const float EastRads = 3 * Mathf.Pi / 2;
    public const float SouthRads = Mathf.Pi;
    public const float WestRads = Mathf.Pi / 2;

    public static float GetRadsFor(Direction dir) {
        return dir switch {
            Direction.North => NorthRads,
            Direction.East => EastRads,
            Direction.South => SouthRads,
            Direction.West => WestRads
        };
    }
    
    public static Direction GetRight(Direction direction) {
        return direction switch {
            Direction.North => Direction.East,
            Direction.East => Direction.South,
            Direction.South => Direction.West,
            Direction.West => Direction.North
        };
    }
    
    public static Direction GetLeft(Direction direction) {
        return direction switch {
            Direction.North => Direction.West,
            Direction.East => Direction.North,
            Direction.South => Direction.East,
            Direction.West => Direction.South
        };
    }
    
    public static Vector3 GetForwardTile(Direction direction) {
        return direction switch {
            Direction.North => ForwardTileConsts.North,
            Direction.East => ForwardTileConsts.East,
            Direction.South => ForwardTileConsts.South,
            Direction.West => ForwardTileConsts.West
        };
    }

    public static Vector3 GetBackwardsTile(Direction direction) {
        return direction switch {
            Direction.North => ForwardTileConsts.South,
            Direction.East => ForwardTileConsts.West,
            Direction.South => ForwardTileConsts.North,
            Direction.West => ForwardTileConsts.East
        };
    }
    
    public static Vector3 GetLeftTile(Direction direction) {
        return direction switch {
            Direction.North => ForwardTileConsts.West,
            Direction.East => ForwardTileConsts.North,
            Direction.South => ForwardTileConsts.East,
            Direction.West => ForwardTileConsts.South
        };
    }
    
    public static Vector3 GetRightTile(Direction direction) {
        return direction switch {
            Direction.North => ForwardTileConsts.East,
            Direction.East => ForwardTileConsts.South,
            Direction.South => ForwardTileConsts.West,
            Direction.West => ForwardTileConsts.North
        };
    }
}