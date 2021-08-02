using UnityEngine;

public class ChangeDirection : MonoBehaviour
{
    [Header("New player direction; 0 - forward, 1 - left")]
    [Range(0,1)]
    public int SetDirection; //Set new direction number

    private void OnTriggerEnter(Collider other) //In case of collision, change the direction of movement to the specified SetDirection
    {
        if (other.tag == "Player") //If collision gameobject.tag == "Player"
        {
            PlayerPrefs.SetInt("Direction", SetDirection); //Set new direction
            Destroy(gameObject); //Destory this trigger
        }
    }
}
