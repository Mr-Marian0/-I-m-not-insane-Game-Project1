using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialFader : MonoBehaviour
{
    [Header("Canvas Groups to Fade In Sequence")]
    public CanvasGroup barsHighlight;      // BarsHighlight
    public CanvasGroup doorHighlight;      // DoorHighlight  
    public CanvasGroup stressHighlight;    // StressHighlight
    public Button tutorialContinueButton;  // TutorialContinue button
    public Timer timerReference;              // Reference to Timer script
    
    [Header("Settings")]
    public float fadeDuration = 0.5f;
    
    private GameEngine gameEngine;
    private int currentStep = 0;
    private bool isFading = false;
    private CanvasGroup currentActiveGroup;
    
    void Start()
    {
        gameEngine = GetComponent<GameEngine>();
        
        // Initially hide all tutorial elements
        if (barsHighlight != null) barsHighlight.alpha = 0f;
        if (doorHighlight != null) doorHighlight.alpha = 0f;
        if (stressHighlight != null) stressHighlight.alpha = 0f;
        
        // Set button to not interactable until its turn
        if (tutorialContinueButton != null)
        {
            tutorialContinueButton.interactable = false;
            tutorialContinueButton.onClick.AddListener(OnContinueButtonPressed);
        }
    }
    
    public void StartTutorialSequence()
    {
        currentStep = 0;
        StartCoroutine(FadeInCurrentStep());
    }
    
    private IEnumerator FadeInCurrentStep()
    {
        isFading = true;
        
        // Hide the previous active group (if any)
        if (currentActiveGroup != null)
        {
            currentActiveGroup.alpha = 0f;
            currentActiveGroup.interactable = false;
            currentActiveGroup.blocksRaycasts = false;
        }
        
        // Get the current canvas group based on step
        CanvasGroup currentGroup = GetCurrentCanvasGroup();
        currentActiveGroup = currentGroup;
        
        if (currentGroup != null)
        {
            // Enable interactable and raycasts for this group
            currentGroup.interactable = true;
            currentGroup.blocksRaycasts = true;
            
            // Start from alpha 0
            currentGroup.alpha = 0f;
            
            // Fade from 0 to 1
            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.unscaledDeltaTime; // Use unscaledDeltaTime for Time.timeScale = 0
                float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
                currentGroup.alpha = alpha;
                yield return null;
            }
            currentGroup.alpha = 1f;
        }
        
        isFading = false;
        
        // Enable continue button for this step
        if (tutorialContinueButton != null)
        {
            tutorialContinueButton.interactable = true;
        }
    }
    
    private CanvasGroup GetCurrentCanvasGroup()
    {
        switch (currentStep)
        {
            case 0: return barsHighlight;
            case 1: return doorHighlight;
            case 2: return stressHighlight;
            default: return null;
        }
    }
    
    public void OnContinueButtonPressed()
    {
        if (isFading) return; // Don't interrupt a fade
        
        // Disable button temporarily
        if (tutorialContinueButton != null)
            tutorialContinueButton.interactable = false;
        
        // Move to next step
        currentStep++;
        
        // Check if we're done with all tutorial steps
        if (currentStep >= 3)
        {
            // Tutorial complete - start the game
            StartGame();
        }
        else
        {
            // Fade in next tutorial element
            StartCoroutine(FadeInCurrentStep());
        }
    }
    
    private void StartGame()
{
    // Hide all tutorial elements
    if (barsHighlight != null) barsHighlight.gameObject.SetActive(false);
    if (doorHighlight != null) doorHighlight.gameObject.SetActive(false);
    if (stressHighlight != null) stressHighlight.gameObject.SetActive(false);
    if (tutorialContinueButton != null) tutorialContinueButton.gameObject.SetActive(false);

    // Resume game time
    Time.timeScale = 1f;

    // Restart timer
    timerReference.StopTimer = false;
    timerReference.StopAllCoroutines();
    StartCoroutine(timerReference.UpdateTimeRandomly());

    Debug.Log("Tutorial complete - Game started!");
}

    
    // Optional: Skip all tutorials and go directly to game
    public void SkipAllTutorials()
    {
        StopAllCoroutines();
        StartGame();
    }
}