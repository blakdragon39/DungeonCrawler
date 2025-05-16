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
    }
    
    public override void _Process(double delta) {
        switch (currentState) {
            case GameState.PlayerControl:
                player.OnFrame(currentLevel);
                break;
        }
    } 
}