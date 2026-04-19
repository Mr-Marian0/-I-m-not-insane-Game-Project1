using UnityEngine;

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
            // Move in a straight line continuously
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}