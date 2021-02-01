using System;
using System.Linq;
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
            AddEffects();

            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
        }

        private void AddEffects()
        {
            effects.Add(HighSpeed);
            effects.Add(LowSpeed);
        }

        int ticksPassed = 0;
        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            if (!Context.IsPlayerFree || !Context.IsWorldReady || Game1.paused
                || Game1.activeClickableMenu != null)
                return;

            ticksPassed += 1;

            if(ticksPassed >= 600)
            {
                ticksPassed = 0;
                RandomEffect();
            }
        }

        static Random rnd = new Random();
        private void RandomEffect()
        {
            int r = rnd.Next(effects.Count);
            effects.ElementAt(r)();
        }

        private void HighSpeed()
        {
            Game1.player.addedSpeed = 10;
            this.Monitor.Log("High Speed", LogLevel.Debug);
        }

        private void LowSpeed()
        {
            Game1.player.addedSpeed = 1;
            this.Monitor.Log("Low Speed", LogLevel.Debug);
        }
    }
}