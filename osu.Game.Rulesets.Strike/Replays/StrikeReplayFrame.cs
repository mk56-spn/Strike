// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Rulesets.Replays;
using osuTK;

namespace osu.Game.Rulesets.Strike.Replays
{
    public class StrikeReplayFrame : ReplayFrame
    {
        public List<StrikeAction> Actions = new List<StrikeAction>();
        public Vector2 Position;

        public StrikeReplayFrame(StrikeAction? button = null)
        {
            if (button.HasValue)
                Actions.Add(button.Value);
        }
    }
}
