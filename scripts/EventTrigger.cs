using DungeonCrawler.scripts.events;
using DungeonCrawler.scripts.utils;
using Godot;
using System.Collections.Generic;
using System.Linq;

namespace DungeonCrawler.scripts;

public partial class EventTrigger : Area3D {

    private List<EventNode> events;
    
    public override void _Ready() {
        events = this.GetChildrenOfType<EventNode>();
    }

    private void OnBodyEntered(Node3D body) {
        if (body is not Player) return;
        StartNextEvent();
    }

    private void StartNextEvent() {
        if (events.Count > 0) {
            var nextEvent = events.First();
            events.Remove(nextEvent);
            
            nextEvent.EventEnded += StartNextEvent;
            nextEvent.StartEvent();
        } else {
            // todo mark this even as complete and remove from game tree?
            //   - might need to differentiate between events that can happen multiple times
            //   - right now the event doesn't repeat because the list in this game node has already removed it
            GameStateMachine.Instance.ReturnPlayerControl();
        }
    }
}
