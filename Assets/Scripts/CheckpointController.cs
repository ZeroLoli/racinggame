using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointController : MonoBehaviour {

    private int nextCPNo, CPCount, lapCount;
    private float lapStartTime;
    private List<float> lapTimes = new List<float>();
    private Checkpoint activeCP;

    public bool isGameActive;
    public Text CPInfoText, LapTimeText, RaceOverText;
    public int lapsInRace;

    // Use this for initialization
    void Start() {
        isGameActive = true;
        lapStartTime = Time.time;
        nextCPNo = 0;
        lapCount = 0;
        RaceOverText.text = "";
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
        if (isGameActive)
        {
            LapTimeText.text = TimeParser(Time.time - lapStartTime);
            CPInfoText.text = ("CHECKPOINT " + nextCPNo + " / " + CPCount + "\nLAP " + (lapCount + 1) + " / " + lapsInRace);
        }
        else
        {
            LapTimeText.text = "";
            CPInfoText.text = "";
            RaceOverText.color = Color.HSVToRGB(Mathf.Abs(Mathf.Sin(Time.time)), 1, 1);
        }
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
            // If final lap, end the game and display race information
            else {
                isGameActive = false;
                float raceTotalTime = 0.0f;
                float fastestLapTime = 99999f;
                for (int i = 0; i < lapsInRace; i++) {
                    Debug.Log("Laptimes: " + lapTimes[i]);
                    if (lapTimes[i] < fastestLapTime)
                    {
                        fastestLapTime = lapTimes[i];
                    }
                    raceTotalTime += lapTimes[i];
                }

                RaceOverText.text = "RACE COMPLETE!\n\nTotal Time:" + TimeParser(raceTotalTime) + "\nBest Lap: " + TimeParser(fastestLapTime);
            }
        }
    }

    private string TimeParser(float time)
    {
        float minutes = Mathf.Floor((time) / 60);
        float seconds = Mathf.Floor((time) % 60);
        float msecs = Mathf.Floor(((time) * 100) % 100);
        return (minutes.ToString() + ":" + seconds.ToString("00") + ":" + msecs.ToString("00"));
    }
}