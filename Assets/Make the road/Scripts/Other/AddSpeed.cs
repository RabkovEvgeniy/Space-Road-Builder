using UnityEngine;

public class AddSpeed : MonoBehaviour
{
    [Header("Block spawn speed")]
    public float spawnerTime = 0.2f; //Block spawn time when the player is at the finish. Every new block will be spawn after this time.

    [Header("Player speed")]
    public float playerSpeed = 8; //Player speed on the finish

    private void OnTriggerEnter(Collider other) //Increasing the spawn speed at the finish
    {
        if (other.tag == "Player") //If collision gameobject.tag == "Player", set new speed
        {
            PlayerPrefs.SetFloat("Speed", playerSpeed);
            PlayerPrefs.SetFloat("spawnerTime", spawnerTime);
        }
    }
}
