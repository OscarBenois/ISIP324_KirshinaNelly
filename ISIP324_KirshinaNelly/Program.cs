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
                if (randnumb.Next(100) < critChance)
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
                frozen = randnumb.Next(100) < freezeChance;
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
                frozen = randnumb.Next(100) < freezeChance;
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

       
        static void Main(string[] args)
        {
            
        }
    }
}