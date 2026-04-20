using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileScripts : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int minProjectilesPerSpawn = 2;
    [SerializeField] private int maxProjectilesPerSpawn = 4;
    [SerializeField] private float minSpawnDelay = 1f;
    [SerializeField] private float maxSpawnDelay = 3f;
    [SerializeField] private float projectileLifetime = 4f;
    
    [Header("Movement Settings")]
    [SerializeField] private float minSpeed = 2f;
    [SerializeField] private float maxSpeed = 8f;
    
    [Header("Spawn Boundaries")]
    [SerializeField] private float leftBoundary = -9f;
    [SerializeField] private float rightBoundary = 9.32f;
    [SerializeField] private float topBoundary = 5.45f;
    [SerializeField] private float bottomBoundary = -5f;

    [Header("Object References")]
    public Slider StressBarReference;

    private Transform playerTransform;
    private bool isSpawning = false; // Start as false, only spawn when stress >= 40
    private Coroutine spawnCoroutine;

    void Start()
    {
        // Find the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure your player has the 'Player' tag.");
        }
    }
    
    void Update()
    {
        // Check stress level and control spawning
        if (StressBarReference != null)
        {
            if (StressBarReference.value >= 40f && !isSpawning)
            {
                // Stress is high enough and not spawning - start spawning
                StartSpawning();
            }
            else if (StressBarReference.value < 40f && isSpawning)
            {
                // Stress is too low and currently spawning - stop spawning
                StopSpawning();
            }
        }
    }
    
    // Main spawn loop - runs forever while active
    private IEnumerator SpawnLoop()
    {
        while (isSpawning)
        {
            // Wait random time between 1-3 seconds
            float waitTime = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(waitTime);
            
            // Only spawn if stress is still high enough
            if (StressBarReference != null && StressBarReference.value >= 40f)
            {
                SpawnProjectileBatch();
            }
        }
    }
    
    // Spawns 2-4 projectiles
    private void SpawnProjectileBatch()
    {
        int projectilesToSpawn = Random.Range(minProjectilesPerSpawn, maxProjectilesPerSpawn + 1);
        
        for (int i = 0; i < projectilesToSpawn; i++)
        {
            SpawnSingleProjectile();
        }
    }
    
    // Spawns a single projectile at a valid spawn point
    private void SpawnSingleProjectile()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile prefab not assigned!");
            return;
        }
        
        if (playerTransform == null)
        {
            Debug.LogError("Player transform missing!");
            return;
        }
        
        // Get valid spawn position (outside the forbidden X range)
        Vector3 spawnPosition = GetValidSpawnPosition();
        
        // Create the projectile
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        
        // Get or add ProjectileMovement component
        ProjectileMovement movement = projectile.GetComponent<ProjectileMovement>();
        if (movement == null)
        {
            movement = projectile.AddComponent<ProjectileMovement>();
        }
        
        // Initialize the projectile
        InitializeProjectile(projectile, movement);
    }
    
    // Returns a position outside the forbidden X range (-9 to 9.32)
    private Vector3 GetValidSpawnPosition()
    {
        float x;
        
        // Randomly choose left side (x < -9) or right side (x > 9.32)
        bool spawnLeft = Random.value < 0.5f;
        
        if (spawnLeft)
        {
            // Spawn on left side: x from -infinity to -9 (using -15 to -9.1 for practical range)
            x = Random.Range(-15f, leftBoundary - 0.1f);
        }
        else
        {
            // Spawn on right side: x from 9.32 to infinity (using 9.42 to 15 for practical range)
            x = Random.Range(rightBoundary + 0.1f, 15f);
        }
        
        // Random Y within the full allowed range (5.45 to -5)
        float y = Random.Range(bottomBoundary, topBoundary);
        
        return new Vector3(x, y, 0f);
    }
    
    // Sets up the projectile's movement and lifetime
    private void InitializeProjectile(GameObject projectile, ProjectileMovement movement)
    {
        // Random speed between min and max
        float speed = Random.Range(minSpeed, maxSpeed);
        
        // Calculate direction towards player's current position
        Vector2 direction = (playerTransform.position - projectile.transform.position).normalized;
        
        // Set up the movement
        movement.Initialize(direction, speed);
        
        // Destroy after lifetime seconds
        Destroy(projectile, projectileLifetime);
    }
    
    // Public method to stop spawning
    public void StopSpawning()
    {
        if (isSpawning)
        {
            isSpawning = false;
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
                spawnCoroutine = null;
            }
        }
    }
    
    // Public method to start spawning
    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            spawnCoroutine = StartCoroutine(SpawnLoop());
        }
    }
}