using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GridGenerator : MonoBehaviour
{
    public GameObject powerupPrefab;
    public GameObject wallPrefab;
    public GameObject dotPrefab;
    public GameObject playerPrefab;
    public GameObject waypointPrefab;
    public GameObject chasePrefab;

    public PlayerChase playerChase;
    public WaypointFollower waypointFollower;
   

    List<Transform> waypointList = new List<Transform>();
    Dictionary<int, Transform> waypointDict = new Dictionary<int, Transform>(); 

    public int dotCount;

    string[] levelData = {
        "##########",
        "#P#..#.#U#",
        "#.#0..1..#",
        "#...#..#.#",
        "###.##.#.#",
        "#..3..2#.#",
        "#.#..#...#",   
        "#.##.#.#.#",
        "#....#..E#",
        "##########"
    };

    void Start()
    {
        dotCount = 0;
        GenerateLevel();
    }

    void GenerateLevel()
    {
        //cleared arrays voor restarts
        waypointDict.Clear();
        waypointList.Clear();

        for (int y = 0; y < levelData.Length; y++)
        {
            string row = levelData[y];
            for (int x = 0; x < row.Length; x++)
            {
                char tile = row[x];
                Vector3 position = new Vector3(x, -y, 0);

                //gebruikt nummers voor waypoints zodat de enemy ze in order kan volgen
                if (char.IsDigit(tile))
                {
                    int index = tile - '0';

                    Instantiate(dotPrefab, position, Quaternion.identity);

                    GameObject waypoint = Instantiate(waypointPrefab, position, Quaternion.identity);
                    waypointDict[index] = waypoint.transform;
                }

                switch (tile)
                {
                    case '#':
                        Instantiate(wallPrefab, position, Quaternion.identity);
                        
                        break;

                    case '.':
                        Instantiate(dotPrefab, position, Quaternion.identity);
                        dotCount++;
                        break;

                    case 'E':
                        Instantiate(dotPrefab, position, Quaternion.identity);
                        Instantiate(chasePrefab, position, Quaternion.identity);

                        break;

                    case 'P':
                        GameObject playerInstance = Instantiate(playerPrefab, position, Quaternion.identity);

                        playerChase.player = playerInstance.transform;
                        break;

                    case 'U':
                        Instantiate(powerupPrefab, position, Quaternion.identity);

                        break;


                }
            }
            
        }
        
        //ze worden in de lijst gezet
        List<Transform> orderedWaypoints = new List<Transform>();

        for (int i = 0; i < waypointDict.Count; i++)
        {
            orderedWaypoints.Add(waypointDict[i]);
        }

        waypointFollower.waypoints = orderedWaypoints.ToArray();
    }
    
}