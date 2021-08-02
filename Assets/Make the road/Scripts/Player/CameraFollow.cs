using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform player; //Player transform
    int Direction; //Player walk direction
    Vector3 offset; //Camera offset

    [Header("Offset of the camera if player run forward")]
    public Vector3 forwardOffset = new Vector3(5, 4, -8);
    [Header("Offset of the camera if player run left")]
    public Vector3 leftOffset = new Vector3(8, 4, 5);

    void Start() //Get the direction of movement of the player
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //Get player transform
        Direction = PlayerPrefs.GetInt("Direction"); //Get direction value
        ChangeDirection(); //Set direction
    }
    void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("Direction") != Direction) //If the direction of the player has changed, run the method
        {
            Direction = PlayerPrefs.GetInt("Direction"); //Get direction
            if (PlayerPrefs.GetInt("IsStart") == 0)
            {
                ChangeDirection();
            }
        }
        if (PlayerPrefs.GetInt("Direction") == 0)//Depending on the direction of the player, we change the direction of the camera
        {
           offset = forwardOffset; //Change camera offset
           ChangeDirection(); //Rotate cam fast
           transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) + offset; //Set camera position == player position + camera offset
        }
        else
        {
           offset = leftOffset;
           ChangeDirection();//Rotate fast
           transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) + offset; //Set camera position == player position + camera offset
        }
    }

    void ChangeDirection()//Changing the camera angle
    {
        if (Direction == 0)
        {
            transform.eulerAngles = new Vector3(15, -30, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(15, -120, 0);
        }
    }
}
