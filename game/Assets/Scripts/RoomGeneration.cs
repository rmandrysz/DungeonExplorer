using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;
using UnityEngine;

public class RoomGeneration : MonoBehaviour
{

    public GameObject roomPrefab;


    private int numberOfRooms;
    private Room[,] rooms;
    private Room currentRoom;



    // Start is called before the first frame update
    void Start()
    {
        numberOfRooms = Random.Range(8, 13);
        Debug.Log(numberOfRooms);
        currentRoom = GenerateDungeon();
        PrintGrid();
    }

    private Room GenerateDungeon()
    {
        int gridSize = 3 * numberOfRooms;
        rooms = new Room[gridSize, gridSize];

        Vector2Int firstRoomCoord = new Vector2Int((gridSize / 2) - 1, (gridSize / 2) - 1);
        Queue<Room> roomsToCreate = new Queue<Room>();
        List<Room> createdRooms = new List<Room>();


        roomsToCreate.Enqueue(new Room(firstRoomCoord));

        while(roomsToCreate.Count > 0 && createdRooms.Count < numberOfRooms)
        {
            Room currentRoom = roomsToCreate.Dequeue();

            rooms[currentRoom.roomCoordinate.x, currentRoom.roomCoordinate.y] = currentRoom;
            createdRooms.Add(currentRoom);
            AddNeighbors(currentRoom, roomsToCreate);
        }

        foreach(Room room in createdRooms)
        {
            List<Vector2Int> neighborCoordinates = room.NeighborCoordinates();

            foreach(Vector2Int coordinate in neighborCoordinates)
            {
                Room neighbor = rooms[coordinate.x, coordinate.y];

                if (neighbor != null)
                {
                    room.Connect(neighbor);
                }
            }
        }

        return rooms[firstRoomCoord.x, firstRoomCoord.y];
    }

    private void AddNeighbors(Room currentRoom, Queue<Room> roomsToCreate)
    {
        List<Vector2Int> neighborCoordinates = currentRoom.NeighborCoordinates();
        List<Vector2Int> availableNeighbors = new List<Vector2Int>();

        foreach(Vector2Int coordinate in neighborCoordinates)
        {
            if (rooms[coordinate.x, coordinate.y] == null)
            {
                availableNeighbors.Add(coordinate);
            }
        }

        int numberOfNeighbors = (int)Random.Range(1, availableNeighbors.Count);

        for(int neighbor = 0; neighbor < numberOfNeighbors; neighbor++)
        {
            float random = Random.value;
            float roomFrac = 1.0f / (float)availableNeighbors.Count;

            Vector2Int chosenNeighbor = new Vector2Int(0, 0);

            foreach(Vector2Int coordinate in availableNeighbors)
            {
                if (random < roomFrac)
                {
                    chosenNeighbor = coordinate;
                    break;
                }
                else
                {
                    roomFrac += 1.0f / (float)availableNeighbors.Count;
                }
            }

            roomsToCreate.Enqueue(new Room(chosenNeighbor));
            availableNeighbors.Remove(chosenNeighbor);
        }
    }

    private void PrintGrid()
    {
        for(int rowIndex = 0; rowIndex < rooms.GetLength(1); rowIndex++)
        {
            string row = "";

            for(int columnIndex = 0; columnIndex < rooms.GetLength(0); columnIndex++)
            {
                if(rooms[columnIndex, rowIndex] == null)
                {
                    row += "X";
                }
                else
                {
                    row += "R";
                }
            }

            Debug.Log(row);
        }

    }
}
