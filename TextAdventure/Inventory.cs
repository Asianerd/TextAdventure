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
            empty.loots = new Dictionary<Loot, int>();
            foreach(Loot x in Loot.loots)
            {
                empty.loots.Add(x, 0);
            }
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
        public Dictionary<Loot, int> loots;

        public void DisplayInventory(InventoryType inventoryType, bool showEquipped = true)
        {
            PrintEquipped(inventoryType, showEquipped);
            Console.WriteLine();
            switch (inventoryType)
            {
                case InventoryType.Weapons:
                    foreach (var x in Player.Instance.inventory.weapons.Select((item, index) => new { item, index }))
                    {
                        Console.Write($"{x.index}. ");
                        Dialogue.ColoredPrint(x.item.itemData.name, GetRarityColor(x.item.itemData));
                    }
                    break;
                case InventoryType.Accessories:
                    foreach (var x in Player.Instance.inventory.accesories.Select((item, index) => new { item, index }))
                    {
                        Console.Write($"{x.index}. ");
                        Dialogue.ColoredPrint(x.item.itemData.name, GetRarityColor(x.item.itemData));
                    }
                    break;
                case InventoryType.Spells:
                    foreach (var x in Player.Instance.inventory.spells.Select((item, index) => new { item, index }))
                    {
                        Console.Write($"{x.index}. ");
                        Dialogue.ColoredPrint(x.item.itemData.name, GetRarityColor(x.item.itemData));
                    }
                    break;
                case InventoryType.Loots:
                    foreach(var x in Player.Instance.inventory.loots)
                    {
                        /*if(x.Value == 0)
                        {
                            continue;
                        }*/

                        if(x.Value != 0)
                        {
                            Dialogue.ColoredPrint($"{x.Key.itemData.name} : {x.Value}", GetRarityColor(x.Key.itemData));
                        }
                    }
                    break;
                default:
                    return;
            }
            Console.WriteLine();
            Console.WriteLine();

            void PrintEquipped(InventoryType _inventoryType, bool _show)
            {
                if (!_show)
                {
                    return;
                }
                
                switch(_inventoryType)
                {
                    case InventoryType.Weapons:
                        Console.Write("Weapon equipped : ");
                        Dialogue.ColoredPrint(Player.Instance.currentWeapon.itemData.name, Dialogue.rarityColors[(int)Player.Instance.currentWeapon.itemData.rarity]);
                        Console.WriteLine();
                        break;
                    case InventoryType.Accessories:
                        Console.Write("Accessories equipped : ");
                        foreach(Accessory x in Player.Instance.accessoriesEquipped)
                        {
                            Dialogue.ColoredPrint($"{x.itemData.name} ; ", Dialogue.rarityColors[(int)x.itemData.rarity], false);
                        }
                        Console.WriteLine();
                        break;
                    case InventoryType.Spells:
                        Console.Write("Spell equipped : ");
                        Dialogue.ColoredPrint(Player.Instance.currentSpell.itemData.name, Dialogue.rarityColors[(int)Player.Instance.currentSpell.itemData.rarity]);
                        break;
                }
            }

            ConsoleColor GetRarityColor(Item _itemData)
            {
                return Dialogue.rarityColors[(int)_itemData.rarity];
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
