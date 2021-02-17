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
            "h"
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

            Player.Instance.PrintStats();
            RunEffects();
            Player.Instance.CheckDeath();
            if(Player.Instance.alive)
            {
                Dialogue.TimedDialogue(new string[] {
                    "Select an action [attack/shield/heal]"
                }, 0);
                string choice = Console.ReadLine();
                if(validMoves.Contains(choice))
                {
                    switch(choice)
                    {
                        default:
                            Enemy enemy;
                            if (Enemy.RemoveDead(Enemies).Length == 1)
                            {
                                enemy = Enemy.RemoveDead(Enemies)[0];
                            }
                            else
                            {
                                enemy = ChooseEnemy(Enemy.RemoveDead(Enemies));
                            }



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
                        case "shield": case "s":
                            Dialogue.TimedDialogue(new string[] {
                                $"$col$7You blocked the attack!",
                                $"$col$8What a pussy"
                            });

                            break;
                        case "heal": case "h":
                            if (Player.Instance.mana >= Player.Instance.HealManaUse())
                            {
                                Dialogue.TimedDialogue(new string[] {
                                    $"$col$2You use {Player.Instance.HealManaUse()} mana to cast a healing spell",
                                    $"$col$2Upon application on thyself you feel a surge in energy and a feeling of reassurance of your presence in battle",
                                    $"$ext$$col$a{Player.Instance.health} => {Player.Instance.health+Player.Instance.healHealthGain} / {Player.Instance.MaxHealth()}HP",
                                    $"$col$1     {Player.Instance.mana} => {Player.Instance.mana-Player.Instance.HealManaUse()} / {Player.Instance.MaxMana()}MN"
                                });
                                Player.Instance.AffectHealth(Player.Instance.healHealthGain);
                                Player.Instance.AffectMana(-Player.Instance.HealManaUse());
                            }
                            else
                            {
                                Dialogue.TimedDialogue(new string[] {
                                    $"$col$1You didnt have enough mana to cast the spell! Needed {Player.Instance.HealManaUse()}",
                                });
                            }
                            break;
                    }
                    Dialogue.ColoredPrint($">{new string('=', 75)}<",ConsoleColor.DarkGray);
                }
            }
            else
            {
                Dialogue.TimedDialogue(new string[] { "That was not a valid choice." });
            }
        }

        void RunEffects()
        {
            string effectMessage = "";
            foreach (Effects x in Player.Instance.effects.ToArray())
            {
                x.AdvanceAge();
                if (x.dead)
                {
                    Player.Instance.effects.Remove(x);
                }
                else
                {
                    effectMessage += $"{x.ApplyEffect()}\n";
                }
            }

            Dialogue.TimedDialogue(new string[] {
                effectMessage
            });
        }

        Enemy ChooseEnemy(Enemy[] Enemies)
        {
            // From the array, have the user select one and then return that enemy
            string choice = "";
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
    }
}
