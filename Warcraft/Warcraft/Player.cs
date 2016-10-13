using System.Collections.Generic;

namespace Warcraft
{

    class Player
    {
        public Player(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Strength { get; set; }
        public int Agillity { get; set; }
        public int Intelligence { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public double ManaRegenrationTime { get; set; }

        public List<Incantation> OurIncantations;
        public List<Incantation> EnemyIncantations;
    }
}
