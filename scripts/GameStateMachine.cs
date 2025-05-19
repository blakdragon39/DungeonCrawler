using Godot;

namespace DungeonCrawler.scripts;

public enum GameState {
    PlayerControl,
    Dialog,
    
}

public partial class GameStateMachine : Node3D {

    [Export] private Player player;
    [Export] private DungeonLevel currentLevel; // todo probably instantiate dynamically, instead of from export
    
    private GameState currentState;

    public override void _Ready() {
        currentState = GameState.PlayerControl; // todo probably remove
        player.OnDungeonStart(currentLevel); // todo also probably doesn't belong in Ready of the whole damn thing
    }
    
    public override void _Process(double delta) {
        switch (currentState) {
            case GameState.PlayerControl:
                player.OnFrame(currentLevel);
                break;
        }
    } 
}