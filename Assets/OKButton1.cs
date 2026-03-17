using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OKButton1 : MonoBehaviour
{
 
    public GameObject PuzzleRewards1;
    public GameObject AnyCongratulation;
    public GameObject YouWIn;
    public GameObject Conffeti;

    public GameObject Answers;
    public GameObject Questions;
    public GameObject Players;
    public GameObject Enemy;

    public void Okay()
    {
      AnyCongratulation.SetActive(false);
      YouWIn.SetActive(false);
      Conffeti.SetActive(false);
      PuzzleRewards1.SetActive(false);

      Answers.SetActive(false);
      Questions.SetActive(false);
      Players.SetActive(false);
      Enemy.SetActive(false);

      LoadScene("SampleScene");
    }

    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

}
