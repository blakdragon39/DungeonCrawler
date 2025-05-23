using Godot;

namespace DungeonCrawler.scripts.events.tutorial;

// todo probably generize this to DialogEvent with an exported resource field
public partial class TutorialEvent1 : EventNode {
    
    [Export] private PackedScene dialogScene;
    
    public override void StartEvent() {
        var dialogResource = GD.Load<Resource>("res://assets/dialog/tutorial.dialogue");
        
        var dialogNode = dialogScene.Instantiate();
        Dialog dialog = (Dialog)dialogNode;
        
        dialog.InitDialog(dialogResource);
        AddChild(dialog);
        dialog.DialogEnded += () => { EmitSignal(SignalName.EventEnded); };
        
        GameStateMachine.Instance.SetGameStateDialog(dialog);
    }
}