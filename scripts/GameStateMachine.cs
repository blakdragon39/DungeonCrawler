using DungeonCrawler.scripts.components;
using Godot;
using System.Collections.Generic;

namespace DungeonCrawler.scripts;

public enum GameState {
    PlayerControl,
    Dialog,
}

public partial class GameStateMachine : Node3D {
    
    public static GameStateMachine Instance { get; private set; }

    [Export] private Player player;
    [Export] public DungeonLevel CurrentLevel { get; private set; } // todo probably instantiate dynamically, instead of from export
    
    private GameState currentState;
    private Dialog dialog;
    private Dictionary<GameState, IHandlesInput> inputtables = new();

    public override void _Ready() {
        Instance = this;
        
        currentState = GameState.PlayerControl;
        player.OnDungeonStart(CurrentLevel); // todo probably doesn't belong in Ready of the whole damn thing

        inputtables[GameState.PlayerControl] = player;
    }
    
    /**
     * _Process of GameStateMachine should only call HandleInput for whatever state the game is currently in.
     * _Process of any other node should not handle input, and should focus on handling their own state, which 
     *     shouldn't be using anything from the GameStateMachine instance.
     */
    public override void _Process(double delta) {
        inputtables[currentState].HandleInput();
    }

    public void SetGameStateDialog(Dialog newDialog) {
        // todo call clear state before any change? can you do that with a property setter? 
        dialog = newDialog;
        inputtables[GameState.Dialog] = dialog;
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