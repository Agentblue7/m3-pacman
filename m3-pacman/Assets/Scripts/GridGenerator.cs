using TMPro;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject dotPrefab;
    public GameObject playerPrefab;

    public int dotCount;

    string[] levelData = {
        "##########",
        "#.#..#.#.#",
        "#.#......#",
        "#...#..#.#",
        "###.##.#.#",
        "#......#.#",
        "#.#..#...#",
        "#.##.#.#.#",
        "#....#...#",
        "##########"
    };

    void Start()
    {
        dotCount = 0;
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int y = 0; y < levelData.Length; y++)
        {
            string row = levelData[y];
            for (int x = 0; x < row.Length; x++)
            {
                char tile = row[x];
                Vector3 position = new Vector3(x, -y, 0);

                switch (tile)
                {
                    case '#':
                        Instantiate(wallPrefab, position, Quaternion.identity);
                        break;

                    case '.':
                        Instantiate(dotPrefab, position, Quaternion.identity);
                        dotCount++;
                        break;

                    case 'P':
                        Instantiate(playerPrefab, position, Quaternion.identity);
                        break;
                }
            }
        }
    }
}