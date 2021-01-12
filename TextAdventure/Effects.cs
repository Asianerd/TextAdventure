using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Effects
    {
        public static Effects None = new Effects(EffectEnum.None,0);

        public int age;// Age = how many turns this effect will last
        public string name;
        public EffectEnum type;
        public bool dead;
        public PlayerValueModifier value;
        public enum EffectEnum
        {
            None,
            Burning,
            Poison,
            Weakness,
            Strength,
            Toughness
        }

        public Effects(EffectEnum EffectType,int Duration)
        {
            switch((int)EffectType)
            {
                default:
                    value = PlayerValueModifier.none;
                    break;
                case 3:
                    value = new PlayerValueModifier(
                        PlayerValueModifier.Health.none,
                        PlayerValueModifier.Mana.none,
                        new PlayerValueModifier.Damage(0.5),
                        new PlayerValueModifier.Defence(0,0.5)
                        );
                    break;
                case 4:
                    value = new PlayerValueModifier(
                        PlayerValueModifier.Health.none,
                        PlayerValueModifier.Mana.none,
                        new PlayerValueModifier.Damage(2),
                        PlayerValueModifier.Defence.none
                        );
                    break;
                case 5:
                    value = new PlayerValueModifier(
                        PlayerValueModifier.Health.none,
                        PlayerValueModifier.Mana.none,
                        PlayerValueModifier.Damage.none,
                        new PlayerValueModifier.Defence(10,2)
                        );
                    break;
            }
            name = EffectType.ToString();
            age = Duration;
            CheckDeath();
        }

        public void AdvanceAge()
        {
            age--;
            CheckDeath();
        }

        public void CheckDeath()
        {
            if(age<=0)
            {
                dead = true;
            }
        }

        public string ApplyEffect()
        {
            string effectMessage = "";
            switch((int)type)
            {
                default:
                    break;
                case 1:
                    Player.Instance.AffectHealth(-5);
                    effectMessage = $"The fire hurts you! {Player.Instance.health + 5} => {Player.Instance.health}/{Player.Instance.MaxHealth()}HP";
                    break;
                case 2:
                    Player.Instance.AffectHealth(-3);
                    effectMessage = $"The poison hurts you! {Player.Instance.health + 3} => {Player.Instance.health}/{Player.Instance.MaxHealth()}HP";
                    break;
            }
            return effectMessage;
        }
    }
}
