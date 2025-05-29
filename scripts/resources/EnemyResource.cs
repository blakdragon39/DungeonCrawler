using Godot;

namespace DungeonCrawler.scripts.resources;

[GlobalClass]
public partial class EnemyResource : Resource {

    [Export] public Texture2D Sprite;
    [Export] public int MaxHealth;
}