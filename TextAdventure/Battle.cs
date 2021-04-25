using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Battle
    {
        public static string[] validMoves = new string[] {
            "attack",
            "shield",
            "heal",
            "a",
            "s",
            "h",


            "inventory",
            "i",
            "spells",
            "s"
        };// Long and short forms

        public static Battle Instance = new Battle();

        public void Start(Enemy[] Enemies)
        {
            while (Enemy.RemoveDead(Enemies).Length > 0)
            {
                Fight(Enemies);
            }
        }

        void Fight(Enemy[] Enemies)
        {
            // Contains the actual action like fighting and stuff
            Dialogue.ColoredPrint($">{new string('=', 75)}<",ConsoleColor.DarkGray);

            Enemy.PrintStats(Enemies);
            Console.WriteLine();

            Player.Instance.Regenerate();
            RunEffects();
            Player.Instance.CheckDeath();
            Player.Instance.PrintStats();

            if (Player.Instance.alive)
            {
                Dialogue.TimedDialogue(new string[] {
                    "Select an action [attack/shield/cast/inventory]"
                }, 0);

                string _choice = Console.ReadLine();
                Moves choice = DetermineMove(_choice);
                
                switch(choice)
                {
                    case Moves.Attack:
                        Enemy enemy = Enemy.RemoveDead(Enemies).Length == 1 ? Enemy.RemoveDead(Enemies)[0] : ChooseEnemy(Enemy.RemoveDead(Enemies));

                        double damageDealt = Player.Instance.Damage();// Change in the future if need be
                        Dialogue.TimedDialogue(new string[] {
                            $"$col$eYou dealt {damageDealt} damage to {(enemy.boss? "":"the ")}{enemy.name}!"
                        });
                        enemy.AffectHealth(-damageDealt);

                        if(enemy.dead)
                        {
                            Dialogue.TimedDialogue(new string[] {
                                $"$col$dYou killed {(enemy.boss? "":"the ")}{enemy.name}!",
                                $"$col$dYou gained absolutely nothing!"
                            });
                        }

                        // Possibly add something to indicate if the attack is powerful or not in the future
                        break;
                    case Moves.Shield:
                        Dialogue.TimedDialogue(new string[] {
                            $"$col$7You blocked the attack!"
                        });

                        break;
                    case Moves.Cast:
                        if (Player.Instance.mana >= PlayerValueModifier.GetFinalMod(Player.Instance.SpellManaUsage(), new List<PlayerValueModifier>(Player.Instance.accessoriesEquipped.Select(n => n.value)), PlayerValueModifier.ModType.ManaUsage))
                        {
                            Dialogue.TimedDialogue(new string[] {
                                $"$col$2You use {Player.Instance.SpellManaUsage()} mana to cast a {Player.Instance.currentSpell.itemData.name} spell",
                                $"$col$2Upon application on thyself you feel a surge in energy and a feeling of reassurance of your presence in battle",
                            });
                            Player.Instance.effectsEquipped.Add(new Effects(Effects.EffectEnum.Regeneration, 5));
                            Player.Instance.AffectMana(-Player.Instance.SpellManaUsage());
                        }
                        else
                        {
                            Dialogue.TimedDialogue(new string[] {
                                $"$col$9You didnt have enough mana to cast the spell! Needed {Player.Instance.SpellManaUsage()}",
                            });
                        }
                        break;
                    case Moves.Inventory:
                        Player.Instance.inventory.DisplayInventory(ChooseInventory());
                        break;
                    default:
                        break;
                }
                Dialogue.ColoredPrint($">{new string('=', 75)}<",ConsoleColor.DarkGray);
            }
        }

        void RunEffects()
        {
            string effectMessage = "";
            foreach (Effects x in Player.Instance.effectsEquipped.ToArray())
            {
                x.AdvanceAge();
                if (x.dead)
                {
                    Player.Instance.effectsEquipped.Remove(x);
                }
                else
                {
                    string message = x.ApplyEffect();
                    if (message != "")
                    {
                        effectMessage += $"{message}\n";
                    }
                }
            }

            if (effectMessage != "")
            {
                Dialogue.TimedDialogue(new string[] { 
                    effectMessage
                });
            }
        }

        Moves DetermineMove(string input)
        {
            foreach(Moves x in Enum.GetValues(typeof(Moves)))
            {
                string lowered = x.ToString().ToLower();
                if((input == lowered) || (input[0] == lowered[0]))
                {
                    return x;
                }
            }
            return Moves.None;
        }

        Enemy ChooseEnemy(Enemy[] Enemies)
        {
            // From the array, have the user select one and then return that enemy
            string choice;
            int finalChoice = 0;
            bool success = false;
            while (!success)
            {
                for(int i = 0;i<Enemies.Length; i++)
                {
                    Console.WriteLine($"{i+1}. {Enemies[i].name}");
                }
                Console.Write("Pick an enemy : ");
                choice = Console.ReadLine();
                success = int.TryParse(choice, out finalChoice);
                if (!success)
                {
                    Console.WriteLine("That was not an integer. Please re-enter");
                }
                else
                {
                    if ((finalChoice - 1 >= Enemies.Length) || (finalChoice <= 0))
                    {
                        Console.WriteLine("That is out of the range. Please re-enter");
                        success = false;
                    }
                }
            }
            return Enemies[finalChoice - 1];
        }

        Inventory.InventoryType ChooseInventory()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Which inventory to view? [weapon/accessory/spell] : ");
                string choice = Console.ReadLine();
                Console.WriteLine();
                foreach (Inventory.InventoryType x in Enum.GetValues(typeof(Inventory.InventoryType)))
                {
                    string lowered = x.ToString().ToLower();
                    if ((choice == lowered) || (choice[0] == lowered[0]))
                    {
                        return x;
                    }
                }
                Dialogue.TimedDialogue(new string[] { "$col$cInventory not available. Please re-select." });
                Console.WriteLine();
            }
        }

        public enum Moves
        {
            None,

            Attack,
            Shield,
            Cast,

            Inventory
        }
    }
}
