using UnityEngine;
using System.Collections;

public class VisualEffectsManager : MonoBehaviour
{
    [System.Serializable]
    public class MagicalEffect
    {
        public string effectName;
        public ParticleSystem particleSystem;
        public AudioClip soundEffect;
        public float duration = 2f;
        public bool looping = false;
    }

    [Header("Magical Effects")]
    public MagicalEffect[] magicalEffects;
    
    [Header("Environment Effects")]
    public ParticleSystem[] backgroundSparkles;
    public ParticleSystem[] floatingLights;
    public ParticleSystem magicMist;
    
    [Header("Achievement Effects")]
    public ParticleSystem correctAnswerBurst;
    public ParticleSystem levelUpSpectacle;
    public ParticleSystem questCompleteShow;
    
    [Header("UI Effects")]
    public ParticleSystem buttonSparkle;
    public ParticleSystem menuTransition;
    public TrailRenderer menuCursorTrail;
    
    [Header("Color Schemes")]
    public Gradient[] magicalGradients;
    public Color[] themeColors;
    
    [Header("References")]
    public AudioSource effectsAudioSource;
    public Camera mainCamera;
    
    private static VisualEffectsManager instance;
    public static VisualEffectsManager Instance => instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        InitializeEffects();
    }

    void InitializeEffects()
    {
        // Setup background effects
        foreach (var sparkle in backgroundSparkles)
        {
            SetupLoopingEffect(sparkle);
        }
        
        foreach (var light in floatingLights)
        {
            SetupLoopingEffect(light);
        }
        
        if (magicMist != null)
        {
            SetupLoopingEffect(magicMist);
        }
    }

    void SetupLoopingEffect(ParticleSystem effect)
    {
        if (effect != null)
        {
            var main = effect.main;
            main.loop = true;
            effect.Play();
        }
    }

    public void PlayMagicalEffect(string effectName, Vector3 position)
    {
        foreach (var effect in magicalEffects)
        {
            if (effect.effectName == effectName)
            {
                StartCoroutine(PlayEffectSequence(effect, position));
                break;
            }
        }
    }

    private IEnumerator PlayEffectSequence(MagicalEffect effect, Vector3 position)
    {
        if (effect.particleSystem != null)
        {
            // Create instance of particle system
            ParticleSystem particleInstance = Instantiate(effect.particleSystem, position, Quaternion.identity);
            
            // Play sound if available
            if (effect.soundEffect != null)
            {
                effectsAudioSource.PlayOneShot(effect.soundEffect);
            }
            
            // Wait for duration
            yield return new WaitForSeconds(effect.duration);
            
            // Clean up if not looping
            if (!effect.looping)
            {
                Destroy(particleInstance.gameObject);
            }
        }
    }

    public void PlayCorrectAnswerEffect(Vector3 position)
    {
        if (correctAnswerBurst != null)
        {
            correctAnswerBurst.transform.position = position;
            correctAnswerBurst.Play();
        }
    }

    public void PlayLevelUpEffect(Vector3 position)
    {
        if (levelUpSpectacle != null)
        {
            StartCoroutine(LevelUpSequence(position));
        }
    }

    private IEnumerator LevelUpSequence(Vector3 position)
    {
        // Create spectacular level up effect
        levelUpSpectacle.transform.position = position;
        levelUpSpectacle.Play();
        
        // Camera shake effect
        StartCoroutine(CameraShake(0.5f, 0.1f));
        
        // Change background effects color
        foreach (var sparkle in backgroundSparkles)
        {
            var main = sparkle.main;
            main.startColor = new ParticleSystem.MinMaxGradient(magicalGradients[Random.Range(0, magicalGradients.Length)]);
        }
        
        yield return new WaitForSeconds(2f);
    }

    private IEnumerator CameraShake(float duration, float magnitude)
    {
        Vector3 originalPos = mainCamera.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            mainCamera.transform.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            
            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = originalPos;
    }

    public void PlayButtonEffect(Vector3 position)
    {
        if (buttonSparkle != null)
        {
            buttonSparkle.transform.position = position;
            buttonSparkle.Play();
        }
    }

    public void StartMenuTransition()
    {
        if (menuTransition != null)
        {
            menuTransition.Play();
        }
    }

    public void UpdateCursorTrail(Vector3 position)
    {
        if (menuCursorTrail != null)
        {
            menuCursorTrail.transform.position = position;
        }
    }

    public void SetThemeColors(int themeIndex)
    {
        if (themeIndex < 0 || themeIndex >= themeColors.Length) return;
        
        Color themeColor = themeColors[themeIndex];
        
        // Update particle effects colors
        foreach (var effect in magicalEffects)
        {
            if (effect.particleSystem != null)
            {
                var main = effect.particleSystem.main;
                main.startColor = new ParticleSystem.MinMaxGradient(themeColor);
            }
        }
    }

    public void PlayQuestCompleteEffect(Vector3 position)
    {
        if (questCompleteShow != null)
        {
            StartCoroutine(QuestCompleteSequence(position));
        }
    }

    private IEnumerator QuestCompleteSequence(Vector3 position)
    {
        // Play main effect
        questCompleteShow.transform.position = position;
        questCompleteShow.Play();
        
        // Enhance background effects
        foreach (var light in floatingLights)
        {
            var emission = light.emission;
            emission.rateOverTime = emission.rateOverTime.constant * 2;
        }
        
        yield return new WaitForSeconds(3f);
        
        // Return background effects to normal
        foreach (var light in floatingLights)
        {
            var emission = light.emission;
            emission.rateOverTime = emission.rateOverTime.constant / 2;
        }
    }
}
