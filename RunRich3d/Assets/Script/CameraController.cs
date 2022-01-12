
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    
    public Vector3 offset;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
