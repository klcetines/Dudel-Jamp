using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_script : MonoBehaviour
{
//    [SerializeField] float jumpForce = 10f;
    private Rigidbody2D player;
    private GameObject lastPLatform;
    public float moveSpeed = 15f;      // Speed of horizontal movement
    private float fallThreshold;
    public Collider2D feet;

    public bool falling = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
           
    }

    // Update is called once per frame
    void Update()
    {
        MoveWithGyroscope();
        WrapAroundScreen();
        falling = CheckIfFalling();
        CheckFall();
    }

    void WrapAroundScreen()
    {
        Vector3 position = transform.position;
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;

        if (position.x > screenWidth)
        {
            position.x = -screenWidth;
        }
        else if (position.x < -screenWidth)
        {
            position.x = screenWidth;
        }

        transform.position = position;
    }

    void CheckFall()
    {
        if (falling)
        {
            fallThreshold += Time.deltaTime;
            if (fallThreshold > 3f)
            {
                Die();
            }
        }
        else
        {
            fallThreshold = 0f;
        }
    }

    bool CheckIfFalling()
    {
        return player.linearVelocity.y <= 0;
    }

    void Die()
    {
        // Handle the player's death (e.g., restart the game, show game over screen, etc.)
        // Example: Reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    void MoveWithGyroscope()
    {
        // Read the device's tilt (accelerometer values)
        float tilt = Input.acceleration.x;

        // Read the arrow key input
        float moveInput = Input.GetAxis("Horizontal");

        // Combine gyroscope and arrow key inputs
        float horizontalMovement = (tilt + moveInput) * moveSpeed * Time.deltaTime;

        // Apply horizontal movement
        Vector3 newPosition = transform.position;
        newPosition.x += horizontalMovement;
        transform.position = newPosition;
    }

    public void Jump(float jumpForce)
    {
        player.linearVelocity = Vector2.up * jumpForce;
    }

}
