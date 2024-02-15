using System.Net.Http.Headers;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ThirdTask
{
    public class Program
    {
        public static void Main()
        {
            var numberOFHeroes = int.Parse(Console.ReadLine());
            var heroes = new List<Heroe>();
            var sb = new StringBuilder();


            for (int i = 0; i < numberOFHeroes; i++)
            {
                var heroInfo = SplitByDelimiter(Console.ReadLine(), " ");

                var name = heroInfo[0];
                var hitPoints = int.Parse(heroInfo[1]);
                var manaPoints = int.Parse(heroInfo[2]);

                var newHeroe = new Heroe(name, hitPoints, manaPoints);
                heroes.Add(newHeroe);
            }

            string line;

            while ((line = Console.ReadLine()) != "End")
            {
                var token = SplitByDelimiter(line, " - ");

                var command = token[0];
                var parameters = token.Skip(1).ToList();

                switch (command)
                {
                    case "CastSpell":
                        sb.AppendLine(CastSpellCommand(parameters, heroes));
                        break;
                    case "TakeDamage":
                        sb.AppendLine(TakeDamageCommand(parameters, heroes));
                        break;
                    case "Recharge":
                        sb.AppendLine(RechargeCommand(parameters, heroes));
                        break;
                    case "Heal":
                        sb.AppendLine(HealCommand(parameters, heroes));
                        break;
                }
            }

            heroes.ForEach(
                h => sb.AppendLine($"{h.Name}\n  HP: {h.HitPoints}\n  MP: {h.ManaPoints}")
            );

            Console.WriteLine(sb.ToString().TrimEnd());
        }

        private static string HealCommand(List<string> parameters, List<Heroe> heroes)
        {
            var name = parameters[0];
            var amount = int.Parse(parameters[1]);

            var currentHero = heroes.FirstOrDefault(h => h.Name == name);

            var increaasedAmount = currentHero.HitPoints + amount > 100 ? 100 - currentHero.HitPoints : amount;

            currentHero.HitPoints += increaasedAmount;

            return $"{name} healed for {increaasedAmount} HP!";
        }

        private static string RechargeCommand(List<string> parameters, List<Heroe> heroes)
        {
            var name = parameters[0];
            var amount = int.Parse(parameters[1]);

            var currentHero = heroes.FirstOrDefault(h => h.Name == name);

            var increaasedAmount = currentHero.ManaPoints + amount > 200 ? 200 - currentHero.ManaPoints : amount;

            currentHero.ManaPoints += increaasedAmount;

            return $"{name} recharged for {increaasedAmount} MP!";
        }

        private static string TakeDamageCommand(List<string> parameters, List<Heroe> heroes)
        {
            var name = parameters[0];
            var damage = int.Parse(parameters[1]);
            var attacker = parameters[2];

            var currentHero = heroes.FirstOrDefault(h => h.Name == name);

            currentHero.HitPoints -= damage;

            if (currentHero.HitPoints <= 0)
            {
                heroes.Remove(currentHero);
                return $"{name} has been killed by {attacker}!";
            }

            return $"{name} was hit for {damage} HP by {attacker} and now has {currentHero.HitPoints} HP left!";
        }

        private static string CastSpellCommand(List<string> parameters, List<Heroe> heroes)
        {
            var name = parameters[0];
            var mpNeeded = int.Parse(parameters[1]);
            var spellName = parameters[2];

            var currentHero = heroes.FirstOrDefault(h => h.Name == name);

            if (currentHero.ManaPoints < mpNeeded)
            {
                return $"{name} does not have enough MP to cast {spellName}!";
            }
            currentHero.ManaPoints -= mpNeeded;

            return $"{name} has successfully cast {spellName} and now has {currentHero.ManaPoints} MP!";
        }

        private static List<string> SplitByDelimiter(string input, string separator)
        {
            return input
                    .Split(new[] { separator }, StringSplitOptions.None)
                    .ToList();
        }
    }

    public class Heroe
    {
        public Heroe(string name, int hitPoints, int manaPoints)
        {
            Name = name;
            HitPoints = hitPoints;
            ManaPoints = manaPoints;
        }
        public string Name { get; set; }

        public int HitPoints { get; set; }

        public int ManaPoints { get; set; }
    }
}