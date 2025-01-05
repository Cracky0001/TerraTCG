﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using TerraTCG.Common.Configs;
using TerraTCG.Common.GameSystem;
using TerraTCG.Common.GameSystem.Drawing;
using TerraTCG.Common.GameSystem.Drawing.Animations;
using TerraTCG.Common.UI.Common;
using TerraTCG.Common.UI.DeckbuildUI;
using static TerraTCG.Common.GameSystem.GameState.GameActions.IGameAction;

namespace TerraTCG.Common.UI.GameFieldUI
{
    internal class GameFieldElement : CustomClickUIElement
    {
        internal Vector2 FieldOrigin => new (
            Position.X + FieldRenderer.FIELD_WIDTH / 2,
            Position.Y + FieldRenderer.FIELD_HEIGHT);

        internal override bool IsClicked() => !((GameFieldState)Parent).actionButtons.ContainsMouse && base.IsClicked();

        private string zoneTooltip;
        private int zoneRare;

        public override bool ContainsPoint(Vector2 point)
        {
            return true; // Element occupies the whole screen
        }

        public override void Update(GameTime gameTime)
        {
            Main.LocalPlayer.mouseInterface = true;
            var localPlayer = TCGPlayer.LocalPlayer;
            localPlayer.GameFieldPosition = Position;

            zoneTooltip = "";
            zoneRare = 0;
            var gamePlayer = localPlayer.GamePlayer;
            if (gamePlayer == null || gamePlayer.Field?.Zones == null)
            {
                return;
            }
            // TODO there is probably a better place for setting animation state
            if(gamePlayer.Game.FieldAnimation?.IsComplete() ?? true)
            {
                gamePlayer.Game.FieldAnimation = null;
            }             
            foreach (var zone in gamePlayer.Game.AllZones())
            {
                zone.UpdateAnimationQueue();
            }
            var mouseField = Main.MouseScreen - Position;
            var prevMouseField = new Vector2(Main.lastMouseX, Main.lastMouseY) - Position;
            // Check if mouse-over-ing stats and set tooltip

            // Check both players' fields
            foreach (var zone in gamePlayer.Game.AllZones())
            {
                if (ProjectedFieldUtils.Instance.ZoneContainsScreenVector(gamePlayer, zone, mouseField))
                {
                    var inProgressAction = localPlayer.GamePlayer?.InProgressAction;
                    if((inProgressAction?.CanAcceptZone(zone) ?? false) && gamePlayer.IsMyTurn)
                    {
                        zoneTooltip = localPlayer.GamePlayer.InProgressAction.GetZoneTooltip(zone);
                    } else if (inProgressAction?.GetCantAcceptZoneTooltip(zone) is string tooltip && gamePlayer.IsMyTurn)
                    {
                        zoneTooltip = tooltip;
                        zoneRare = ItemRarityID.Red;
                    }
                    if(zone.HasPlacedCard())
                    {
                        localPlayer.MouseoverZone = zone;
                        localPlayer.MouseoverCard = zone.PlacedCard.Template;
                    }
                    if(IsClicked())
                    {
                        SoundEngine.PlaySound(SoundID.MenuTick);
                        gamePlayer.SelectZone(zone);
                    }
                    break;
                }
            }
        }

        private void DrawZoneNPCs(SpriteBatch spriteBatch)
        {
            var gamePlayer = TCGPlayer.LocalGamePlayer;
            // Iterate backwards to layer closer zones on top of farther zones
            foreach (var zone in gamePlayer.Game.AllZones().Reverse())
            {
                var yLerpPoint = gamePlayer.Owns(zone) ? 0.3f : 0.8f;
                var placement = ProjectedFieldUtils.Instance.WorldSpaceToScreenSpace(gamePlayer, zone, new(0.5f, yLerpPoint));
                var scale = ProjectedFieldUtils.Instance.GetXScaleForZone(gamePlayer, zone, yLerpPoint);
                zone.DrawNPC(spriteBatch, Position + placement, scale);
            }
        }

        private void DrawPlayerStats(SpriteBatch spriteBatch)
        {
            var texture = TextureCache.Instance.PlayerStatsZone;
            // My player
            var gamePlayer = TCGPlayer.LocalGamePlayer;
            var anchorZonePos = 
                ProjectedFieldUtils.Instance.WorldSpaceToScreenSpace(gamePlayer, gamePlayer.Field.Zones[3], new(0, 1));

            var pos = Position + new Vector2(
                anchorZonePos.X - texture.Width(),
                anchorZonePos.Y - texture.Height());
            PlayerStatRenderer.Instance.DrawPlayerStats(spriteBatch, pos, gamePlayer, 1f);

            // Opposing player
            var opponent = gamePlayer.Opponent;
            var scale = ProjectedFieldUtils.Instance.GetXScaleForZone(gamePlayer, opponent.Field.Zones[3], 0f);
            anchorZonePos = 
                ProjectedFieldUtils.Instance.WorldSpaceToScreenSpace(gamePlayer, opponent.Field.Zones[3], new(1, 0));
            var oppPos = Position + new Vector2(
                anchorZonePos.X + FieldRenderer.CARD_MARGIN,
                anchorZonePos.Y);
            PlayerStatRenderer.Instance.DrawPlayerStats(spriteBatch, oppPos, opponent, scale);
        }

        private void DrawFieldOverlays(SpriteBatch spriteBatch)
        {
            TCGPlayer.LocalGamePlayer.Game.FieldAnimation?.DrawFieldOverlay(spriteBatch, Position);
        }

        private void DrawMapBg(SpriteBatch spriteBatch)
        {
            var texture = FieldRenderer.Instance.MapBGRenderTarget;
            var mapScaleX = Main.screenWidth / (float)texture.Width;
            var mapScaleY = Main.screenHeight / (float)texture.Height;
            var scale = Math.Max(mapScaleX, mapScaleY);
            var origin = new Vector2(texture.Width, texture.Height) / 2;
            var drawPos = new Vector2(Main.screenWidth, Main.screenHeight) / 2;

            spriteBatch.Draw(texture, drawPos, texture.Bounds, Color.White * TCGPlayer.FieldTransitionPoint, 0, origin, scale, SpriteEffects.None, 0);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var texture = FieldRenderer.Instance.PerspectiveRenderTarget;
            if(texture != null && TCGPlayer.LocalGamePlayer != null)
            {
                // draw the map background overlay
                DrawMapBg(spriteBatch);
                // draw the perspective-rendered game field
                spriteBatch.Draw(texture, Position, Color.White);
                DrawZoneNPCs(spriteBatch);
                DrawPlayerStats(spriteBatch);
                DrawFieldOverlays(spriteBatch);

                if(zoneTooltip != "" && zoneTooltip != "" && ModContent.GetInstance<ClientConfig>().ShowTooltips)
                {
                    DeckbuildState.SetTooltip(zoneTooltip, zoneRare);
                }
            }
        }
    }
}
