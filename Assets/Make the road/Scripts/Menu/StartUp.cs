using UnityEngine;

public class StartUp : MonoBehaviour
{
    [Header("Game mode")]
    public bool TestingMode; //Testing Mode on\off
    [Range(1,40)]
    public int LevelIdForTest; //Level id which you want to test

    void Awake() //Save the basic settings
    {
        if (PlayerPrefs.GetInt("DataSetted") == 1) //If the game has already started, just set the temporary values ​​to 0
        {
            //Set all changable player prefs to defaut
            PlayerPrefs.SetInt("IsStart", 0);
            PlayerPrefs.SetInt("Direction", 0);
            PlayerPrefs.SetInt("Deads", 0);

            PlayerPrefs.SetFloat("PlayerX", 0);
            PlayerPrefs.SetFloat("PlayerZ", -2);

            PlayerPrefs.SetInt("xScore", 0);
            PlayerPrefs.SetInt("LevelComplete", 0);

            if (TestingMode)
            {
                PlayerPrefs.SetInt("levelId", LevelIdForTest);
            }
        }
        else //If this is the first start, save the default settings
        {
            //Set ALL to defalut
            PlayerPrefs.SetInt("IsStart", 0);

            PlayerPrefs.SetInt("levelId", 1);
            PlayerPrefs.SetInt("Score", 0);

            PlayerPrefs.SetInt("Direction", 0);

            PlayerPrefs.SetInt("Deads", 0);
            PlayerPrefs.SetInt("Dead", 0);

            PlayerPrefs.SetInt("gamesPlayed", 0);

            PlayerPrefs.SetInt("DataSetted", 1);

            PlayerPrefs.SetInt("SpawnOn", 1);

            PlayerPrefs.SetFloat("PlayerX", 0);
            PlayerPrefs.SetFloat("PlayerZ", -2);

            PlayerPrefs.SetInt("xScore", 0);
            PlayerPrefs.SetInt("LevelComplete", 0);

            PlayerPrefs.SetInt("Music", 1);
            PlayerPrefs.SetInt("Vibration", 1);

            if (TestingMode)
            {
                PlayerPrefs.SetInt("levelId", LevelIdForTest);
            }

        }
    }
}
