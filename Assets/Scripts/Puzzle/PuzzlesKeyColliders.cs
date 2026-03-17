using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlesKeyColliders : MonoBehaviour
{

    public GameObject Congratulation1;
    public GameObject Congratulation2;
    public GameObject Congratualtion3;
    public GameObject Congratualtion4;
    public GameObject Congratualtion5;
    public GameObject Congratualtion6;
    public GameObject Congratualtion7;
    public GameObject Congratualtion8;
    public GameObject Congratualtion9;
    public GameObject Congratualtion10;
    public GameObject Congratualtion11;
    public GameObject Congratualtion12;
    public GameObject Congratualtion13;
    public GameObject Congratualtion14;
    public GameObject Congratualtion15;
    public GameObject Congratualtion16;
    public GameObject Congratualtion17;
    public GameObject Congratualtion18;
    public GameObject Congratualtion19;
    public GameObject Congratualtion20;

    public GameObject PuzzleKey1;
    public GameObject PuzzleKey2;
    public GameObject PuzzleKey3;
    public GameObject PuzzleKey4;
    public GameObject PuzzleKey5;
    public GameObject PuzzleKey6;
    public GameObject PuzzleKey7;
    public GameObject PuzzleKey8;
    public GameObject PuzzleKey9;
    public GameObject PuzzleKey10;
    public GameObject PuzzleKey11;
    public GameObject PuzzleKey12;
    public GameObject PuzzleKey13;
    public GameObject PuzzleKey14;
    public GameObject PuzzleKey15;
    public GameObject PuzzleKey16;
    public GameObject PuzzleKey17;
    public GameObject PuzzleKey18;
    public GameObject PuzzleKey19;
    public GameObject PuzzleKey20;

    public GameObject YouWin;
    public GameObject Conffeti;

    //REMAINING TIME AFTER YOU FINISH THE PUZZLE
    public StartTimer InheritStartTimer;

    //PUZZLE IS FINISHED
    public bool IsFinished;

    public int RunItOnce = 0;

    void Update()
    {
        
        if(PuzzleKey1.transform.position.x == -1 && RunItOnce != 1)
        {

            InheritStartTimer.PauseTheTimer = false;

            //If the player solve the puzzle: pop up the CONGRATULATION! -&- You Win! -&- Conffeti
            Congratulation1.SetActive(true);
            YouWin.SetActive(true);
            Conffeti.SetActive(true);

            //Warn the PuzzleReward to True
            IsFinished = true;

            RunItOnce++;
        }
        if(PuzzleKey2.transform.position.x == -1 && RunItOnce != 1)
        {
            InheritStartTimer.PauseTheTimer = false;

            Congratulation2.SetActive(true);
            YouWin.SetActive(true);
            Conffeti.SetActive(true);

             IsFinished = true;

             RunItOnce++;
        }
        if(PuzzleKey3.transform.position.x == -1 && RunItOnce != 1)
        {
            InheritStartTimer.PauseTheTimer = false;

            Congratualtion3.SetActive(true);
            YouWin.SetActive(true);
            Conffeti.SetActive(true);

             IsFinished = true;

             RunItOnce++;
        }

        if(PuzzleKey4.transform.position.x == -1 && RunItOnce != 1)
        {

            InheritStartTimer.PauseTheTimer = false;

            Congratualtion4.SetActive(true);
            YouWin.SetActive(true);
            Conffeti.SetActive(true);

             IsFinished = true;
            
            RunItOnce++;
        }

         if(PuzzleKey5.transform.position.x == -1 && RunItOnce != 1)
        {

            InheritStartTimer.PauseTheTimer = false;

            Congratualtion5.SetActive(true);
            YouWin.SetActive(true);
            Conffeti.SetActive(true);

             IsFinished = true;
            
            RunItOnce++;
        }

         if(PuzzleKey6.transform.position.x == -1 && RunItOnce != 1)
        {

            InheritStartTimer.PauseTheTimer = false;

            Congratualtion6.SetActive(true);
            YouWin.SetActive(true);
            Conffeti.SetActive(true);

             IsFinished = true;
            
            RunItOnce++;
        }

         if(PuzzleKey7.transform.position.x == -1 && RunItOnce != 1)
        {

            InheritStartTimer.PauseTheTimer = false;

            Congratualtion7.SetActive(true);
            YouWin.SetActive(true);
            Conffeti.SetActive(true);

             IsFinished = true;
            
            RunItOnce++;
        }

         if(PuzzleKey8.transform.position.x == -1 && RunItOnce != 1)
        {

            InheritStartTimer.PauseTheTimer = false;

            Congratualtion8.SetActive(true);
            YouWin.SetActive(true);
            Conffeti.SetActive(true);

             IsFinished = true;
            
            RunItOnce++;
        }

         if(PuzzleKey9.transform.position.x == -1 && RunItOnce != 1)
        {

            InheritStartTimer.PauseTheTimer = false;

            Congratualtion9.SetActive(true);
            YouWin.SetActive(true);
            Conffeti.SetActive(true);

             IsFinished = true;
            
            RunItOnce++;
        }

         if(PuzzleKey10.transform.position.x == -1 && RunItOnce != 1)
        {

            InheritStartTimer.PauseTheTimer = false;

            Congratualtion10.SetActive(true);
            YouWin.SetActive(true);
            Conffeti.SetActive(true);

             IsFinished = true;
            
            RunItOnce++;
        }

         if(PuzzleKey11.transform.position.x == -1 && RunItOnce != 1)
        {

            InheritStartTimer.PauseTheTimer = false;

            Congratualtion11.SetActive(true);
            YouWin.SetActive(true);
            Conffeti.SetActive(true);

             IsFinished = true;
            
            RunItOnce++;
        }

         if(PuzzleKey12.transform.position.x == -1 && RunItOnce != 1)
        {

            InheritStartTimer.PauseTheTimer = false;

            Congratualtion12.SetActive(true);
            YouWin.SetActive(true);
            Conffeti.SetActive(true);

             IsFinished = true;
            
            RunItOnce++;
        }

         if(PuzzleKey13.transform.position.x == -1 && RunItOnce != 1)
        {

            InheritStartTimer.PauseTheTimer = false;

            Congratualtion13.SetActive(true);
            YouWin.SetActive(true);
            Conffeti.SetActive(true);

             IsFinished = true;
            
            RunItOnce++;
        }

         if(PuzzleKey14.transform.position.x == -1 && RunItOnce != 1)
        {

            InheritStartTimer.PauseTheTimer = false;

            Congratualtion14.SetActive(true);
            YouWin.SetActive(true);
            Conffeti.SetActive(true);

             IsFinished = true;
            
            RunItOnce++;
        }

         if(PuzzleKey15.transform.position.x == -1 && RunItOnce != 1)
        {

            InheritStartTimer.PauseTheTimer = false;

            Congratualtion15.SetActive(true);
            YouWin.SetActive(true);
            Conffeti.SetActive(true);

             IsFinished = true;
            
            RunItOnce++;
        }

         if(PuzzleKey16.transform.position.x == -1 && RunItOnce != 1)
        {

            InheritStartTimer.PauseTheTimer = false;

            Congratualtion16.SetActive(true);
            YouWin.SetActive(true);
            Conffeti.SetActive(true);

             IsFinished = true;
            
            RunItOnce++;
        }

         if(PuzzleKey17.transform.position.x == -1 && RunItOnce != 1)
        {

            InheritStartTimer.PauseTheTimer = false;

            Congratualtion17.SetActive(true);
            YouWin.SetActive(true);
            Conffeti.SetActive(true);

             IsFinished = true;
            
            RunItOnce++;
        }

         if(PuzzleKey18.transform.position.x == -1 && RunItOnce != 1)
        {

            InheritStartTimer.PauseTheTimer = false;

            Congratualtion18.SetActive(true);
            YouWin.SetActive(true);
            Conffeti.SetActive(true);

             IsFinished = true;
            
            RunItOnce++;
        }

         if(PuzzleKey19.transform.position.x == -1 && RunItOnce != 1)
        {

            InheritStartTimer.PauseTheTimer = false;

            Congratualtion19.SetActive(true);
            YouWin.SetActive(true);
            Conffeti.SetActive(true);

             IsFinished = true;
            
            RunItOnce++;
        }

         if(PuzzleKey20.transform.position.x == -1 && RunItOnce != 1)
        {

            InheritStartTimer.PauseTheTimer = false;

            Congratualtion20.SetActive(true);
            YouWin.SetActive(true);
            Conffeti.SetActive(true);

             IsFinished = true;
            
            RunItOnce++;
        }
    }



}
