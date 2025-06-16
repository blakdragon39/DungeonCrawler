using Godot;
using DungeonCrawler.scripts.bindings;
using DungeonCrawler.scripts.components;
using DungeonCrawler.scripts.dungeon;
using DungeonCrawler.scripts.party;
using DungeonCrawler.scripts.utils;
using Vector3 = Godot.Vector3;

namespace DungeonCrawler.scripts;

public partial class Player : Node3D, IHandlesInput {

    [Signal] public delegate void PlayerMovedEventHandler();
    
    private const float MOVE_SPEED = 2.5f;
    private const float ROTATE_SPEED = 30f;
    
    private Vector3? moveTo;

    private PartyMember sent;
    
    private Rotatable3D rotatable3D;

    public override void _Ready() {
        rotatable3D = this.GetChildOfType<Rotatable3D>();
        rotatable3D.Face(Direction.North);
        sent = new PartyMember();

        PartyEventBus.Instance.SentAttacked += () => AttackWith(sent);
        // todo other party stuff
        DungeonEventBus.Instance.SetPlayer(this);
    }

    public override void _PhysicsProcess(double delta) {
        if (moveTo != null) {
            Position = Position.MoveToward(moveTo.Value, (float)delta * MOVE_SPEED);
            
            if (Position == moveTo) {
                EmitSignal(SignalName.PlayerMoved);
                moveTo = null;
            }
        }
    }

    public void OnDungeonStart(DungeonLevel level) {
        level.MoveTo(Position.ToVector3I());
    }

    public void HandleInput() {
        if (moveTo != null || rotatable3D.IsRotating) return;

        HandleMoveInput();
        HandleRotateInput();
        HandleMenuInput();
    }

    private void HandleMoveInput() {
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
        
        if (checkPos != null && GameStateMachine.Instance.CurrentLevel.CanMoveTo(checkPos.Value.ToVector3I())) {
            moveTo = checkPos;
            GameStateMachine.Instance.CurrentLevel.MoveTo(moveTo.Value.ToVector3I());
        } // todo else do "bump" animation to show you tried but it didn't work
    }

    private void HandleRotateInput() {
        if (Input.IsActionPressed(InputBindings.LookRight)) {
            rotatable3D.RotateWithSpeed(DirectionUtils.GetRight(rotatable3D.CurDirection), ROTATE_SPEED);
        }

        if (Input.IsActionPressed(InputBindings.LookLeft)) {
            rotatable3D.RotateWithSpeed(DirectionUtils.GetLeft(rotatable3D.CurDirection), ROTATE_SPEED);
        }
    }

    private void HandleMenuInput() {
        if (Input.IsActionJustPressed(InputBindings.OpenMenu)) {
            GameStateMachine.Instance.OpenGameMenu();
        }
    }

    private void AttackWith(PartyMember partyMember) {
        var forwardTile = Position + DirectionUtils.GetForwardTile(rotatable3D.CurDirection);
        var enemy = GameStateMachine.Instance.CurrentLevel.GetEnemyAt(forwardTile.ToVector3I());

        if (enemy != null) {
            partyMember.DealDamage(enemy.Damageable);
        } else {
            GD.Print("No enemy found");
        }
    }
}
