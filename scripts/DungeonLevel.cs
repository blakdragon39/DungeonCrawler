using DungeonCrawler.scripts.utils;
using Godot;
using System.Collections.Generic;
using System.Linq;

namespace DungeonCrawler.scripts;

public partial class DungeonLevel : GridMap {

    [Signal] public delegate void WalkedOnNewTileEventHandler();
    
    public HashSet<Vector3I> WalkedOnTiles { get; private set; }

    private const int GROUND_LEVEL = -1;
    private const int WALL_LEVEL = 0;
    
    public override void _Ready() {
        WalkedOnTiles = new HashSet<Vector3I>();
    }

    // todo also check other things, like enemies or chests, because grid blocks cannot have scripts
    public bool CanMoveTo(Vector3 pos) {
        return GetCellItem(pos.ToVector3I()) == InvalidCellItem;
    }

    public void MoveTo(Vector3 pos) {
        var wasNewTile = WalkedOnTiles.Add(pos.ToVector3I());
        if (wasNewTile) {
            EmitSignal(SignalName.WalkedOnNewTile);
        }
    }

    public List<Direction> GetWallsAroundTile(Vector3 groundTile) {
        var pos = new Vector3(groundTile.X, WALL_LEVEL, groundTile.Z);
        var wallDirections = new List<Direction>();
        
        var checkNorth = pos + DirectionUtils.GetForwardTile(Direction.North);
        if (GetCellItem(checkNorth.ToVector3I()) != InvalidCellItem) {
            wallDirections.Add(Direction.North);
        }
        
        var checkSouth = pos + DirectionUtils.GetForwardTile(Direction.South);
        if (GetCellItem(checkSouth.ToVector3I()) != InvalidCellItem) {
            wallDirections.Add(Direction.South);
        }
        
        var checkEast = pos + DirectionUtils.GetForwardTile(Direction.East);
        if (GetCellItem(checkEast.ToVector3I()) != InvalidCellItem) {
            wallDirections.Add(Direction.East);
        }
        
        var checkWest = pos + DirectionUtils.GetForwardTile(Direction.West);
        if (GetCellItem(checkWest.ToVector3I()) != InvalidCellItem) {
            wallDirections.Add(Direction.West);
        }

        return wallDirections;
    }
}