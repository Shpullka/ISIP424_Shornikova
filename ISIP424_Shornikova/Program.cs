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

    public interface IIgnoreDefense { }

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
        public bool IsDefending { get; set; } = false;

        public Player(string name, int health, int attack, int defense) : base(name, health, attack, defense) { }

        public int GetTotalAttack()
        {
            int total = AttackPower;
            if (EquippedWeapon != null)
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
            }
            else if (item is Armor armor)
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

        public Enemy(string name, int health, int attack, int defense, EnemyType type) : base(name, health, attack, defense)
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

    public class Game
    {
        private Player _player;
        private Random _random = new Random();
        private List<IItem> _lootTable = new List<IItem>();
        private int _turnCount = 0;
        private const int BossTurnInterval = 10;

        public Game()
        {
            _player = new Player("Герой", 100, 15, 5);
            InitializeLootTable();
        }

        private void InitializeLootTable()
        {
            _lootTable.Add(new Potion());
            _lootTable.Add(new Weapon("Ржавый меч", 5));
            _lootTable.Add(new Weapon("Стальной меч", 12));
            _lootTable.Add(new Weapon("Топор берсерка", 20));
            _lootTable.Add(new Armor("Кожаная куртка", 3));
            _lootTable.Add(new Armor("Кольчуга", 8));
            _lootTable.Add(new Armor("Латные доспехи", 15));
        }

        public void Start()
        {
            Console.WriteLine("Добро пожаловать в текстовый рогалик!");
            Console.WriteLine($"Ваши стартовые характеристики: HP {_player.Health}, Атака {_player.AttackPower}, Защита {_player.Defense}\n");
            while (_player.IsAlive)
            {
                _turnCount++;
                Console.WriteLine($"Ход {_turnCount}");
                if (_turnCount % BossTurnInterval == 0)
                {
                    Console.WriteLine("!!! ВНИМАНИЕ! Появляется БОСС !!!");
                    Enemy boss = GenerateBoss();
                    Console.WriteLine($"Появился босс: {boss.Name}!");
                    Console.WriteLine($"Характеристики: HP {boss.Health}, Атака {boss.AttackPower}, Защита {boss.Defense}");
                    Fight(boss);
                    if (!_player.IsAlive) break;
                    continue;
                }

                if (_random.Next(2) == 0)
                {
                    OpenChest();
                }
                else
                {
                    Enemy enemy = GenerateRandomEnemy();
                    Console.WriteLine($"Вы столкнулись с врагом: {enemy.Name} (HP: {enemy.Health}, Атака: {enemy.AttackPower}, Защита: {enemy.Defense})");
                    Fight(enemy);
                }

                if (_player.IsAlive)
                {
                    Console.WriteLine($"\nТекущее состояние: HP {_player.Health}/{_player.MaxHealth}");
                }
            }
            Console.WriteLine("\nИгра окончена");
            Console.WriteLine($"Вы продержались {_turnCount} ходов. Ваш герой пал смертью храбрых");
        }

        private Enemy GenerateBoss()
        {
            int roll = _random.Next(4);
            Enemy boss;
            switch (roll)
            {
                case 0: boss = new BossVVG(); break;
                case 1: boss = new BossKovalsky(); break;
                case 2: boss = new BossArchmage(); break;
                case 3: boss = new BossPestov(); break;
                default: boss = new BossVVG(); break;
            }

            if (boss is IIgnoreDefense)
            {
                Console.WriteLine("Особенность: Полностью игнорирует вашу защиту!");
            }
            if (boss.CritChance > 0.15)
            {
                Console.WriteLine($"Особенность: Высокий шанс критического удара ({boss.CritChance * 100:0}%)!");
            }
            if (boss.FreezeChance > 0)
            {
                Console.WriteLine($"Особенность: Может заморозить вас ({boss.FreezeChance * 100:0}%)!");
            }
            return boss;
        }

        private Enemy GenerateRandomEnemy()
        {
            int roll = _random.Next(3);
            Enemy enemy;
            switch (roll)
            {
                case 0: enemy = new Goblin("Гоблин", 30, 8, 2); break;
                case 1: enemy = new Skeleton("Скелет", 25, 10, 5); break;
                case 2: enemy = new Mage("Маг", 20, 12, 1); break;
                default: enemy = new Goblin("Гоблин", 30, 8, 2); break;
            }
            return enemy;
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
                    _player.IsFrozen = false;
                }
                else
                {
                    Console.WriteLine("Ваш ход. Выберите действие: ");
                    Console.WriteLine("1. Атаковать");
                    Console.WriteLine("2. Защититься");
                    Console.Write("Ваш выбор: ");
                    string input = Console.ReadLine();
                    _player.IsDefending = false;
                    switch (input)
                    {
                        case "1": PlayerAttack(enemy); break;
                        case "2": PlayerDefend(); break;
                        default: Console.WriteLine("Неверный ввод. Вы пропускаете ход"); break;
                    }
                }
                if (!enemy.IsAlive) break;
                EnemyAttack(enemy);
            }
            if (_player.IsAlive)
            {
                Console.WriteLine($"\nВы победили {enemy.Name}!");
            }
        }

        private void PlayerAttack(Enemy enemy)
        {
            int damage = _player.GetTotalAttack();
            int FinalDamage = damage - enemy.Defense;
            if (FinalDamage < 1)
            {
                FinalDamage = 1;
            }
            enemy.TakeDamage(FinalDamage);
            Console.WriteLine($"Вы нанесли {FinalDamage} урона. У {enemy.Name} осталось {enemy.Health} HP");
        }

        private void PlayerDefend()
        {
            Console.WriteLine("Вы встали в защитную стойку");
            _player.IsDefending = true;
        }

        private void EnemyAttack(Enemy enemy)
        {
            if (_player.IsDefending)
            {
                if (_random.NextDouble() <= 0.4)
                {
                    Console.WriteLine("Вы полностью уклонились от атаки!");
                    _player.IsDefending = false;
                    return;
                }
            }
            bool isCrit;
            bool isIgnoringDefense;
            int damage = enemy.CalculateAttackDamage(_player, out isCrit, out isIgnoringDefense);

            int playerDefense = _player.GetTotalDefense();
            int finalDamage = damage;

            if (!isIgnoringDefense)
            {
                double blockPercent = _random.Next(70, 101) / 100.0;
                int blockedAmount = (int)(playerDefense * blockPercent);
                finalDamage = damage - blockedAmount;
                if (finalDamage < 0)
                {
                    finalDamage = 0;
                }
                Console.WriteLine($"Блок поглотил {blockedAmount} урона");
            }
            else
            {
                Console.WriteLine("Враг игнорирует вашу защиту!");
            }
            if (isCrit)
            {
                Console.WriteLine("КРИТИЧЕСКИЙ УДАР!");
            }

            _player.TakeDamage(finalDamage);
            Console.WriteLine($"{enemy.Name} наносит вам {finalDamage} урона. У вас осталось {_player.Health} HP");

            if (enemy.TryFreeze())
            {
                _player.IsFrozen = true;
                Console.WriteLine("Враг наложил на вас заморозку! Вы пропустите следующий ход");
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }
}
