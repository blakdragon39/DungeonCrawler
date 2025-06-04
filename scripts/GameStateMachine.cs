using DungeonCrawler.scripts.components;
using Godot;
using System.Collections.Generic;

namespace DungeonCrawler.scripts;

public enum GameState {
    PlayerControl,
    Dialog,
    DungeonMenu,
}

public partial class GameStateMachine : Node3D {
    
    public static GameStateMachine Instance { get; private set; }

    [Export] public DungeonLevel CurrentLevel { get; private set; } // todo probably instantiate dynamically, instead of from export
    
    [Export] private Player player;
    [Export] private PackedScene dungeonMenuScene; 
    
    private GameState currentState;
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
        inputtables[GameState.Dialog] = newDialog;
        currentState = GameState.Dialog;
    }

    public void OpenDungeonMenu() {
        var menu = dungeonMenuScene.Instantiate<DungeonMenu>();
        AddChild(menu);
        inputtables[GameState.DungeonMenu] = menu;
        currentState = GameState.DungeonMenu;
    }

    public void ReturnPlayerControl() {
        ClearState();
        currentState = GameState.PlayerControl;
    }

    private void ClearState() {
        inputtables[GameState.Dialog] = null;
        inputtables[GameState.DungeonMenu] = null;
    }
}