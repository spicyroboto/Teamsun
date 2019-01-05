using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform target;
    private float xOffset = 5;


    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x + xOffset, transform.position.y, transform.position.z);
    }

}
