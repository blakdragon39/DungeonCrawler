using Godot;

namespace DungeonCrawler.scripts.events;

public partial class EventNode : Node {
    
    [Signal] public delegate void EventEndedEventHandler();

    public virtual void StartEvent() { }
}