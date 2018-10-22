using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointController : MonoBehaviour
{

    private int nextCP, CPCount, lapCount;
    private float lapStart;
    private List<float> lapTimes = new List<float>();
    private Checkpoint currCP;

    public bool gameOver = false;
    public Text CPInfoText, LapTimeText;
    public int maxLaps;

    // Use this for initialization
    void Start()
    {
        lapStart = Time.time;
        nextCP = 0;
        lapCount = 0;
        // Initialize the checkpoints
        CPCount = this.transform.childCount;
        for (int i = 0; i < CPCount; i++) {
            Checkpoint cp = transform.GetChild(i).GetComponent<Checkpoint>();
            cp.checkpointNo = i;
        }
        currCP = transform.GetChild(nextCP).GetComponent<Checkpoint>();
        currCP.currCP = true;
    }

    // Update is called once per frame
    void Update() {
        float minutes = Mathf.Floor((Time.time - lapStart) / 60);
        float seconds = Mathf.Floor((Time.time - lapStart) % 60);
        float msecs = Mathf.Floor(((Time.time - lapStart) * 100) % 100);
        LapTimeText.text = (minutes.ToString() + ":" + seconds.ToString("00") + ":" + msecs.ToString("00"));
        CPInfoText.text = ("CHECKPOINT " + nextCP + " / " + CPCount + "\nLAP " + lapCount + " / " + maxLaps);
    }

    public void CPIncrease()
    {
        // A checkpoint was passed, so we make it inactive and activate the next one.
        currCP.currCP = false;
        nextCP++;
        if (nextCP < CPCount) {
            currCP = transform.GetChild(nextCP).GetComponent<Checkpoint>();
            currCP.currCP = true;
        }
        // If a lap was finished, we enter the new lap, and the checkpoint-counter is reset
        else {
            lapTimes.Add(Time.time - lapStart);
            lapCount++;
            lapStart = Time.time;
            nextCP = 0;
            // Check whether the finished lap was the final one or not
            if (lapCount < maxLaps) {
                currCP = transform.GetChild(nextCP).GetComponent<Checkpoint>();
                currCP.currCP = true;
            }
            else {
                gameOver = true;
            }
        }
    }
}