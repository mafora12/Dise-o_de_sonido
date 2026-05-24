using UnityEngine;
using FMODUnity;

public class KartMusicController : MonoBehaviour
{
    public StudioEventEmitter musicEmitter;
    public int totalCheckpoints = 3;
    private int checkpointsPassed = 0;
    private bool lastLap = false;

    public void CheckpointPassed()
    {
        checkpointsPassed++;
        float intensity = (float)checkpointsPassed / totalCheckpoints * 10f;
        musicEmitter.SetParameter("Intensity", intensity);
    }

    public void LastLap()
    {
        lastLap = true;
        musicEmitter.SetParameter("Intensity", 10f);
    }
}