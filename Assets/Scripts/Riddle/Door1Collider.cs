using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1Collider : MonoBehaviour
{

    public SpriteRenderer DefaultRoom;
    public Sprite CloseTheDoor;
    public Sprite Door1Open;

    //Used to tell the CheckAnswer(Script) the location of the player
    public bool PlayerChooseDoor1 = false;

    //Enable the button
    public GameObject Door1Button;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChangeSprite();
        Door1Button.SetActive(true);

        PlayerChooseDoor1 = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        BackToDefault();
        Door1Button.SetActive(false);

        PlayerChooseDoor1 = false;
    }

    void ChangeSprite()
    {
        DefaultRoom.sprite = Door1Open;
    }

    void BackToDefault()
    {
        DefaultRoom.sprite = CloseTheDoor;
    }

}
