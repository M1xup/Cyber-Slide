using UnityEngine;

public class RotateAnimation : MonoBehaviour
{ 
    [SerializeField] private float speed = 0; // скорость
    
    void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime, Space.Self); // вращение лопастей
    }    
}
