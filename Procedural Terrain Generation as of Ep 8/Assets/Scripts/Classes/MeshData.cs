﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshData {

	public  List<Vector3> vertices;
	public  List<Vector2> UVs;
	public  List<int> triangles;

	public MeshData (int x, int y, int width, int height, bool mountainLayer = false) {

		vertices = new List<Vector3> ();
		UVs = new List<Vector2> ();
		triangles = new List<int> ();

		if (mountainLayer) {

			for (int i = x; i < width + x; i++) {
				for (int j = y; j < height + y; j++) {

					CreateSquareWithQuadrants (i, j);
				}
			}

			return;
		}


		for (int i = x; i < width + x; i++) {
			for (int j = y; j < height + y; j++) {

				CreateSquare (i, j);
			}
		}
	}

    public void CallCreateSquare(int x, int y)
    {
        CreateSquare(x, y);
    }

	public void CreateSquare (int x, int y) {
		
		Tile tile = World.instance.GetTileAt (x, y);

		vertices.Add (new Vector3(x + 0, y + 0));
		vertices.Add (new Vector3(x + 1, y + 0));
		vertices.Add (new Vector3(x + 0, y + 1));
		vertices.Add (new Vector3(x + 1, y + 1));

		triangles.Add (vertices.Count - 1);
		triangles.Add (vertices.Count - 3);
		triangles.Add (vertices.Count - 4);

		triangles.Add (vertices.Count - 2);
		triangles.Add (vertices.Count - 1);
		triangles.Add (vertices.Count - 4);

		UVs.AddRange (SpriteLoader.instance.GetTileUVs (tile));
	}

	void CreateSquareWithQuadrants (int x, int y) {

		Tile tile = World.instance.GetTileAt (x, y);
		Tile[] neighbors = World.instance.GetNeighbors (x, y, true);

		//In order of quadrants
		//1st quad: x += 0.5f, y += 0.5f
		CreateQuadrant (tile, neighbors, x + 0.5f, y + 0.5f, 1);

		//In order of quadrants
		//2nd quad: x += 0.5f, y += 0f
		CreateQuadrant (tile, neighbors, x + 0.5f, y + 0f, 2);

		//In order of quadrants
		//3rd quad: x += 0f, y += 0f
		CreateQuadrant (tile, neighbors, x + 0f, y + 0f, 3);

		//In order of quadrants
		//4th quad: x += 0f, y += 0.5f
		CreateQuadrant (tile, neighbors, x + 0f, y + 0.5f, 4);
	}

	void CreateQuadrant (Tile tile, Tile[] neighbors, float x, float y, int quadrant) {
		
		vertices.Add (new Vector3(x + 0, y + 0));
		vertices.Add (new Vector3(x + 0.5f, y + 0));
		vertices.Add (new Vector3(x + 0, y + 0.5f));
		vertices.Add (new Vector3(x + 0.5f, y + 0.5f));

		triangles.Add (vertices.Count - 1);
		triangles.Add (vertices.Count - 3);
		triangles.Add (vertices.Count - 4);

		triangles.Add (vertices.Count - 2);
		triangles.Add (vertices.Count - 1);
		triangles.Add (vertices.Count - 4);

		UVs.AddRange (SpriteLoader.instance.GetWallUVsAtQuadrant(tile.wall, quadrant, neighbors));
	}
}
