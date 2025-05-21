using DialogueManagerRuntime;
using DungeonCrawler.scripts.bindings;
using Godot;

public partial class Dialog : Control {

    [Signal] public delegate void DialogEndedEventHandler();
    
    [Export] private RichTextLabel nameField;
    [Export] private RichTextLabel dialogField;

    private const float TYPING_TIME = 0.2f;
    
    private Resource dialogResource;
    private string nextId;
    
    private string typingText;
    private int typingIndex = 1;
    
    public void InitDialog(Resource resource) {
        dialogResource = resource;
        ShowNextDialogLine();
    }
    
    public void HandleInput() {
        if (Input.IsActionJustPressed(InputBindings.AdvanceDialogue) && typingText == null) {
            ShowNextDialogLine();
        }
    }

    public override void _Process(double delta) {
        TypeText();
    }

    private async void ShowNextDialogLine() {
        DialogueLine line = await DialogueManager.GetNextDialogueLine(dialogResource, nextId);

        if (line != null) {
            nameField.Text = line.Character;
            typingText = line.Text;
            nextId = line.NextId;
        } else {
            EmitSignal(SignalName.DialogEnded);
            QueueFree();
        }
    }

    private async void TypeText() {
        if (typingText == null) return;

        dialogField.Text = typingText.Substring(0, typingIndex);
        typingIndex += 1;

        if (typingIndex > typingText.Length) {
            typingText = null;
            typingIndex = 1;
        }

        await ToSignal(GetTree().CreateTimer(TYPING_TIME), SceneTreeTimer.SignalName.Timeout);
    }
}
