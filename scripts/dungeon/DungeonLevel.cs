using DungeonCrawler.scripts.utils;
using Godot;
using System.Collections.Generic;
using System.Linq;

namespace DungeonCrawler.scripts.dungeon;

public partial class DungeonLevel : GridMap {

    [Signal] public delegate void WalkedOnNewTileEventHandler();
    
    public HashSet<Vector3I> WalkedOnTiles { get; private set; }
    public List<Enemy> Enemies { get; private set; }

    private const int GROUND_LEVEL = -1;
    private const int WALL_LEVEL = 0;
    
    public override void _Ready() {
        WalkedOnTiles = new HashSet<Vector3I>();
        Enemies = new List<Enemy>(); // todo find enemies in child nodes
    }

    public bool CanMoveTo(Vector3I pos) {
        // check other blocking items
        if (Enemies.Any(enemy => enemy.Position.ToVector3I() == pos)) return false;
        if (GetCellItem(new Vector3I(pos.X, GROUND_LEVEL, pos.Z)) == InvalidCellItem) return false;

        return true;
    }

    public void MoveTo(Vector3I pos) {
        var wasNewTile = WalkedOnTiles.Add(pos);
        if (wasNewTile) {
            EmitSignal(SignalName.WalkedOnNewTile);
            // todo enemies can now move/take turns if they have things to do
        }
    }

    public void AddEnemy(Enemy enemy) {
        // todo add IBlockMovement?
        Enemies.Add(enemy);
    }

    public Enemy GetEnemyAt(Vector3I pos) {
        GD.Print($"Looking at {pos}");
        foreach (Enemy enemy in Enemies) {
            GD.Print($"Enemy {enemy.Position}");
        }

        return Enemies.Find(enemy => enemy.Position.ToVector3I() == pos); // todo this not working?
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