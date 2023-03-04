// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Beatmaps;
using osu.Game.Rulesets.Strike.Objects;
using osu.Game.Rulesets.Replays;

namespace osu.Game.Rulesets.Strike.Replays
{
    public class StrikeAutoGenerator : AutoGenerator<StrikeReplayFrame>
    {
        public new Beatmap<StrikeHitObject> Beatmap => (Beatmap<StrikeHitObject>)base.Beatmap;

        public StrikeAutoGenerator(IBeatmap beatmap)
            : base(beatmap)
        {
        }

        protected override void GenerateFrames()
        {
            Frames.Add(new StrikeReplayFrame());

            foreach (StrikeHitObject hitObject in Beatmap.HitObjects)
            {
                Frames.Add(new StrikeReplayFrame
                {
                    Time = hitObject.StartTime,
                    Position = hitObject.Position,
                    // todo: add required inputs and extra frames.
                });
            }
        }
    }
}
