using System;
using System.Collections.Generic;
using System.Text;

namespace lab7
{
    using System;

    // ---------- Base Character ----------
    public abstract class Character
    {
        public string Name { get; private set; }
        public int MaxHealthPoints { get; private set; }
        public int MaxEnergyPoints { get; private set; }

        private int _healthPoints;
        private int _energyPoints;

        public int HealthPoints
        {
            get => _healthPoints;
            protected set
            {
                // Cap at maximum, allow negative (knocked out when <= 0)
                _healthPoints = value < MaxHealthPoints ? value : MaxHealthPoints;
            }
        }

        public int EnergyPoints
        {
            get => _energyPoints;
            protected set
            {
                _energyPoints = value < MaxEnergyPoints ? value : MaxEnergyPoints;
            }
        }

        public bool IsKnockedOut => HealthPoints <= 0;

        protected Character(string name, int maxHealth, int maxEnergy)
        {
            Name = name;
            MaxHealthPoints = maxHealth;
            MaxEnergyPoints = maxEnergy;
            HealthPoints = maxHealth;
            EnergyPoints = maxEnergy;
        }

        // Common action: restores health and energy to max (only if conscious)
        public void Rest()
        {
            if (!IsKnockedOut)
            {
                HealthPoints = MaxHealthPoints;
                EnergyPoints = MaxEnergyPoints;
                Console.WriteLine($"{Name} rests and recovers completely.");
            }
        }

        // Helper to reduce health (used by attacks)
        // Made public so other Character instances can inflict damage on targets.
        public void TakeDamage(int damage)
        {
            if (damage <= 0) return;
            HealthPoints -= damage;
        }

        // Helper to heal (capped at max)
        // Made public so other Character instances (e.g., Mage) can heal targets.
        public void Heal(int amount)
        {
            if (amount <= 0) return;
            HealthPoints += amount;
        }
    }

    // ---------- Ranger ----------
    public class Ranger : Character
    {
        public int NumberOfArrows { get; private set; }
        public int FiredArrows { get; private set; }

        public Ranger(string name) : base(name, 10, 8)
        {
            NumberOfArrows = 10;
            FiredArrows = 0;
        }

        public void FireArrows(Character target)
        {
            if (IsKnockedOut) return;
            if (NumberOfArrows <= 0 || EnergyPoints < 1)
            {
                Console.WriteLine($"{Name} cannot fire an arrow (no arrows or insufficient energy).");
                return;
            }

            EnergyPoints -= 1;
            NumberOfArrows--;
            FiredArrows++;
            target.TakeDamage(1);
            Console.WriteLine($"{Name} the ranger shot an arrow at {target.Name}.");
        }

        public void CollectArrows()
        {
            if (IsKnockedOut) return;
            var collected = FiredArrows;
            NumberOfArrows += collected;
            FiredArrows = 0;   // reset after collection
            Console.WriteLine($"{Name} collects {collected} arrows.");
        }
    }

    // ---------- Barbarian ----------
    public class Barbarian : Character
    {
        public Barbarian(string name) : base(name, 18, 12) { }

        public void SwingAxe(Character target)
        {
            if (IsKnockedOut) return;
            if (EnergyPoints < 3)
            {
                Console.WriteLine($"{Name} doesn't have enough energy to swing an axe.");
                return;
            }

            EnergyPoints -= 3;
            target.TakeDamage(3);
            Console.WriteLine($"{Name} the barbarian swung his mighty axe at {target.Name} for 3 damage.");
        }
    }

    // ---------- Mage ----------
    public class Mage : Character
    {
        public Mage(string name) : base(name, 8, 8) { }

        public void ThrowFireball(Character target)
        {
            if (IsKnockedOut) return;
            if (EnergyPoints < 2)
            {
                Console.WriteLine($"{Name} doesn't have enough energy to throw a fireball.");
                return;
            }

            EnergyPoints -= 2;
            target.TakeDamage(2);
            Console.WriteLine($"{Name} the mage threw a fireball at {target.Name} for 2 damage.");
        }

        public void HealTarget(Character target)
        {
            if (IsKnockedOut) return;
            if (EnergyPoints < 1)
            {
                Console.WriteLine($"{Name} doesn't have enough energy to heal.");
                return;
            }

            EnergyPoints -= 1;
            target.Heal(5);
            Console.WriteLine($"{Name} the mage healed {target.Name} for 5 health.");
        }
    }
}
