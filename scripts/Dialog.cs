using DialogueManagerRuntime;
using DungeonCrawler.scripts.bindings;
using Godot;

public partial class Dialog : Control {

    [Signal] public delegate void DialogEndedEventHandler();
    
    [Export] private RichTextLabel nameField;
    [Export] private RichTextLabel dialogField;

    private Resource dialogResource;
    private string nextId;
    
    public void InitDialog(Resource resource) {
        dialogResource = resource;
        ShowNextDialogLine();
    }
    
    public void HandleInput() {
        if (Input.IsActionJustPressed(InputBindings.AdvanceDialogue)) {
            ShowNextDialogLine();
        }
    }

    private async void ShowNextDialogLine() {
        DialogueLine line = await DialogueManager.GetNextDialogueLine(dialogResource, nextId);

        if (line != null) {
            nameField.Text = line.Character;
            dialogField.Text = line.Text;
            nextId = line.NextId;
        } else {
            EmitSignal(SignalName.DialogEnded);
            QueueFree();
        }
    }
}
