using Godot;
using DungeonCrawler.scripts.bindings;
using DungeonCrawler.scripts.utils;
using Vector3 = Godot.Vector3;

namespace DungeonCrawler.scripts;

public partial class Player : Node3D {

    private const float MOVE_SPEED = 2.5f;
    private const float ROTATE_SPEED = 30f;
    
    private Vector3? moveTo;
    private float? rotateToRads;

    private Direction curDirection = Direction.North; // todo get from level setup?

    public override void _PhysicsProcess(double delta) {
        var deltaF = (float)delta;
        
        if (moveTo != null) {
            Position = Position.MoveToward(moveTo.Value, deltaF * MOVE_SPEED);
            
            if (Position == moveTo) {
                moveTo = null;
            }
        }

        if (rotateToRads != null) {
            var newRotation = Mathf.LerpAngle(Rotation.Y, rotateToRads.Value, deltaF * ROTATE_SPEED);
            if (Mathf.IsEqualApprox(Mathf.DegToRad(RotationDegrees.Y), newRotation, 0.001)) {
                rotateToRads = null;
            } else {
                Rotation = Rotation.Lerp(new Vector3(RotationDegrees.X, newRotation, RotationDegrees.Z), deltaF * ROTATE_SPEED);
            }
        }
    }

    public void OnDungeonStart(DungeonLevel level) {
        level.MoveTo(Position.ToVector3I());
    }

    public void OnFrame(DungeonLevel level) {
        HandleInput(level);
    }

    private void HandleInput(DungeonLevel level) {
        if (moveTo != null || rotateToRads != null) return;
        
        if (Input.IsActionPressed(InputBindings.MoveForward)) {
            var checkPos = Position + DirectionUtils.GetForwardTile(curDirection);

            if (level.CanMoveTo(checkPos)) {
                moveTo = checkPos;
                level.MoveTo(moveTo.Value);
            }
        }

        if (Input.IsActionPressed(InputBindings.LookRight)) {
            curDirection = DirectionUtils.GetRight(curDirection);
            rotateToRads = DirectionUtils.GetRadsFor(curDirection);
        }

        if (Input.IsActionPressed(InputBindings.LookLeft)) {
            curDirection = DirectionUtils.GetLeft(curDirection);
            rotateToRads = DirectionUtils.GetRadsFor(curDirection);
        }
    }
}
