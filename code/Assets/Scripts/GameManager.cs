using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
        
    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Enemy")
        {
            score+= 10;
            Debug.Log("Score = " + score);
        }
    }
}
