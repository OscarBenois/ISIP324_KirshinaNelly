using System;
using System.Collections.Generic;
using System.Linq;

namespace ISIP324_KirshinaNelly
{
    internal class Program
    {
        public enum EnemyType { Гоблин, Скелет, Маг }
        public enum BossType { ВВГ, Ковальский, АрхимагСрр, ПестовСмм }
        public enum ChestLoot { ЛечебноеЗелье, Оружие, Доспех }
        public enum CombatAction { Атака, Защита }

        public abstract class Character
        {
            private int hp;
            protected Character(string name, int maxhp, int attack, int defense)
            {
                Name = name;
                MaxHp = maxhp;
                hp = maxhp;
                Attack = attack;
                Defense = defense;
            }
            public string Name { get; }
            public int MaxHp { get; }
            public int Hp
            {
                get { return hp; }
                protected set
                {
                    if (value < 0) hp = 0;
                    else if (value > MaxHp) hp = MaxHp;
                    else hp = value;
                }
            }
            public int Attack { get; protected set; }
            public int Defense { get; protected set; }
            public bool IsAlive => hp > 0;
            public void TakeDamage(int dmg)
            {
                if (dmg < 0) dmg = 0;
                Hp = Hp - dmg;
            }
            public void Heal(int amount)
            {
                Hp = Hp + amount;
            }

            public abstract void PrintInfo();
        }

        public class Player : Character
        {
            private Weapon weapon;
            private Armor armor;
            private bool isFrozen;

            public Player() : base("Игрок", 100, 10, 5) { }

            public Weapon Weapon
            {
                get { return weapon; }
                set { weapon = value; }
            }
            public Armor Armor
            {
                get { return armor; }
                set { armor = value; }
            }
            public bool IsFrozen
            {
                get { return isFrozen; }
                set { isFrozen = value; }
            }
            public int TotalAttack => Weapon == null ? Attack : Attack + Weapon.AttackBonus;
            public int TotalDefense => Armor == null ? Defense : Defense + Armor.DefenseBonus;

            public override void PrintInfo()
            {
                Console.WriteLine($"  {Name}: HP: {Hp}/{MaxHp} | Атака: {TotalAttack} | Защита: {TotalDefense}");

                if (weapon == null) Console.WriteLine("  Оружие: нет");
                else Console.WriteLine($"  Оружие: {weapon.Name} (+{weapon.AttackBonus})");

                if (armor == null) Console.WriteLine("  Доспех: нет");
                else Console.WriteLine($"  Доспех: {armor.Name} (+{armor.DefenseBonus})");
            }
        }

        public abstract class Enemy : Character
        {
            protected Enemy(string name, int maxhp, int attack, int defense) : base(name, maxhp, attack, defense) { }
            public abstract bool IgnoresDefense { get; }

            public abstract int AttackPlayer(out bool frozen);
            public override void PrintInfo()
            {
                Console.WriteLine($"  {Name}: HP: {Hp}/{MaxHp} | Атака: {Attack} | Защита: {Defense}");
                if (IgnoresDefense)
                    Console.WriteLine("  Особенность: игнорирует защиту игрока");
            }
        }

        public class Goblin : Enemy
        {
            private int critChance;
            public Goblin() : this("Гоблин", 30, 8, 2, 25) { }
            protected Goblin(string name, int maxhp, int attack, int defense, int critChance) : base(name, maxhp, attack, defense)
            {
                this.critChance = critChance;
            }
            public override bool IgnoresDefense
            {
                get { return false; }
            }
            public override int AttackPlayer(out bool frozen)
            {
                frozen = false;
                int damage = Attack;
                if (rng.Next(100) < critChance)
                {
                    damage = damage * 2;
                    Console.WriteLine($" {Name}: Критический удар!");
                }
                return damage;
            }
            public override void PrintInfo()
            {
                base.PrintInfo();
                Console.WriteLine($" Шанс крит урона: {critChance}%");
            }
        }

        public class VVG : Goblin
        {
            public VVG() : base("ВВГ (босс)", (int)(30 * 2.0), (int)(8 * 1.5), (int)(2 * 1.2), 25 + 10) { }
        }

        public class Skeleton : Enemy
        {
            public Skeleton() : this("Скелет", 25, 7, 3) { }
            protected Skeleton(string name, int maxhp, int attack, int defense) : base(name, maxhp, attack, defense) { }
            public override bool IgnoresDefense
            {
                get { return true; }
            }
            public override int AttackPlayer(out bool frozen)
            {
                frozen = false;
                return Attack;
            }
        }

        public class Kovalsky : Skeleton
        {
            public Kovalsky() : base("Ковальский (босс)", (int)(25 * 2.5), (int)(7 * 1.3), (int)(3 * 1.4)) { }
        }

        public class Pestov : Skeleton
        {
            private int freezeChance = 20 * 15;
            public Pestov() : base("Пестов С-- (босс)", (int)(25 * 1.3), (int)(7 * 1.8), (int)(3 * 0.6)) { }
            public override int AttackPlayer(out bool frozen)
            {
                frozen = rng.Next(100) < freezeChance;
                return Attack;
            }
            public override void PrintInfo()
            {
                base.PrintInfo();
                Console.WriteLine($" Шанс заморозки: {freezeChance}%");
            }
        }

        public class Magician : Enemy
        {
            private int freezeChance;
            public Magician() : this("Маг", 20, 6, 1, 20) { }
            protected Magician(string name, int maxhp, int attack, int defense, int freezeChance) : base(name, maxhp, attack, defense)
            {
                this.freezeChance = freezeChance;
            }
            public override bool IgnoresDefense
            {
                get { return false; }
            }
            public override int AttackPlayer(out bool frozen)
            {
                frozen = rng.Next(100) < freezeChance;
                return Attack;
            }
            public override void PrintInfo()
            {
                base.PrintInfo();
                Console.WriteLine($" Шанс заморозки: {freezeChance}%");
            }
        }

        public class ArchmagicianCpp : Magician
        {
            public ArchmagicianCpp() : base("Архимаг С++ (босс)", (int)(20 * 1.8), (int)(6 * 1.6), (int)(1 * 1.1), 20 + 10) { }
        }

        public abstract class Item
        {
            private string name = "";
            public string Name
            {
                get { return name; }
                protected set { name = value; }
            }
            public abstract void PrintInfo();
        }

        public class Weapon : Item
        {
            private int attackBonus;
            public int AttackBonus
            {
                get { return attackBonus; }
            }

            public Weapon(string name, int attackBonus)
            {
                Name = name;
                this.attackBonus = attackBonus;
            }
            public override void PrintInfo()
            {
                Console.WriteLine($" {Name} (+{AttackBonus} к атаке)");
            }
        }

        public class Armor : Item
        {
            private int defenseBonus;
            public int DefenseBonus
            {
                get { return defenseBonus; }
            }
            public Armor(string name, int defenseBonus)
            {
                Name = name;
                this.defenseBonus = defenseBonus;
            }
            public override void PrintInfo()
            {
                Console.WriteLine($" {Name} (+{DefenseBonus} к защите)");
            }
        }

        public class Potion : Item
        {
            public Potion()
            {
                Name = "Лечебное зелье";
            }
            public override void PrintInfo()
            {
                Console.WriteLine($" {Name}");
            }
        }

        static Player player = new Player();
        static int turn;
        static List<Enemy> defeatedEnemies = new List<Enemy>();
        static Random rng = new Random();

        static string[] WeaponNames = { "Ржавый меч", "Топор", "Стальной меч", "Лук", "Булава" };
        static string[] ArmorNames = { "Кожаный доспех", "Стальной доспех", "Щит", "Шлем" };

        static void Main(string[] args)
        {
            try
            {
                Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
            }
        }

        static void Run()
        {
            Console.WriteLine("\\РОГАЛИК/");
            Console.WriteLine("Каждые 10 ходов - босс.");

            player = new Player();

            while (player.IsAlive)
            {
                turn = turn + 1;
                Console.WriteLine($"Ход {turn}");
                player.PrintInfo();

                if (turn % 10 == 0)
                {
                    EncounterBoss();
                }
                else
                {
                    int eventRoll = rng.Next(2);
                    if (eventRoll == 0) OpenChest();
                    else EncounterEnemy();
                }
            }

            PrintGameOver();
        }

        static void PrintGameOver()
        {
            Console.WriteLine($"Вас убили. Пройдено ходов: {turn}");
            Console.WriteLine($"Побеждено врагов: {defeatedEnemies.Count}");

            if (defeatedEnemies.Count > 0)
            {
                Console.WriteLine("Статиска:");
                var groups = defeatedEnemies
                    .GroupBy(e => e.GetType().Name)
                    .OrderByDescending(g => g.Count());
                foreach (var g in groups)
                {
                    Console.WriteLine($" {g.Key}: {g.Count()}");
                }
            }
        }

        static void EncounterBoss()
        {
            Array values = Enum.GetValues(typeof(BossType));
            BossType boss = (BossType)values.GetValue(rng.Next(values.Length));

            Enemy enemy = null;
            switch (boss)
            {
                case BossType.ВВГ:
                    enemy = new VVG();
                    break;
                case BossType.Ковальский:
                    enemy = new Kovalsky();
                    break;
                case BossType.АрхимагСрр:
                    enemy = new ArchmagicianCpp();
                    break;
                case BossType.ПестовСмм:
                    enemy = new Pestov();
                    break;
            }

            Console.WriteLine("!БОСС!");
            Combat(enemy);
        }

        static void EncounterEnemy()
        {
            Array values = Enum.GetValues(typeof(EnemyType));
            EnemyType type = (EnemyType)values.GetValue(rng.Next(values.Length));

            Enemy enemy = null;
            switch (type)
            {
                case EnemyType.Гоблин:
                    enemy = new Goblin();
                    break;
                case EnemyType.Скелет:
                    enemy = new Skeleton();
                    break;
                case EnemyType.Маг:
                    enemy = new Magician();
                    break;
            }
            Combat(enemy);
        }

        static void OpenChest()
        {
            Console.WriteLine("Вы нашли сундук!");

            Array values = Enum.GetValues(typeof(ChestLoot));
            ChestLoot loot = (ChestLoot)values.GetValue(rng.Next(values.Length));

            switch (loot)
            {
                case ChestLoot.ЛечебноеЗелье:
                    player.Heal(player.MaxHp);
                    Console.WriteLine("Вы нашли лечебное зелье. HP полностью восстановлено");
                    break;
                case ChestLoot.Оружие:
                    TryEquipWeapon();
                    break;
                case ChestLoot.Доспех:
                    TryEquipArmor();
                    break;
            }
        }

        static void TryEquipWeapon()
        {
            string name = WeaponNames[rng.Next(WeaponNames.Length)];
            int bonus = rng.Next(2, 9);
            Weapon w = new Weapon(name, bonus);

            Console.WriteLine("Внутри оружие.");
            Console.WriteLine("Новое: ");
            w.PrintInfo();

            Console.WriteLine("Текущее: ");
            if (player.Weapon != null) player.Weapon.PrintInfo();
            else Console.WriteLine("нет");
            if (AskYesNo("Взять новое оружие?"))
            {
                player.Weapon = w;
                Console.WriteLine("Оружие заменено.");
            }
            else
            {
                Console.WriteLine("Вы выбросили предмет.");
            }
        }

        static void TryEquipArmor()
        {
            string name = ArmorNames[rng.Next(ArmorNames.Length)];
            int bonus = rng.Next(1, 6);
            Armor a = new Armor(name, bonus);

            Console.WriteLine("Внутри доспех.");
            Console.WriteLine("Новый: ");
            a.PrintInfo();

            Console.WriteLine("Текущий: ");
            if (player.Armor != null) player.Armor.PrintInfo();
            else Console.WriteLine("нет");

            if (AskYesNo("Взять новый доспех?"))
            {
                player.Armor = a;
                Console.WriteLine("Доспех заменён.");
            }
            else
            {
                Console.WriteLine("Вы выбросили доспех.");
            }
        }

        static void Combat(Enemy enemy)
        {
            Console.WriteLine($"Бой с {enemy.Name}");
            enemy.PrintInfo();

            while (enemy.IsAlive && player.IsAlive)
            {
                Console.WriteLine($"\nHP: вы {player.Hp}/{player.MaxHp} | {enemy.Name} {enemy.Hp}/{enemy.MaxHp}");
                if (player.IsFrozen)
                {
                    Console.WriteLine("Вы заморожены. Пропуск хода.");
                    player.IsFrozen = false;
                    EnemyAttack(enemy, false);
                    continue;
                }
                CombatAction action = ReadCombatAction();
                if (action == CombatAction.Атака)
                {
                    int dmg = player.TotalAttack - enemy.Defense;
                    if (dmg < 1) dmg = 1;
                    enemy.TakeDamage(dmg);
                    Console.WriteLine($"Вы нанесли {dmg} урона.");
                }
                else
                {
                    Console.WriteLine("Вы готовитесь защищаться.");
                }
                if (enemy.IsAlive)
                {
                    bool defending = (action == CombatAction.Защита);
                    EnemyAttack(enemy, defending);
                }
            }
            if (player.IsAlive)
            {
                Console.WriteLine($"Победа над {enemy.Name}!");
                defeatedEnemies.Add(enemy);
                player.IsFrozen = false;
            }
            else
            {
                Console.WriteLine($"{enemy.Name} убивает вас.");
            }
        }

        static void EnemyAttack(Enemy enemy, bool defending)
        {
            if (defending)
            {
                int dodgeRoll = rng.Next(100);
                if (dodgeRoll < 40)
                {
                    Console.WriteLine($"{enemy.Name} атакует. Вы уклонились!");
                    return;
                }
            }

            bool frozen;
            int rawDmg = enemy.AttackPlayer(out frozen);
            int finalDmg = rawDmg;
            if (enemy.IgnoresDefense == false)
            {
                finalDmg = finalDmg - player.TotalDefense;
                if (finalDmg < 0) finalDmg = 0;

                if (defending)
                {
                    int blockPercent = rng.Next(70, 101);
                    int blockAmount = player.TotalDefense * blockPercent / 100;
                    finalDmg = finalDmg - blockAmount;
                    if (finalDmg < 0) finalDmg = 0;
                    Console.WriteLine($"Блок {blockPercent}%: -{blockAmount} урона");
                }
            }
            else
            {
                Console.WriteLine($"{enemy.Name} игнорирует вашу защиту.");
            }

            player.TakeDamage(finalDmg);
            Console.WriteLine($"{enemy.Name} наносит {finalDmg} урона. Ваше HP: {player.Hp}/{player.MaxHp}");

            if (frozen)
            {
                player.IsFrozen = true;
                Console.WriteLine("Вы заморожены. Следующий ход прорущен.");
            }
        }

        static CombatAction ReadCombatAction()
        {
            while (true)
            {
                Console.WriteLine("1. Атака");
                Console.WriteLine("2. Защита (40% уклониться, иначе блок 70-100% защиты");
                Console.WriteLine("Выбор: ");
                int v;
                if (int.TryParse(Console.ReadLine(), out v))
                {
                    if (v == 1) return CombatAction.Атака;
                    if (v == 2) return CombatAction.Защита;
                }
                Console.WriteLine("Ошибка ввода. Поопробуйте снова");
            }
        }

        static bool AskYesNo(string question)
        {
            while (true)
            {
                Console.WriteLine($"{question} (y/n): ");
                string a = Console.ReadLine();
                if (a == null) a = "";
                a = a.Trim().ToLower();

                if (a == "y") return true;
                if (a == "n") return false;

                Console.WriteLine("Введите y или n.");
            }
        }
    }
}