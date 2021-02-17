﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Player
    {
        public static Player Instance { get { return _instance; } }
        static Player _instance = new Player();
        private Player() { }

        public bool alive = true;
        public List<Accessory> accessories = new List<Accessory>();
        public List<Effects> effects = new List<Effects>();
        public Weapon currentWeapon;

        public double health = 100;
        private double maxHealth = 100;

        public double mana = 20;
        private double maxMana = 20;

        private double baseDamage = 5;

        private double healManaUsage = 20;
        public double healHealthGain = 50;

        public double MaxHealth(bool raw = false)
        {
            if (raw)
            {
                return maxHealth;
            }
            else
            {
                return PlayerValueModifier.GetFinalMod(baseDamage, new List<PlayerValueModifier>(Instance.accessories.Select(n => n.value)), PlayerValueModifier.ModType.Damage);
            }
        }
        public double MaxMana(bool raw = false)
        {
            if (raw)
            {
                return maxMana;
            }
            else
            {
                return PlayerValueModifier.GetFinalMod(maxMana, new List<PlayerValueModifier>(Instance.accessories.Select(n => n.value)), PlayerValueModifier.ModType.MaxMana);
            }
        }
        public double Damage(bool raw = false)
        {
            if (raw)
            {
                return baseDamage;
            }
            else
            {
                return PlayerValueModifier.GetFinalMod(baseDamage,new List<PlayerValueModifier>(Instance.accessories.Select(n => n.value)), PlayerValueModifier.ModType.Damage);
            }
        }

        public double HealManaUse(bool raw = false)
        {
            if (raw)
            {
                return healManaUsage;
            }
            else
            {
                return PlayerValueModifier.GetFinalMod(healManaUsage,new List<PlayerValueModifier>(Instance.accessories.Select(n => n.value)), PlayerValueModifier.ModType.ManaUsage);
            }
        }

        public void AffectHealth(double amount)
        {
            health += amount;
            CheckDeath();
        }

        public void AffectMana(double amount)
        {
            mana += amount;
        }

        public void CheckDeath()
        {
            alive = (health > 0);
        }

        public void PrintStats()
        {
            string effects = string.Join("",Instance.effects.Select(n => $";[{n.age}]:{n.name}; "));
            string[] stats =
            {
                $"\n{new string('-',25)}",
                $"  Health : {MaxHealth()}/{maxHealth}      {(effects.Length>=1?($"Effects : {effects}"):(""))}",
                $"  Mana   : {MaxMana()}/{maxMana}",
                $"  Damage : {Damage()}",
                $"{new string('-',25)}\n",
            };
            Dialogue.TimedDialogue(stats, 0);
        }

        public void PrintAcessories()
        {
            Console.WriteLine();
            foreach(Accessory x in accessories)
            {
                Console.WriteLine($"{x.acessoryItem.name}");
            }
            Console.WriteLine();
        }
    }
}
