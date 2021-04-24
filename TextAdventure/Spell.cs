﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Spell
    {
        /*public static Spell[] spells = new Spell[] { 
            new Spell("Regeneration", 50, SpellType.Debuff, new Effects[] { new Effects(Effects.EffectEnum.Regeneration, 10) }), 
            new Spell("Strength", 50, SpellType.Debuff, new Effects[] { new Effects(Effects.EffectEnum.Strength, 4) })
        };*/

        public static List<Spell> spells;

        public static void initializeSpells()
        {
            spells = new List<Spell>();
            spells.Add(new Spell("Regeneration", 50, SpellType.Debuff, new Effects[] { new Effects(Effects.EffectEnum.Regeneration, 10) }));
            spells.Add(new Spell("Strength", 50, SpellType.Debuff, new Effects[] { new Effects(Effects.EffectEnum.Strength, 4) }));
        }

        public string name;
        public double manaUsage;
        public SpellType type;
        public Effects[] effect;

        public Spell(string _name, double _manaUsage, SpellType _type, Effects[] _effects)
        {
            name = _name;
            manaUsage = _manaUsage;
            type = _type;
            effect = _effects;
        }

        public void ApplyEffect()
        {
            foreach (Effects x in effect)
            {
                Player.Instance.effectsEquipped.Add(x);
            }
        }

        public enum SpellType
        {
            Impulse,
            Debuff
        }
    }
}