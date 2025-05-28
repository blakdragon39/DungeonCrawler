using Godot;

namespace DungeonCrawler.scripts.enemies;

[GlobalClass]
public partial class Enemy : Resource {

    [Export] public Texture2D Sprite;
    [Export] public int MaxHealth;
}