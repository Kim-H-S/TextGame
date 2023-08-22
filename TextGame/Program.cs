using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace TextGame
{
    internal class Program
    {
        static Player player;
        static Inventory inventory = new Inventory();

        static void Main(string[] args)
        {
            

            StartGameDataSetting();
            DisplayGameIntro();

            
        }

        static void StartGameDataSetting()
        {
            // 캐릭터 정보 셋팅
            player = new Player(1, "Chad", "전사", 10, 5, 100, 1500);

            // 리스트로 아이템 정보 셋팅
            Item IronArmor = new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 9, 0, 0, true, true);
            inventory.Add(IronArmor);
            Item WornSword = new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 2, 0, 0, 0, true, true);
            inventory.Add(WornSword);

            UpdatePlayerInfo();

            Item TraineeArmor = new Item("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 0, 5, 0, 1000);
            inventory.Add(TraineeArmor);
            Item SpartanArmor = new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 0, 15, 0, 3500);
            inventory.Add(SpartanArmor);
            Item BronzeAxe = new Item("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 5, 0, 0, 1500);
            inventory.Add(BronzeAxe);
            Item SpartanSpear = new Item("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, 0, 0, 10);
            inventory.Add(SpartanSpear);
        }

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    DisplayPlayerInfo();
                    break;

                case 2:
                    DisplayInventory();
                    break;

            }

        }

        static void DisplayPlayerInfo()
        {
            Console.Clear();

            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();

            //
            player.DisplayInfo();
            //

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        static void UpdatePlayerInfo()
        {

            foreach (Item item in inventory.itemList)
            {
                if (!item.bEquip)
                    return;

                if(item.attack != 0) { player.attack += item.attack; }
                if(item.defense != 0) { player.defense += item.defense; }
                if(item.health != 0) { player.health += item.health; }
            }

        }

        static void DisplayInventory()
        {
            Console.Clear();

            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            inventory.Display();

            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;

                case 1:
                    ManageEquippedItems();
                    break;
            }

        }

        static void ManageEquippedItems()
        {
            Console.Clear();

            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            inventory.ManageEquippedItems();

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int input = CheckValidInput(0, inventory.itemList.Count);

            switch (input)
            {
                case 0:
                    DisplayInventory();
                    break;

                default:
                    Item result = inventory.itemList[input - 1];
                    result.bEquip = !(result.bEquip);

                    ManageEquippedItems();

                    UpdatePlayerInfo();
                    break;
            }

        }


        static public int CheckValidInput(int min, int max)
        {

            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (min <= ret && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }

        }


    }


    public class Player
    {
        public int level { get; set; }
        public string name { get; set; }
        public string classType { get; set; }
        public float attack { get; set; }
        public float defense { get; set; }
        public float health { get; set; }
        public long gold { get; set; }

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
            Console.WriteLine($"Lv. {level}");
            Console.WriteLine($"{name} ( {classType} )");
            Console.WriteLine($"공격력 : {attack}");
            Console.WriteLine($"방어력 : {defense}");
            Console.WriteLine($"체력 : {health}");
            Console.WriteLine($"Gold : {gold} G");
        }


    }

    public class Item
    {
        public string name;
        public string description;

        public int attack;
        public int defense;
        public int health;

        public long gold;

        // 구입 유무
        public bool bBuy;
        // 장착 유무
        public bool bEquip;

        public Item(string name, string description, int attack, int defense, int health, long gold, bool isBuy = false, bool isEquip = false)
        {
            this.name = name;
            this.description = description;
            this.attack = attack;
            this.defense = defense;
            this.health = health;
            this.gold = gold;
            this.bBuy = isBuy;
            this.bEquip = isEquip;
        }
    }

    public class Inventory
    {
        public List<Item> itemList;

        public Inventory()
        {
            itemList = new List<Item>();    
        }

        public void Add(Item item) 
        {
            itemList.Add(item);
        }

        public void RemoveItem(Item item) 
        {
            itemList.Remove(item);
        }

        public void Display()
        {
            foreach (var item in itemList)
            {
                Console.Write("- ");

                if (item.bEquip) { Console.Write("[E]"); }

                Console.Write($"{item.name} | ");

                if (item.attack > 0) { Console.Write($"공격력 +{item.attack} "); }
                if (item.defense > 0) { Console.Write($"방어력 +{item.defense} "); }
                if (item.health > 0) { Console.Write($"체력 +{item.health} "); }

                Console.WriteLine($" | {item.description}");
            }
        }

        public void ManageEquippedItems()
        {

            int itemCount = 1;

            foreach (var item in itemList)
            {
                if (!item.bBuy)
                    return;

                Console.Write("- ");
                Console.Write($"{itemCount} ");

                if (item.bEquip) { Console.Write("[E]"); }

                Console.Write($"{item.name} | ");

                if (item.attack > 0) { Console.Write($"공격력 +{item.attack} "); }
                if (item.defense > 0) { Console.Write($"방어력 +{item.defense} "); }
                if (item.health > 0) { Console.Write($"체력 +{item.health} "); }

                Console.WriteLine($" | {item.description}");

                itemCount++;
            }

        }

    }

}

