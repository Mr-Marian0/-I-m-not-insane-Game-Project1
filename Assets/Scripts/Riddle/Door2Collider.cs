using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2Collider : MonoBehaviour
{

    public SpriteRenderer DefaultRoom;
    public Sprite CloseTheDoor;
    public Sprite Door2Open;

    //Used to tell the CheckAnswer(Script) the location of the player
    public bool PlayerChooseDoor2 = false;

    //Enable the button
    public GameObject Door2Button;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChangeSprite();
        Door2Button.SetActive(true);

        PlayerChooseDoor2 = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        BackToDefault();
        Door2Button.SetActive(false);

        PlayerChooseDoor2 = false;
    }

    void ChangeSprite()
    {
        DefaultRoom.sprite = Door2Open;
    }

    void BackToDefault()
    {
        DefaultRoom.sprite = CloseTheDoor;
    }

}
