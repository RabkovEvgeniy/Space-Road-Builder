using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(PlayerController))]
public class CustomPlayerController : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PlayerController playerCont = (PlayerController)target;

        if (GUILayout.Button("Reset to standard")) //If the button was pressed, restore the default values
        {
            playerCont.speed = 3.5f;
            playerCont.maxLives = 1;
        }
        if (GUI.changed) //Saving changes
        {
            EditorUtility.SetDirty(playerCont);
            EditorSceneManager.MarkSceneDirty(playerCont.gameObject.scene);
        }
    }
}
