using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProjectileMovement : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;
    private bool isInitialized = false;
    
    // Call this to set up the projectile
    public void Initialize(Vector2 direction, float speed)
    {
        moveDirection = direction;
        moveSpeed = speed;
        isInitialized = true;
    }
    
    void Update()
    {
        if (isInitialized)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
{
    if (other.gameObject.name == "RealPlayerHitBox")
    {
        GameObject player = other.transform.parent.gameObject;
        player.GetComponent<PlayerHitEffect>().FlashRed(0.05f);
        
        Slider slider = FindObjectOfType<Slider>();
        if (slider != null)
        {
            slider.value += 1;
        }
        
        Destroy(gameObject);
    }
}

}