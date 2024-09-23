using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    // Reference to the AudioMixer to control the game's audio
    public AudioMixer audioMixer;

    // Dropdown to display and select screen resolutions
    public TMP_Dropdown resolutionDropdown;

    // Array to hold available screen resolutions
    Resolution[] resolutions;

    void Start()
    {
        // Get all available screen resolutions for the current display
        resolutions = Screen.resolutions;

        // Clear any existing options in the dropdown
        resolutionDropdown.ClearOptions();

        // Create a list to store resolution options as strings
        List<string> options = new List<string>();

        // Store the index of the current resolution
        int currentResolutionIndex = 0;

        // Loop through each available resolution
        for (int i = 0; i < resolutions.Length; i++)
        {
            // Create a formatted string for each resolution, including refresh rate
            string option = resolutions[i].width + " x " + resolutions[i].height + ", " + resolutions[i].refreshRate + "Hz";
            options.Add(option);  // Add the option to the list

            // Check if this resolution is the same as the current screen resolution
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                // If it's the current resolution, store its index
                currentResolutionIndex = i;
            }
        }

        // Add the resolution options to the dropdown
        resolutionDropdown.AddOptions(options);
        // Set the dropdown to show the current resolution
        resolutionDropdown.value = currentResolutionIndex;
        // Refresh the dropdown to reflect the changes
        resolutionDropdown.RefreshShownValue();
    }

    // Method to change the screen resolution based on the selected index
    public void SetResolution(int resolutionIndex)
    {
        // Get the selected resolution from the list
        Resolution resolution = resolutions[resolutionIndex];
        // Set the screen resolution and fullscreen state
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // Method to adjust the volume using the AudioMixer
    public void SetVolume(float volume)
    {
        // Set the volume level in the audio mixer (using a parameter called "volume")
        audioMixer.SetFloat("volume", volume);
    }

    // Method to toggle fullscreen mode
    public void SetFullscreen(bool isFullscreen)
    {
        // Set the fullscreen state based on the checkbox value
        Screen.fullScreen = isFullscreen;
    }
}
