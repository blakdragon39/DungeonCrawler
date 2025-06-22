using DungeonCrawler.scripts.party;
using Godot;

namespace DungeonCrawler.scripts.dungeon;

public partial class EnemyTurnEventBus : Node {
    
    public static EnemyTurnEventBus Instance { get; private set; }

    [Signal] public delegate void EnemyTurnEventHandler();
    
    public override void _Ready() {
        Instance = this;

        PartyEventBus.Instance.SentAttacked += () => EmitSignal(SignalName.EnemyTurn);
        PartyEventBus.Instance.NootAttacked += () => EmitSignal(SignalName.EnemyTurn);
    }

    public void SetPlayer(Player player) {
        player.PlayerMoved += () => EmitSignal(SignalName.EnemyTurn);
    }
}