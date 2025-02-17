﻿using DeepWoodsMod.API.Impl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.TerrainFeatures;
using System;

namespace DeepWoodsMod
{
    public class HealingFountain : LargeTerrainFeature
    {
        public HealingFountain()
           : base(false)
        {
        }

        public HealingFountain(Vector2 tile)
            : this()
        {
            this.Tile = tile;
        }

        public override Rectangle getBoundingBox()
        {
            return new Rectangle((int)Tile.X * 64, (int)Tile.Y * 64, 5 * 64, 3 * 64);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            Vector2 globalPosition = Tile * 64f;

            Rectangle fountainTopSourceRectangle = new Rectangle(0, 0, 5 * 32, 2 * 32);
            Rectangle fountainBottomSourceRectangle = new Rectangle(0, 2 * 32, 5 * 32, 3 * 32);

            Vector2 globalTopPosition = new Vector2(globalPosition.X, globalPosition.Y - 2 * 64);
            Vector2 globalBottomPosition = new Vector2(globalPosition.X, globalPosition.Y);

            spriteBatch.Draw(DeepWoodsTextures.Textures.HealingFountain, Game1.GlobalToLocal(Game1.viewport, globalTopPosition), fountainTopSourceRectangle, Color.White, 0.0f, Vector2.Zero, 2f, SpriteEffects.None, ((Tile.Y + 1f) * 64f / 10000f + Tile.X / 100000f));
            spriteBatch.Draw(DeepWoodsTextures.Textures.HealingFountain, Game1.GlobalToLocal(Game1.viewport, globalBottomPosition), fountainBottomSourceRectangle, Color.White, 0.0f, Vector2.Zero, 2f, SpriteEffects.None, ((Tile.Y + 1f) * 64f / 10000f + Tile.X / 100000f));
        }

        public override bool performToolAction(Tool t, int damage, Vector2 tileLocation)
        {
            return false;
        }

        public override bool performUseAction(Vector2 tileLocation)
        {
            Game1.player.health = Game1.player.maxHealth;
            Game1.player.Stamina = Game1.player.MaxStamina;
            //Game1.player.addedLuckLevel.Value = Math.Max(10, Game1.player.addedLuckLevel.Value);
            //Game1.player.buffs.Apply(new Buff());

            Location.playSound("gulp", tileLocation);
            DelayedAction.playSoundAfterDelay("yoba", 800, Location);
            Game1.addHUDMessage(new HUDMessage(I18N.HealingFountainDrinkMessage) { noIcon = true });

            return true;
        }
    }
}
