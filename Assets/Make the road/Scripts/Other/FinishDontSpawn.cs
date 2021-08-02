using UnityEngine;

public class FinishDontSpawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") //If collision gameobject.tag == "Player"
        {
            PlayerPrefs.SetInt("SpawnOn", 0); //Set player can't spawn blocks. This is necessary so that the player, having reached the end of the finish line, stops spawning blocks.
        }
    }
}
