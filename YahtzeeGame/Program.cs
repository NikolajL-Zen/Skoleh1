
using System;
using System.Collections.Generic;
using System.Linq;

namespace YahtzeeGame
{
    class Program
    {
        private static void Main(string[] args)
        {
            YahtzeeGame game = new YahtzeeGame();
            game.StartGame();
        }
    }

    public class Die
    {
        public int Value { get; set; }
        public bool IsLocked { get; set; }

        public Die()
        {
            Value = Roll();
            IsLocked = false;
        }

        public int Roll()
        {
            return new Random().Next(1, 7);
        }
    }


    public class ScoreCalculator
    {
        private const int MAX_DICE_VALUE = 6;

        public int GetDiceValueCount(Die[] dice, int value)
        {
            return dice.Count(d => d.Value == value) * value;
        }

        public int GetThreeOfAKind(Die[] dice)
        {
            return Enumerable.Range(1, MAX_DICE_VALUE)
                .FirstOrDefault(value => dice.Count(d => d.Value == value) >= 3) * 3;
        }

        public int GetFourOfAKind(Die[] dice)
        {
            return Enumerable.Range(1, MAX_DICE_VALUE)
                .FirstOrDefault(value => dice.Count(d => d.Value == value) >= 4) * 4;
        }

        public int GetFullHouse(Die[] dice)
        {
            var valueCounts = dice.GroupBy(d => d.Value)
                .ToDictionary(g => g.Key, g => g.Count());

            return valueCounts.Values.Contains(3) && valueCounts.Values.Contains(2) ? 25 : 0;
        }

        public int GetSmallStraight(Die[] dice)
        {
            var sortedValues = dice.Select(d => d.Value).OrderBy(v => v).ToArray();

            if (sortedValues.SequenceEqual(new[] { 1, 2, 3, 4 }) ||
                sortedValues.SequenceEqual(new[] { 2, 3, 4, 5 }))
            {
                return 30;
            }

            return 0;
        }

        public int GetLargeStraight(Die[] dice)
        {
            var sortedValues = dice.Select(d => d.Value).OrderBy(v => v).ToArray();

            if (sortedValues.SequenceEqual(new[] { 1, 2, 3, 4, 5 }))
            {
                return 40;
            }

            return 0;
        }

        public int GetYahtzee(Die[] dice)
        {
            return dice.All(d => d.Value == dice[0].Value) ? 50 : 0;
        }
    }

    public class Player
    {
        public int Id { get; set; }
        public Die[] Dice { get; set; }
        public Dictionary<string, int> Score { get; set; }

        public Player(int id)
        {
            Id = id;
            Dice = new Die[5];
            for (int i = 0; i < 5; i++)
            {
                Dice[i] = new Die();
            }
            Score = new Dictionary<string, int>();
        }

        public void RollDice()
        {
            foreach (Die die in Dice)
            {
                die.Value = die.Roll();
            }
        }

        public void ScoreCategory(string category, ScoreCalculator scoreCalculator)
        {
            int score = 0;
            switch (category)
            {
                case "Ones":
                    score = scoreCalculator.GetDiceValueCount(Dice, 1);
                    break;
                case "Twos":
                    score = scoreCalculator.GetDiceValueCount(Dice, 2);
                    break;
                case "Threes":
                    score = scoreCalculator.GetDiceValueCount(Dice, 3);
                    break;
                case "Fours":
                    score = scoreCalculator.GetDiceValueCount(Dice, 4);
                    break;
                case "Fives":
                    score = scoreCalculator.GetDiceValueCount(Dice, 5);
                    break;
                case "Sixes":
                    score = scoreCalculator.GetDiceValueCount(Dice, 6);
                    break;
                case "Three of a Kind":
                    score = scoreCalculator.GetThreeOfAKind(Dice);
                    break;
                case "Four of a Kind":
                    score = scoreCalculator.GetFourOfAKind(Dice);
                    break;
                case "Full House":
                    score = scoreCalculator.GetFullHouse(Dice);
                    break;
                case "Small Straight":
                    score = scoreCalculator.GetSmallStraight(Dice);
                    break;
                case "Large Straight":
                    score = scoreCalculator.GetLargeStraight(Dice);
                    break;
                case "Yahtzee":
                    score = scoreCalculator.GetYahtzee(Dice);
                    break;
            }
            Score[category] = score;
        }
    }


    public class YahtzeeGame
    {
        private const int NUM_PLAYERS = 3;
        private const int NUM_ROUNDS = 13;

        public void StartGame()
        {
            ScoreCalculator scoreCalculator = new ScoreCalculator();
            Player[] players = new Player[NUM_PLAYERS];

            for (int i = 0; i < NUM_PLAYERS; i++)
            {
                players[i] = new Player(i + 1);
            }

            for (int round = 1; round <= NUM_ROUNDS; round++)
            {
                foreach (Player player in players)
                {
                    player.RollDice(); // Roll all dice initially

                    for (int roll = 1; roll <= 3; roll++)
                    {
                        Console.WriteLine($"Player {player.Id} - Roll {roll}:");

                        // Roll only the unlocked dice
                        int numDiceToRoll = player.Dice.Count(d => !d.IsLocked);
                        for (int i = 0; i < numDiceToRoll; i++)
                        {
                            Die die = player.Dice.First(d => !d.IsLocked);
                            die.Roll();
                            Console.WriteLine($"Die {Array.IndexOf(player.Dice, die) + 1} rolled to {die.Value}");
                        }

                        Console.WriteLine("Dice values: " + string.Join(", ", player.Dice.Select((d, i) => $"{i + 1}: {d.Value}")));

                        Console.WriteLine("Do you want to keep any dice? (y/n)");
                        string keepInput = Console.ReadLine();

                        if (keepInput.ToLower() == "y")
                        {
                            Console.WriteLine("Enter the numbers of the dice you want to keep (separated by commas):");
                            string keepDiceInput = Console.ReadLine();
                            int[] keepIndices = keepDiceInput.Split(',').Select(int.Parse).ToArray();

                            foreach (Die die in player.Dice)
                            {
                                if (keepIndices.Contains(Array.IndexOf(player.Dice, die) + 1))
                                {
                                    die.IsLocked = true;
                                    Console.WriteLine($"Die {Array.IndexOf(player.Dice, die) + 1} is locked with value {die.Value}");
                                }
                            }
                        }
                        else
                        {
                            // If no dice are kept, unlock all dice
                            foreach (Die die in player.Dice)
                            {
                                die.IsLocked = false;
                            }
                        }
                    }

                    // Score the player's roll
                    Console.WriteLine("Choose a category to score:");
                    string categoryInput = Console.ReadLine();
                    if (IsValidCategory(categoryInput))
                    {
                        player.ScoreCategory(categoryInput, scoreCalculator);
                        Console.WriteLine($"Player {player.Id} scored {player.Score[categoryInput]} points in {categoryInput}.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid category. Please try again.");
                    }
                }
            }

            // Determine the winner based on the scores
            Player winner = players.OrderByDescending(p => p.Score.Values.Sum()).First();
            Console.WriteLine($"The winner is Player {winner.Id} with a total score of {winner.Score.Values.Sum()}!");
        }

        private bool IsValidCategory(string category)
        {
            string[] validCategories = { "Ones", "Twos", "Threes", "Fours", "Fives", "Sixes", "Three of a Kind", "Four of a Kind", "Full House", "Small Straight", "Large Straight", "Yahtzee" };
            return validCategories.Contains(category);
        }
    }
}