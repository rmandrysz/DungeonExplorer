using UnityEngine;

public class EnterDoor : Door
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

            ChangeRoom();
            
        }
    }

    private void ChangeRoom()
    {
        PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().UpdateTarget(doorPosition);
        playerController.MoveThroughDoor(doorPosition);

        RoomGeneration roomGeneration = GameObject.FindGameObjectWithTag("Dungeon").GetComponent<RoomGeneration>();

        Room currentRoom = roomGeneration.GetRoom (playerController.GetRoomCoords());

        if(currentRoom != null)
            currentRoom.DisableEnemies();

        playerController.ChangeRoom(doorPosition);

        currentRoom.Neighbor(doorPosition).EnableEnemies();
    }

    public override void Close()
    {
        externalCollider.enabled = true;
    }

    public override void Open()
    {
        externalCollider.enabled = false;
    }
}
