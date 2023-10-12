using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    Button settings;
    bool isPaused = false;

    void Start()
    {
        settings = GetComponent<Button>();
        settings.onClick.AddListener(TogglePause);
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f; 
        }
    }
}
