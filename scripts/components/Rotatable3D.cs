using DungeonCrawler.scripts.utils;
using Godot;

namespace DungeonCrawler.scripts.components;

public partial class Rotatable3D : Node {

    public Direction CurDirection { get; private set; }
    public bool IsRotating => rotateToRads != null;

    private Node3D parent;
    
    private float? rotateToRads;
    private float rotateSpeed;

    public override void _Ready() {
        parent = GetParent<Node3D>();
    }

    public override void _PhysicsProcess(double delta) {
        if (rotateToRads != null) {
            var deltaF = (float)delta;
            var newRotation = Mathf.LerpAngle(parent.Rotation.Y, rotateToRads.Value, deltaF * rotateSpeed);
            if (Mathf.IsEqualApprox(Mathf.DegToRad(parent.RotationDegrees.Y), newRotation, 0.001)) {
                parent.Rotation = new Vector3(parent.RotationDegrees.X, rotateToRads.Value, parent.RotationDegrees.Z);
                rotateToRads = null;
            } else {
                parent.Rotation = parent.Rotation.Lerp(new Vector3(parent.RotationDegrees.X, newRotation, parent.RotationDegrees.Z), deltaF * rotateSpeed);
            }
        }
    }

    public void RotateWithSpeed(Direction direction, float speed) {
        CurDirection = direction;
        rotateToRads = DirectionUtils.GetRadsFor(direction);
        rotateSpeed = speed;
    }

    public void Face(Direction direction) {
        CurDirection = direction;
        parent.Rotation = new Vector3(parent.RotationDegrees.X, DirectionUtils.GetRadsFor(direction), parent.RotationDegrees.Z);
    }
}