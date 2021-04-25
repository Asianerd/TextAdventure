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
         */
        public static Player player = Player.Instance;
        static void Main(string[] args)
        {
            Dialogue.initializeColors();
            Weapon.initializeWeapons();
            Accessory.initializeAccessories();
            Loot.initializeLoots();
            Spell.initializeSpells();
            Inventory.initializeInventory();
            Player.Instance.initializePlayer();

            Dialogue.TimedDialogue(new string[] {
                "$col$dHello there.",
                "$col$dWelcome to TextAdventure!",
                "$col$dPlease enter either 'attack','shield','cast' or 'inventory' as moves.",
                "$col$dThis game is a turn-based strategy game",
                "$col$dThere are various weapons and accessories to be used too.",
            }, 0);

            Enemy[] enemies = new Enemy[] { new Enemy("Spider", 100, 3), new Enemy("Moon Lord", 10, 10, Boss: true) };
            Battle.Instance.Start(enemies);
        }
    }
}
