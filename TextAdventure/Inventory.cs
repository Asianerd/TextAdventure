using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Inventory
    {
        public static void initializeInventory()
        {
            empty = new Inventory();
            empty.weapons = new List<Weapon>();
            empty.accesories = new List<Accessory>();
            empty.spells = new List<Spell>();
            empty.loots = new List<Loot>();
        }

        /* What to store : 
         *  - Weapons
         *  - Acessories
         *  - Spells
         *  - Loot
         */

        public static Inventory empty;

        public List<Weapon> weapons;
        public List<Accessory> accesories;
        public List<Spell> spells;
        public List<Loot> loots;

        public void DisplayInventory(InventoryType type, bool showEquipped = true)
        {
            switch(type)
            {
                case InventoryType.Weapons:
                    if(showEquipped)
                    {
                        Console.Write($"Weapon equipped : ");
                        Dialogue.ColoredPrint(Player.Instance.currentWeapon.itemData.name, Dialogue.rarityColors[(int)Player.Instance.currentWeapon.itemData.rarity]);
                        Console.WriteLine();
                    }
                    break;
                case InventoryType.Accessories:
                    break;
                case InventoryType.Spells:
                    break;
                case InventoryType.Loots:
                    break;
                default:
                    break;
            }
        }

        public enum InventoryType
        {
            Weapons,
            Accessories,
            Spells,
            Loots
        }
    }
}
