using Godot;
using System;

namespace DungeonCrawler.scripts.components.dungeon;

public partial class Damageable(int maxHealth) : Node {

    [Signal] public delegate void DamageTakenEventHandler();

    public int CurrentHealth { get; private set; } = maxHealth;
    
    public void TakeDamage(int damage) {
        CurrentHealth = Math.Clamp(CurrentHealth - damage, 0, CurrentHealth);
        EmitSignal(SignalName.DamageTaken);
    }
}