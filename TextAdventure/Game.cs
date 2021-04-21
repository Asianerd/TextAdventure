using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Game
    {
        // Development related changes:
        /* Intro dialogue delay set to 0
         * Printing accessories at start of game
         */
        public static Player player = Player.Instance;
        static void Main(string[] args)
        {
            Dialogue.InitColors();
            Dialogue.TimedDialogue(new string[] {
                "$col$dHello there.",
                "$col$dWelcome to TextAdventure!",
                "$col$dPlease enter either 'attack','shield' or 'heal' as moves.",
                "$col$dThis game is a turn-based strategy game",
                "$col$dThere are various weapons and accessories to be used too.",
            }, 0);

            player.PrintAcessories();
            player.accessories.Add(new Accessory(new Item("Necklace", Item.ItemType.Accessory, Item.RarityEnum.Common),
                new PlayerValueModifier(
                    PlayerValueModifier.Health.none,
                    PlayerValueModifier.Mana.none,
                    new PlayerValueModifier.Damage(0, 0.5),
                    PlayerValueModifier.Defence.none
                    )));
            player.accessories.Add(new Accessory(new Item("Rabbit's ear", Item.ItemType.Accessory, Item.RarityEnum.Legendary), new PlayerValueModifier(new PlayerValueModifier.Health(1000, 20, 2, 2), new PlayerValueModifier.Mana(1000, 0, 20, 1, 0.5, 5), new PlayerValueModifier.Damage(50, 2), new PlayerValueModifier.Defence(20, 5))));
            player.PrintAcessories();
            Enemy[] enemies = new Enemy[] { new Enemy("Spider", 10, 3), new Enemy("Moon Lord", 10, 10, Boss: true) };
            Battle.Instance.Start(enemies);
        }
    }
}
