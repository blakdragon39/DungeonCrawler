using DungeonCrawler.scripts.utils;
using Godot;
using System.Linq;

namespace DungeonCrawler.scripts;

public partial class Minimap : Control {

    [Export] private DungeonLevel level;
    [Export] private Control playerIndicator;
    [Export] private Node3D centerOn;

    private const float TileSize = 14f;
    private const float WallSize = 2f;
    private const float DividerSize = 1f;
    private Color WallColor = Colors.Red;
    private Color DividerColor = Colors.Black;

    public override void _Ready() {
        level.WalkedOnNewTile += QueueRedraw;
    }

    public override void _Process(double delta) {
        Position = new Vector2(-(centerOn.Position.X * TileSize), -(centerOn.Position.Z * TileSize));
        playerIndicator.Rotation = -centerOn.Rotation.Y;
    }

    public override void _Draw() {
        var xOffset = GetRect().Size.X / 2;
        var yOffset = GetRect().Size.Y / 2;
        
        level.WalkedOnTiles.ToList().ForEach(tile => {
            var rect = new Rect2((tile.X * TileSize) + xOffset, (tile.Z * TileSize) + yOffset, TileSize, TileSize); 
            DrawRect(rect, Colors.Aqua);
            
            var walls = level.GetWallsAroundTile(tile);
            var topLeft = new Vector2(rect.Position.X, rect.Position.Y);
            var topRight = new Vector2(rect.Position.X + TileSize, rect.Position.Y);
            var bottomLeft = new Vector2(rect.Position.X, rect.Position.Y + TileSize);
            var bottomRight = new Vector2(rect.Position.X + TileSize, rect.Position.Y + TileSize);

            // Draw dividers first...
            if (!walls.Contains(Direction.North)) {
                DrawLine(topLeft, topRight, DividerColor, DividerSize);    
            }
            
            if (!walls.Contains(Direction.East)) {
                DrawLine(topRight, bottomRight, DividerColor, DividerSize);    
            }
            
            if (!walls.Contains(Direction.South)) {
                DrawLine(bottomLeft, bottomRight, DividerColor, DividerSize);    
            }
            
            if (!walls.Contains(Direction.West)) {
                DrawLine(topLeft, bottomLeft, DividerColor, DividerSize);    
            }
            
            // ... so that walls are drawn on top
            if (walls.Contains(Direction.North)) {
                DrawLine(topLeft, topRight, WallColor, WallSize);    
            }
            
            if (walls.Contains(Direction.East)) {
                DrawLine(topRight, bottomRight, WallColor, WallSize);    
            }
            
            if (walls.Contains(Direction.South)) {
                DrawLine(bottomLeft, bottomRight, WallColor, WallSize);    
            }
            
            if (walls.Contains(Direction.West)) {
                DrawLine(topLeft, bottomLeft, WallColor, WallSize);    
            }
        });
    }
}
