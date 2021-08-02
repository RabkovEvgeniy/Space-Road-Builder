using UnityEngine;

public class Rotate : MonoBehaviour
{
    Vector3 rotationAngle; //Rotation angle.
    bool rotationForward; //Rotate forward or rotate left.  True = forward, false = left.

    [Header("Rotation speed")]
    public float speed; //Rotation speed

    void Start() //Setting the rotation angle
    {
        if (transform.parent.transform.localEulerAngles.y == 0) //If obstacle trasform angle Y == 0
        {
            rotationAngle = new Vector3(0, 0, 10); //Set rotation angle 
            rotationForward = true; //This is rotation forward
        }
        else //If obstacle trasform angle Y == -90
        {
            rotationAngle = new Vector3(0, 0, -10); //Set rotation angle 
            rotationForward = false; //This is left rotation
        }
    }

    void Update()
    {
            transform.Rotate(rotationAngle * speed * Time.deltaTime); //Rotate obstacle
    }
}
