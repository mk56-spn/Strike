using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Utils;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Strike.UI;
using osuTK;

namespace osu.Game.Rulesets.Strike.Objects.Controller;

public partial class ControllerArea : Container, IKeyBindingHandler<StrikeAction>
{
    [Resolved]
    private StrikePlayfield playfield { get; set; } = null!;

    private readonly ControllerContainer controllerContainer;

    private readonly OsuSpriteText testTextHorizontal;
    private readonly OsuSpriteText testTextVertical;

    private int horizontalCheck;
    private int verticalCheck;

    public ControllerArea()
    {
        AddRange(new Drawable[]
        {
            controllerContainer = new ControllerContainer(),
            testTextHorizontal = new OsuSpriteText
            {
                Font = OsuFont.Numeric.With(size: 20),
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Y = -130,
            },
            testTextVertical = new OsuSpriteText
            {
                Font = OsuFont.Numeric.With(size: 20),
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Y = -150
            },
            new Container
            {
                Colour = Colour4.White,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Masking = true,
                MaskingSmoothness = 0,
                BorderColour = Colour4.Red,
                BorderThickness = 5,
                Size = new Vector2(10010),
                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Transparent
                }
            }
        });
    }

    public bool OnPressed(KeyBindingPressEvent<StrikeAction> e)
    {
        switch (e.Action)
        {
            case StrikeAction.Button1:
                horizontalCheck--;
                break;

            case StrikeAction.Button2:
                horizontalCheck++;
                break;

            case StrikeAction.Button3:
                verticalCheck--;
                break;

            case StrikeAction.Button4:
                verticalCheck++;
                break;
        }

        return false;
    }

    public void OnReleased(KeyBindingReleaseEvent<StrikeAction> e)
    {
        switch (e.Action)
        {
            case StrikeAction.Button1 or StrikeAction.Button2:
                horizontalCheck = e.Action == StrikeAction.Button1 ? horizontalCheck + 1 : horizontalCheck - 1;
                break;

            case StrikeAction.Button3 or StrikeAction.Button4:
                verticalCheck = e.Action == StrikeAction.Button3 ? verticalCheck + 1 : verticalCheck - 1;
                break;
        }
    }

    protected override void Update()
    {
        float xSpeed = 0;
        float ySpeed = 0;

        const int speed = 1000;

        xSpeed = horizontalCheck switch
        {
            -1 => -speed,
            1 => speed,
            _ => xSpeed
        };

        ySpeed = verticalCheck switch
        {
            -1 => -speed,
            1 => speed,
            _ => ySpeed
        };

        var newPos = controllerContainer.Position + new Vector2(xSpeed, ySpeed) * new Vector2((float)(Clock.ElapsedFrameTime / 1000f));

        if (horizontalCheck == 0)
        {
            newPos.X = controllerContainer.Position.X;
        }

        controllerContainer.MoveTo(newPos);

        testTextHorizontal.Text = $"Horizontal: {horizontalCheck}";
        testTextVertical.Text = $"Vertical: {verticalCheck}";

        clampToPlayfield();
        moveViewPort();

        base.Update();
    }

    private void clampToPlayfield()
    {
        controllerContainer.MoveToX(Math.Clamp(controllerContainer.Position.X, -StrikePlayfield.SIZE / 2f + controllerContainer.Width / 2, StrikePlayfield.SIZE / 2f - controllerContainer.Width / 2));
        controllerContainer.MoveToY(Math.Clamp(controllerContainer.Position.Y, -StrikePlayfield.SIZE / 2f + controllerContainer.Width / 2, StrikePlayfield.SIZE / 2f - controllerContainer.Width / 2));
    }

    private void moveViewPort() =>
        easeTo(this, -controllerContainer.Position);

    private void easeTo(Drawable drawable, Vector2 destination)
    {
        double dampLength = Interpolation.Lerp(3000, 40, 0.95);

        float x = (float)Interpolation.DampContinuously(drawable.X, destination.X, dampLength, Clock.ElapsedFrameTime);
        float y = (float)Interpolation.DampContinuously(drawable.Y, destination.Y, dampLength, Clock.ElapsedFrameTime);

        drawable.Position = new Vector2(x, y);
    }
}
