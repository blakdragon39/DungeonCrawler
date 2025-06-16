using DungeonCrawler.scripts.bindings;
using DungeonCrawler.scripts.components;
using DungeonCrawler.scripts.party;
using DungeonCrawler.scripts.resources;
using Godot;
using System.Collections.Generic;

namespace DungeonCrawler.scripts.menus;

public partial class DungeonMenu : Node, IHandlesInput {
    
    [Export] private PackedScene gameMenuScene;
    
    private Stack<GameMenu> activeMenus = new ();
    
    public void OpenMenu() {
        var menu = gameMenuScene.Instantiate<GameMenu>();
        menu.Init([
            new MenuItem("Sent", SentSelected),
            new MenuItem("Noot", NootSelected),
        ]);
        activeMenus.Push(menu);
        AddChild(menu);
    }
    
    public void HandleInput() {
        if (Input.IsActionJustPressed(InputBindings.MenuExit)) {
            PopMenu();
        } else if (activeMenus.Count > 0) {
            activeMenus.Peek().HandleInput();
        }
    }

    private void SentSelected() {
        StackNewMenu([
            new MenuItem("Attack", () => EmitSignalAndClose(PartyEventBus.SignalName.SentAttacked)),
        ]);
    }

    private void NootSelected() {
        StackNewMenu([
            new MenuItem("Attack", () => EmitSignalAndClose(PartyEventBus.SignalName.NootAttacked)),
        ]);
    }

    private void StackNewMenu(List<MenuItem> menuItems) {
        activeMenus.Peek().ProcessMode = ProcessModeEnum.Disabled;
        activeMenus.Peek().Visible = false;
        var menu = gameMenuScene.Instantiate<GameMenu>();
        menu.Init(menuItems);
        activeMenus.Push(menu);
        AddChild(menu);
    }

    private void PopMenu() {
        var lastMenu = activeMenus.Pop();
        lastMenu.QueueFree();
        if (activeMenus.Count > 0) {
            activeMenus.Peek().ProcessMode = ProcessModeEnum.Inherit;
            activeMenus.Peek().Visible = true;
        } else {
            GameStateMachine.Instance.ReturnPlayerControl();
        }
    }

    private void EmitSignalAndClose(StringName signalName) {
        foreach (GameMenu menu in activeMenus) {
            menu.QueueFree();
        }
        activeMenus.Clear();
        
        PartyEventBus.Instance.EmitSignal(signalName);
        GameStateMachine.Instance.ReturnPlayerControl();
    }
}