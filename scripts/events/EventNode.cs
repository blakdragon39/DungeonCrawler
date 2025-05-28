using Godot;

namespace DungeonCrawler.scripts.events;

public abstract partial class EventNode : Node {
    
    [Signal] public delegate void EventEndedEventHandler();

    public abstract void StartEvent();
}