using System;
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
        public List<Accessory> accessoriesEquipped;
        public List<Effects> effectsEquipped;
        public Weapon currentWeapon;
        public Spell currentSpell;
        public Inventory inventory;

        public double health;
        private double maxHealth = 100;
        public double healthRegen = 2;

        public double mana;
        private double maxMana = 100;
        public double manaRegen = 10;

        private double baseDamage = 5;

        #region Initialization
        public void initializePlayer()
        {
            health = maxHealth;
            mana = maxMana;

            accessoriesEquipped = new List<Accessory>();
            effectsEquipped = new List<Effects>();

            currentWeapon = Weapon.weapons[3]; // Copper broadsword
            currentSpell = Spell.spells[0]; // Regeneration spell

            inventory = Inventory.empty;
        }
        #endregion

        #region Data Fetching
        public double MaxHealth(bool raw = false)
        {
            if (raw)
            {
                return maxHealth;
            }
            else
            {
                return PlayerValueModifier.GetFinalMod(health, new List<PlayerValueModifier>(Instance.accessoriesEquipped.Select(n => n.value)), PlayerValueModifier.ModType.MaxHealth);
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
                return PlayerValueModifier.GetFinalMod(maxMana, new List<PlayerValueModifier>(Instance.accessoriesEquipped.Select(n => n.value)), PlayerValueModifier.ModType.MaxMana);
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
                return PlayerValueModifier.GetFinalMod(baseDamage + currentWeapon.damage, new List<PlayerValueModifier>(Instance.accessoriesEquipped.Select(n => n.value)), PlayerValueModifier.ModType.Damage);
            }
        }

        public double SpellManaUsage(bool raw = false)
        {
            if (raw)
            {
                return currentSpell.manaUsage;
            }
            else
            {
                return PlayerValueModifier.GetFinalMod(currentSpell.manaUsage, new List<PlayerValueModifier>(Instance.accessoriesEquipped.Select(n => n.value)), PlayerValueModifier.ModType.ManaUsage);
            }
        }

        public double HealthRegen(bool raw = false)
        {
            if(raw)
            {
                return healthRegen;
            }
            else
            {
                return PlayerValueModifier.GetFinalMod(healthRegen, new List<PlayerValueModifier>(Instance.accessoriesEquipped.Select(n => n.value)), PlayerValueModifier.ModType.HealthRegen);
            }
        }

        public double ManaRegen(bool raw = false)
        {
            if(raw)
            {
                return manaRegen;
            }
            else
            {
                return PlayerValueModifier.GetFinalMod(manaRegen, new List<PlayerValueModifier>(Instance.accessoriesEquipped.Select(n => n.value)), PlayerValueModifier.ModType.ManaRegen);
            }
        }
        #endregion

        #region Affect Data
        public void Regenerate(float rate = 1)
        {
            if (health < maxHealth)
            {
                AffectHealth(HealthRegen());
            }
            if (mana < maxMana)
            {
                AffectMana(ManaRegen());
            }
            if(health>maxHealth)
            {
                health = maxHealth;
            }
            if(mana>maxMana)
            {
                mana = maxMana;
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
        #endregion

        #region Checks
        public void CheckDeath()
        {
            alive = (health > 0);
        }
        #endregion

        #region Visual
        public void PrintStats()
        {
            string effects = string.Join("",Instance.effectsEquipped.Select(n => $";[{n.age}]:{n.name}; "));
            string[] stats =
            {
                $"\n{new string('-',25)}",
                $"  Health : {health}/{maxHealth}      {(effects.Length>=1?($"Effects : {effects}"):(""))}",
                $"  Mana   : {mana}/{maxMana}",
                $"  Damage : {Damage()}",
                $"{new string('-',25)}\n",
            };
            Dialogue.TimedDialogue(stats, 0);
        }
        #endregion
    }
}
