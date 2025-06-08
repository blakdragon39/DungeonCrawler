using DungeonCrawler.scripts.bindings;
using DungeonCrawler.scripts.components;
using DungeonCrawler.scripts.resources;
using DungeonCrawler.scripts.utils;
using Godot;
using System.Collections.Generic;

namespace DungeonCrawler.scripts.menus;

public partial class GameMenu : Node, IHandlesInput {

    private const float MOVE_SPEED = 400f;

    private Control highlight;
    private List<RichTextLabel> labels = new(); // todo currently max of 5. gonna have to handle scrolling
    private List<MenuItem> items;

    private float yOffset;

    private int currentSelectionIndex;
    private int moveToIndex = -1;
    
    public override void _Ready() {
        highlight = GetNode<Control>("HighlightTexture");
        yOffset = labels[0].Position.Y - highlight.Position.Y;
        GD.Print($"Label.y: {labels[0].Position.Y}, highlight.y: {highlight.Position.Y}, yOffset: {yOffset}");
    }
    
    public void Init(List<MenuItem> menuItems) {
        var textLabels = this.GetChildrenOfType<RichTextLabel>();
        for (int i = 0; i < 5; i += 1) {
            if (menuItems.Count > i) {
                labels.Add(textLabels[i]);
                labels[i].Text = menuItems[i].Label;
            } else {
                textLabels[i].Text = ""; // todo deactivate somehow
            }
        }
    }

    public override void _Process(double delta) {
        if (moveToIndex == -1) return;

        var movingTo = new Vector2(highlight.Position.X, labels[moveToIndex].Position.Y + yOffset);
        
        highlight.Position = highlight.Position.MoveToward(
            movingTo,
            (float)delta * MOVE_SPEED
        );

        if (Mathf.IsEqualApprox(highlight.Position.Y, movingTo.Y)) {
            moveToIndex = -1;
        }
    }

    public void HandleInput() {
        if (Input.IsActionPressed(InputBindings.MenuSelect)) {
            // todo
        }
        
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