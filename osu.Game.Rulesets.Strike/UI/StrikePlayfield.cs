// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Strike.Objects.Controller;
using osu.Game.Rulesets.UI;
using osuTK;

namespace osu.Game.Rulesets.Strike.UI
{
    [Cached]
    public partial class StrikePlayfield : Playfield
    {
        public const int SIZE = 10000;

        [BackgroundDependencyLoader]
        private void load()
        {
            AddRangeInternal(new Drawable[]
            {
                new ControllerArea
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(SIZE)
                },
            });
        }
    }
}
