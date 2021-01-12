using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class PlayerValueModifier
    {
        // Houses the class that contains what player data can be altered
        // Refer to PValues.txt for the values that will be altered

        public class Health
        {
            public static Health none = new Health(0, 0, 1);
            public Health(double AMaxHealth,double ARegen, double MRegen)
            {
                maxHealth = AMaxHealth;
                regenAddition = ARegen;

                regenMultiplier = MRegen;
            }

            // Additions
            public double maxHealth = 0;
            public double regenAddition = 0;

            // Multipliers
            public double regenMultiplier = 1;
        }

        public class Mana
        {
            public static Mana none = new Mana(0,1,1,1);
            public Mana(double AMaxMana,double ARegen,double MUsage, double MRegen)
            {
                maxMana = AMaxMana;
                regenAddition = ARegen;

                usage = MUsage;
                regenMultiplier = MRegen;
            }

            // Additions
            public double maxMana = 0;
            public double regenAddition = 0;

            // Multipliers
            public double usage = 1;
            public double regenMultiplier = 1;
        }

        public class Damage
        {
            public static Damage none = new Damage(1);
            public Damage(double MDamage)
            {
                damageMultiplier = MDamage;
            }
            // Multipliers
            public double damageMultiplier = 1;
        }

        public class Defence
        {
            public static Defence none = new Defence(0,1);
            public Defence(double ADefence,double MDefence)
            {
                defence = ADefence;
                defenceMultiplier = MDefence;
            }
            // Additions
            public double defence;
            // Multipliers
            public double defenceMultiplier;
        }

        public static PlayerValueModifier none = new PlayerValueModifier(Health.none, Mana.none, Damage.none,Defence.none);

        public Health healthMod;
        public Mana manaMod;
        public Damage damageMod;
        public Defence defenceMod;

        public PlayerValueModifier(Health HealthClass, Mana ManaClass, Damage DamageClass, Defence DefenceClass)
        {
            healthMod = HealthClass;
            manaMod = ManaClass;
            damageMod = DamageClass;
            defenceMod = DefenceClass;
        }

/*        public void GetFinalMod(modClass ClassWanted, modType TypeWanted)
        {
            double total = 0;
            int typeWanted = (int)TypeWanted;
            switch((int)ClassWanted)
            {
                default:
                    if(typeWanted == 0)
                    {

                    }
                    break;
            }
        }

        public enum modClass
        {
            Health,
            Mana,
            Damage
        }

        public enum modType
        {
            Addition,
            Multiplier,
            Both
        }*/
    }
}
