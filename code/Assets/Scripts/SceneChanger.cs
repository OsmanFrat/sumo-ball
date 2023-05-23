using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene("SumoArena1");
        Time.timeScale = 1.0f;
    }


    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

}
