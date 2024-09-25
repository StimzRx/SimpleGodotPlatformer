using Godot;
using System;

public partial class MainCharacter : CharacterBody2D
{
	public const float Speed = 400.0f;
	public const float JumpVelocity = -900.0f;

	[Export]
	private AnimatedSprite2D _characterSprite;

	public override void _PhysicsProcess(double delta)
	{
		if (MathF.Abs(Velocity.X) > 1f)
		{
			_characterSprite.Animation = "running";
		}
		else
		{
			_characterSprite.Animation = "default";
		}
		
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
			_characterSprite.Animation = "jumping";
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("move_jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_left", "move_right", "ui_up", "ui_down");
		
		if (direction.X != 0f)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, 10);
		}

		Velocity = velocity;
		MoveAndSlide();

		bool isLeft = Velocity.X < 0;

		_characterSprite.FlipH = isLeft;
	}
}
