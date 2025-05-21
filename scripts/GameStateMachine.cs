using Godot;

namespace DungeonCrawler.scripts;

public enum GameState {
    PlayerControl,
    Dialog,
    
}

public partial class GameStateMachine : Node3D {

    [Export] private Player player;
    [Export] private PackedScene dialogScene;
    [Export] private DungeonLevel currentLevel; // todo probably instantiate dynamically, instead of from export
    
    private GameState currentState;
    private Dialog dialog;

    public override void _Ready() {
        currentState = GameState.Dialog; // todo probably remove
        player.OnDungeonStart(currentLevel); // todo also probably doesn't belong in Ready of the whole damn thing
        
        var resource = GD.Load<Resource>("res://assets/dialog/tutorial.dialogue"); // todo this is a mess
        var dialogNode = dialogScene.Instantiate();
        dialog = (Dialog)dialogNode;
        dialog.InitDialog(resource);
        AddChild(dialog);
        dialog.DialogEnded += ReturnPlayerControl;
    }
    
    public override void _Process(double delta) {
        // GD.Print("surely this is happening");
        switch (currentState) {
            case GameState.PlayerControl:
                player.HandleInput(currentLevel);
                break;
            case GameState.Dialog:
                dialog.HandleInput();
                break;
        }
    }

    private void ReturnPlayerControl() {
        currentState = GameState.PlayerControl;
    }
}