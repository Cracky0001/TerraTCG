﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraTCG.Common.GameSystem.GameState.Modifiers;

namespace TerraTCG.Common.GameSystem.GameState
{
    internal delegate void DoAttack(Attack attack, Zone sourceZone, Zone targetZone);

    internal delegate List<ICardModifier> ApplyModifiers(Zone appliedZone);
    internal struct Attack()
    {
        internal string Name { get; set; }
        internal string Description { get; set; }
        internal int Damage { get; set; }
        internal int SelfDamage { get; set; }
        internal int Cost { get; set; }

        internal DoAttack DoAttack { get; set; } = DefaultAttack;

        // Modifiers applied to the attacker after the attack goes through
        internal ApplyModifiers SourceModifiers { get; set; }

        // Modifiers applied to the target after the attack goes through
        internal ApplyModifiers TargetModifiers { get; set; }


        // How much damage this attack does per mana spent, used by bots
        // in decision sequencing
        internal readonly float ManaEfficiency => Damage / (float)(Math.Max(Cost, 1));

        internal static void DefaultAttack(Attack attack, Zone sourceZone, Zone targetZone)
        {
            targetZone.PlacedCard.CurrentHealth -= attack.Damage;
            sourceZone.PlacedCard.CurrentHealth -= attack.SelfDamage;

            sourceZone.PlacedCard.CardModifiers.AddRange([.. attack.SourceModifiers?.Invoke(sourceZone) ?? []]);
            targetZone.PlacedCard.CardModifiers.AddRange([.. attack.TargetModifiers?.Invoke(targetZone) ?? []]);
        }

        internal Attack Copy()
        {
            return new Attack() 
            {
                Name = Name,
                Description = Description,
                Damage = Damage,
                SelfDamage = SelfDamage,
                Cost = Cost,
                SourceModifiers = SourceModifiers,
                TargetModifiers = TargetModifiers,
                DoAttack = DoAttack
            };
        }
    }
}
