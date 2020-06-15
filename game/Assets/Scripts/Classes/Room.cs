using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room
{
    public Vector2Int arrayCoordinate;
    public Vector2 instantiationCoordinate;

    public Dictionary<string, Room> neighbors;
    private string[,] population;

    private GameObject gameObject;
    private Transform transform;

    public List<GameObject> createdEnemies;

    private bool closedDoors = false;
    private bool openedDoors = true;

    public Room(int givenX, int givenY, float instantiateX, float instantiateY)
    {
        arrayCoordinate = new Vector2Int(givenX, givenY);
        instantiationCoordinate = new Vector2(instantiateX, instantiateY);

        neighbors = new Dictionary<string, Room>();
        createdEnemies = new List<GameObject>();

        population = new string[9, 9];

        for (int xId = 0; xId < 9; xId++)
        {
            for (int yId = 0; yId < 9; yId++)
            {
                population[xId, yId] = "";
            }
        }
    }

    public Room(Vector2Int givenArrayCoordinate, Vector2 givenInstantiationCoordinate)
    {
        arrayCoordinate = givenArrayCoordinate;
        neighbors = new Dictionary<string, Room>();
        createdEnemies = new List<GameObject>();


        instantiationCoordinate = givenInstantiationCoordinate;

        population = new string[9, 9];

        for (int xId = 0; xId < 9; xId++)
        {
            for (int yId = 0; yId < 9; yId++)
            {
                population[xId, yId] = "";
            }
        }
    }

    public Dictionary<Vector2Int, Vector2> NeighborCoordinates()
    {
        Dictionary<Vector2Int, Vector2> neighborCoordinates = new Dictionary<Vector2Int, Vector2>();
        neighborCoordinates.Add (new Vector2Int(this.arrayCoordinate.x - 1, this.arrayCoordinate.y), new Vector2 (this.instantiationCoordinate.x - 20f, this.instantiationCoordinate.y));
        neighborCoordinates.Add (new Vector2Int(this.arrayCoordinate.x + 1, this.arrayCoordinate.y), new Vector2 (this.instantiationCoordinate.x + 20f, this.instantiationCoordinate.y));
        neighborCoordinates.Add (new Vector2Int(this.arrayCoordinate.x, this.arrayCoordinate.y + 1), new Vector2 (this.instantiationCoordinate.x, this.instantiationCoordinate.y + 20f));
        neighborCoordinates.Add (new Vector2Int(this.arrayCoordinate.x, this.arrayCoordinate.y - 1), new Vector2 (this.instantiationCoordinate.x, this.instantiationCoordinate.y - 20f));

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

    public void PlaceEnemies(int amount)
    {
        for (int enemyId = 0; enemyId < amount; enemyId++)
        {
            List<Vector2Int> area = FindFreeArea(new Vector2Int(1, 1));

            population[area[0].x, area[0].y] = "Slime";
        }
    }

    public void PlaceHatch()
    {
        population[(int)Mathf.Ceil(population.GetLength(0) / 2), (int)Mathf.Ceil(population.GetLength(1) / 2)] = "Hatch";
    }

    private List<Vector2Int> FindFreeArea(Vector2Int size)
    {
        List<Vector2Int> area = new List<Vector2Int>();

        do
        {
            area.Clear();

            Vector2Int center = new Vector2Int(Random.Range(1 + 2, 9 - 2), Random.Range(1 + 2, 9 - 2));

            area.Add(center);

            int initialX = (center.x - (int)Mathf.Floor(size.x / 2));
            int initialY = (center.y - (int)Mathf.Floor(size.y / 2));

            for (int x = initialX; x < initialX + size.x; x++)
            {
                for (int y = initialY; y < initialY + size.y; y++)
                {
                    area.Add(new Vector2Int(x, y));
                }
            }
        } while (!IsFree(area));
        return area;
    }

    private bool IsFree (List<Vector2Int> area)
    {
        foreach (Vector2Int tile in area)
        {
            if (population[tile.x, tile.y] != "")
                return false;
        }

        return true;
    }

    public List<GameObject> SpawnPopulation(GameObject enemy)
    {
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                if (population[x, y] == "Slime")
                {
                    GameObject prefab = GameObject.Instantiate(enemy);
                    prefab.transform.position = new Vector2((float)x + instantiationCoordinate.x - 4 + 0.5f, (float)y + instantiationCoordinate.y - 4 - 0.5f);
                    createdEnemies.Add(prefab);
                }

                if (population[x, y] == "Hatch")
                {
                    GameObject prefab = (GameObject)Resources.Load("Prefabs/Doors/Hatch");
                    GameObject hatch = Object.Instantiate(prefab);
                    hatch.transform.parent = transform;
                    hatch.transform.localPosition = new Vector2(0.5f, -0.5f);
                }
            }
        }
        return createdEnemies;
    }

    public void SetGameObject(Transform trans)
    {
        gameObject = trans.gameObject;
        transform = trans;
    }

    public void EnableEnemies()
    {
        Enemy[] scripts = gameObject.GetComponentsInChildren<Enemy>();

        foreach (Enemy script in scripts)
        {
            script.enabled = true;
        }
    }

    public void DisableEnemies()
    {
        Enemy[] scripts = gameObject.GetComponentsInChildren<Enemy>();

        foreach (Enemy script in scripts)
        {
            script.enabled = false;
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void ManageDoors()
    {
        Enemy[] enemies = gameObject.GetComponentsInChildren<Enemy>();
        Door[] doors = gameObject.GetComponentsInChildren<Door>();

        if (!closedDoors && enemies.Length > 0)
        {

            foreach (Door door in doors)
            {
                door.GetComponent<Animator>().SetTrigger("Close");
                door.Close();
            }

            closedDoors = true;
            openedDoors = false;
            //Debug.Log("Doors closed.");
        }
        else if (!openedDoors && enemies.Length == 0)
        {
            foreach (Door door in doors)
            {
                door.GetComponent<Animator>().SetTrigger("Open");
                door.Open();
            }
            openedDoors = true;
            closedDoors = false;

            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().ClearRoom();
            //Debug.Log("Doors opened.");
        }
    }
}

