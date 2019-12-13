using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class World : MonoBehaviour {

	public static World instance;

	public Material material;

	public int width;
	public int height;

	public Tile[,] tiles;

	public string seed;
	public bool randomSeed;

	public float frequency;
	public float amplitude;

	public float lacunarity;
	public float persistance;

	public int octaves;

	public float seaLevel;

	public float beachStartHeight;
	public float beachEndHeight;

	public float grassStartHeight;
	public float grassEndHeight;

	public float dirtStartHeight;
	public float dirtEndHeight;

	public float stoneStartHeight;
	public float stoneEndHeight;


	public float mountainStartHeight;

    public MeshData data;
    public Mesh mesh;
    public int money = 0;


    Noise noise;

    private Grid grid;



	// Use this for initialization
	void Awake () {

		instance = this;

		

		if (randomSeed == true) {

			int value = Random.Range (-10000, 10000);
			seed = value.ToString ();
		}

		noise = new Noise (seed.GetHashCode (), frequency, amplitude, lacunarity, persistance, octaves);
        
    }

	void Start () {

		CreateTiles ();
		SubdivideTilesArray ();
		SubdivideMountainArray ();
        //grid = new Grid(width, height, 1f);

        //Debug.Log("DETTE ER EN TSET " + tiles[5, 5].type);





    }
	
	// Update is called once per frame
	void Update () {
        
        
        if (Input.GetMouseButton(0))
        {
            //grid.SetValue(UtilsClass.GetMouseWorldPosition(), 56);
            //SetTile(UtilsClass.GetMouseWorldPosition());
            ResourcesCollected.SetTile(UtilsClass.GetMouseWorldPosition(), data, mesh);

        }

      }

    void DirtRes()
    {
        ResourcesCollected.SetDirt();
    }
    void StoneRes()
    {
        ResourcesCollected.SetStone();
    }
    void WaterRes()
    {
        ResourcesCollected.SetWater();
    }


    void CreateTiles () {

        tiles = new Tile[width, height];

        float[,] noiseValues = noise.GetNoiseValues (width, height);

		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				tiles [i, j] = MakeTileAtHeight (noiseValues [i, j]);
            }
		}
        
	}

	Tile MakeTileAtHeight (float currentHeight) {

		if (currentHeight <= seaLevel)
			return new Tile (Tile.Type.Water);

		if (currentHeight >= beachStartHeight && currentHeight <= beachEndHeight)
			return new Tile (Tile.Type.Sand);

		if (currentHeight >= grassStartHeight && currentHeight <= grassEndHeight)
			return new Tile (Tile.Type.Grass);

		if (currentHeight >= dirtStartHeight && currentHeight <= dirtEndHeight)
			return new Tile (Tile.Type.Dirt);

		if (currentHeight >= stoneStartHeight && currentHeight <= stoneEndHeight) {

			if (currentHeight >= mountainStartHeight)
				return new Tile (Tile.Type.Stone, Tile.Wall.Brick);

			return new Tile (Tile.Type.Stone);

		}

		return new Tile (Tile.Type.Void);
	}

	void SubdivideTilesArray (int i1 = 0, int i2 = 0) {

		if (i1 > tiles.GetLength (0) && i2 > tiles.GetLength (1))
			return;

		//Get size of segment
		int sizeX, sizeY;

		if (tiles.GetLength (0) - i1 > 100) {

			sizeX = 100;
		} else
			sizeX = tiles.GetLength (0) - i1;

		if (tiles.GetLength (1) - i2 > 100) {

			sizeY = 100;
		} else
			sizeY = tiles.GetLength (1) - i2;

		GenerateTilesLayer (i1, i2, sizeX, sizeY);

		if (tiles.GetLength (0) > i1 + 100) {
			SubdivideTilesArray (i1 + 100, i2);
			return;
		}

		if (tiles.GetLength (1) > i2 + 100) {
			SubdivideTilesArray (0, i2 + 100);
			return;
		}
	}

	void GenerateTilesLayer (int x, int y, int width, int height) {

		data = new MeshData (x, y, width, height);

		GameObject meshGO = new GameObject ("TileLayer_" + x + "_" + y);
		meshGO.transform.SetParent (this.transform);

		MeshFilter filter = meshGO.AddComponent<MeshFilter> ();
		MeshRenderer render = meshGO.AddComponent<MeshRenderer> ();
		render.material = material;

		mesh = filter.mesh;

		mesh.vertices = data.vertices.ToArray ();
		mesh.triangles = data.triangles.ToArray ();
		mesh.uv = data.UVs.ToArray ();
	}


	void SubdivideMountainArray (int i1 = 0, int i2 = 0) {

		if (i1 > tiles.GetLength (0) && i2 > tiles.GetLength (1))
			return;

		//Get size of segment
		int sizeX, sizeY;

		if (tiles.GetLength (0) - i1 > 25) {

			sizeX = 25;
		} else
			sizeX = tiles.GetLength (0) - i1;

		if (tiles.GetLength (1) - i2 > 25) {

			sizeY = 25;
		} else
			sizeY = tiles.GetLength (1) - i2;

		GenerateMountainLayer (i1, i2, sizeX, sizeY);

		if (tiles.GetLength (0) > i1 + 25) {
			SubdivideMountainArray (i1 + 25, i2);
			return;
		}

		if (tiles.GetLength (1) > i2 + 25) {
			SubdivideMountainArray (0, i2 + 25);
			return;
		}
	}

	void GenerateMountainLayer (int x, int y, int width, int height) {

		MeshData data = new MeshData (x, y, width, height, true);

		GameObject meshGO = new GameObject ("MountainLayer_" + x + "_" + y);
		meshGO.transform.SetParent (this.transform);

		MeshFilter filter = meshGO.AddComponent<MeshFilter> ();
		MeshRenderer render = meshGO.AddComponent<MeshRenderer> ();
		render.material = material;

		Mesh mesh = filter.mesh;

		mesh.vertices = data.vertices.ToArray ();
		mesh.triangles = data.triangles.ToArray ();
		mesh.uv = data.UVs.ToArray ();
	}


	public Tile GetTileAt (int x, int y) {

		if (x < 0 || x >= width || y < 0 || y >= height) {

			return null;
		}

		return tiles [x, y];
	}

	public Tile[] GetNeighbors (int x, int y, bool diagonals = false) {

		Tile[] neighbors = new Tile[ diagonals ? 8 : 4];

		// N E S W
		neighbors [0] = GetTileAt (x + 0, y + 1);
		neighbors [1] = GetTileAt (x + 1, y + 0);
		neighbors [2] = GetTileAt (x + 0, y - 1);
		neighbors [3] = GetTileAt (x - 1, y + 0);

		//NE SE SW NW
		if (diagonals == true) {

			neighbors [4] = GetTileAt (x + 1, y + 1);
			neighbors [5] = GetTileAt (x + 1, y - 1);
			neighbors [6] = GetTileAt (x - 1, y - 1);
			neighbors [7] = GetTileAt (x - 1, y + 1);
		}

		return neighbors;
	}

}
