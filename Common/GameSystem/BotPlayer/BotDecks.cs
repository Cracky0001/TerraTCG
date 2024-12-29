﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using TerraTCG.Common.GameSystem.CardData;
using TerraTCG.Common.GameSystem.GameState;

namespace TerraTCG.Common.GameSystem.BotPlayer
{
    internal class BotDecks
    {
        private static readonly Random random = new Random();
        public static Card CreateCard<T>() where T : BaseCardTemplate, ICardTemplate
            => ModContent.GetInstance<T>().Card;
        
        public static CardCollection GetDeck(int deckIdx = -1)
        {
            var allDecks = new List<Func<CardCollection>> {
                GetJungleDeck,
                GetForestDeck,
                GetBloodMoonDeck,
                GetSkeletonDeck,
                GetGoblinDeck,
                GetMimicDeck,
                GetCrabDeck,
                GetMushroomDeck,
                GetCurseDeck,
                GetSlimeDeck,
            };
            var randIdx = Math.Abs((int)random.NextInt64()) % allDecks.Count;
            return allDecks[deckIdx == -1 ? randIdx : deckIdx].Invoke();
        }

        public static CardCollection GetJungleDeck() =>
            new ()
            {
                Cards = [
                    CreateCard<Bunny>(), 
                    CreateCard<Bunny>(), 
                    CreateCard<Dryad>(), 
                    CreateCard<Goldfish>(), 
                    CreateCard<Guide>(),
                    CreateCard<Guide>(),
                    CreateCard<JungleTurtle>(), 
                    CreateCard<JungleTurtle>(), 
                    CreateCard<GiantTortoise>(),
                    CreateCard<GiantTortoise>(),
                    CreateCard<Wizard>(),
                    CreateCard<Wizard>(),
                    CreateCard<Piranha>(),
                    CreateCard<Piranha>(),
                    CreateCard<SpikedJungleSlime>(),
                    CreateCard<SpikedJungleSlime>(),
                    CreateCard<HealingPotion>(), 
                    CreateCard<HealingPotion>(), 
                    CreateCard<CobaltShield>(), 
                    CreateCard<CobaltShield>(), 
                ]
            };
        public static CardCollection GetBloodMoonDeck() =>
            new()
            {
                Cards = [
                    CreateCard<Guide>(), 
                    CreateCard<Guide>(), 
                    CreateCard<Dryad>(), 
                    CreateCard<Dryad>(), 
                    CreateCard<Wizard>(), 
                    CreateCard<ArmsDealer>(), 
                    CreateCard<ArmsDealer>(), 
                    CreateCard<SwiftnessPotion>(), 
                    CreateCard<ViciousGoldfish>(), 
                    CreateCard<ViciousGoldfish>(), 
                    CreateCard<ZombieMerman>(), 
                    CreateCard<ZombieMerman>(), 
                    CreateCard<RagePotion>(), 
                    CreateCard<RagePotion>(), 
                    CreateCard<BloodZombie>(), 
                    CreateCard<BloodZombie>(), 
                    CreateCard<Drippler>(), 
                    CreateCard<Drippler>(), 
                    CreateCard<WanderingEyeFish>(), 
                    CreateCard<WanderingEyeFish>(), 
                ]
            };
        public static CardCollection GetForestDeck() =>
            new()
            {
                Cards = [
                    CreateCard<Guide>(), 
                    CreateCard<Guide>(), 
                    CreateCard<Wizard>(), 
                    CreateCard<OldMan>(), 
                    CreateCard<Bunny>(), 
                    CreateCard<Bunny>(), 
                    CreateCard<Squirrel>(), 
                    CreateCard<CopperShortsword>(), 
                    CreateCard<CopperShortsword>(), 
                    CreateCard<Zombie>(), 
                    CreateCard<Zombie>(), 
                    CreateCard<BlueSlime>(), 
                    CreateCard<GreenSlime>(), 
                    CreateCard<DemonEye>(), 
                    CreateCard<DemonEye>(), 
                    CreateCard<HealingPotion>(), 
                    CreateCard<FledglingWings>(), 
                    CreateCard<PosessedArmor>(), 
                    CreateCard<Wraith>(), 
                    CreateCard<WanderingEye>(), 
                ]
            };

        public static CardCollection GetSkeletonDeck() =>
            new()
            {
                Cards = [
                    CreateCard<Guide>(), 
                    CreateCard<Guide>(), 
                    CreateCard<Wizard>(), 
                    CreateCard<CopperShortsword>(), 
                    CreateCard<FledglingWings>(), 
                    CreateCard<PlatinumBroadsword>(), 
                    CreateCard<PlatinumBroadsword>(), 
                    CreateCard<CobaltShield>(), 
                    CreateCard<CobaltShield>(), 
                    CreateCard<FeralClaws>(), 
                    CreateCard<FeralClaws>(), 
                    CreateCard<AngryBones>(), 
                    CreateCard<Muramasa>(), 
                    CreateCard<UndeadMiner>(), 
                    CreateCard<UndeadMiner>(), 
                    CreateCard<ArmoredSkeleton>(), 
                    CreateCard<ArmoredSkeleton>(), 
                    CreateCard<UndeadViking>(), 
                    CreateCard<UndeadViking>(), 
                    CreateCard<HealingPotion>(), 
                ]
            };
        public static CardCollection GetGoblinDeck() =>
            new()
            {
                Cards = [
                    CreateCard<Guide>(), 
                    CreateCard<Guide>(), 
                    CreateCard<GoblinThief>(), 
                    CreateCard<GoblinThief>(), 
                    CreateCard<ThrowingKnife>(), 
                    CreateCard<ThrowingKnife>(), 
                    CreateCard<GoblinWarlock>(), 
                    CreateCard<GoblinWarlock>(), 
                    CreateCard<GoblinArcher>(), 
                    CreateCard<GoblinArcher>(), 
                    CreateCard<GoblinScout>(), 
                    CreateCard<GoblinScout>(), 
                    CreateCard<HealingPotion>(), 
                    CreateCard<HealingPotion>(), 
                    CreateCard<RagePotion>(), 
                    CreateCard<RagePotion>(), 
                    CreateCard<AngelStatue>(), 
                    CreateCard<AngelStatue>(), 
                    CreateCard<SwiftnessPotion>(), 
                    CreateCard<SwiftnessPotion>(), 
                ]
            };

        public static CardCollection GetMimicDeck() =>
            new()
            {
                Cards = [
                    CreateCard<Guide>(), 
                    CreateCard<Guide>(), 
                    CreateCard<LostGirl>(), 
                    CreateCard<LostGirl>(), 
                    CreateCard<ThrowingKnife>(), 
                    CreateCard<ThrowingKnife>(), 
                    CreateCard<Mimic>(), 
                    CreateCard<Mimic>(), 
                    CreateCard<Tim>(), 
                    CreateCard<Tim>(), 
                    CreateCard<GoblinThief>(), 
                    CreateCard<GoblinThief>(), 
                    CreateCard<HealingPotion>(), 
                    CreateCard<HealingPotion>(), 
                    CreateCard<IronskinPotion>(), 
                    CreateCard<IronskinPotion>(), 
                    CreateCard<AngelStatue>(), 
                    CreateCard<AngelStatue>(), 
                    CreateCard<SwiftnessPotion>(), 
                    CreateCard<SwiftnessPotion>(), 
                ]
            };
        public static CardCollection GetCrabDeck() =>
            new()
            {
                Cards = [
                    CreateCard<Guide>(), 
                    CreateCard<Guide>(), 
                    CreateCard<PartyGirl>(), 
                    CreateCard<PartyGirl>(), 
                    CreateCard<CopperShortsword>(), 
                    CreateCard<CopperShortsword>(), 
                    CreateCard<Muramasa>(), 
                    CreateCard<RagePotion>(), 
                    CreateCard<RagePotion>(), 
                    CreateCard<Dolphin>(), 
                    CreateCard<Dolphin>(), 
                    CreateCard<Crab>(), 
                    CreateCard<Crab>(), 
                    CreateCard<Jellyfish>(), 
                    CreateCard<Jellyfish>(), 
                    CreateCard<Shark>(), 
                    CreateCard<Shark>(), 
                    CreateCard<FeralClaws>(), 
                    CreateCard<FeralClaws>(), 
                    CreateCard<HealingPotion>(), 
                ]
            };
        public static CardCollection GetMushroomDeck() =>
            new ()
            {
                Cards = [
                    CreateCard<GlowingSnail>(), 
                    CreateCard<GlowingSnail>(), 
                    CreateCard<Guide>(),
                    CreateCard<Guide>(),
                    CreateCard<MushroomZombie>(), 
                    CreateCard<MushroomZombie>(), 
                    CreateCard<AnomuraFungus>(),
                    CreateCard<AnomuraFungus>(),
                    CreateCard<Wizard>(),
                    CreateCard<Wizard>(),
                    CreateCard<SporeSkeleton>(),
                    CreateCard<SporeSkeleton>(),
                    CreateCard<SporeBat>(),
                    CreateCard<SporeBat>(),
                    CreateCard<HealingPotion>(), 
                    CreateCard<HealingPotion>(), 
                    CreateCard<CobaltShield>(), 
                    CreateCard<CobaltShield>(), 
                    CreateCard<PlatinumBroadsword>(),
                    CreateCard<PlatinumBroadsword>(),
                ]
            };

        public static CardCollection GetCurseDeck() =>
            new ()
            {
                Cards = [
                    CreateCard<GlowingSnail>(), 
                    CreateCard<GlowingSnail>(), 
                    CreateCard<Guide>(),
                    CreateCard<Guide>(),
                    CreateCard<CursedSkull>(), 
                    CreateCard<ToxicSludge>(),
                    CreateCard<Wizard>(),
                    CreateCard<Wizard>(),
                    CreateCard<DarkCaster>(),
                    CreateCard<DarkCaster>(),
                    CreateCard<JungleTurtle>(),
                    CreateCard<JungleTurtle>(),
                    CreateCard<HealingPotion>(), 
                    CreateCard<HealingPotion>(), 
                    CreateCard<CobaltShield>(), 
                    CreateCard<CobaltShield>(), 
                    CreateCard<IronskinPotion>(),
                    CreateCard<IronskinPotion>(),
                    CreateCard<GiantTortoise>(),
                    CreateCard<GiantTortoise>(),
                ]
            };

        public static CardCollection GetSlimeDeck() =>
            new ()
            {
                Cards = [
                    CreateCard<BlueSlime>(), 
                    CreateCard<BlueSlime>(), 
                    CreateCard<Guide>(),
                    CreateCard<Guide>(),
                    CreateCard<GreenSlime>(), 
                    CreateCard<GreenSlime>(),
                    CreateCard<Wizard>(),
                    CreateCard<Wizard>(),
                    CreateCard<ToxicSludge>(),
                    CreateCard<ToxicSludge>(),
                    CreateCard<SpikedJungleSlime>(),
                    CreateCard<SpikedJungleSlime>(),
                    CreateCard<HealingPotion>(), 
                    CreateCard<HealingPotion>(), 
                    CreateCard<Pinky>(), 
                    CreateCard<Pinky>(), 
                    CreateCard<KingSlime>(),
                    CreateCard<KingSlime>(),
                    CreateCard<MotherSlime>(),
                    CreateCard<MotherSlime>(),
                ]
            };
    }
}
