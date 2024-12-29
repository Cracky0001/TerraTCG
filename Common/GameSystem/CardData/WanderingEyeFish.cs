﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTCG.Common.GameSystem.GameState;
using TerraTCG.Common.GameSystem.GameState.Modifiers;

namespace TerraTCG.Common.GameSystem.CardData
{
    internal class WanderingEyeFish : BaseCardTemplate, ICardTemplate
    {
        public override Card CreateCard() => new ()
        {
            Name = "WanderingEyeFish",
            MaxHealth = 5,
            MoveCost = 2,
            CardType = CardType.CREATURE,
            NPCID = NPCID.EyeballFlyingFish,
            SubTypes = [CardSubtype.BLOOD_MOON, CardSubtype.SCOUT],
            Attacks = [
                new() {
                    Damage = 2,
                    Cost = 2,
                }
            ],
            Modifiers = () => [
                new ZealousModifier(),
                new EvasiveModifier(),
            ]
        };
    }
}
