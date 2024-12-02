﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using TerraTCG.Common.GameSystem.Drawing.Animations;

namespace TerraTCG.Common.GameSystem.GameState.GameActions
{
    internal class DeployCardAction(Card card, GamePlayer player) : IGameAction
    {
        private Zone zone;
        public bool CanAcceptCardInHand(Card card) => false;

        public bool CanAcceptZone(Zone zone) => player.Owns(zone) && zone.IsEmpty();

        public bool AcceptCardInHand(Card card) => false;

        public bool AcceptZone(Zone zone)
        {
            this.zone = zone;
            return true;
        }

        public void Complete()
        {
            zone.PlaceCard(card);
            zone.Animation = new PlaceCardAnimation(zone, Main._drawInterfaceGameTime.TotalGameTime);
            player.Hand.Remove(card);
        }

        public void Cancel()
        {
            // No-op
        }

    }
}
