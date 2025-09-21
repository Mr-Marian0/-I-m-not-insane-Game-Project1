using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPositions : MonoBehaviour
{
    [SerializeField]int HowManyBlocks;

    Vector3 Block1;
    Vector3 Block2;
    Vector3 Block3;
    Vector3 Block4;
    Vector3 Block5;
    Vector3 Block6;
    Vector3 Block7;
    Vector3 Block8;
    Vector3 Block9;
    Vector3 Block10;
    Vector3 Block11;
    Vector3 Block12;
    Vector3 Block13;
    Vector3 Block14;

    public GameObject positionBlock1;
    public GameObject positionBlock2;
    public GameObject positionBlock3;
    public GameObject positionBlock4;
    public GameObject positionBlock5;
    public GameObject positionBlock6;
    public GameObject positionBlock7;
    public GameObject positionBlock8;
    public GameObject positionBlock9;
    public GameObject positionBlock10;
    public GameObject positionBlock11;
    public GameObject positionBlock12;
    public GameObject positionBlock13;
    public GameObject positionBlock14;
    void Start()
    {

        if(HowManyBlocks == 6)
        {
            Block1 = positionBlock1.transform.position;
            Block2 = positionBlock2.transform.position;
            Block3 = positionBlock3.transform.position;
            Block4 = positionBlock4.transform.position;
            Block5 = positionBlock5.transform.position;
            Block6 = positionBlock6.transform.position;
        }
        else if (HowManyBlocks == 7)
        {
            Block1 = positionBlock1.transform.position;
            Block2 = positionBlock2.transform.position;
            Block3 = positionBlock3.transform.position;
            Block4 = positionBlock4.transform.position;
            Block5 = positionBlock5.transform.position;
            Block6 = positionBlock6.transform.position;
            Block7 = positionBlock7.transform.position;
        }
        else if (HowManyBlocks == 8)
        {
            Block1 = positionBlock1.transform.position;
            Block2 = positionBlock2.transform.position;
            Block3 = positionBlock3.transform.position;
            Block4 = positionBlock4.transform.position;
            Block5 = positionBlock5.transform.position;
            Block6 = positionBlock6.transform.position;
            Block7 = positionBlock7.transform.position;
            Block8 = positionBlock8.transform.position;
        }
         else if (HowManyBlocks == 7)
        {
            Block1 = positionBlock1.transform.position;
            Block2 = positionBlock2.transform.position;
            Block3 = positionBlock3.transform.position;
            Block4 = positionBlock4.transform.position;
            Block5 = positionBlock5.transform.position;
            Block6 = positionBlock6.transform.position;
            Block7 = positionBlock7.transform.position;
        }
        else if (HowManyBlocks == 9)
        {
            Block1 = positionBlock1.transform.position;
            Block2 = positionBlock2.transform.position;
            Block3 = positionBlock3.transform.position;
            Block4 = positionBlock4.transform.position;
            Block5 = positionBlock5.transform.position;
            Block6 = positionBlock6.transform.position;
            Block7 = positionBlock7.transform.position;
            Block8 = positionBlock8.transform.position;
            Block9 = positionBlock9.transform.position;
        }
        else if (HowManyBlocks == 11)
        {
            Block1 = positionBlock1.transform.position;
            Block2 = positionBlock2.transform.position;
            Block3 = positionBlock3.transform.position;
            Block4 = positionBlock4.transform.position;
            Block5 = positionBlock5.transform.position;
            Block6 = positionBlock6.transform.position;
            Block7 = positionBlock7.transform.position;
            Block8 = positionBlock8.transform.position;
            Block9 = positionBlock9.transform.position;
            Block10 = positionBlock10.transform.position;
            Block11 = positionBlock11.transform.position;
        }
        else if (HowManyBlocks == 10)
        {
            Block1 = positionBlock1.transform.position;
            Block2 = positionBlock2.transform.position;
            Block3 = positionBlock3.transform.position;
            Block4 = positionBlock4.transform.position;
            Block5 = positionBlock5.transform.position;
            Block6 = positionBlock6.transform.position;
            Block7 = positionBlock7.transform.position;
            Block8 = positionBlock8.transform.position;
            Block9 = positionBlock9.transform.position;
            Block10 = positionBlock10.transform.position;
        }
        else if (HowManyBlocks == 12)
        {
            Block1 = positionBlock1.transform.position;
            Block2 = positionBlock2.transform.position;
            Block3 = positionBlock3.transform.position;
            Block4 = positionBlock4.transform.position;
            Block5 = positionBlock5.transform.position;
            Block6 = positionBlock6.transform.position;
            Block7 = positionBlock7.transform.position;
            Block8 = positionBlock8.transform.position;
            Block9 = positionBlock9.transform.position;
            Block10 = positionBlock10.transform.position;
            Block11 = positionBlock11.transform.position;
            Block12 = positionBlock12.transform.position;
        }
        else if (HowManyBlocks == 13)
        {
            Block1 = positionBlock1.transform.position;
            Block2 = positionBlock2.transform.position;
            Block3 = positionBlock3.transform.position;
            Block4 = positionBlock4.transform.position;
            Block5 = positionBlock5.transform.position;
            Block6 = positionBlock6.transform.position;
            Block7 = positionBlock7.transform.position;
            Block8 = positionBlock8.transform.position;
            Block9 = positionBlock9.transform.position;
            Block10 = positionBlock10.transform.position;
            Block11 = positionBlock11.transform.position;
            Block12 = positionBlock12.transform.position;
            Block13 = positionBlock13.transform.position;
        }
        else if (HowManyBlocks == 14)
        {
            Block1 = positionBlock1.transform.position;
            Block2 = positionBlock2.transform.position;
            Block3 = positionBlock3.transform.position;
            Block4 = positionBlock4.transform.position;
            Block5 = positionBlock5.transform.position;
            Block6 = positionBlock6.transform.position;
            Block7 = positionBlock7.transform.position;
            Block8 = positionBlock8.transform.position;
            Block9 = positionBlock9.transform.position;
            Block10 = positionBlock10.transform.position;
            Block11 = positionBlock11.transform.position;
            Block12 = positionBlock12.transform.position;
            Block13 = positionBlock13.transform.position;
            Block14 = positionBlock14.transform.position;
        }
        
    }

    private void OnDisable()
    {

        if (HowManyBlocks == 6)
        {
            positionBlock1.transform.position = Block1;
            positionBlock2.transform.position = Block2;
            positionBlock3.transform.position = Block3;
            positionBlock4.transform.position = Block4;
            positionBlock5.transform.position = Block5;
            positionBlock6.transform.position = Block6;
        }
        else if (HowManyBlocks == 8)
        {
            positionBlock1.transform.position = Block1;
            positionBlock2.transform.position = Block2;
            positionBlock3.transform.position = Block3;
            positionBlock4.transform.position = Block4;
            positionBlock5.transform.position = Block5;
            positionBlock6.transform.position = Block6;
            positionBlock7.transform.position = Block7;
            positionBlock8.transform.position = Block8;
        }
         else if (HowManyBlocks == 7)
        {
            positionBlock1.transform.position = Block1;
            positionBlock2.transform.position = Block2;
            positionBlock3.transform.position = Block3;
            positionBlock4.transform.position = Block4;
            positionBlock5.transform.position = Block5;
            positionBlock6.transform.position = Block6;
            positionBlock7.transform.position = Block7;
        }
        else if (HowManyBlocks == 9)
        {
            positionBlock1.transform.position = Block1;
            positionBlock2.transform.position = Block2;
            positionBlock3.transform.position = Block3;
            positionBlock4.transform.position = Block4;
            positionBlock5.transform.position = Block5;
            positionBlock6.transform.position = Block6;
            positionBlock7.transform.position = Block7;
            positionBlock8.transform.position = Block8;
            positionBlock9.transform.position = Block9;
        }
        else if (HowManyBlocks == 11)
        {
            positionBlock1.transform.position = Block1;
            positionBlock2.transform.position = Block2;
            positionBlock3.transform.position = Block3;
            positionBlock4.transform.position = Block4;
            positionBlock5.transform.position = Block5;
            positionBlock6.transform.position = Block6;
            positionBlock7.transform.position = Block7;
            positionBlock8.transform.position = Block8;
            positionBlock9.transform.position = Block9;
            positionBlock10.transform.position = Block10;
            positionBlock11.transform.position = Block11;
        }
        else if (HowManyBlocks == 10)
        {
            positionBlock1.transform.position = Block1;
            positionBlock2.transform.position = Block2;
            positionBlock3.transform.position = Block3;
            positionBlock4.transform.position = Block4;
            positionBlock5.transform.position = Block5;
            positionBlock6.transform.position = Block6;
            positionBlock7.transform.position = Block7;
            positionBlock8.transform.position = Block8;
            positionBlock9.transform.position = Block9;
            positionBlock10.transform.position = Block10;
        }
        else if (HowManyBlocks == 12)
        {
            positionBlock1.transform.position = Block1;
            positionBlock2.transform.position = Block2;
            positionBlock3.transform.position = Block3;
            positionBlock4.transform.position = Block4;
            positionBlock5.transform.position = Block5;
            positionBlock6.transform.position = Block6;
            positionBlock7.transform.position = Block7;
            positionBlock8.transform.position = Block8;
            positionBlock9.transform.position = Block9;
            positionBlock10.transform.position = Block10;
            positionBlock11.transform.position = Block11;
            positionBlock12.transform.position = Block12;
        }
        else if (HowManyBlocks == 13)
        {
            positionBlock1.transform.position = Block1;
            positionBlock2.transform.position = Block2;
            positionBlock3.transform.position = Block3;
            positionBlock4.transform.position = Block4;
            positionBlock5.transform.position = Block5;
            positionBlock6.transform.position = Block6;
            positionBlock7.transform.position = Block7;
            positionBlock8.transform.position = Block8;
            positionBlock9.transform.position = Block9;
            positionBlock10.transform.position = Block10;
            positionBlock11.transform.position = Block11;
            positionBlock12.transform.position = Block12;
            positionBlock13.transform.position = Block13;
        }
        else if (HowManyBlocks == 7)
        {
            positionBlock1.transform.position = Block1;
            positionBlock2.transform.position = Block2;
            positionBlock3.transform.position = Block3;
            positionBlock4.transform.position = Block4;
            positionBlock5.transform.position = Block5;
            positionBlock6.transform.position = Block6;
            positionBlock7.transform.position = Block7;
        }
        else if (HowManyBlocks == 14)
        {
            positionBlock1.transform.position = Block1;
            positionBlock2.transform.position = Block2;
            positionBlock3.transform.position = Block3;
            positionBlock4.transform.position = Block4;
            positionBlock5.transform.position = Block5;
            positionBlock6.transform.position = Block6;
            positionBlock7.transform.position = Block7;
            positionBlock8.transform.position = Block8;
            positionBlock9.transform.position = Block9;
            positionBlock10.transform.position = Block10;
            positionBlock11.transform.position = Block11;
            positionBlock12.transform.position = Block12;
            positionBlock13.transform.position = Block13;
            positionBlock14.transform.position = Block14;
        }
    }

}
