using System.Collections.Generic;

namespace Warcraft
{
    public class MultiplyIncantation : Incantation
    {
        public MultiplyIncantation(string name, double timeOfAction = 0, int mana = 0, int strength = 0, int agillity = 0, int intelligence = 0,
            int damage = 0, int armor = 0, int health = 0,  double reloadingTime = 10) : base(name, timeOfAction, health, mana,
                strength, agillity, intelligence, damage, armor, reloadingTime)
        {
        }

        public override void Execute(Player sourcePlayer, List<Player> destionationPlayers) { }
    }
}