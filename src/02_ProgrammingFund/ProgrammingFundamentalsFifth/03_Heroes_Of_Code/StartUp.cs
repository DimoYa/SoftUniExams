using System.Text;

namespace _03_Heroes_Of_Code
{
    public class StartUp
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var heroes = new List<Hero>();
            var result = new StringBuilder();

            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var heroName = input[0];
                var hitPoints = int.Parse(input[1]);
                var manaPoints = int.Parse(input[2]);

                var hero = new Hero(heroName, hitPoints, manaPoints);
                heroes.Add(hero);
            }

            string command;

            while ((command = Console.ReadLine()) != "End")
            {
                var token = command.Split(" - ", StringSplitOptions.RemoveEmptyEntries);

                var commandName = token[0];
                var commandParameters = token.Skip(1).ToArray();

                switch (commandName)
                {
                    case "CastSpell":
                        result.AppendLine(CastSpellCommand(commandParameters, heroes));
                        break;
                    case "TakeDamage":
                        result.AppendLine(TakeDamageCommand(commandParameters, heroes));
                        break;
                    case "Recharge":
                        result.AppendLine(RechargeCommand(commandParameters, heroes));
                        break;
                    case "Heal":
                        result.AppendLine(HealCommand(commandParameters, heroes));
                        break;
                }
            }

            heroes.ForEach(h => result.AppendLine($"{h.Name}\n  HP: {h.HP}\n  MP: {h.MP}"));
            Console.WriteLine(result.ToString().TrimEnd());
        }

        private static string HealCommand(string[] commandParameters, List<Hero> heroes)
        {
            var heroName = commandParameters[0];
            var amount = int.Parse(commandParameters[1]);

            var currentHero = heroes.FirstOrDefault(h => h.Name == heroName);

            var actualHeal= Math.Min(amount, 100 - currentHero.HP);
            currentHero.HP += actualHeal;

            return $"{heroName} healed for {actualHeal} HP!";
        }

        private static string RechargeCommand(string[] commandParameters, List<Hero> heroes)
        {
            var heroName = commandParameters[0];
            var amount = int.Parse(commandParameters[1]);

            var currentHero = heroes.FirstOrDefault(h => h.Name == heroName);

            var actualRecharge = Math.Min(amount, 200 - currentHero.MP);
            currentHero.MP += actualRecharge;

            return $"{heroName} recharged for {actualRecharge} MP!";
        }

        private static string TakeDamageCommand(string[] commandParameters, List<Hero> heroes)
        {
            var heroName = commandParameters[0];
            var damage = int.Parse(commandParameters[1]);
            var attacker = commandParameters[2];

            var currentHero = heroes.FirstOrDefault(h => h.Name == heroName);

            currentHero.HP -= damage;

            if (currentHero.HP <= 0)
            {
                heroes.Remove(currentHero);
                return $"{heroName} has been killed by {attacker}!";
            }
            return $"{heroName} was hit for {damage} HP by {attacker} and now has {currentHero.HP} HP left!";
        }

        private static string CastSpellCommand(string[] commandParameters, List<Hero> heroes)
        {
            var heroName = commandParameters[0];
            var mpNeeded = int.Parse(commandParameters[1]);
            var spellName = commandParameters[2];

            var currentHero = heroes.FirstOrDefault(h => h.Name == heroName);

            if (currentHero.MP < mpNeeded)
            {
                return $"{heroName} does not have enough MP to cast {spellName}!";
            }

            currentHero.MP -= mpNeeded;
            return $"{heroName} has successfully cast {spellName} and now has {currentHero.MP} MP!";
        }
    }
    public class Hero
    {
        public Hero(string name, int hitPoints, int manaPoints)
        {
            this.Name = name;
            this.HP = hitPoints;
            this.MP = manaPoints;
        }

        public String Name { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }
    }
}