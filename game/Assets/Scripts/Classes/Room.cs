using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Vector2Int arrayCoordinate;
    public Dictionary<string, Room> neighbors;
    public Vector2 instantiationCoordinate;

    public Room(int givenX, int givenY, float instantiateX, float instantiateY)
    {
        arrayCoordinate = new Vector2Int(givenX, givenY);
        instantiationCoordinate = new Vector2(instantiateX, instantiateY);

        neighbors = new Dictionary<string, Room>();
    }

    public Room(Vector2Int givenArrayCoordinate, Vector2 givenInstantiationCoordinate)
    {
        arrayCoordinate = givenArrayCoordinate;
        neighbors = new Dictionary<string, Room>();

        instantiationCoordinate = givenInstantiationCoordinate;
    }

    public Dictionary<Vector2Int, Vector2> NeighborCoordinates()
    {
        Dictionary<Vector2Int, Vector2> neighborCoordinates = new Dictionary<Vector2Int, Vector2>();
        neighborCoordinates.Add (new Vector2Int(this.arrayCoordinate.x - 1, this.arrayCoordinate.y), new Vector2 (this.instantiationCoordinate.x - 15f, this.instantiationCoordinate.y));
        neighborCoordinates.Add (new Vector2Int(this.arrayCoordinate.x + 1, this.arrayCoordinate.y), new Vector2 (this.instantiationCoordinate.x + 15f, this.instantiationCoordinate.y));
        neighborCoordinates.Add (new Vector2Int(this.arrayCoordinate.x, this.arrayCoordinate.y + 1), new Vector2 (this.instantiationCoordinate.x, this.instantiationCoordinate.y + 15f));
        neighborCoordinates.Add (new Vector2Int(this.arrayCoordinate.x, this.arrayCoordinate.y - 1), new Vector2 (this.instantiationCoordinate.x, this.instantiationCoordinate.y - 15f));

        return neighborCoordinates;
    }

    public void Connect(Room neighbor)
    {
        string dir;

        if(neighbor.arrayCoordinate.x < this.arrayCoordinate.x)
        {
            dir = "L";
        }
        else if (neighbor.arrayCoordinate.x > this.arrayCoordinate.x)
        {
            dir = "R";
        }
        else if (neighbor.arrayCoordinate.y > this.arrayCoordinate.y)
        {
            dir = "T";
        }
        else
        {
            dir = "B";
        }

        this.neighbors.Add (dir, neighbor);
    }

    public string PrefabName()
    {
        string name = "Room_";

        foreach(KeyValuePair<string, Room> neighborPair in neighbors)
        {
            name += neighborPair.Key;
        }

        return name;
    }

    public Room Neighbor(string direction)
    {
        return neighbors[direction];
    }
}
