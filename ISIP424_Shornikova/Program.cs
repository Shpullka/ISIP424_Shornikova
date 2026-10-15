using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP424_Shornikova
{
    public enum EnemyType
    {
        Goblin,
        Skeleton, 
        Mage, 
        Boss
    }

    public enum ItemType
    {
        Weapon,
        Armor,
        Potion
    }

    public interface IItem
    {
        string Name { get; }
        ItemType Type { get; }
        string GetDescription();
    }

    public class Weapon : IItem 
    { 
        public string Name { get; private set; }
        public ItemType Type => ItemType.Weapon;
        public int DamageBonus { get; private set; }

        public Weapon(string name, int damageBonus)
        {
            Name = name;
            DamageBonus = damageBonus;
        }

        public string GetDescription() => $"{Name} (Урон: +{DamageBonus})";
    }

    public class Armor : IItem
    {
        public string Name { get; private set; }
        public ItemType Type => ItemType.Armor;
        public int DefenseBonus { get; private set; }

        public Armor(string name, int defenseBonus)
        {
            Name = name;
            DefenseBonus = defenseBonus;
        }

        public string GetDescription() => $"{Name} (Защита: +{DefenseBonus})";
    }

    public class Potion : IItem
    {
        public string Name { get; private set; } = "Лечебное зелье";
        public ItemType Type => ItemType.Potion;
        public string GetDescription() => "Мгновенно восстанавливает здоровье";
    }

    public abstract class Entity
    {
        public string Name { get; protected set; }
        public int Health { get; set; }
        public int MaxHealth { get; protected set; }
        public int AttackPower { get; protected set; }
        public int Defense { get; protected set; }
        public bool IsAlive => Health > 0;

        public Entity(string name, int health, int attack, int defense)
        {
            Name = name;
            Health = health;
            MaxHealth = health;
            AttackPower = attack;
            Defense = defense;
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0)
            {
                Health = 0;
            }
        }
    }

    public class Player : Entity
    {
        public Weapon EquippedWeapon { get; set; }
        public Armor EquippedArmor { get; set; }
        public bool IsFrozen { get; set; } = false;

        public Player(string name, int health, int attack, int defense) : base(name, health, attack, defense) { }

        public int GetTotalAttack()
        {
            int total = AttackPower;
            if (EquippedWeapon  != null)
            {
                total = total + EquippedWeapon.DamageBonus;
            }
            return total;
        }

        public int GetTotalDefense()
        {
            int defense = Defense;
            if (EquippedArmor != null)
            {
                defense = defense + EquippedArmor.DefenseBonus;
            }
            return defense;
        }

        public void EquipItem(IItem item)
        {
            if (item is Weapon weapon)
            {
                EquippedWeapon = weapon;
                Console.WriteLine($"Вы экипировали {weapon.Name}");
            } else if (item is Armor armor)
            {
                EquippedArmor = armor;
                Console.WriteLine($"Вы экипировали {armor.Name}");
            }
        }
    }

    public class Enemy : Entity 
    {
        public EnemyType Type { get; protected set; }
        public double CritChance { get; set; }
        public double FreezeChance { get; set; }

        public Enemy(string name, int health, int  attack, int defense, EnemyType type) : base(name, health, attack, defense)
        {
            Type = type;
            CritChance = 0.1;
            FreezeChance = 0.0;
        }

        public virtual int CalculateAttackDamage(Player player, out bool IsCrit, out bool isIgnoringDefense)
        {
            IsCrit = false;
            isIgnoringDefense = false;
            int damage = AttackPower;

            if (new Random().NextDouble() <= CritChance)
            {
                IsCrit = true;
                damage = (int)(damage * 1.5); 
            }
            if (this is IIgnoreDefense)
            {
                isIgnoringDefense = true;
            }
            return damage;
        }

        public virtual bool TryFreeze()
        {
            return new Random().NextDouble() <= FreezeChance;
        }
    }

    public class Goblin : Enemy
    {
        public Goblin(string name, int health, int attack, int defense) : base(name, health, attack, defense, EnemyType.Goblin)
        {
            CritChance = 0.15;
        }
    }

    public class Skeleton : Enemy, IIgnoreDefense
    {
        public Skeleton(string name, int health, int attack, int defense) : base(name, health, attack, defense, EnemyType.Skeleton) { }
    }

    public class Mage : Enemy
    {
        public Mage(string name, int health, int attack, int defense) : base(name, health, attack, defense, EnemyType.Mage)
        {
            FreezeChance = 0.20;
        }
    }

    public class BossVVG : Goblin
    {
        public BossVVG() : base("ВВГ", 60, 12, 4)
        {
            CritChance = 0.25;
        }
    }

    public class BossKovalsky : Skeleton
    {
        public BossKovalsky() : base("Ковальский", 75, 13, 7) { }
    }

    public class BossArchmage : Mage
    {
        public BossArchmage() : base("Архимаг C++", 54, 19, 2)
        {
            FreezeChance = 0.30;
        }
    }

    public class BossPestov : Mage
    {
        public BossPestov() : base("Пестов C--", 38, 18, 1)
        {
            FreezeChance = 0.15;
        }
    }

    private void OpenChest()
    {
            Console.WriteLine("Вы нашли сундук!");
            var item = _lootTable[_random.Next(_lootTable.Count)];
            if (item is Potion potion)
            {
                _player.Health = _player.MaxHealth;
                Console.WriteLine($"Вы выпили {potion.Name}. {potion.GetDescription()} (HP: {_player.Health})");
            }
            else
            {
                Console.WriteLine($"В сундуке лежит {item.GetDescription()}");
                Console.WriteLine("Ваша текущая экипировка: ");
                string WeaponDesc = "Пусто";
                if (_player.EquippedWeapon != null)
                {
                    WeaponDesc = _player.EquippedWeapon.GetDescription();
                }
                string ArmorDesc = "Пусто";
                if (_player.EquippedArmor != null)
                {
                    ArmorDesc = _player.EquippedArmor.GetDescription();
                }
                Console.WriteLine($"Ваше оружие: {WeaponDesc})");
                Console.WriteLine($"Ваши доспехи: {ArmorDesc}");
                Console.Write("Взять предмет? (Да/Нет): ");
                string choice = Console.ReadLine();
                if (choice != null) 
                {
                    choice = choice.ToLower();
                }
                if (choice == "да")
                {
                    _player.EquipItem(item);
                }
                else
                {
                    Console.WriteLine("Вы выбросили предмет");
                }
            }
    }

    private void Fight(Enemy enemy)
    {
            while (enemy.IsAlive && _player.IsAlive)
            {
                Console.WriteLine($"Бой с {enemy.Name}"); 
                if (_player.IsFrozen)
                {
                    Console.WriteLine("Вы заморожены и пропускаете ход!");
                    IsFrozen = false;
                }

            }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
