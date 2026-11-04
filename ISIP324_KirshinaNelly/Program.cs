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