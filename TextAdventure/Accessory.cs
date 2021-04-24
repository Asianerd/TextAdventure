using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Accessory
    {
        public static List<Accessory> accessories;

        public static void initializeAccessories()
        {
            accessories = new List<Accessory>();
            accessories.Add(new Accessory(new Item("Necklace", Item.ItemType.Accessory, Item.RarityEnum.Common), new PlayerValueModifier(PlayerValueModifier.Health.none, PlayerValueModifier.Mana.none, new PlayerValueModifier.Damage(0, 0.5), PlayerValueModifier.Defence.none)));
            accessories.Add(new Accessory(new Item("Rabbit's ear", Item.ItemType.Accessory, Item.RarityEnum.Legendary), new PlayerValueModifier(new PlayerValueModifier.Health(1000, 20, 2, 2), new PlayerValueModifier.Mana(1000, 0, 20, 1, 0.5, 5), new PlayerValueModifier.Damage(50, 2), new PlayerValueModifier.Defence(20, 5))));
        }

        public Item itemData;
        public PlayerValueModifier value;
        public Accessory(Item ItemData,PlayerValueModifier Value)
        {
            itemData = ItemData;
            value = Value;
        }
    }
}
