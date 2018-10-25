using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeScene(int sceneNo) {
        SceneManager.LoadScene(sceneNo);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
