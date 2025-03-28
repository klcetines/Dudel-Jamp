using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feet_script : MonoBehaviour
{
    public player_script player;  // Reference to the main player script
    //private GameObject lastPLatform = null;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(player.falling)
        {
            // Check if the collider belongs to a platform
            if (collision.gameObject.CompareTag("Platform"))
            {
                player.Jump(10f);
                //lastPLatform = collision.gameObject;
            }
            else if (collision.gameObject.CompareTag("DeusablePlatform"))
            {
                player.Jump(15f);
                Rigidbody2D platformRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (platformRb != null)
                {
                    platformRb.isKinematic = false; // Make the platform affected by physics
                    platformRb.AddForce(Vector2.down * 5f, ForceMode2D.Impulse); // Apply a downward force
                }
            }
            else if (collision.gameObject.CompareTag("BoostPlatform"))
            {
                player.Jump(30f);
            }
        }
    }
}
