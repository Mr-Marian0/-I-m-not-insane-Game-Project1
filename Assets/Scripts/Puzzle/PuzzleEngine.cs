using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleEngine : MonoBehaviour
{

    public StartTimer InheritStartTimer;
    public GameObject PuzzleOwn;
    public GameObject YouLoseReference;
    public GameObject YouWinReference;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioSource audioSourceForCongratulation;

    private bool hasPlayedLoseSound = false;
    private bool hasPlayedWinSound = false;

    public int StartRandomJigsawPuzzle;
    void Start()
    {
        StartRandomJigsawPuzzle = Random.Range(1,21);

        Debug.Log("Random Puzzle: " + StartRandomJigsawPuzzle);

        // Activate the corresponding child
        Transform chosenPuzzle = transform.GetChild(StartRandomJigsawPuzzle - 1);
        chosenPuzzle.gameObject.SetActive(true);
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
