using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Game
    {
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
            });

            //player.effects.Add(new Effects(Effects.EffectEnum.Burning, 3));
            player.PrintStats();
            Enemy[] enemies = new Enemy[] { new Enemy("Spider", 10, 3), new Enemy("Moon Lord", 10, 10, Boss: true) };
            Battle.Instance.Start(enemies);
        }
    }
}
