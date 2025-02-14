using UnityEngine;

[CreateAssetMenu(fileName = "QuestConfiguration", menuName = "Mathventures/Quest Configuration")]
public class QuestConfiguration : ScriptableObject
{
    [System.Serializable]
    public class StageTheme
    {
        [Header("Stage Identity")]
        public string stageName;
        public string description;
        public int requiredLevel;

        [Header("Visual Theme")]
        public Color primaryColor = Color.blue;
        public Color secondaryColor = Color.cyan;
        public string backgroundPrefabPath;
        
        [Header("Math Focus")]
        public string mathTopic;
        public string[] learningObjectives;
        public MathProblemGenerator.MathOperation[] focusOperations;
        
        [Header("Rewards")]
        public string[] achievements;
        public string[] unlockables;
        
        [Header("Story Elements")]
        public string introductionText;
        public string completionText;
        public string[] characterDialogue;
    }

    public StageTheme[] stages = new StageTheme[]
    {
        new StageTheme
        {
            stageName = "Enchanted Forest of Addition",
            description = "Begin your journey in a magical forest where friendly creatures help you master the art of addition!",
            requiredLevel = 1,
            mathTopic = "Addition with Numbers 1-20",
            learningObjectives = new string[]
            {
                "Add single-digit numbers fluently",
                "Solve simple word problems involving addition",
                "Understand the relationship between numbers"
            },
            focusOperations = new MathProblemGenerator.MathOperation[]
            {
                MathProblemGenerator.MathOperation.Addition,
                MathProblemGenerator.MathOperation.WordProblem
            },
            introductionText = "Welcome young mathematician! The magical creatures of the Enchanted Forest need your help to count their treasures!",
            completionText = "Wonderful! You've mastered the magic of addition and helped the forest creatures!",
            characterDialogue = new string[]
            {
                "Can you help me count my magical acorns?",
                "The forest fairies love your math skills!",
                "Together, we make math magical!"
            }
        },
        new StageTheme
        {
            stageName = "Subtraction Caves",
            description = "Explore mysterious caves where ancient number dragons teach the secrets of subtraction!",
            requiredLevel = 3,
            mathTopic = "Subtraction within 100",
            learningObjectives = new string[]
            {
                "Subtract two-digit numbers",
                "Understand regrouping",
                "Solve word problems involving subtraction"
            },
            focusOperations = new MathProblemGenerator.MathOperation[]
            {
                MathProblemGenerator.MathOperation.Subtraction,
                MathProblemGenerator.MathOperation.WordProblem
            },
            introductionText = "The wise number dragons have a challenge for you! Master subtraction to unlock their ancient treasures!",
            completionText = "Amazing! You've proven yourself worthy of the dragons' wisdom!",
            characterDialogue = new string[]
            {
                "Can you help solve this ancient riddle?",
                "The dragons are impressed by your skills!",
                "Each problem solved reveals more treasure!"
            }
        },
        new StageTheme
        {
            stageName = "Multiplication Mountain",
            description = "Climb to new heights as you learn the power of multiplication in this magical mountain realm!",
            requiredLevel = 5,
            mathTopic = "Multiplication Facts up to 12",
            learningObjectives = new string[]
            {
                "Learn multiplication facts through 12",
                "Understand patterns in multiplication",
                "Solve story problems with multiplication"
            },
            focusOperations = new MathProblemGenerator.MathOperation[]
            {
                MathProblemGenerator.MathOperation.Multiplication,
                MathProblemGenerator.MathOperation.WordProblem
            },
            introductionText = "Welcome to the peaks of possibility! Here, mountain spirits will teach you the magic of multiplication!",
            completionText = "Incredible! You've reached the summit of multiplication mastery!",
            characterDialogue = new string[]
            {
                "Let's multiply our way to the top!",
                "The mountain spirits celebrate your progress!",
                "You're climbing higher with each solution!"
            }
        },
        new StageTheme
        {
            stageName = "Division Gardens",
            description = "Discover the beauty of division in these magical gardens where numbers bloom into understanding!",
            requiredLevel = 7,
            mathTopic = "Division and Equal Groups",
            learningObjectives = new string[]
            {
                "Understand division as equal sharing",
                "Master basic division facts",
                "Solve practical division problems"
            },
            focusOperations = new MathProblemGenerator.MathOperation[]
            {
                MathProblemGenerator.MathOperation.Division,
                MathProblemGenerator.MathOperation.WordProblem
            },
            introductionText = "The garden sprites need your help to share their magical flowers equally! Learn the art of division!",
            completionText = "Magnificent! You've helped the garden flourish with your division skills!",
            characterDialogue = new string[]
            {
                "Help us share these magical seeds!",
                "Watch how division makes everything fair!",
                "The garden grows with your knowledge!"
            }
        },
        new StageTheme
        {
            stageName = "Fraction Castle",
            description = "Enter the grand Fraction Castle where numbers split and combine in magical ways!",
            requiredLevel = 9,
            mathTopic = "Introduction to Fractions",
            learningObjectives = new string[]
            {
                "Understand basic fractions",
                "Compare fraction sizes",
                "Solve fraction word problems"
            },
            focusOperations = new MathProblemGenerator.MathOperation[]
            {
                MathProblemGenerator.MathOperation.Fractions,
                MathProblemGenerator.MathOperation.WordProblem
            },
            introductionText = "Welcome to the majestic Fraction Castle! Here you'll discover how numbers can be split into magical pieces!",
            completionText = "Outstanding! You've mastered the royal art of fractions!",
            characterDialogue = new string[]
            {
                "Let's split this magical pie fairly!",
                "The royal mathematicians are impressed!",
                "Fractions make math even more magical!"
            }
        }
    };
}
