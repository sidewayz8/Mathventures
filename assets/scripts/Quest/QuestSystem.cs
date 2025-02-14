using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestSystem : MonoBehaviour
{
    [System.Serializable]
    public class QuestStage
    {
        public string stageName;
        public string description;
        public Sprite backgroundImage;
        public GameObject environmentPrefab;
        public ParticleSystem[] magicalEffects;
        public AudioClip backgroundMusic;
        public int requiredLevel;
        public string completionMessage;
    }

    [Header("Quest Configuration")]
    public QuestStage[] questStages;
    public float transitionDuration = 2f;
    
    [Header("Visual Effects")]
    public ParticleSystem portalEffect;
    public ParticleSystem starburstEffect;
    public ParticleSystem magicTrailEffect;
    
    [Header("Audio")]
    public AudioSource musicSource;
    public AudioSource effectsSource;
    public AudioClip portalSound;
    public AudioClip victorySound;
    
    [Header("References")]
    public Animator environmentAnimator;
    public GameObject mainCharacter;
    
    private int currentStageIndex = 0;
    private QuestStage currentStage;
    private GameFlowManager gameFlow;

    void Start()
    {
        gameFlow = GameFlowManager.Instance;
        InitializeQuest();
    }

    void InitializeQuest()
    {
        if (questStages.Length > 0)
        {
            SetStage(0);
        }
    }

    public void SetStage(int stageIndex)
    {
        if (stageIndex >= questStages.Length) return;

        StartCoroutine(TransitionToStage(stageIndex));
    }

    private IEnumerator TransitionToStage(int newStageIndex)
    {
        // Start transition effects
        portalEffect.Play();
        effectsSource.PlayOneShot(portalSound);

        // Fade out current stage
        if (environmentAnimator != null)
        {
            environmentAnimator.SetTrigger("FadeOut");
            yield return new WaitForSeconds(1f);
        }

        // Update stage
        currentStageIndex = newStageIndex;
        currentStage = questStages[currentStageIndex];

        // Clean up previous stage
        if (currentStage.environmentPrefab != null)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject != mainCharacter)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        // Setup new stage
        if (currentStage.environmentPrefab != null)
        {
            Instantiate(currentStage.environmentPrefab, transform);
        }

        // Start magical effects
        foreach (var effect in currentStage.magicalEffects)
        {
            effect.Play();
        }

        // Change background music
        if (currentStage.backgroundMusic != null)
        {
            StartCoroutine(CrossFadeMusic(currentStage.backgroundMusic));
        }

        // Fade in new stage
        if (environmentAnimator != null)
        {
            environmentAnimator.SetTrigger("FadeIn");
        }

        // Show stage introduction
        StartCoroutine(ShowStageIntroduction());
    }

    private IEnumerator CrossFadeMusic(AudioClip newMusic)
    {
        float startVolume = musicSource.volume;
        
        // Fade out current music
        float elapsed = 0;
        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0, elapsed / transitionDuration);
            yield return null;
        }

        // Change and fade in new music
        musicSource.clip = newMusic;
        musicSource.Play();
        
        elapsed = 0;
        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0, startVolume, elapsed / transitionDuration);
            yield return null;
        }
    }

    private IEnumerator ShowStageIntroduction()
    {
        starburstEffect.Play();
        
        // Wait for effects to complete
        yield return new WaitForSeconds(2f);
        
        // Enable gameplay
        gameFlow.GenerateNewQuestion();
    }

    public void OnLevelUp(int newLevel)
    {
        // Check if we should advance to next stage
        for (int i = currentStageIndex + 1; i < questStages.Length; i++)
        {
            if (questStages[i].requiredLevel == newLevel)
            {
                SetStage(i);
                break;
            }
        }
    }

    public void PlayVictoryEffects()
    {
        starburstEffect.Play();
        magicTrailEffect.Play();
        effectsSource.PlayOneShot(victorySound);
    }
}
