using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door3Collider : MonoBehaviour
{

    public SpriteRenderer DefaultRoom;
    public Sprite CloseTheDoor;
    public Sprite Door3Open;

    //Used to tell the CheckAnswer(Script) the location of the player
    public bool PlayerChooseDoor3 = false;

    //Enable the button
    public GameObject Door3Button;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChangeSprite();
        Door3Button.SetActive(true);

        PlayerChooseDoor3 = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        BackToDefault();
        Door3Button.SetActive(false);

        PlayerChooseDoor3 = false;
    }

    void ChangeSprite()
    {
        DefaultRoom.sprite = Door3Open;
    }

    void BackToDefault()
    {
        DefaultRoom.sprite = CloseTheDoor;
    }

}
