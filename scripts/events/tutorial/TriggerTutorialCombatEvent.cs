using DungeonCrawler.scripts.components;
using DungeonCrawler.scripts.dungeon;
using DungeonCrawler.scripts.resources;
using DungeonCrawler.scripts.utils;
using Godot;

namespace DungeonCrawler.scripts.events.tutorial;

public partial class TriggerTutorialCombatEvent : EventNode {

    [Export] private EnemyResource enemyResource;
    [Export] private PackedScene enemyScene;
    
    public override void StartEvent() {
        var enemyNode = enemyScene.Instantiate<Enemy>();
        enemyNode.Init(enemyResource);
        enemyNode.GetChildOfType<Sprite3D>().Texture = enemyResource.Sprite;
        enemyNode.Position = new Vector3(2.5f, 0.5f, -2.5f);
        AddChild(enemyNode);
        
        // todo should face player
        enemyNode.GetChildOfType<Rotatable3D>().Face(Direction.West);

        GameStateMachine.Instance.CurrentLevel.AddEnemy(enemyNode);
        
        EmitSignal(SignalName.EventEnded);
    }
}