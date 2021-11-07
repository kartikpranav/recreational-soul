using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridBehavior : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating("SlowUpdate", 0.0f, 0.05f);
    }
    public Tilemap tileMap;
    public Tile whiteTile;
    public BoundsInt area;

    // Update is called once per frame
    void Update()
    {
        //t.ClearAllTiles();
        //t.SetTile(new Vector3Int(0, 0, 0), whiteTile);
    }

    private void SlowUpdate()
    {
        List<Vector3Int> alivePos = new List<Vector3Int>();
        for (int x = area.xMin; x < area.xMax; x++)
        {
            for (int y = area.yMin; y < area.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                int gridCount = countAlive9Grid(x, y);
                if (gridCount == 3 || (gridCount == 4 && tileMap.GetTile(pos) != null))
                    alivePos.Add(pos);
            }
        }

        tileMap.ClearAllTiles();
        TileBase[] tiles = new TileBase[alivePos.Count];
        for (int i = 0; i < tiles.Length; i++) tiles[i] = whiteTile;
        tileMap.SetTiles(alivePos.ToArray(), tiles);
    }

    private void setTile(Vector3Int pos)
    {
        if (tileMap.HasTile(pos))
        {
            tileMap.SetTile(pos, null);
        }
        else
        {
            tileMap.SetTile(pos, whiteTile);
        }
    }

    private int countAlive9Grid(int x, int y)
    {
        int count = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                TileBase current = tileMap.GetTile(new Vector3Int(x + i, y + j, 0));
                if (current != null) count++;
            }
        }
        return count;
    }
}
