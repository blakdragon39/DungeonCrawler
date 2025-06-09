using DungeonCrawler.scripts.bindings;
using DungeonCrawler.scripts.components;
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
            new MenuItem("Attack", () => GD.Print("Sent Attacked")),
            new MenuItem("Defend", () => GD.Print("Sent Defended")),
        ]);
    }

    private void NootSelected() {
        StackNewMenu([
            new MenuItem("Attack", () => GD.Print("Noot Attacked")),
            new MenuItem("Defend", () => GD.Print("Noot Defended")),
        ]);
    }

    private void StackNewMenu(List<MenuItem> menuItems) {
        // activeMenus.Peek().SetProcess(false); // todo this isn't hiding it
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
        }
    }
}