using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    public GameObject floorsText;
    public GameObject roomsText;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void Update()
    {
        floorsText.GetComponent<TextMeshProUGUI>().text = "FLOORS PASSED: " + gameManager.GetFloorsPassed();
        roomsText.GetComponent<TextMeshProUGUI>().text = "ROOMS CLEARED: " + gameManager.GetRoomsCleared();
    }
}
