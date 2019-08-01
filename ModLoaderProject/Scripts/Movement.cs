using Godot;

public class Movement : KinematicBody2D
{
    [Export] public int Speed = 200;

    Vector2 velocity = new Vector2();

    public void GetInput()
    {
        velocity = new Vector2();

        if (Input.IsActionPressed("right"))
            velocity.x += 1;

        if (Input.IsActionPressed("left"))
            velocity.x -= 1;

        if (Input.IsActionPressed("down"))
            velocity.y += 1;

        if (Input.IsActionPressed("up"))
            velocity.y -= 1;

        velocity = velocity.Normalized() * Speed;
    }

    public override void _PhysicsProcess(float delta)
    {
        GetInput();
        velocity = MoveAndSlide(velocity);
    }
}
