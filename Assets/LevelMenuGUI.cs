﻿using UnityEngine;
using System.Collections;

public class LevelMenuGUI : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        // Make a background box
        GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 180, 200, 400), "Level Menu");

        // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
        if (GUI.Button(new Rect(Screen.width / 2 - 100 + 20, Screen.height / 2 - 125, 160, 60), "Level 1"))
        {
            Application.LoadLevel("scene0");
        }

        // Make the second button.
        if (GUI.Button(new Rect(Screen.width / 2 - 100 + 20, Screen.height / 2 - 55, 160, 60), "Level 2"))
        {
            Application.LoadLevel("scene0rock");
        }

		if (GUI.Button(new Rect(Screen.width / 2 - 100 + 20, Screen.height / 2 + 15, 160, 60), "Level 3"))
		{
			Application.LoadLevel("scene1rock");
		}

		if (GUI.Button(new Rect(Screen.width / 2 - 100 + 20, Screen.height / 2 + 85, 160, 60), "Level 4"))
		{
			Application.LoadLevel("scene1gum");
		}

		if (GUI.Button(new Rect(Screen.width / 2 - 100 + 20, Screen.height / 2 + 155, 160, 60), "Wstecz"))
		{
			Application.LoadLevel("mainMenu");
		}
    }
}
