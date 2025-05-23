using Godot;

namespace DungeonCrawler.scripts;

public enum GameState {
    PlayerControl,
    Dialog,
}

public partial class GameStateMachine : Node3D {
    
    public static GameStateMachine Instance { get; private set; }

    [Export] private Player player;
    [Export] private DungeonLevel currentLevel; // todo probably instantiate dynamically, instead of from export
    
    private GameState currentState;
    private Dialog dialog;

    public override void _Ready() {
        Instance = this;
        
        currentState = GameState.PlayerControl; // todo probably remove
        player.OnDungeonStart(currentLevel); // todo also probably doesn't belong in Ready of the whole damn thing
    }
    
    public override void _Process(double delta) {
        switch (currentState) {
            case GameState.PlayerControl:
                player.HandleInput(currentLevel);
                break;
            case GameState.Dialog:
                dialog.HandleInput();
                break;
        }
    }

    public void SetGameStateDialog(Dialog newDialog) {
        // todo call clear state before any change? can you do that with a property setter? 
        dialog = newDialog;
        currentState = GameState.Dialog;
    }

    public void ReturnPlayerControl() {
        ClearState();
        currentState = GameState.PlayerControl;
    }

    private void ClearState() {
        dialog = null;
        // todo other.. things? Not sure if this is a good idea
    }
}