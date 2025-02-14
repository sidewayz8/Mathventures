using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

[System.Serializable]
public class QuestionData
{
    public string question;
    public int correctAnswer;
    public string[] options;
}

[System.Serializable]
public class ProgressData
{
    public string userId;
    public int chapter;
    public int points;
    public int currentLevel;
}

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance { get; private set; }

    [Header("Components")]
    public QuestUI questUI;
    public MathProblemGenerator problemGenerator;
    public ProgressManager progressManager;

    [Header("Game Settings")]
    public int pointsPerCorrectAnswer = 10;
    public int pointsNeededForLevelUp = 50;
    
    private string baseUrl = "http://localhost:3000";
    private QuestionData currentQuestion;
    private int currentScore;
    private int currentLevel = 1;
    private string currentUserId;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GenerateNewQuestion();
        UpdateUI();
    }

    public void GenerateNewQuestion()
    {
        var problem = problemGenerator.GenerateProblem(currentLevel);
        currentQuestion = new QuestionData
        {
            question = problem.question,
            correctAnswer = problem.correctAnswer,
            options = problem.options
        };
        
        questUI.DisplayQuestion(currentQuestion.question, currentQuestion.options);
    }

    public void CheckAnswer(int selectedAnswerIndex)
    {
        if (currentQuestion == null) return;

        bool isCorrect = int.Parse(currentQuestion.options[selectedAnswerIndex]) == currentQuestion.correctAnswer;

        if (isCorrect)
        {
            HandleCorrectAnswer();
        }
        else
        {
            HandleWrongAnswer();
        }

        // Generate next question after a delay
        StartCoroutine(NextQuestionDelay());
    }

    private void HandleCorrectAnswer()
    {
        questUI.ShowCorrectAnswer();
        currentScore += pointsPerCorrectAnswer;
        
        // Check for level up
        if (currentScore >= pointsNeededForLevelUp * currentLevel)
        {
            LevelUp();
        }
        
        UpdateUI();
        SaveProgress();
    }

    private void HandleWrongAnswer()
    {
        questUI.ShowWrongAnswer();
    }

    private void LevelUp()
    {
        currentLevel++;
        questUI.UpdateLevel(currentLevel);
        // Save progress on level up
        SaveProgress();
    }

    private void UpdateUI()
    {
        questUI.UpdateScore(currentScore);
        questUI.UpdateLevel(currentLevel);
    }

    private IEnumerator NextQuestionDelay()
    {
        yield return new WaitForSeconds(2f); // Wait for animations/effects
        GenerateNewQuestion();
    }

    public void SaveProgress()
    {
        if (string.IsNullOrEmpty(currentUserId)) return;

        ProgressData progressData = new ProgressData
        {
            userId = currentUserId,
            chapter = currentLevel,
            points = currentScore,
            currentLevel = currentLevel
        };

        StartCoroutine(progressManager.SaveUserProgress(progressData.userId, progressData.chapter, progressData.points));
    }

    public void SetUserId(string userId)
    {
        currentUserId = userId;
    }
}

