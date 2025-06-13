using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Button : MonoBehaviour
{
   
    public AudioSource audioSource;   // Reference to the AudioSource component
    public AudioClip buttonSound;     // The sound clip you want to play
     //Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //play button script 
     public void play_Button(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
        audioSource.PlayOneShot(buttonSound);
    }

        // exit button script  
    public void Quit_Button()
    {
         Application.Quit();
         audioSource.PlayOneShot(buttonSound);
    }
}
