using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float timeHolder;
    public float timescaleIncrease;
    public float changeTime;
    public float repeatTime;

    // Use this for initialization
    void Awake ()
    {
        timeHolder = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Linearly increases the timeScale
        if (Time.time >= timeHolder + repeatTime + changeTime)
        {
            StartCoroutine(LerpTime(Time.timeScale + timescaleIncrease, changeTime));
            timeHolder = Time.time;
        }

        //Debug.Log(Time.timeScale);
    }

    // Lerps between the desired timeScale amount (lerpTimeTo) by the amount of time (timeToTake) for it to take action
    IEnumerator LerpTime(float lerptimeTo, float timetoTake)
    {
        float endTime = Time.time + timetoTake;
        float starttimeScale = Time.timeScale;
        float i = 0.0f;
        while (Time.time < endTime)
        {
            i += (1 / timetoTake) * Time.deltaTime;
            Time.timeScale = Mathf.Lerp(starttimeScale, lerptimeTo, i);
            //print(Time.timeScale);
            yield return null;
        }
        Time.timeScale = lerptimeTo;
    }
}
