using DungeonCrawler.scripts.components;
using DungeonCrawler.scripts.resources;
using Godot;
using System.Collections.Generic;
using System.Linq;

namespace DungeonCrawler.scripts.menus;

public partial class DungeonMenu : Node, IHandlesInput {
    
    [Export] private PackedScene gameMenuScene;
    
    private Stack<GameMenu> activeMenus = new ();
    
    public void OpenMenu() {
        var menu = gameMenuScene.Instantiate<GameMenu>();
        menu.Init([
            new MenuItem("Sent"),
            new MenuItem("Noot"),
        ]);
        activeMenus.Push(menu);
        AddChild(menu);
    }
    
    public void HandleInput() {
        activeMenus.Last().HandleInput();
    }
}