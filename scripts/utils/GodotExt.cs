using Godot;
using System.Collections.Generic;
using System.Linq;

namespace DungeonCrawler.scripts.utils;

public static class GodotExt {

    public static Vector3I ToVector3I(this Vector3 v3) =>
        new (Mathf.FloorToInt(v3.X), Mathf.FloorToInt(v3.Y), Mathf.FloorToInt(v3.Z));

    public static List<T> GetChildrenOfType<T>(this Node node) =>
        node.GetChildren().OfType<T>().ToList();
}