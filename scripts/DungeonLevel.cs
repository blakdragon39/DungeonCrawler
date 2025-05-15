using Godot;
using Godot.Collections;
using System.Linq;

namespace DungeonCrawler.scripts;

public partial class DungeonLevel : GridMap {

    private Array<Vector3I> ground;
    private Array<Vector3I> walls;
    
    public override void _Ready() {
        

        GD.Print("Ground Items:");
        ground.ToList().ForEach(item => {
            GD.Print(item);
        });
        
        GD.Print("Wall Items:");
        walls.ToList().ForEach(item => {
            GD.Print(item);
        });
    }

    // todo also check other things, like enemies or chests, because grid blocks cannot have scripts
    public bool CanMoveTo(Vector3 pos) {
        var posI = new Vector3I(Mathf.FloorToInt(pos.X), Mathf.FloorToInt(pos.Y), Mathf.FloorToInt(pos.Z));
        var result = GetCellItem(posI); 
        GD.Print($"CanMoveTo {posI.X} {posI.Y} {posI.Z}: {result}");
        return result == InvalidCellItem;
    }
}