using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OkButton : MonoBehaviour
{


    public GameObject AnyPuzz;
    public GameObject AnyCongratulation;
    public GameObject YouWIn;
    public GameObject Conffeti;

    //USED TO CONTINUE THE TIMER
    public StartTimer InheritStartTimer;

    //problema sa else if to para ma run ng isang beses lang bwisit!
    public PuzzlesKeyColliders InheritPuzzleKeyCollliders;

    public void Okay()
    {

        Debug.Log("NAPINDOT YUNG BUTTON 4!!!!!!!!!");
        InheritStartTimer.PauseTheTimer = true;

        AnyCongratulation.SetActive(false);
        YouWIn.SetActive(false);
        Conffeti.SetActive(false);

        AnyPuzz.SetActive(false);
        
        InheritPuzzleKeyCollliders.RunItOnce = 0;

        LoadScene("SampleScene");

    }

    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

}
