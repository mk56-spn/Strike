using osu.Framework.Graphics;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Strike.Objects.Controller;
using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Strike.Tests;

public partial class TestSceneControllerMovement : OsuTestScene
{
    private OsuSpriteText testTextHorizontal;
    private OsuSpriteText testTextVertical;

    private ControllerArea controllerArea;

    protected override void LoadComplete()
    {
        base.LoadComplete();

        AddRange(new Drawable[]
        {
            controllerArea = new ControllerArea(),
            testTextHorizontal = new OsuSpriteText
            {
                Font = OsuFont.Numeric.With(size: 20),
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Y = -130
            },
            testTextVertical = new OsuSpriteText
            {
                Name = "thing",
                Font = OsuFont.Numeric.With(size: 20),
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Y = -150
            }
        });
    }

    protected override void Update()
    {
        base.Update();

        testTextHorizontal.Text = $"Horizontal: {controllerArea.HorizontalCheck}";
        testTextVertical.Text = $"Vertical: {controllerArea.VerticalCheck}";
    }
}
