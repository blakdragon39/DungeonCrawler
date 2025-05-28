using Godot;

namespace DungeonCrawler.scripts.events;

public partial class DialogEvent : EventNode {
    
    [Export] private PackedScene dialogScene;
    [Export] private Resource dialogResource;
    
    public override void StartEvent() {
        var dialog = dialogScene.Instantiate<Dialog>();
        
        dialog.InitDialog(dialogResource);
        AddChild(dialog);
        dialog.DialogEnded += () => { EmitSignal(SignalName.EventEnded); };
        
        GameStateMachine.Instance.SetGameStateDialog(dialog);
    }
}