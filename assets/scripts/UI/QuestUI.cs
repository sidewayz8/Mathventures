using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public Button[] answerButtons;
    public ParticleSystem correctAnswerEffect;
    public ParticleSystem wrongAnswerEffect;
    public Animator questAnimator;
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip levelUpSound;

    [Header("Visual Effects")]
    public GameObject magicalSparkles;
    public GameObject questCompletionEffect;

    private void Start()
    {
        // Initialize UI elements
        foreach (Button button in answerButtons)
        {
            button.onClick.AddListener(() => OnAnswerSelected(button));
        }
    }

    public void DisplayQuestion(string question, string[] answers)
    {
        questionText.text = question;
        for (int i = 0; i < answers.Length && i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answers[i];
        }
        
        // Animate question appearance
        questAnimator.SetTrigger("ShowQuestion");
        magicalSparkles.SetActive(true);
    }

    private void OnAnswerSelected(Button selectedButton)
    {
        int answerIndex = System.Array.IndexOf(answerButtons, selectedButton);
        GameFlowManager.Instance.CheckAnswer(answerIndex);
    }

    public void ShowCorrectAnswer()
    {
        correctAnswerEffect.Play();
        audioSource.PlayOneShot(correctSound);
        questAnimator.SetTrigger("CorrectAnswer");
    }

    public void ShowWrongAnswer()
    {
        wrongAnswerEffect.Play();
        audioSource.PlayOneShot(wrongSound);
        questAnimator.SetTrigger("WrongAnswer");
    }

    public void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void UpdateLevel(int level)
    {
        levelText.text = $"Level {level}";
        audioSource.PlayOneShot(levelUpSound);
        questCompletionEffect.SetActive(true);
    }
}
