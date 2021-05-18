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
            accessories.Add(new Accessory("Necklace", Item.RarityEnum.Common, new PlayerValueModifier(PlayerValueModifier.Health.none, PlayerValueModifier.Mana.none, new PlayerValueModifier.Damage(0, 0.5), PlayerValueModifier.Defence.none)));
            accessories.Add(new Accessory("Rabbit's ear", Item.RarityEnum.Legendary, new PlayerValueModifier(new PlayerValueModifier.Health(1000, 20, 2, 2), new PlayerValueModifier.Mana(1000, 0, 20, 1, 0.5, 5), new PlayerValueModifier.Damage(50, 2), new PlayerValueModifier.Defence(20, 5))));
            accessories.Add(new Accessory("Lava charm", Item.RarityEnum.Rare, PlayerValueModifier.none));
            accessories.Add(new Accessory("Water charm", Item.RarityEnum.Rare, PlayerValueModifier.none));
            accessories.Add(new Accessory("Air charm", Item.RarityEnum.Rare, PlayerValueModifier.none));
            accessories.Add(new Accessory("Earth charm", Item.RarityEnum.Rare, PlayerValueModifier.none));
            accessories.Add(new Accessory("Bean husk", Item.RarityEnum.Uncommon, PlayerValueModifier.none));
            accessories.Add(new Accessory("Metal can", Item.RarityEnum.Common, PlayerValueModifier.none));
            accessories.Add(new Accessory("Pringles can", Item.RarityEnum.Common, PlayerValueModifier.none));
            accessories.Add(new Accessory("Quartz chunk", Item.RarityEnum.Rare, PlayerValueModifier.none));
            accessories.Add(new Accessory("Dragon's ear", Item.RarityEnum.Mythical, PlayerValueModifier.none));
            accessories.Add(new Accessory("Cat's tail", Item.RarityEnum.Rare, PlayerValueModifier.none));
            accessories.Add(new Accessory("Bracelet", Item.RarityEnum.Common, PlayerValueModifier.none));
            accessories.Add(new Accessory("Mana chain", Item.RarityEnum.Uncommon, PlayerValueModifier.none));
            accessories.Add(new Accessory("Cube o' Rubik", Item.RarityEnum.Legendary, PlayerValueModifier.none));
            accessories.Add(new Accessory("Blue Switch", Item.RarityEnum.Uncommon, PlayerValueModifier.none));
            accessories.Add(new Accessory("Red Switch", Item.RarityEnum.Uncommon, PlayerValueModifier.none));
            accessories.Add(new Accessory("Brown Switch", Item.RarityEnum.Uncommon, PlayerValueModifier.none));
            accessories.Add(new Accessory("Black switch", Item.RarityEnum.Uncommon, PlayerValueModifier.none));
            accessories.Add(new Accessory("Cherry red switch", Item.RarityEnum.Rare, PlayerValueModifier.none));
            accessories.Add(new Accessory("Cherry blue switch", Item.RarityEnum.Rare, PlayerValueModifier.none));
            accessories.Add(new Accessory("Lava mask", Item.RarityEnum.Rare, PlayerValueModifier.none));
            accessories.Add(new Accessory("Frost mask", Item.RarityEnum.Rare, PlayerValueModifier.none));
            accessories.Add(new Accessory("Something.unkown", Item.RarityEnum.Legendary, PlayerValueModifier.none));
            accessories.Add(new Accessory("null?", Item.RarityEnum.Mythical, PlayerValueModifier.none));
            accessories.Add(new Accessory("Stone o charms", Item.RarityEnum.Common, PlayerValueModifier.none));
        }

        public Item itemData;
        public PlayerValueModifier value;
        public Accessory(string name, Item.RarityEnum rarity,PlayerValueModifier Value)
        {
            itemData = new Item(name, Item.ItemType.Accessory, rarity);
            value = Value;
        }

        public Accessory(Accessory _accessory)
        {
            itemData = new Item(_accessory.itemData.name, Item.ItemType.Accessory, _accessory.itemData.rarity);
            value = _accessory.value;
        }
    }
}
