using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {



	public enum Type { Dirt, Grass, Sand, Water, Stone, Void, Castle }
	public Type type;

	public enum Wall { Empty, Brick }
	public Wall wall;

	public Tile (Type type, Wall wall = Wall.Empty) {

		this.type = type;
		this.wall = wall;

	}

	public void SetTile (Type newType) {

		type = newType;
	}
}
