using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleEngine : MonoBehaviour
{

    public StartTimer InheritStartTimer;
    public GameObject PuzzleOwn;

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

    }
}
