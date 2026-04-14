using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryandMechanics : MonoBehaviour
{
    public RectTransform spinningImage; // Assign the UI Image's RectTransform here
    public AudioSource firstAudioSource;
    public AudioSource secondAudioSource;
    public AudioSource thirdAudioSource; // Optional, if you want to play a third audio after the second one
    public TMP_Text subtitleText; // Assign the TextMeshPro text object here

    private bool isSpinning = false;

    public void StartStory()
    {
        if (spinningImage == null || firstAudioSource == null || secondAudioSource == null)
        {
            Debug.LogError("SpinningImage, FirstAudioSource, or SecondAudioSource is not assigned!");
            return;
        }

        isSpinning = true;
        StartCoroutine(SpinImage());
        firstAudioSource.Play();
        StartCoroutine(PlayAudiosSequence());
        StartCoroutine(DelayAndType());
    }

    private IEnumerator SpinImage()
    {
        yield return new WaitForSeconds(0.2f);
        while (isSpinning)
        {
            spinningImage.Rotate(0f, 0f, 360f * Time.deltaTime - 1.2f); // Rotate 360 degrees per second
            yield return null;
        }
    }

    private IEnumerator PlayAudiosSequence()
    {
        // Wait for the first audio to finish
        yield return new WaitForSeconds(firstAudioSource.clip.length);

        // Play the second audio
        secondAudioSource.Play();

        // Wait for the second audio to finish
        yield return new WaitForSeconds(secondAudioSource.clip.length + 4f);

        thirdAudioSource.Play();

        yield return new WaitForSeconds(thirdAudioSource.clip.length); // Optional delay before playing the third audio

        // Stop spinning
        isSpinning = false;
    }

    private IEnumerator DelayAndType()
    {
        yield return new WaitForSeconds(10f);
        string paragraph = "In the old asylum records, Dr. John Wayne, a controversial psychiatrist, claimed that puzzle games and riddles could restore fragments of sanity in d3$ply d1stvrb3d p@t!3n7s. According to his notes, forcing the mind to solve patterns and hidden answers could slowly reconnect broken thoughts and r3build a p@tient's s3nse of re@l!ty. Though many doctors dismissed his theory as unproven, he continued his strange experiments within the asylum walls. Now the halls are filled with locked doors, cryptic riddles, and mechanical puzzles—each one part of Dr. Wayne's belief that only those who can solve them are ready to return to the world outside.";
        subtitleText.text = "";
        foreach (char c in paragraph)
        {
            subtitleText.text += c;
            yield return new WaitForSeconds(0.07f);
        }
    }
}
