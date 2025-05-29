using DungeonCrawler.scripts.components;
using DungeonCrawler.scripts.resources;
using DungeonCrawler.scripts.utils;
using Godot;

namespace DungeonCrawler.scripts.events.tutorial;

public partial class TriggerTutorialCombatEvent : EventNode {

    [Export] private EnemyResource enemy;
    [Export] private PackedScene enemyScene;
    
    public override void StartEvent() {
        var enemyNode = enemyScene.Instantiate<Enemy>();
        enemyNode.GetChildOfType<Sprite3D>().Texture = enemy.Sprite;
        enemyNode.Position = new Vector3(2.5f, 0.5f, -2.5f);
        AddChild(enemyNode);
        
        // todo should face player
        //  and then should face player again every time player moves
        //  or any time enemy moves
        enemyNode.GetChildOfType<Rotatable3D>().Face(Direction.West);

        GameStateMachine.Instance.CurrentLevel.AddEnemy(enemyNode);
        
        EmitSignal(SignalName.EventEnded);
    }
}