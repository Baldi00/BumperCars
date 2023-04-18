using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{

    [System.Serializable]
    private class Coord
    {
        public int x;
        public int y;

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool Equals(Coord other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return x == other.x && y == other.y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != this.GetType()) return false;

            return Equals((Coord)obj);
        }

        public override int GetHashCode()
        {
            return 0;
        }

    }

    [SerializeField]
    private GameObject environmentTilemapPrefab;
    [SerializeField]
    private int tilemapSizeX = 60;
    [SerializeField]
    private int tilemapSizeY = 40;
    [SerializeField]
    private float offsetSpawnerX = 10;
    [SerializeField]
    private float offsetSpawnerY = 7.5f;

    private new Transform camera;
    private Dictionary<Coord, GameObject> tiles;
    private Dictionary<string, Coord> nearCoords;
    private Coord currentTileCoord;
    private Coord cameraPositionNormalized;

    void Awake()
    {
        camera = Camera.main.transform;
        tiles = new Dictionary<Coord, GameObject>();
        currentTileCoord = new Coord(0, 0);
        tiles.Add(currentTileCoord, transform.GetChild(0).gameObject);

        nearCoords = new Dictionary<string, Coord>();
        nearCoords.Add("right", new Coord(currentTileCoord.x + tilemapSizeX, currentTileCoord.y));
        nearCoords.Add("left", new Coord(currentTileCoord.x - tilemapSizeX, currentTileCoord.y));
        nearCoords.Add("up", new Coord(currentTileCoord.x, currentTileCoord.y + tilemapSizeY));
        nearCoords.Add("down", new Coord(currentTileCoord.x, currentTileCoord.y - tilemapSizeY));
        nearCoords.Add("up_right", new Coord(currentTileCoord.x + tilemapSizeX, currentTileCoord.y + tilemapSizeY));
        nearCoords.Add("up_left", new Coord(currentTileCoord.x - tilemapSizeX, currentTileCoord.y + tilemapSizeY));
        nearCoords.Add("down_right", new Coord(currentTileCoord.x + tilemapSizeX, currentTileCoord.y - tilemapSizeY));
        nearCoords.Add("down_left", new Coord(currentTileCoord.x - tilemapSizeX, currentTileCoord.y - tilemapSizeY));
    }

    void Update()
    {
        cameraPositionNormalized = new Coord(Mathf.RoundToInt(camera.position.x / tilemapSizeX) * tilemapSizeX, Mathf.RoundToInt(camera.position.y / tilemapSizeY) * tilemapSizeY);
        if (currentTileCoord != cameraPositionNormalized)
        {
            currentTileCoord = cameraPositionNormalized;
            nearCoords["right"] = new Coord(currentTileCoord.x + tilemapSizeX, currentTileCoord.y);
            nearCoords["left"] = new Coord(currentTileCoord.x - tilemapSizeX, currentTileCoord.y);
            nearCoords["up"] = new Coord(currentTileCoord.x, currentTileCoord.y + tilemapSizeY);
            nearCoords["down"] = new Coord(currentTileCoord.x, currentTileCoord.y - tilemapSizeY);
            nearCoords["up_right"] = new Coord(currentTileCoord.x + tilemapSizeX, currentTileCoord.y + tilemapSizeY);
            nearCoords["up_left"] = new Coord(currentTileCoord.x - tilemapSizeX, currentTileCoord.y + tilemapSizeY);
            nearCoords["down_right"] = new Coord(currentTileCoord.x + tilemapSizeX, currentTileCoord.y - tilemapSizeY);
            nearCoords["down_left"] = new Coord(currentTileCoord.x - tilemapSizeX, currentTileCoord.y - tilemapSizeY);
        }

        if (camera.position.x >= currentTileCoord.x + offsetSpawnerX && !tiles.ContainsKey(nearCoords["right"]))
            tiles.Add(nearCoords["right"], Instantiate(environmentTilemapPrefab, new Vector2(nearCoords["right"].x, nearCoords["right"].y), Quaternion.identity, transform));
        if (camera.position.x <= currentTileCoord.x - offsetSpawnerX && !tiles.ContainsKey(nearCoords["left"]))
            tiles.Add(nearCoords["left"], Instantiate(environmentTilemapPrefab, new Vector2(nearCoords["left"].x, nearCoords["left"].y), Quaternion.identity, transform));
        if (camera.position.y >= currentTileCoord.y + offsetSpawnerY && !tiles.ContainsKey(nearCoords["up"]))
            tiles.Add(nearCoords["up"], Instantiate(environmentTilemapPrefab, new Vector2(nearCoords["up"].x, nearCoords["up"].y), Quaternion.identity, transform));
        if (camera.position.y <= currentTileCoord.y - offsetSpawnerY && !tiles.ContainsKey(nearCoords["down"]))
            tiles.Add(nearCoords["down"], Instantiate(environmentTilemapPrefab, new Vector2(nearCoords["down"].x, nearCoords["down"].y), Quaternion.identity, transform));
        if (camera.position.x >= currentTileCoord.x + offsetSpawnerX && camera.position.y >= currentTileCoord.y + offsetSpawnerY && !tiles.ContainsKey(nearCoords["up_right"]))
            tiles.Add(nearCoords["up_right"], Instantiate(environmentTilemapPrefab, new Vector2(nearCoords["up_right"].x, nearCoords["up_right"].y), Quaternion.identity, transform));
        if (camera.position.x <= currentTileCoord.x - offsetSpawnerX && camera.position.y >= currentTileCoord.y + offsetSpawnerY && !tiles.ContainsKey(nearCoords["up_left"]))
            tiles.Add(nearCoords["up_left"], Instantiate(environmentTilemapPrefab, new Vector2(nearCoords["up_left"].x, nearCoords["up_left"].y), Quaternion.identity, transform));
        if (camera.position.x >= currentTileCoord.x + offsetSpawnerX && camera.position.y <= currentTileCoord.y - offsetSpawnerY && !tiles.ContainsKey(nearCoords["down_right"]))
            tiles.Add(nearCoords["down_right"], Instantiate(environmentTilemapPrefab, new Vector2(nearCoords["down_right"].x, nearCoords["down_right"].y), Quaternion.identity, transform));
        if (camera.position.x <= currentTileCoord.x - offsetSpawnerX && camera.position.y <= currentTileCoord.y - offsetSpawnerY && !tiles.ContainsKey(nearCoords["down_left"]))
            tiles.Add(nearCoords["down_left"], Instantiate(environmentTilemapPrefab, new Vector2(nearCoords["down_left"].x, nearCoords["down_left"].y), Quaternion.identity, transform));

        List<Coord> keysToRemove = new List<Coord>();
        foreach (KeyValuePair<Coord, GameObject> tile in tiles)
            if (!nearCoords.ContainsValue(tile.Key) && !tile.Key.Equals(currentTileCoord))
            {
                Destroy(tile.Value);
                keysToRemove.Add(tile.Key);
            }

        foreach (Coord keyToRemove in keysToRemove)
            tiles.Remove(keyToRemove);
    }
}
