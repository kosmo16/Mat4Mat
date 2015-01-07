using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        // Make a background box
        GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 90, 200, 180), "Main Menu");

        // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
        if (GUI.Button(new Rect(Screen.width / 2 - 100 + 20, Screen.height / 2 - 55, 160, 60), "Play"))
        {
            Application.LoadLevel("LevelMenu");
        }

        // Make the second button.
        if (GUI.Button(new Rect(Screen.width / 2 - 100 + 20, Screen.height / 2 + 15, 160, 60), "Quit"))
        {
            Application.Quit();
        }
    }
}
