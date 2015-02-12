using UnityEngine;
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

    public void OnLevel1()
    {
        Application.LoadLevel("scene0");
    }

    public void OnLevel2()
    {
        Application.LoadLevel("scene0rock");
    }

    public void OnLevel3()
    {
        Application.LoadLevel("scene1rock");
    }

    public void OnLevel4()
    {
        Application.LoadLevel("scene2");
    }

    public void OnLevel5()
    {
        Application.LoadLevel("scene1gum");
    }

    public void OnLevel6()
    {
        Application.LoadLevel("scene0electric");
    }

    public void OnBack()
    {
        Application.LoadLevel("mainMenu");
    }
}
