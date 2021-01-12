using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Weapon
    {
        public static List<Weapon> weapons = new List<Weapon>();

        public double damage;

        public Weapon(double Damage)
        {
            damage = Damage;
        }
    }
}
