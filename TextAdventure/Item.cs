using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Item
    {
        public string name;
        public ItemType type;
        public RarityEnum rarity;

        public Item(string Name, ItemType Type ,RarityEnum Rarity)
        {
            name = Name;
            type = Type;
            rarity = Rarity;
        }

        public string FetchFormattedName(List<Item> items, Item equippedItem)
        {
            if (equippedItem == this)
            {
                return $"> {items.IndexOf(this)+1}. {name} <";
            }
            else
            {
                return $"{items.IndexOf(this)+1}. {name}";
            }
        }

        public string FetchFormattedName(List<Item> items, List<Item> equippedItems)
        {
            if (equippedItems.Contains(this))
            {
                return $"> {items.IndexOf(this) + 1}. {name} <";
            }
            else
            {
                return $"{items.IndexOf(this) + 1}. {name}";
            }
        }

        public string FetchFormattedName(List<Item> items, string keyword)
        {
            return $"{items.IndexOf(this)+1}. {(name.ToLower()).Replace(keyword.ToLower(), "|")}";
        }

        public enum ItemType
        {
            /*Accessory,
            Weapon,
            Loot*/
            Weapon,
            Accessory,
            Spell,
            Loot
        }

        public enum RarityEnum
        {
            Common,
            Uncommon,
            Rare,
            Legendary,
            Mythical
        }
    }
}
