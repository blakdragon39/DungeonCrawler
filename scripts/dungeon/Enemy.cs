using DungeonCrawler.scripts.components;
using DungeonCrawler.scripts.components.dungeon;
using DungeonCrawler.scripts.resources;
using Godot;

namespace DungeonCrawler.scripts.dungeon;

public partial class Enemy : Node3D, IBlockMovement {
    
    public Damageable Damageable { get; private set; }
    
    private EnemyResource resource;
    
    public void Init(EnemyResource newResource) {
        resource = newResource;
        Damageable = new Damageable(resource.MaxHealth);
        Damageable.DamageTaken += () => { GD.Print($"Current Health: {Damageable.CurrentHealth}"); };
        EnemyTurnEventBus.Instance.EnemyTurn += () => { GD.Print("Enemy turn"); };
        // todo health and stats and whatnot
        // todo should rotate to face player when player moves, or when enemy moves
    }

    // todo show health bar
}