using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;


public class ResourcesCollected : MonoBehaviour
{
    public static int stone, water, money;
    public static int dirt = 10;

 

    public void addResource()
    {
        
    }

    public static void SetTile(Vector3 worldPosition, MeshData data, Mesh mesh)
    {
        int x, y;
        Tile.Type tempTile, newTile;
        GetXY(worldPosition, out x, out y);
        tempTile = World.instance.tiles[x, y].type;


        if (tempTile == Tile.Type.Castle && dirt >= 50)
        {
            Debug.Log("UPGRADE!! ");
            dirt -= 50;
        }

        if (tempTile != Tile.Type.Castle && dirt >= 10)
        {
            World.instance.tiles[x, y].SetTile(Tile.Type.Castle);
            data.CreateSquare(x, y);
            mesh.vertices = data.vertices.ToArray();
            mesh.triangles = data.triangles.ToArray();
            mesh.uv = data.UVs.ToArray();
            newTile = World.instance.tiles[x, y].type;
            dirt -= 10;

            if (tempTile == Tile.Type.Grass && newTile == Tile.Type.Castle)
            {
                tick(Tile.Type.Grass);
            }
            if (tempTile == Tile.Type.Stone && newTile == Tile.Type.Castle)
            {
                tick(Tile.Type.Stone);
            }
            if (tempTile == Tile.Type.Water && newTile == Tile.Type.Castle)
            {
                tick(Tile.Type.Water);
            }
        }
        


    }

    static void tick(Tile.Type tile)
    {

        if (Tile.Type.Grass == tile)
        {

            World.instance.InvokeRepeating("DirtRes", 0.0f, 3.0f);
            
        }
        else if (Tile.Type.Stone == tile)
        {

            World.instance.InvokeRepeating("StoneRes", 0.0f, 3.0f);

        }
        else if (Tile.Type.Water == tile)
        {

            World.instance.InvokeRepeating("WaterRes", 0.0f, 3.0f);

        }


    }

    public static void SetDirt()
    {
        dirt++;
        Debug.Log(dirt);
    }
    public static void SetStone()
    {
        stone++;
        Debug.Log(stone);
    }
    public static void SetWater()
    {
        water++;
        Debug.Log(water);
    }

    private static void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x);
        y = Mathf.FloorToInt(worldPosition.y);
    }


}
