using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Vector2Int roomCoordinate;
    public Dictionary<string, Room> neighbors;

    public Room(int givenX, int givenY)
    {
        this.roomCoordinate = new Vector2Int(givenX, givenY);
        this.neighbors = new Dictionary<string, Room>();
    }

    public Room(Vector2Int givenRoomCoordinate)
    {
        this.roomCoordinate = givenRoomCoordinate;
        this.neighbors = new Dictionary<string, Room>();
    }

    public List<Vector2Int> NeighborCoordinates()
    {
        List<Vector2Int> neighborCoordinates = new List<Vector2Int>();
        if(roomCoordinate.x != 0)
            neighborCoordinates.Add (new Vector2Int(this.roomCoordinate.x - 1, this.roomCoordinate.y));
        neighborCoordinates.Add (new Vector2Int(this.roomCoordinate.x, this.roomCoordinate.y + 1));
        neighborCoordinates.Add (new Vector2Int(this.roomCoordinate.x + 1, this.roomCoordinate.y));
        if(roomCoordinate.y != 0)
            neighborCoordinates.Add (new Vector2Int(this.roomCoordinate.x, this.roomCoordinate.y - 1));

        return neighborCoordinates;
    }

    public void Connect(Room neighbor)
    {
        string dir;

        if(neighbor.roomCoordinate.x < this.roomCoordinate.x)
        {
            dir = "L";
        }
        else if (neighbor.roomCoordinate.y > this.roomCoordinate.y)
        {
            dir = "T";
        }
        else if (neighbor.roomCoordinate.x > this.roomCoordinate.x)
        {
            dir = "R";
        }
        else
        {
            dir = "B";
        }

        this.neighbors.Add (dir, neighbor);
    }
}
