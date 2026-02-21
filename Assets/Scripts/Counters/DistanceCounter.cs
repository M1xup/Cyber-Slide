using UnityEngine;
using TMPro;

public class DistanceCounter : MonoBehaviour
{
    private Vector3 oldPos;
    private float totalDistance = 0.0f;
    public float AllDistance = 0.0f;
    [SerializeField] private TMP_Text _distanceCounter;
    
 
    void Start()
    {
        oldPos = transform.position;
    }
 
    void Update()
    {
        totalDistance += (transform.position - oldPos).magnitude;
        AllDistance += (transform.position - oldPos).magnitude;
        oldPos = transform.position;
 
        if (totalDistance > 1.0f)
        {
            // speedMove += (int)totalDistance * 0.0001f;
            //totalDistance -= (int)totalDistance;
        }

        _distanceCounter.text = Mathf.RoundToInt(AllDistance).ToString() + "м";
    }
}
