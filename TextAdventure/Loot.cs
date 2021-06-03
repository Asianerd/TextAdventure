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
            loots.Add(new Loot("Lorem", Item.RarityEnum.Legendary));
            loots.Add(new Loot("Ipsum", Item.RarityEnum.Mythical));
        }

        public Item itemData;

        Loot(string Name, Item.RarityEnum Rarity)
        {
            itemData = new Item(Name, Item.ItemType.Loot, Rarity);
        }

        #region Translation
        public static List<LootBuffer> DictToList(Dictionary<Loot,int> dict)
        {
            List<LootBuffer> final = new List<LootBuffer>();
            foreach(KeyValuePair<Loot, int> x in dict)
            {
                final.Add(new LootBuffer(x));
            }
            return final;
        }

        public static Dictionary<Loot, int> ListToDict(List<LootBuffer> list)
        {
            Dictionary<Loot, int> final = new Dictionary<Loot, int>();

            foreach(LootBuffer x in list)
            {
                final.Add(x.Key, x.Value);
            }

            return final;
        }
        #endregion

        public class LootBuffer
        {
            public Loot Key;
            public int Value;

            public LootBuffer(KeyValuePair<Loot,int> kvp)
            {
                Key = kvp.Key;
                Value = kvp.Value;
            }

            public KeyValuePair<Loot, int> GetKVP { get { return new KeyValuePair<Loot, int>(Key, Value); } }
        }
    }
}
