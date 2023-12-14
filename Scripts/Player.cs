using Godot;
using System;
using System.Reflection.Metadata;

public partial class Player : CharacterBody2D
{
    [Export] public float Speed = 100.0f;
    [Export] public float JumpVelocity = -150.0f;

    [Export] private AnimatedSprite2D _sprite;

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void _Ready()
    {
    }


    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
        {
            velocity.Y += gravity * (float)delta;
            _sprite.Play("Jump");
        }

        // Handle Jump.
        if (Input.IsActionJustPressed("Action") && IsOnFloor())
        {
            velocity.Y = JumpVelocity;
        }

        // Get the input direction and handle the movement/deceleration.
        Vector2 direction = Input.GetVector("WalkLeft", "WalkRight", "Up", "Down");
        if (direction != Vector2.Zero)
        {
            velocity.X = direction.X * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
        }

        Velocity = velocity;
        if (IsOnFloor())
            UpdateAnimation(direction);
        UpdateFacingDirection(direction).MoveAndSlide();
    }

    private Player UpdateAnimation(Vector2 directon)
    {
        if (directon.X != 0)
        {
            _sprite.Play("Move");
        }
        else
        {
            _sprite.Play("Idle");
        }
        return this;
    }

    private Player UpdateFacingDirection(Vector2 drection)
    {
        if (drection.X > 0)
            _sprite.FlipH = false;
        else if (drection.X < 0)
            _sprite.FlipH = true;
        else { }

        return this;
    }
}
