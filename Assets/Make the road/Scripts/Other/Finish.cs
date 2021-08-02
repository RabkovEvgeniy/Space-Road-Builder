using UnityEngine;

public class Finish : MonoBehaviour
{
    [Header("Finish x Score")]
    public int xScore; //Finish xScore, x1, x2, etc.
    int levelId; //Level id now

    void Start()
    {
        levelId = PlayerPrefs.GetInt("levelId"); //Getting level id
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") //If player collision with finish
        {
            if (levelId == PlayerPrefs.GetInt("levelId")) //If level id match up
            {
                PlayerPrefs.SetInt("IsStart", 0); //Stop game
                PlayerPrefs.SetInt("xScore", xScore); //Set xScore
                PlayerPrefs.SetInt("LevelComplete", 1); //Finishing level and open finish panel in menu
            }
        }
    }
}
