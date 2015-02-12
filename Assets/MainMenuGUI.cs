using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPlay()
    {
        Application.LoadLevel("LevelMenu");
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
