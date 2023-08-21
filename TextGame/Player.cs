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

        public void DisplayInfo()
        {
            Console.Clear();

            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv. {level}");
            Console.WriteLine($"{name} ( {classType} )");
            Console.WriteLine($"공격력 : {attack}");
            Console.WriteLine($"방어력 : {defense}");
            Console.WriteLine($"체력 : {health}");
            Console.WriteLine($"Gold : {gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            Console.ReadLine();
        }
    }
}
