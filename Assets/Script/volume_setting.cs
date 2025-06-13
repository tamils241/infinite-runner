using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volume_setting : MonoBehaviour
{
     public Slider volumeSlider; // Reference to the UI slider for volume control
     public AudioSource audioSource; // Reference to the AudioSource you want to control

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial volume level based on the slider value
        audioSource.volume = volumeSlider.value;

        // Add a listener to the slider's value change event
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChanged(); }); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnVolumeChanged()
    {
        // Update the audio source's volume when the slider value changes
        audioSource.volume = volumeSlider.value;
    }
}
