using UnityEngine;
using System.Collections.Generic;

public class MathProblemGenerator : MonoBehaviour
{
    public enum MathOperation
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
        Fractions,
        WordProblem
    }

    [System.Serializable]
    public class DifficultyRange
    {
        public int minNumber = 1;
        public int maxNumber = 10;
        public MathOperation[] allowedOperations;
    }

    [Header("Difficulty Settings")]
    public DifficultyRange[] difficultyLevels;

    private System.Random random = new System.Random();

    public struct MathProblem
    {
        public string question;
        public int correctAnswer;
        public string[] options;
    }

    private string[] wordProblemTemplates = new string[]
    {
        "Sarah has {0} apples and finds {1} more. How many apples does she have now?",
        "Tom has {0} marbles and gives {1} to his friend. How many marbles does he have left?",
        "There are {0} rows of stars with {1} stars in each row. How many stars are there in total?",
        "A pizza is cut into {0} equal pieces. If you eat {1} pieces, how many are left?",
        "You have {0} cookies to share equally among {1} friends. How many cookies does each friend get?"
    };

    public MathProblem GenerateProblem(int level)
    {
        DifficultyRange difficulty = difficultyLevels[Mathf.Clamp(level, 0, difficultyLevels.Length - 1)];
        MathOperation operation = difficulty.allowedOperations[Random.Range(0, difficulty.allowedOperations.Length)];
        
        int num1 = Random.Range(difficulty.minNumber, difficulty.maxNumber + 1);
        int num2 = Random.Range(difficulty.minNumber, difficulty.maxNumber + 1);

        MathProblem problem = new MathProblem();
        
        switch (operation)
        {
            case MathOperation.Addition:
                problem.question = $"{num1} + {num2} = ?";
                problem.correctAnswer = num1 + num2;
                break;

            case MathOperation.Subtraction:
                // Ensure larger number comes first
                if (num2 > num1) { int temp = num1; num1 = num2; num2 = temp; }
                problem.question = $"{num1} - {num2} = ?";
                problem.correctAnswer = num1 - num2;
                break;

            case MathOperation.Multiplication:
                problem.question = $"{num1} × {num2} = ?";
                problem.correctAnswer = num1 * num2;
                break;

            case MathOperation.Division:
                // Ensure division results in whole number
                num2 = Mathf.Max(1, num2);
                int product = num1 * num2;
                problem.question = $"{product} ÷ {num2} = ?";
                problem.correctAnswer = num1;
                break;

            case MathOperation.WordProblem:
                string template = wordProblemTemplates[Random.Range(0, wordProblemTemplates.Length)];
                problem.question = string.Format(template, num1, num2);
                // Calculate answer based on template type
                if (template.Contains("more") || template.Contains("total"))
                    problem.correctAnswer = num1 + num2;
                else if (template.Contains("gives") || template.Contains("left"))
                    problem.correctAnswer = num1 - num2;
                else if (template.Contains("rows"))
                    problem.correctAnswer = num1 * num2;
                else if (template.Contains("share"))
                    problem.correctAnswer = num1 / num2;
                break;
        }

        // Generate multiple choice options
        problem.options = GenerateOptions(problem.correctAnswer, difficulty);
        
        return problem;
    }

    private string[] GenerateOptions(int correctAnswer, DifficultyRange difficulty)
    {
        HashSet<int> options = new HashSet<int> { correctAnswer };
        
        // Generate 3 wrong options
        while (options.Count < 4)
        {
            int offset = Random.Range(1, Mathf.Max(5, correctAnswer / 2));
            int option = Random.value < 0.5f ? 
                correctAnswer + offset : 
                Mathf.Max(0, correctAnswer - offset);
            
            options.Add(option);
        }

        // Convert to array and shuffle
        string[] shuffledOptions = new string[4];
        int index = 0;
        foreach (int option in options)
        {
            shuffledOptions[index++] = option.ToString();
        }
        
        // Fisher-Yates shuffle
        for (int i = shuffledOptions.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            string temp = shuffledOptions[i];
            shuffledOptions[i] = shuffledOptions[j];
            shuffledOptions[j] = temp;
        }

        return shuffledOptions;
    }
}
