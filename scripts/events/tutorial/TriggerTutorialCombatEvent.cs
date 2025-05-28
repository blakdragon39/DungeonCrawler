using DungeonCrawler.scripts.enemies;
using DungeonCrawler.scripts.utils;
using Godot;

namespace DungeonCrawler.scripts.events.tutorial;

public partial class TriggerTutorialCombatEvent : EventNode {

    [Export] private Enemy enemy;
    [Export] private PackedScene enemyScene;
    
    public override void StartEvent() {
        var enemyNode = enemyScene.Instantiate<Node3D>();
        enemyNode.GetNode<Sprite3D>("Sprite3D").Texture = enemy.Sprite;
        enemyNode.Position = new Vector3(4.5f, 0.5f, -2.5f);
        enemyNode.Rotation = new Vector3(0f, DirectionUtils.GetRadsFor(Direction.West), 0f);
        AddChild(enemyNode);
        
        EmitSignal(SignalName.EventEnded);
    }
}