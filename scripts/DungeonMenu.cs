using DungeonCrawler.scripts.bindings;
using DungeonCrawler.scripts.components;
using Godot;
using Godot.Collections;

namespace DungeonCrawler.scripts;

public partial class DungeonMenu : Node, IHandlesInput {

    private const float MOVE_SPEED = 400f;

    [Export] private Control highlight;
    [Export] private Array<Control> labels;

    private float yOffset;

    private int currentSelectionIndex;
    private int moveToIndex = -1;
    
    public override void _Ready() {
        yOffset = labels[0].Position.Y - highlight.Position.Y;
    }

    public override void _Process(double delta) {
        if (moveToIndex == -1) return;

        var movingTo = new Vector2(highlight.Position.X, labels[moveToIndex].Position.Y - yOffset);
        
        highlight.Position = highlight.Position.MoveToward(
            movingTo,
            (float)delta * MOVE_SPEED
        );

        if (Mathf.IsEqualApprox(highlight.Position.Y, movingTo.Y)) {
            moveToIndex = -1;
        }
    }

    public void HandleInput() {
        // todo handle selection before returning on next line
        
        if (moveToIndex != -1) return; // Don't change selection if already moving
        
        if (Input.IsActionPressed(InputBindings.MenuDown)) {
            if (currentSelectionIndex < labels.Count - 1) {
                moveToIndex = currentSelectionIndex + 1;
                currentSelectionIndex += 1;
            }
        } else if (Input.IsActionPressed(InputBindings.MenuUp)) {
            if (currentSelectionIndex > 0) {
                moveToIndex = currentSelectionIndex - 1;
                currentSelectionIndex -= 1;
            }
        }
    }
}