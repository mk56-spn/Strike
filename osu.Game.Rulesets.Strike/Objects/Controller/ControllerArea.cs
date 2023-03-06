using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osuTK;

namespace osu.Game.Rulesets.Strike.Objects.Controller;

public partial class ControllerArea : Container, IKeyBindingHandler<StrikeAction>
{
    private ControllerVectorInfo vectorInfo;

    private readonly ControllerContainer controller;

    public ControllerArea()
    {
        AddInternal(controller = new ControllerContainer());
        vectorInfo = new ControllerVectorInfo();
    }

    public bool OnPressed(KeyBindingPressEvent<StrikeAction> e)
    {
        vectorInfo.Velocity = e.Action switch
        {
            StrikeAction.Button1 => -new Vector2(15, 0),
            StrikeAction.Button2 => new Vector2(15, 0),
            _ => vectorInfo.Velocity
        };

        controller.Colour = Colour4.Yellow;

        return false;
    }

    public void OnReleased(KeyBindingReleaseEvent<StrikeAction> e)
    {
        vectorInfo.Velocity = Vector2.Zero;
    }

    public struct ControllerVectorInfo
    {
        public Vector2 Acceleration { get; set; }
        public Vector2 Velocity { get; set; }
    }

    protected override void Update()
    {
        controller.MoveTo(controller.Position + vectorInfo.Velocity * new Vector2((float)(Clock.ElapsedFrameTime / 1000f)));

        base.Update();
    }
}
