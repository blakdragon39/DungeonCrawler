using Godot;

namespace DungeonCrawler.scripts;

public enum GameState {
    PlayerControl,
    Dialog,
    
}

public partial class GameStateMachine : Node3D {

    private GameState currentState;
    private DungeonLevel currentLevel;

    [Export] private Player player;

    public override void _Ready() {
        currentState = GameState.PlayerControl; // todo probably remove
        currentLevel = GetNode<DungeonLevel>("DungeonLevel"); // todo probably instantiate dynamically, instead of existing in the scene tree
    }
    
    public override void _Process(double delta) {
        switch (currentState) {
            case GameState.PlayerControl:
                player.OnFrame(currentLevel);
                break;
        }
    } 
}