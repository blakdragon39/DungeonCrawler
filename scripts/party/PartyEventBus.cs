using Godot;

namespace DungeonCrawler.scripts.party;

public partial class PartyEventBus : Node {

    public static PartyEventBus Instance { get; private set; }
    
    [Signal] public delegate void SentAttackedEventHandler();
    [Signal] public delegate void NootAttackedEventHandler();

    public override void _Ready() {
        Instance = this;
    }
}