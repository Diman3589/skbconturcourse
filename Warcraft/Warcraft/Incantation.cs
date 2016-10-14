namespace Warcraft
{
    class Incantation
    {
        public Incantation(int mana = 0, int strength = 0, int agillity = 0, int intelligence = 0,
            int damage = 0, int armor = 0, int health = 0,  double reloadingTime = 10)
        {
            Health = health;
            Mana = mana;
            Strength = strength;
            Agillity = agillity;
            Intelligence = intelligence;
            Damage = damage;
            Armor = armor;
            ReloadingTime = reloadingTime;
        }

        public string Name { get; set; }
        public double TimeOfAction { get; set; }
        public int Health { get; }
        public int Mana { get; }
        public int Strength { get;}
        public int Agillity { get; }
        public int Intelligence { get; }
        public int Damage { get; }
        public int Armor { get; }
        public double ReloadingTime { get; }

        public void Execute(Player sourcePlayer, Player destinationPlayer) { }
    }
}
