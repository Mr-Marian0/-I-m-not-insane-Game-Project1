using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleEngine : MonoBehaviour
{
    public StartTimer InheritStartTimer;
    public GameObject PuzzleOwn;
    public GameObject YouLoseReference;
    public GameObject YouWinReference;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioSource audioSourceForCongratulation;
    
    // FOR INTRO OF POP-UP TASKS
    public GameObject popUpParentIntro;      // Parent object with Image component
    public GameObject popUpChildTextIntro;       // Child object with Text component
    public AudioClip popUpSound;        // Sound to play when pop-up appears
    public float countdownDelay = 1.5f;  // Delay before pop-up appears
    public float fadeDuration = 1.5f;    // Duration of fade out
    
    private bool hasPlayedLoseSound = false;
    private bool hasPlayedWinSound = false;
    private bool hasTriggeredPopUp = false;
    
    public int StartRandomJigsawPuzzle;
    
    void Start()
    {
        StartRandomJigsawPuzzle = Random.Range(1, 21);
        Debug.Log("Random Puzzle: " + StartRandomJigsawPuzzle);
        
        // Activate the corresponding child
        Transform chosenPuzzle = transform.GetChild(StartRandomJigsawPuzzle - 1);
        chosenPuzzle.gameObject.SetActive(true);
        
        // Start the pop-up coroutine
        StartCoroutine(ShowPopUpWithDelay());
    }
    
    IEnumerator ShowPopUpWithDelay()
    {
        // Wait for the countdown delay
        yield return new WaitForSeconds(countdownDelay);
        
        // Activate the pop-up
        if (popUpParentIntro != null)
            popUpParentIntro.SetActive(true);
        
        if (popUpChildTextIntro != null)
            popUpChildTextIntro.SetActive(true);
        
        // Play the pop-up sound
        if (popUpSound != null && audioSourceForCongratulation != null)
        {
            audioSourceForCongratulation.PlayOneShot(popUpSound);
        }
        
        // Start fade out coroutine
        StartCoroutine(FadeOutPopUp());
    }
    
    IEnumerator FadeOutPopUp()
    {

        yield return new WaitForSeconds(1f);

        // Get Image component from parent
        Image parentImage = null;
        Text childText = null;
        CanvasGroup parentCanvasGroup = null;
        
        // Try to get CanvasGroup (better for fading entire objects)
        if (popUpParentIntro != null)
        {
            parentCanvasGroup = popUpParentIntro.GetComponent<CanvasGroup>();
            if (parentCanvasGroup == null)
            {
                parentCanvasGroup = popUpParentIntro.AddComponent<CanvasGroup>();
            }
        }
        
        // Also get individual components as fallback
        if (popUpParentIntro != null)
            parentImage = popUpParentIntro.GetComponent<Image>();
        
        if (popUpChildTextIntro != null)
            childText = popUpChildTextIntro.GetComponent<Text>();
        
        float elapsedTime = 0f;
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1f - (elapsedTime / fadeDuration);
            
            // Fade using CanvasGroup (affects parent and children together)
            if (parentCanvasGroup != null)
            {
                parentCanvasGroup.alpha = alpha;
            }
            else
            {
                // Fallback: fade individually
                if (parentImage != null)
                {
                    Color color = parentImage.color;
                    color.a = alpha;
                    parentImage.color = color;
                }
                
                if (childText != null)
                {
                    Color color = childText.color;
                    color.a = alpha;
                    childText.color = color;
                }
            }
            
            yield return null;
        }
        
        // After fade completes, deactivate the objects
        if (popUpParentIntro != null)
            popUpParentIntro.SetActive(false);
        
        if (popUpChildTextIntro != null)
            popUpChildTextIntro.SetActive(false);
        
        // Reset alpha for next time (if needed)
        if (parentCanvasGroup != null)
            parentCanvasGroup.alpha = 1f;
    }
    
    void Update()
    {
        if(YouLoseReference.activeSelf == true && !hasPlayedLoseSound)
        {
            audioSourceForCongratulation.clip = loseSound;
            audioSourceForCongratulation.PlayOneShot(loseSound);
            hasPlayedLoseSound = true;
        }
        else if(YouWinReference.activeSelf == true && !hasPlayedWinSound)
        {
            audioSourceForCongratulation.clip = winSound;
            audioSourceForCongratulation.PlayOneShot(winSound);
            hasPlayedWinSound = true;
        }
    }
}