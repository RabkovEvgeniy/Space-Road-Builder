using UnityEngine.SceneManagement;
using UnityEngine;

public class DontOnDestroy : MonoBehaviour
{
    int levelId; //Level id value

    void Start() //Load the game level into the menu scene.
    {
        levelId = PlayerPrefs.GetInt("levelId"); //Getting level id

        DontDestroyOnLoad(gameObject); //Add to DontDestroyOnLoad
        SceneManager.LoadScene(levelId); //Load level scene
    }

}
