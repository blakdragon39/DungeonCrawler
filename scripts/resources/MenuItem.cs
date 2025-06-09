using System;

namespace DungeonCrawler.scripts.resources;

public class MenuItem {

    public string Label { get; private set; }
    public Action OnSelected { get; private set; }
    
    public MenuItem(string label, Action onSelected) {
        Label = label;
        OnSelected = onSelected;
    }
}