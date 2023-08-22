using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Xml.Linq;
using System.IO;

namespace TextGame
{
    [Flags]
    enum EquipmentFlags
    {
        None = 0,
        Weapon = 1,
        Armor = 2,
        Helmet = 4,
        Shield = 8,
        Accessory = 16,
    }

    internal class Program
    {
        // 장비 플래그
        static EquipmentFlags equipment;

        //static int totalWidth = 50; // 출력할 전체 너비

        static Player player;

        static string directoryPath = @"C:\Workspace\TextGame\GameData";
        static string fileName = "player.txt";
        static string filePath = Path.Combine(directoryPath, fileName);


        static Inventory inventory = new Inventory();

        static void Main(string[] args)
        {
            StartGameDataSetting();

            //

            

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            player.SaveToFile(filePath);

            player.LoadFromFile(filePath, player);

            //


            DisplayGameIntro();

            
        }

        static void StartGameDataSetting()
        {
            // 캐릭터 정보 셋팅
            player = new Player(1, "Chad", "전사", 10, 5, 100, 1500);
            //

            // 리스트로 아이템 정보 셋팅
            Item IronArmor = new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 9, 0, 1800, true, true);
            inventory.Add(IronArmor);
            Item WornSword = new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 2, 0, 0, 450, true, true);
            inventory.Add(WornSword);

            equipment = EquipmentFlags.Weapon | EquipmentFlags.Armor;
            Console.WriteLine($"Equipment Flags: {equipment}");

            UpdatePlayerInfo();

            Item TraineeArmor = new Item("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 0, 5, 0, 1000);
            inventory.Add(TraineeArmor);
            Item BronzeAxe = new Item("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 5, 0, 0, 1500);
            inventory.Add(BronzeAxe);
            Item BronzeArmor = new Item("청동 갑옷", "어디선가 사용됐던거 같은 갑옷입니다.", 0, 10, 0, 2500);
            inventory.Add(BronzeArmor);
            Item SpartanArmor = new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 0, 15, 0, 3500);
            inventory.Add(SpartanArmor);
            Item SpartanSpear = new Item("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, 0, 0, 5000);
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
            Console.WriteLine("3. 상점");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int input = CheckValidInput(1, 3);
            switch (input)
            {
                case 1:
                    DisplayPlayerInfo();
                    break;

                case 2:
                    DisplayInventory();
                    break;

                case 3:
                    DisplayShop();
                    break;
            }

        }

        static void DisplayPlayerInfo()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상태 보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();

            UpdatePlayerInfo();

            Console.WriteLine($"Lv. {player.level}");
            Console.WriteLine($"{player.name} ( {player.classType} )");
            Console.WriteLine($"공격력 : {player.attack}");
            Console.WriteLine($"방어력 : {player.defense}");
            Console.WriteLine($"체력 : {player.health}");
            Console.WriteLine($"Gold : {player.gold} G");

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

            player.SaveToFile(filePath);

            player.LoadFromFile(filePath, player);
        }

        static void DisplayInventory()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
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
                    DIsplayEquippedInventory();
                    break;
            }

        }

        static void DIsplayEquippedInventory()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            inventory.DIsplayEquippedInventory();

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

                    UpdatePlayerInfo();

                    DIsplayEquippedInventory();
                    break;
            }

        }


        static void DisplayShop()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");

            Console.WriteLine($"{player.gold} G");

            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();

            inventory.DisplayShop();

            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;

                case 1:
                    ShopBuying();
                    break;

                case 2:
                    ShopSelling();
                    break;
            }
        }

        /// <summary>
        /// 상점에서 구매한다.
        /// </summary>
        static void ShopBuying()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점 - 아이템 구매");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");

            Console.WriteLine($"{player.gold} G");

            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            inventory.ShopBuying();

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int input = CheckValidInput(0, 10);
            switch (input)
            {
                case 0:
                    DisplayShop();
                    break;

                default:
                    Item result = inventory.itemList[input - 1];

                    if (result.bBuy)
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.");

                        Thread.Sleep(1000);

                        //return;
                    }
                    else if(player.gold > result.gold)
                    {
                        Console.WriteLine("구매를 완료했습니다.");

                        // 재화 감소
                        player.gold -= result.gold;

                        // 인벤토리에 아이템 추가
                        // -

                        // 상점에 구매완료 표시
                        result.bBuy = true;

                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine("Gold 가 부족합니다.");

                        Thread.Sleep(1000);
                    }
                        

                    //UpdatePlayerInfo();

                    ShopBuying();
                    break;
            }
        }
        
        /// <summary>
        /// 상점에서 판매한다.
        /// </summary>
        static void ShopSelling()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점 - 아이템 판매");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");

            Console.WriteLine($"{player.gold} G");

            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            inventory.ShopSelling();

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int input = CheckValidInput(0, 10);
            switch (input)
            {
                case 0:
                    break;

                default:
                    Item result = inventory.itemList[input - 1];

                    if (result.bEquip)
                    {
                        result.bEquip = false;
                    }

                    if(result.bBuy)
                    {
                        // 판매가 = gold * 0.85
                        player.gold += result.gold * 0.85;

                        result.bBuy = false;
                        Thread.Sleep(1000);
                    }

                   
                    ShopSelling();
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
        public double gold { get; set; }

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

       public void SaveToFile(string fileName) 
       { 

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine(level);
                writer.WriteLine(name);
                writer.WriteLine(classType);
                writer.WriteLine(attack);
                writer.WriteLine(defense);
                writer.WriteLine(health);
                writer.WriteLine(gold);
            }

       }



        public void LoadFromFile(string fileName, Player player)
        {

            using (StreamReader reader = new StreamReader(fileName))
            {
                player.level = int.Parse(reader.ReadLine());
                player.name = reader.ReadLine();
                player.classType = reader.ReadLine();
                player.attack = float.Parse(reader.ReadLine());
                player.defense = float.Parse(reader.ReadLine());
                player.health = float.Parse(reader.ReadLine());
                player.gold = long.Parse(reader.ReadLine());
            }


            //return player;
        }

    }

    public class Item
    {
        public string name;
        public string description;

        public int attack;
        public int defense;
        public int health;

        public double gold;

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
            List<Item> itemNameLongSortList = itemList.OrderByDescending(x => x.name.Length).ToList();

            foreach (var item in itemNameLongSortList)
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

        
        
        public void DIsplayEquippedInventory()
        {
            List<Item> itemNameLongSort_AND_bBuyTrue_List = itemList
    .Where(item => item.bBuy) // bBuy가 true인 아이템만 필터링
    .OrderByDescending(x => x.name.Length) // 이름 길이로 내림차순 정렬
    .ToList();

            int itemCount = 1;

            foreach (var item in itemNameLongSort_AND_bBuyTrue_List)
            {
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

        public void DisplayShop()
        {

            foreach (var item in itemList)
            {
                Console.Write("- ");

                Console.Write($"{item.name} | ");

                if (item.attack > 0) { Console.Write($"공격력 +{item.attack} "); }
                if (item.defense > 0) { Console.Write($"방어력 +{item.defense} "); }
                if (item.health > 0) { Console.Write($"체력 +{item.health} "); }

                Console.Write($" | {item.description}");

                if (item.bBuy) { Console.WriteLine($" | 구매완료"); }
                else { Console.WriteLine($" | {item.gold} G"); }
            }
        }

        /// <summary>
        /// 상점에서 구매한다.
        /// </summary>
        public void ShopBuying()
        {
            int itemCount = 1;

            foreach (var item in itemList)
            {
                Console.Write("- ");
                Console.Write($"{itemCount} ");

                if (item.bEquip) { Console.Write("[E]"); }

                Console.Write($"{item.name} | ");

                if (item.attack > 0) { Console.Write($"공격력 +{item.attack} "); }
                if (item.defense > 0) { Console.Write($"방어력 +{item.defense} "); }
                if (item.health > 0) { Console.Write($"체력 +{item.health} "); }

                Console.Write($" | {item.description}");

                if (item.bBuy) { Console.WriteLine($" | 구매완료"); }
                else { Console.WriteLine($" | {item.gold} G"); }

                itemCount++;
            }

        }

        /// <summary>
        /// 상점에서 판매한다.
        /// </summary>

        public void ShopSelling() 
        {
            List<Item> bBuyTrueItems = itemList
    .Where(item => item.bBuy == true) // bBuy가 true인 아이템만 필터링
    .ToList();

            int itemCount = 1;

            foreach (var item in bBuyTrueItems)
            {
                //if (!item.bBuy) return;

                Console.Write("- ");
                Console.Write($"{itemCount} ");

                if (item.bEquip) { Console.Write("[E]"); }

                Console.Write($"{item.name} | ");

                if (item.attack > 0) { Console.Write($"공격력 +{item.attack} "); }
                if (item.defense > 0) { Console.Write($"방어력 +{item.defense} "); }
                if (item.health > 0) { Console.Write($"체력 +{item.health} "); }

                Console.Write($" | {item.description}");

                // 판매가 = gold * 0.85
                //Console.WriteLine($" | {item.gold.ToString("F")} G");
                Console.WriteLine($" | {(item.gold * 0.85).ToString("F")} G");

                itemCount++;
            }
        }

    }

}

