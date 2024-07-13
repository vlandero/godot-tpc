using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export] public float WalkSpeed = 3.0f;
	[Export] public float RunSpeed = 7.0f;
	[Export] public float JumpVelocity = 4.5f;
	[Export] public float SensX = 0.1f;
	[Export] public float SensY = 0.1f;
	[Export] public Node3D VisualsNode;
	[Export] public AnimationPlayer AnimPlayer;
	[Export] public StateMachine StateMachineNode;
	[Export] public Vector3 Direction { get; private set; }

	[Export] private Node3D cameraMount;
	public float currentSpeed;
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

    public override void _Input(InputEvent @event)
    {
		if(@event is InputEventMouseMotion)
		{
			RotateY(-Mathf.DegToRad(((InputEventMouseMotion)@event).Relative.X * SensX));
			VisualsNode.RotateY(Mathf.DegToRad(((InputEventMouseMotion)@event).Relative.X * SensX));
			cameraMount.RotateX(-Mathf.DegToRad(((InputEventMouseMotion)@event).Relative.Y * SensY));
		}
    }

	public void ApplyDirection(double delta)
	{
		Vector3 velocity = Velocity;
		velocity.X = Direction.X * currentSpeed;
		velocity.Z = Direction.Z * currentSpeed;
		Velocity = velocity;
		if(Direction == Vector3.Zero)
		{
			return;
		}
		VisualsNode.LookAt(Position + new Vector3(-Direction.X, Direction.Y, -Direction.Z));
	}

	public void ApplyIdle()
	{
		Vector3 velocity = Velocity;
		velocity.X = Mathf.MoveToward(Velocity.X, 0, currentSpeed);
		velocity.Z = Mathf.MoveToward(Velocity.Z, 0, currentSpeed);
		Velocity = velocity;
	}

	public void Jump()
	{
		Vector3 velocity = Velocity;
		velocity.Y = JumpVelocity;
		Velocity = velocity;
	}

	public void Move(double delta)
	{
		Vector3 velocity = Velocity;

		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;
        
        Velocity = velocity;
        MoveAndSlide();
	}

    public override void _PhysicsProcess(double delta)
	{
		Vector2 inputDir = Input.GetVector(GameConstants.INPUT_MOVE_LEFT, GameConstants.INPUT_MOVE_RIGHT, GameConstants.INPUT_MOVE_FORWARD, GameConstants.INPUT_MOVE_BACKWARD);
		Direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
	}
}
