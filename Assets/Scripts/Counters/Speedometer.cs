using System.Collections;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    public float Speed;

    private void Start()
    {
        StartCoroutine(CalcSpeed());
    }
    IEnumerator CalcSpeed()
    {
        bool isPlaying = true;

        while (isPlaying)
        {
            Vector3 presPos = transform.position;
            yield return new WaitForFixedUpdate();
            Speed = Mathf.RoundToInt(Vector3.Distance(transform.position, presPos) / Time.fixedDeltaTime);
        }
    }
}
