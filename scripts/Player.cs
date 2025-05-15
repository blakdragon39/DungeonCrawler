using Godot;
using DungeonCrawler.scripts.bindings;
using Vector3 = Godot.Vector3;

namespace DungeonCrawler.scripts;

public partial class Player : Node3D {

    private const float MOVE_SPEED = 2.5f;
    private const float ROTATE_SPEED = 20f;
    
    private Vector3? moveTo;
    private float? rotateToRads;

    private FacingDirection curDirection = FacingDirection.East; // todo get from level setup?

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

    public void OnFrame(DungeonLevel level) {
        HandleInput(level);
    }

    private void HandleInput(DungeonLevel level) {
        if (moveTo != null || rotateToRads != null) return;
        
        if (Input.IsActionPressed(InputBindings.MoveForward)) {
            var checkPos = Position + DirectionUtilities.GetForwardTile(curDirection);

            if (level.CanMoveTo(checkPos)) {
                moveTo = checkPos;
            }
        }

        if (Input.IsActionPressed(InputBindings.LookRight)) {
            curDirection = DirectionUtilities.GetRight(curDirection);
            rotateToRads = DirectionUtilities.GetRadsFor(curDirection);
        }

        if (Input.IsActionPressed(InputBindings.LookLeft)) {
            curDirection = DirectionUtilities.GetLeft(curDirection);
            rotateToRads = DirectionUtilities.GetRadsFor(curDirection);
        }
    }
}
