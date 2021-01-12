using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Enemy
    {
        public Enemy(string Name, double MaxHealth, double Damage, double Health = -999, bool Boss = false, Effects EffectInflicted = null)
        {
            name = Name;
            maxHealth = MaxHealth;
            if(Health == -999)
            {
                health = MaxHealth;
            }
            else
            {
                health = Health;
            }
            damage = Damage;
            dead = false;
            boss = Boss;
            CheckHealth();
        }

        public string name;

        public double health;
        public double maxHealth;

        public double damage;

        public bool dead;

        public bool boss; // For proper text formatting (not 'the Moon Lord' but instead 'Moon Lord')

        public void AffectHealth(double amount)
        {
            health += amount;
            CheckHealth();
        }

        void CheckHealth()
        {
            dead = health <= 0;
        }

        public void PrintStats()
        {
            Dialogue.TimedDialogue(new string[] {
            $"Name : {name}",
            $"  HP : {health}/{maxHealth}",
            $" ATK : {damage}"
            }, 0);
        }

        public string[] GetStats()
        {
            return new string[]
            {
                $"{name}",
                $"  HP : {health}/{maxHealth}",
                $" ATK : {damage}"
            };
        }

        public static Enemy[] RemoveDead(Enemy[] enemies)
        {
            // Removes the dead Enemies from the given array
            List<Enemy> end = new List<Enemy>();

            foreach (Enemy x in enemies)
            {
                if (!x.dead)
                {
                    end.Add(x);
                }
            }

            return end.ToArray();
        }

        public static void PrintStats(Enemy[] enemies)
        {
            List<string>[] end = new List<string>[]
            {
                new List<string>(),
                new List<string>(),
                new List<string>()
            };

            foreach(Enemy i in RemoveDead(enemies))
            {
                for (int ii = 0; ii < 3; ii++)
                {
                    end[ii].Add(EndString(i.GetStats())[ii]);
                }
            }


            int barLength = end[0].Select(n => n.Length).Sum();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"          /{new string('-',barLength-1)}\\");
            foreach (List<string> i in end)
            {
                Console.Write("          |");
                foreach(string ii in i)
                {
                    Console.Write(ii);
                }
                Console.WriteLine();
            }
            Console.WriteLine($"          \\{new string('-', barLength - 1)}/");
            Console.ForegroundColor = ConsoleColor.White;

            string[] EndString(string[] inStr)
            {
                int finLength = inStr.Max(n => n.Length);

                List<string> outStr = new List<string>();
                foreach (string x in inStr)
                {
                    outStr.Add($" {x}{new string(' ', finLength - x.Length + 1)}|");
                }
                return outStr.ToArray();
            }
        }
    }
}
