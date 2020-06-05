using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{

    public string doorPosition;

    public Collider2D externalCollider;
    public Collider2D internalCollider;

    // Start is called before the first frame update
    void Start()
    {
        externalCollider.enabled = false;
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Door Entered");

            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().UpdateTarget(doorPosition);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().MoveThroughDoor(doorPosition);
            
        }
    }
}
