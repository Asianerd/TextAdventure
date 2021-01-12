using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Accessory
    {
        public Accessory(double MaxHealth,
            double Damage,
            double MaxMana,
            double MaxHealthMultiplier = 1,
            double DamageMultiplier = 1,
            double MaxManaMultiplier = 1)
        {
            maxHealth = MaxHealth;
            maxHealthMultiplier = MaxHealthMultiplier;
            maxMana = MaxMana;
            maxManaMultiplier = MaxManaMultiplier;
            damage = Damage;
            damageMultiplier = DamageMultiplier;
        }

        public double maxHealth = 0;
        public double maxHealthMultiplier = 1;



        public double maxMana = 0;
        public double maxManaMultiplier = 1;

        public int manaUseReductionMultiplier = 1;


        public double damage = 0;
        public double damageMultiplier = 1;


        public static double GetFinal(string type)
        {
            double total = 0;
            double multiplierTotal = 1;
            switch(type)
            {
                default:
                    foreach(Accessory x in Game.player.accessories)
                    {
                        total += x.maxHealth;
                        multiplierTotal += x.maxHealthMultiplier;
                    }
                    break;
                case "damage":
                    foreach (Accessory x in Game.player.accessories)
                    {
                        total += x.damage;
                        multiplierTotal += x.damageMultiplier;
                    }
                    break;
                case "maxMana":
                    foreach (Accessory x in Game.player.accessories)
                    {
                        total += x.maxMana;
                        multiplierTotal += x.maxManaMultiplier;
                    }
                    break;
                case "manaUse":
                    total = 1;
                    foreach (Accessory x in Game.player.accessories)
                    {
                        if (multiplierTotal > x.manaUseReductionMultiplier)
                        {
                            multiplierTotal = x.manaUseReductionMultiplier;
                        }
                    }
                    break;
            }
            if (!(new string[] {
                "manaUse"
            }.Contains(type)))// Types that only rely on multipliers and not totals
            {
                if (Game.player.accessories.Count <= 0)
                {
                    multiplierTotal = 0;
                }
                else
                {
                    multiplierTotal /= Game.player.accessories.Count;
                }
                return total * multiplierTotal;
            }
            else
            {
                if(Game.player.accessories.Count<= 0)
                {
                    multiplierTotal = 1;
                }
                return multiplierTotal;
            }
        }
    }
}
