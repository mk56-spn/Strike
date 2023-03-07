using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Utils;
using osu.Game.Rulesets.Strike.UI;
using osuTK;

namespace osu.Game.Rulesets.Strike.Objects.Controller;

public partial class ControllerArea : Container, IKeyBindingHandler<StrikeAction>
{
    private readonly ControllerContainer controllerContainer;

    public int HorizontalCheck;
    public int VerticalCheck;

    public ControllerArea()
    {
        Anchor = Anchor.Centre;
        Origin = Anchor.Centre;
        Size = new Vector2(StrikePlayfield.SIZE);

        AddRange(new Drawable[]
        {
            controllerContainer = new ControllerContainer(),

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
                HorizontalCheck--;
                break;

            case StrikeAction.Button2:
                HorizontalCheck++;
                break;

            case StrikeAction.Button3:
                VerticalCheck--;
                break;

            case StrikeAction.Button4:
                VerticalCheck++;
                break;
        }

        return false;
    }

    public void OnReleased(KeyBindingReleaseEvent<StrikeAction> e)
    {
        switch (e.Action)
        {
            case StrikeAction.Button1 or StrikeAction.Button2:
                HorizontalCheck = e.Action == StrikeAction.Button1 ? HorizontalCheck + 1 : HorizontalCheck - 1;
                break;

            case StrikeAction.Button3 or StrikeAction.Button4:
                VerticalCheck = e.Action == StrikeAction.Button3 ? VerticalCheck + 1 : VerticalCheck - 1;
                break;
        }
    }

    protected override void Update()
    {
        float xSpeed = 0;
        float ySpeed = 0;

        const int speed = 1000;

        xSpeed = HorizontalCheck switch
        {
            -1 => -speed,
            1 => speed,
            _ => xSpeed
        };

        ySpeed = VerticalCheck switch
        {
            -1 => -speed,
            1 => speed,
            _ => ySpeed
        };

        var newPos = controllerContainer.Position + new Vector2(xSpeed, ySpeed) * new Vector2((float)(Clock.ElapsedFrameTime / 1000f));

        if (HorizontalCheck == 0)
        {
            newPos.X = controllerContainer.Position.X;
        }

        controllerContainer.MoveTo(newPos);

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
