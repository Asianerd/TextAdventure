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
         * Adding random amount of items to inventory
         */
        public static Player player = Player.Instance;
        public static Random rand = new Random();
        static void Main(string[] args)
        {
            Dialogue.initializeColors();
            Weapon.initializeWeapons();
            Accessory.initializeAccessories();
            Loot.initializeLoots();
            Spell.initializeSpells();
            Inventory.initializeInventory();
            player.initializePlayer();

            Dialogue.TimedDialogue(new string[] {
                "$col$dHello there.",
                "$col$dWelcome to TextAdventure!",
                "$col$dPlease enter either 'attack','shield','cast' or 'inventory' as moves.",
                "$col$dThis game is a turn-based strategy game",
                "$col$dThere are various weapons and accessories to be used too.",
            }, 0);


            for (int i = 0; i < 50; i++)
            {
                player.inventory.accessories.Add(new Accessory(Accessory.accessories[rand.Next(0, (Accessory.accessories.Count))]));
                player.inventory.weapons.Add(new Weapon(Weapon.weapons[rand.Next(0, (Weapon.weapons.Count))]));
                player.inventory.spells.Add(new Spell(Spell.spells[rand.Next(0, (Spell.spells.Count))]));
                player.inventory.loots[Loot.loots[rand.Next(0, Loot.loots.Count)]]++;
            }

            //Item searchItem = Inventory.Search(player.inventory.weapons.Select(n => n.itemData).ToList(), player.currentWeapon.itemData);

            Battle.Instance.Start(new Enemy[] { new Enemy("Spider", 100, 3), new Enemy("Moon Lord", 10, 10, Boss: true) });
        }
    }
}
