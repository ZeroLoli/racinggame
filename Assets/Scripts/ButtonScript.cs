using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

    public void ChangeScene(int sceneNo) {
        SceneManager.LoadScene(sceneNo);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
