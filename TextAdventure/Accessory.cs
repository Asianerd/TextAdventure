using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Accessory
    {
        public Item acessoryItem;
        public PlayerValueModifier value;
        public Accessory(Item Accessory,PlayerValueModifier Value)
        {
            acessoryItem = Accessory;
            value = Value;
        }
    }
}
