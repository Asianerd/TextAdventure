using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class PlayerValueModifier
    {
        // Houses the class that contains what player data can be altered
        // Refer to PValues.txt for the values that will be altered

        public class Health
        {
            public static Health none = new Health(0, 0, 1, 1);
            public Health(double AMaxHealth,double ARegen,double MMaxHealth, double MRegen)
            {
                maxHealth = AMaxHealth;
                regenAddition = ARegen;

                maxHealthMultiplier = MMaxHealth;
                regenMultiplier = MRegen;
            }

            // Additions
            public double maxHealth;
            public double regenAddition;

            // Multipliers
            public double maxHealthMultiplier;
            public double regenMultiplier;
        }

        public class Mana
        {
            public static Mana none = new Mana(0, 0, 0, 1, 1, 1);
            public Mana(double AMaxMana, double AUsage, double ARegen, double MMaxMana, double MUsage, double MRegen)
            {
                maxMana = AMaxMana;
                usageAddition = AUsage;
                regenAddition = ARegen;

                maxManaMultiplier = MMaxMana;
                usageMultiplier = MUsage;
                regenMultiplier = MRegen;
            }

            // Additions
            public double maxMana;
            public double usageAddition;
            public double regenAddition;

            // Multipliers
            public double maxManaMultiplier;
            public double usageMultiplier;
            public double regenMultiplier;
        }

        public class Damage
        {
            public static Damage none = new Damage(0, 1);
            public Damage(double ADamage, double MDamage)
            {
                damageAddition = ADamage;
                damageMultiplier = MDamage;
            }
            // Addition
            public double damageAddition;
            // Multipliers
            public double damageMultiplier;
        }

        public class Defence
        {
            public static Defence none = new Defence(0, 1);
            public Defence(double ADefence,double MDefence)
            {
                defenceAddition = ADefence;
                defenceMultiplier = MDefence;
            }
            // Additions
            public double defenceAddition;
            // Multipliers
            public double defenceMultiplier;
        }

        public static PlayerValueModifier none = new PlayerValueModifier(Health.none, Mana.none, Damage.none,Defence.none);

        public Health healthMod;
        public Mana manaMod;
        public Damage damageMod;
        public Defence defenceMod;

        public PlayerValueModifier(Health HealthClass = null, Mana ManaClass = null, Damage DamageClass = null, Defence DefenceClass = null)
        {
            healthMod = HealthClass != null ? HealthClass : Health.none;
            manaMod = ManaClass != null ? ManaClass : Mana.none;
            damageMod = DamageClass != null ? DamageClass : Damage.none;
            defenceMod = DefenceClass != null ? DefenceClass : Defence.none;
        }

        public enum ModType
        {
            MaxHealth,
            HealthRegen,

            MaxMana,
            ManaUsage,
            ManaRegen,

            Damage,

            Defence
        }

        public static double GetFinalMod(double baseValue, List<PlayerValueModifier> values, ModType type)
        {
            // yea code looks ugly but its so that it doesnt have to run a switch statement for each equipped item of the player
            double final;
            double addTotal = 0, multiTotal = 1;
            if (values.Count > 0)
            {
                switch (type)
                {
                    default:
                        break;
                    #region Health
                    case ModType.MaxHealth:
                        foreach (PlayerValueModifier x in values)
                        {
                            addTotal += x.healthMod.maxHealth;
                            multiTotal *= x.healthMod.maxHealthMultiplier;
                        }
                        break;
                    case ModType.HealthRegen:
                        foreach (PlayerValueModifier x in values)
                        {
                            addTotal += x.healthMod.regenAddition;
                            multiTotal *= x.healthMod.regenMultiplier;
                        }
                        break;
                    #endregion
                    #region Mana
                    case ModType.MaxMana:
                        foreach (PlayerValueModifier x in values)
                        {
                            addTotal += x.manaMod.maxMana;
                            multiTotal *= x.manaMod.maxManaMultiplier;
                        }
                        break;
                    case ModType.ManaUsage:
                        foreach (PlayerValueModifier x in values)
                        {
                            addTotal += x.manaMod.usageAddition;
                            multiTotal *= x.manaMod.usageMultiplier;
                        }
                        break;
                    case ModType.ManaRegen:
                        foreach (PlayerValueModifier x in values)
                        {
                            addTotal += x.manaMod.regenAddition;
                            multiTotal *= x.manaMod.regenMultiplier;
                        }
                        break;
                    #endregion
                    #region Damage
                    case ModType.Damage:
                        foreach (PlayerValueModifier x in values)
                        {
                            addTotal += x.damageMod.damageAddition;
                            multiTotal *= x.damageMod.damageMultiplier;
                        }
                        break;
                    #endregion
                    #region Defence
                    case ModType.Defence:
                        foreach (PlayerValueModifier x in values)
                        {
                            addTotal += x.defenceMod.defenceAddition;
                            multiTotal *= x.defenceMod.defenceMultiplier;
                        }
                        break;
                        #endregion
                }
            }
            else
            {
                addTotal = 0;
                multiTotal = 1;
            }
            final = addTotal + (baseValue * multiTotal);
            return final;
        }
    }
}
