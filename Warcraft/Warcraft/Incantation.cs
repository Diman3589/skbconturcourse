using System.Collections.Generic;

namespace Warcraft
{
    public abstract class Incantation
    {
        protected Incantation(string name, double timeOfAction = 0, int health = 0, int mana = 0, int strength = 0,
            int agillity = 0, int intelligence = 0, int damage = 0, int armor = 0, double reloadingTime = 0)
        {
            Name = name;
            TimeOfAction = timeOfAction;
            Health = health;
            Mana = mana;
            Strength = strength;
            Agillity = agillity;
            Intelligence = intelligence;
            Damage = damage;
            Armor = armor;
            ReloadingTime = reloadingTime;
        }

        public string Name { get; }
        public double TimeOfAction { get;}
        public int Health { get; }
        public int Mana { get; }
        public int Strength { get; }
        public int Agillity { get; }
        public int Intelligence { get; }
        public int Damage { get; }
        public int Armor { get; }
        public double ReloadingTime { get; }
        public virtual void Execute(Player sourcePlayer, Player destinationPlayer) { }
        public virtual void Execute(Player sourcePlayer, List<Player> destinationPlayers) { }

    }
}