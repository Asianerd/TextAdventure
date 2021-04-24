using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Weapon
    {
        /*public static Weapon Instance { get {  return } }
        static Weapon _instance = new Weapon();
*/
        public static List<Weapon> weapons;

        public static void initializeWeapons()
        {
            weapons = new List<Weapon>();
            weapons.Add(new Weapon("Copper broadsword", 10f, Item.RarityEnum.Common));
            weapons.Add(new Weapon("Iron broadsword", 13f, Item.RarityEnum.Uncommon));
            weapons.Add(new Weapon("Karambit", 20f, Item.RarityEnum.Rare, 0.005f));
            weapons.Add(new Weapon("Metal bat", 35f, Item.RarityEnum.Legendary, 0.15f));
            weapons.Add(new Weapon("Katana", 50f, Item.RarityEnum.Mythical, 0f));
        }

        public double damage;
        public Item itemData;
        public float missChance;

        public Weapon(string Name, double Damage, Item.RarityEnum Rarity, float MissChance = 0.1f)
        {
            // Miss chance = chance to get a number below missChance when generating a number from 0 to 1
            damage = Damage;
            itemData = new Item(Name, Item.ItemType.Weapon, Rarity);
            missChance = MissChance;
        }
    }
}
