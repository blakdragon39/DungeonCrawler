using Godot;

namespace DungeonCrawler.scripts;

public partial class Minimap : Control {

    [Export] private DungeonLevel level;

    private const float TileSize = 12f;
    private const float WallSize = 2f;
    private const float DividerSize = 1f;
    private Color WallColor = Colors.Red;
    private Color DividerColor = Colors.Black;
    
    public override void _Draw() {
        level.Ground.ForEach(tile => {
            var rect = new Rect2(tile.X * TileSize, tile.Z * TileSize, TileSize, TileSize); 
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
