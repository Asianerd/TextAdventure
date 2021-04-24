using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Loot
    {
        public static List<Loot> loots;
        public static void initializeLoots()
        {
            loots = new List<Loot>();
            loots.Add(new Loot("Cube", Item.RarityEnum.Common));
            loots.Add(new Loot("Cuboid", Item.RarityEnum.Uncommon));
        }

        public Item itemData;

        Loot(string Name, Item.RarityEnum Rarity)
        {
            itemData = new Item(Name, Item.ItemType.Loot, Rarity);
        }
    }
}
