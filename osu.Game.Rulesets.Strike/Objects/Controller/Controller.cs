using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace osu.Game.Rulesets.Strike.Objects.Controller;

public partial class ControllerContainer : Container
{
    public const int CONTROLLER_SIZE = 100;

    public ControllerContainer()
    {
        Masking = true;
        Anchor = Anchor.Centre;

        Add(new Circle
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            RelativeSizeAxes = Axes.Both,
        });
        Size = new Vector2(CONTROLLER_SIZE);
    }
}
