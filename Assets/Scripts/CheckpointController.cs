using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointController : MonoBehaviour {

    private int nextCPNo, CPCount, lapCount;
    private float lapStartTime;
    private List<float> lapTimes = new List<float>();
    private Checkpoint activeCP;

    public bool gameOver = false;
    public Text CPInfoText, LapTimeText;
    public int lapsInRace;

    // Use this for initialization
    void Start() {
        lapStartTime = Time.time;
        nextCPNo = 0;
        lapCount = 0;
        // Initialize the checkpoints
        CPCount = this.transform.childCount;
        for (int i = 0; i < CPCount; i++) {
            Checkpoint cp = transform.GetChild(i).GetComponent<Checkpoint>();
            cp.checkpointNo = i;
        }
        activeCP = transform.GetChild(nextCPNo).GetComponent<Checkpoint>();
        activeCP.isNextCP = true;
    }

    // Update is called once per frame
    void Update() {
        float minutes = Mathf.Floor((Time.time - lapStartTime) / 60);
        float seconds = Mathf.Floor((Time.time - lapStartTime) % 60);
        float msecs = Mathf.Floor(((Time.time - lapStartTime) * 100) % 100);
        LapTimeText.text = (minutes.ToString() + ":" + seconds.ToString("00") + ":" + msecs.ToString("00"));
        CPInfoText.text = ("CHECKPOINT " + nextCPNo + " / " + CPCount + "\nLAP " + (lapCount + 1) + " / " + lapsInRace);
    }

    public void CPIncrease() {
        // A checkpoint was passed, so we make it inactive and activate the next one.
        activeCP.isNextCP = false;
        nextCPNo++;
        if (nextCPNo < CPCount) {
            activeCP = transform.GetChild(nextCPNo).GetComponent<Checkpoint>();
            activeCP.isNextCP = true;
        }
        // If a lap was finished, we enter the new lap, and the checkpoint-counter is reset
        else {
            // Add the laptime to the list of laptimes
            lapTimes.Add(Time.time - lapStartTime);
            lapCount++;
            // Reset the lap timer
            lapStartTime = Time.time;
            nextCPNo = 0;
            // Check whether the finished lap was the final one or not
            if (lapCount < lapsInRace) {
                activeCP = transform.GetChild(nextCPNo).GetComponent<Checkpoint>();
                activeCP.isNextCP = true;
            }
            else {
                gameOver = true;
            }
        }
    }
}