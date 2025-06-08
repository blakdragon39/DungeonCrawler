namespace DungeonCrawler.scripts.resources;

public class MenuItem {

    public string Label { get; private set; }
    
    public MenuItem(string label) { // todo add action IMenuAction?
        Label = label;
    }
}