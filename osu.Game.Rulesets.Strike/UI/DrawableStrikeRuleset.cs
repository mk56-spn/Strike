// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Input;
using osu.Game.Beatmaps;
using osu.Game.Input.Handlers;
using osu.Game.Replays;
using osu.Game.Rulesets.Strike.Objects;
using osu.Game.Rulesets.Strike.Objects.Drawables;
using osu.Game.Rulesets.Strike.Replays;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Strike.UI
{
    [Cached]
    public partial class DrawableStrikeRuleset : DrawableRuleset<StrikeHitObject>
    {
        public DrawableStrikeRuleset(StrikeRuleset ruleset, IBeatmap beatmap, IReadOnlyList<Mod> mods = null)
            : base(ruleset, beatmap, mods)
        {
        }

        protected override Playfield CreatePlayfield() => new StrikePlayfield();

        protected override ReplayInputHandler CreateReplayInputHandler(Replay replay) => new StrikeFramedReplayInputHandler(replay);

        public override DrawableHitObject<StrikeHitObject> CreateDrawableRepresentation(StrikeHitObject h) => new DrawableStrikeHitObject(h);

        protected override PassThroughInputManager CreateInputManager() => new StrikeInputManager(Ruleset?.RulesetInfo);
    }
}
