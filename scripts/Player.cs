using Godot;
using DungeonCrawler.scripts.bindings;
using DungeonCrawler.scripts.components;
using DungeonCrawler.scripts.utils;
using Vector3 = Godot.Vector3;

namespace DungeonCrawler.scripts;

public partial class Player : Node3D {

    private const float MOVE_SPEED = 2.5f;
    private const float ROTATE_SPEED = 30f;
    
    private Vector3? moveTo;
    
    private Rotatable3D rotatable3D;

    public override void _Ready() {
        rotatable3D = this.GetChildOfType<Rotatable3D>();
        rotatable3D.Face(Direction.North);
    }

    public override void _PhysicsProcess(double delta) {
        if (moveTo != null) {
            Position = Position.MoveToward(moveTo.Value, (float)delta * MOVE_SPEED);
            
            if (Position == moveTo) {
                moveTo = null;
            }
        }
    }

    public void OnDungeonStart(DungeonLevel level) {
        level.MoveTo(Position.ToVector3I());
    }

    public void HandleInput(DungeonLevel level) {
        if (moveTo != null || rotatable3D.IsRotating) return;

        Vector3? checkPos = null;
        
        if (Input.IsActionPressed(InputBindings.MoveForward)) {
            checkPos = Position + DirectionUtils.GetForwardTile(rotatable3D.CurDirection);
        } else if (Input.IsActionPressed(InputBindings.MoveBack)) {
            checkPos = Position + DirectionUtils.GetBackwardsTile(rotatable3D.CurDirection);
        } else if (Input.IsActionPressed(InputBindings.StrafeLeft)) {
            checkPos = Position + DirectionUtils.GetLeftTile(rotatable3D.CurDirection);
        } else if (Input.IsActionPressed(InputBindings.StrafeRight)) {
            checkPos = Position + DirectionUtils.GetRightTile(rotatable3D.CurDirection);
        }
        
        if (checkPos != null && level.CanMoveTo(checkPos.Value.ToVector3I())) {
            moveTo = checkPos;
            level.MoveTo(moveTo.Value.ToVector3I());
        }
        

        if (Input.IsActionPressed(InputBindings.LookRight)) {
            rotatable3D.RotateWithSpeed(DirectionUtils.GetRight(rotatable3D.CurDirection), ROTATE_SPEED);
        }

        if (Input.IsActionPressed(InputBindings.LookLeft)) {
            rotatable3D.RotateWithSpeed(DirectionUtils.GetLeft(rotatable3D.CurDirection), ROTATE_SPEED);
        }
    }
}
