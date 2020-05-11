using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int doorDir;
    /*
    1 --> Left
    2 --> Top
    3 --> Right
    4 --> Bottom
    */

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    [Space]
    [Header("Generation Position Offsets:")]
    private Vector3 rightOffset = new Vector3(-0.5f, -0.5f, 0);

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("RoomTemplates").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }


    private void Spawn()
    {
        if (!spawned)
        {

            if (doorDir == 1)
            {
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position + rightOffset, templates.rightRooms[rand].transform.rotation);
            }

            if (doorDir == 2)
            {
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position + rightOffset, templates.bottomRooms[rand].transform.rotation);
            }

            if (doorDir == 3)
            {
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position + rightOffset, templates.leftRooms[rand].transform.rotation);
            }

            if (doorDir == 4)
            {
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position + rightOffset, templates.topRooms[rand].transform.rotation);
            }

        }
        spawned = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint"))
        {
            Destroy(gameObject);
        }
    }
}
