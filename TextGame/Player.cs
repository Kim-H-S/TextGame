using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame
{
    public class Player
    {
        public int level { get; }
        public string name { get; }
        public string classType { get; }
        public float attack { get; }
        public float defense { get; }
        public float health { get; }
        public long gold { get; }

        public Player(int level, string name, string classType, float attack, float defense, float health, long gold)
        {
            this.level = level;
            this.name = name;
            this.classType = classType;
            this.attack = attack;
            this.defense = defense;
            this.health = health;
            this.gold = gold;
        }
    }
}
