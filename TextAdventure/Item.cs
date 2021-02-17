using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Item
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

        public enum ItemType
        {
            Accessory,
            Weapon
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
