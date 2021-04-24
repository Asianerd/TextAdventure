using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Effects
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
            Toughness,
            Regeneration
        }

        public Effects(EffectEnum EffectType,int Duration)
        {
            switch(EffectType)
            {
                default:
                    value = PlayerValueModifier.none;
                    break;
                case EffectEnum.Weakness:
                    value = new PlayerValueModifier(
                        PlayerValueModifier.Health.none,
                        PlayerValueModifier.Mana.none,
                        new PlayerValueModifier.Damage(0, 0.5),
                        new PlayerValueModifier.Defence(0, 0.5)
                        );
                    break;
                case EffectEnum.Strength:
                    value = new PlayerValueModifier(
                        PlayerValueModifier.Health.none,
                        PlayerValueModifier.Mana.none,
                        new PlayerValueModifier.Damage(0, 2),
                        PlayerValueModifier.Defence.none
                        );
                    break;
                case EffectEnum.Toughness:
                    value = new PlayerValueModifier(
                        PlayerValueModifier.Health.none,
                        PlayerValueModifier.Mana.none,
                        PlayerValueModifier.Damage.none,
                        new PlayerValueModifier.Defence(10, 2)
                        );
                    break;
                case EffectEnum.Regeneration:
                    value = new PlayerValueModifier(
                        new PlayerValueModifier.Health(0, 5, 0, 5),
                        PlayerValueModifier.Mana.none,
                        PlayerValueModifier.Damage.none,
                        PlayerValueModifier.Defence.none
                        );
                    break;
            }
            type = EffectType;
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
                    effectMessage = $"The fire hurts you! {Player.Instance.health + 5} => {Player.Instance.health}/{Player.Instance.health}HP";
                    break;
                case 2:
                    Player.Instance.AffectHealth(-3);
                    effectMessage = $"The poison hurts you! {Player.Instance.health + 3} => {Player.Instance.health}/{Player.Instance.health}HP";
                    break;
            }
            return effectMessage;
        }
    }
}
