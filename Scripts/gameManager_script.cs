using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameManager_script : MonoBehaviour
{
    public TextMeshProUGUI heightText;
    public GameObject player;
    float topPlayerY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > topPlayerY)
        {
            topPlayerY = player.transform.position.y;
            heightText.text = player.transform.position.y.ToString("F1") + " m";  
        }
    }
}
