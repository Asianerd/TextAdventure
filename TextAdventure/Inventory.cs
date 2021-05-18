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
                // I rather have this mess than having c# be duck-typed
                case InventoryType.Weapons:
                    foreach (var x in Player.Instance.inventory.weapons.Select((item, index) => new { item, index }))
                    {
                        Console.Write($"{x.index + 1}. ");
                        Dialogue.ColoredPrint(x.item.itemData.name, GetRarityColor(x.item.itemData));
                    }
                    break;
                case InventoryType.Accessories:
                    foreach (var x in Player.Instance.inventory.accesories.Select((item, index) => new { item, index }))
                    {
                        Console.Write($"{x.index + 1}. ");
                        Dialogue.ColoredPrint(x.item.itemData.name, GetRarityColor(x.item.itemData));
                    }
                    break;
                case InventoryType.Spells:
                    foreach (var x in Player.Instance.inventory.spells.Select((item, index) => new { item, index }))
                    {
                        Console.Write($"{x.index + 1}. ");
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

        public static Item Search(List<Item> items)
        {
            Item final;
            WriteAll();
            while (true)
            {
                string searchKeyword;
                int index;
                Console.Write("Search/Index : ");
                searchKeyword = Console.ReadLine();


                if (int.TryParse(searchKeyword, out index))
                {
                    if (index > 0 && index < items.Count)
                    {
                        final = items[index];
                        break;
                    }
                }

                foreach (var x in items.Where(n => n.name.ToLower().Contains(searchKeyword.ToLower())).Select(n => new Tuple<Item, string>(n, n.FetchFormattedName(items))))
                {
                    HighlightKeyword(x.Item1, searchKeyword, items);
                }
            }

            return final;

            void WriteAll()
            {
                foreach (var x in items.Select(n => new Tuple<Item, string>(n, n.FetchFormattedName(items))))
                {
                    //Console.WriteLine(x.Item2);
                    Dialogue.ColoredPrint(x.Item2, Dialogue.rarityColors[(int)x.Item1.rarity]);
                }
            }

            void HighlightKeyword(Item item, string keyword, List<Item> itemList)
            {
                string word = item.FetchFormattedName(itemList, keyword);

                foreach (char x in word.ToCharArray())
                {
                    if (x == '|')
                    {
                        Console.ForegroundColor = Dialogue.rarityColors[(int)item.rarity];
                        Console.Write(keyword);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        //Console.Write(x);
                        Dialogue.ColoredPrint(x.ToString(), Dialogue.ToDarkerVariant(Dialogue.rarityColors[(int)item.rarity]), false);
                    }
                }
                Console.WriteLine();
            }
        }

        public object FetchInventoryItem<T>(InventoryType inventoryType, List<T> inventory)
        {
            /* Things to do : 
             *  - Display inventory (with index)
             *  - Take user input
             *  - Validate user input ( re-enter if invalid)
             *  - Return item at the inputted index
             */

            int finalIndex;
            string choice;

            while (true)
            {
                DisplayInventory(inventoryType);
                choice = Dialogue.Ask("Index : ");
                if(IsValidIndex(choice, out finalIndex))
                {
                    break;
                }
                Dialogue.TimedDialogue(new string[] { "$col$cIndex not found, please re-enter" });
            }

            return inventory[finalIndex-1];

            bool IsValidIndex(string _choice, out int _index)
            {
                int parsedChoice = 0;
                if(int.TryParse(_choice, out parsedChoice))
                {
                    _index = parsedChoice;
                    return (((parsedChoice - 1) < inventory.Count) && (parsedChoice > 0));
                }
                else
                {
                    _index = parsedChoice;
                    return false;
                }
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
