using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace ChaosMod
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        List<Action> effects = new List<Action>();

        public override void Entry(IModHelper helper)
        {
            effects.Add(RandomEffect);
            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
        }

        private void AddEffects()
        {
            effects.Add(HighSpeed);
            effects.Add(LowSpeed);
        }


        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            if (!Context.IsPlayerFree || !Context.IsWorldReady || Game1.paused
                || Game1.activeClickableMenu != null)
                return;

            RandomEffect();
        }

        static Random rnd = new Random();
        private void RandomEffect()
        {
            int r = rnd.Next(effects.Count);
            effects[r]();
        }

        private void HighSpeed()
        {
            Game1.player.addedSpeed = 10;
        }

        private void LowSpeed()
        {
            Game1.player.addedSpeed = 0.2;
        }
    }
}