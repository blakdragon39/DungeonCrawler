using DungeonCrawler.scripts.utils;
using Godot;
using System.Collections.Generic;
using System.Linq;

namespace DungeonCrawler.scripts;

public partial class DungeonLevel : GridMap {
    
    public List<Vector3I> Ground { get; private set; }
    public List<Vector3I> Walls { get; private set; }
    
    public override void _Ready() {
        Ground = GetUsedCells().ToList().FindAll(cell => cell.Y < 0);
        Walls = GetUsedCells().ToList().FindAll(cell => cell.Y >= 0);
    }

    // todo also check other things, like enemies or chests, because grid blocks cannot have scripts
    public bool CanMoveTo(Vector3 pos) {
        return GetCellItem(pos.ToVector3I()) == InvalidCellItem;
    }

    public List<Direction> GetWallsAroundTile(Vector3 groundTile) {
        var pos = new Vector3(groundTile.X, groundTile.Y + 1, groundTile.Z); // Walls are one level higher than ground
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