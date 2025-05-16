using Godot;

namespace DungeonCrawler.scripts.utils;

public static class VectorExt {

    public static Vector3I ToVector3I(this Vector3 v3) =>
        new (Mathf.FloorToInt(v3.X), Mathf.FloorToInt(v3.Y), Mathf.FloorToInt(v3.Z));
}