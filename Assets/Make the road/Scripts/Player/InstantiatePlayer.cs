using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePlayer : MonoBehaviour
{
    [Header("Player prefab")]
    public GameObject playerPrefab;
    [Header("Player start local position")]
    public Vector3 startPosition;

    void Awake()
    {
        GameObject player = Instantiate(playerPrefab); //Instantiate player
        player.transform.parent = gameObject.transform; //Add parent to player
        player.transform.localPosition = startPosition; //Set position for player
    }
}
