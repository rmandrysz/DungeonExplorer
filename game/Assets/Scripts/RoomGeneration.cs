﻿using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;

public class RoomGeneration : MonoBehaviour
{

    private static RoomGeneration instance;


    private int numberOfRooms;
    private Room[,] rooms;
    private Room currentRoom;

    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
            numberOfRooms = Random.Range(8, 13);
            //Debug.Log(numberOfRooms);
            GenerateDungeon();
            GameObject player = (GameObject)Instantiate(Resources.Load("Prefabs/Player"));
        }
        else
        {

            Input.ResetInputAxes();
            Destroy (this.gameObject);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(numberOfRooms);
        GenerateDungeon();
        SpawnRooms();
        //PrintGrid();
    }

    private Room GenerateDungeon()
    {
        int gridSize = 3 * numberOfRooms;
        rooms = new Room[gridSize, gridSize];

        Vector2Int firstRoomCoord = new Vector2Int((gridSize / 2) - 1, (gridSize / 2) - 1);
        Vector2 firstRoomInstantiation = new Vector2(-0.5f, 0);
        Queue<Room> roomsToCreate = new Queue<Room>();
        List<Room> createdRooms = new List<Room>();


        roomsToCreate.Enqueue(new Room(firstRoomCoord, firstRoomInstantiation));

        while (roomsToCreate.Count > 0 && createdRooms.Count < numberOfRooms)
        {
            Room currentRoom = roomsToCreate.Dequeue();

            rooms[currentRoom.arrayCoordinate.x, currentRoom.arrayCoordinate.y] = currentRoom;
            createdRooms.Add(currentRoom);
            AddNeighbors(currentRoom, roomsToCreate);
        }

        foreach (Room room in createdRooms)
        {
            Dictionary<Vector2Int, Vector2> neighborCoordinates = room.NeighborCoordinates();

            foreach (KeyValuePair<Vector2Int, Vector2> coordinates in neighborCoordinates)
            {
                Room neighbor = rooms[coordinates.Key.x, coordinates.Key.y];

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
        Dictionary<Vector2Int, Vector2> neighborCoordinates = currentRoom.NeighborCoordinates();
        Dictionary<Vector2Int, Vector2> availableNeighbors = new Dictionary<Vector2Int, Vector2>();

        foreach (KeyValuePair<Vector2Int, Vector2> coordinates in neighborCoordinates)
        {
            if (rooms[coordinates.Key.x, coordinates.Key.y] == null)
            {
                availableNeighbors.Add(coordinates.Key, coordinates.Value);
            }
        }

        int numberOfNeighbors = (int)Random.Range(1, availableNeighbors.Count);

        for (int neighbor = 0; neighbor < numberOfNeighbors; neighbor++)
        {
            float random = Random.value;
            float roomFrac = 1.0f / (float)availableNeighbors.Count;

            Vector2Int chosenNeighborCoordinate = new Vector2Int(0, 0);
            Vector2 chosenNeighborInstantiation = new Vector2(0, 0);

            foreach (KeyValuePair<Vector2Int, Vector2> coordinates in availableNeighbors)
            {
                if (random < roomFrac)
                {
                    chosenNeighborCoordinate = coordinates.Key;
                    chosenNeighborInstantiation = coordinates.Value;
                    break;
                }
                else
                {
                    roomFrac += 1.0f / (float)availableNeighbors.Count;
                }
            }

            roomsToCreate.Enqueue(new Room(chosenNeighborCoordinate, chosenNeighborInstantiation));
            availableNeighbors.Remove(chosenNeighborCoordinate);
        }
    }

    private void PrintGrid()
    {
        for (int rowIndex = 0; rowIndex < rooms.GetLength(1); rowIndex++)
        {
            string row = "";

            for (int columnIndex = 0; columnIndex < rooms.GetLength(0); columnIndex++)
            {
                if (rooms[columnIndex, rowIndex] == null)
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

    private void SpawnRooms()
    {
        for(int rowIndex = 0; rowIndex < rooms.GetLength(1); rowIndex++)
        {

            for (int columnIndex = 0; columnIndex < rooms.GetLength(0); columnIndex++)
            {
                if (rooms[columnIndex, rowIndex] != null)
                {
                    string roomPrefabName = instance.rooms[columnIndex, rowIndex].PrefabName();
                    GameObject roomObject = (GameObject)Instantiate(Resources.Load("Prefabs/Rooms/" + roomPrefabName), rooms[columnIndex, rowIndex].instantiationCoordinate, Quaternion.identity);
                }
            }
        }
    }

    public Room CurrentRoom()
    {
        return currentRoom;
    }

    public void MoveToRoom(Room targetRoom)
    {
        currentRoom = targetRoom;
    }

}