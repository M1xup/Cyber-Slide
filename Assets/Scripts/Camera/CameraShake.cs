using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private Speedometer speedometer;
    [SerializeField] private AnimationCurve magnitudeCurve;
    [SerializeField] private float _delayTime = 0.5f;
    private float magnitude;
    public bool CanShake = true;

    private void Update()
    {
        if (speedometer.Speed > 20 && CanShake)
        {
            StartCoroutine(Shake());
        }
    }

    public IEnumerator Shake ()
    {
        Vector3 originalPos = transform.localPosition;
        magnitude = magnitudeCurve.Evaluate(speedometer.Speed);

        while (speedometer.Speed != 0)
        {
            float x = Random.Range(-0.6f, 0.6f) * magnitude;
            float y = Random.Range(-0.6f, 0.6f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            yield return new WaitForSeconds(_delayTime);
        }

        transform.localPosition = originalPos;
    }
}
