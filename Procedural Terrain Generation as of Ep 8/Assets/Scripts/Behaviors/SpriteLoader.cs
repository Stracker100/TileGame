using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLoader : MonoBehaviour {

	public static SpriteLoader instance;

	Dictionary<string, Vector2[]> tileUVMap;

	// Use this for initialization
	void Awake () {

		instance = this;

		tileUVMap = new Dictionary<string, Vector2[]> ();

		Sprite[] sprites = Resources.LoadAll<Sprite> ("");

		float imageWidth = 0f;
		float imageHeight = 0f;

		foreach (Sprite s in sprites) {

			if (s.rect.x + s.rect.width > imageWidth)
				imageWidth = s.rect.x + s.rect.width;

			if (s.rect.y + s.rect.height > imageHeight)
				imageHeight = s.rect.y + s.rect.height;
		}

		foreach (Sprite s in sprites) {

			Vector2[] uvs = new Vector2[4];

			uvs [0] = new Vector2 (s.rect.x / imageWidth, s.rect.y / imageHeight);
			uvs [1] = new Vector2 ((s.rect.x + s.rect.width) / imageWidth, s.rect.y / imageHeight);
			uvs [2] = new Vector2 (s.rect.x / imageWidth, (s.rect.y + s.rect.height) / imageHeight);
			uvs [3] = new Vector2 ((s.rect.x + s.rect.width) / imageWidth, (s.rect.y + s.rect.height)  / imageHeight);

			tileUVMap.Add (s.name, uvs);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector2[] GetTileUVs (Tile tile) {

		string key = tile.type.ToString ();

		if (tileUVMap.ContainsKey (key) == true) {
            return tileUVMap [key];
            
		} else {

			Debug.LogError ("There is no UV map for tile type: " + key);
			return tileUVMap ["Void"];
		}
	}

	public Vector2[] GetWallUVsAtQuadrant (Tile.Wall wall, int quadrant, Tile[] neighbors) {

		if (wall == Tile.Wall.Empty)
			return tileUVMap ["Empty"];

		string key = GetKeyForWall (wall, neighbors, quadrant);

		if (tileUVMap.ContainsKey (key) == true) {

			return tileUVMap [key];
		} else {

			Debug.LogError ("There is no UV map for tile type: " + key);
			return tileUVMap ["Void"];
		}
	}

	string GetKeyForWall (Tile.Wall wall, Tile[] neighbors, int quadrant) {

		string key = wall.ToString() + "_" + quadrant.ToString ();

		if (quadrant == 1) {

			if (IsWallEmptyOrNull (neighbors [0]) && IsWallEmptyOrNull (neighbors [1]) && IsWallEmptyOrNull (neighbors [4])) {
				key += "Corner";
				return key;
			}

			if (!IsWallEmptyOrNull (neighbors [0]) && !IsWallEmptyOrNull (neighbors [1]) && IsWallEmptyOrNull (neighbors [4])) {
				key += "iCorner";
				return key;
			}

			if (IsWallEmptyOrNull (neighbors [0])) {
				key += "N";
				return key;
			}

			if (IsWallEmptyOrNull (neighbors [1])) {
				key += "E";
				return key;
			}
		}

		if (quadrant == 2) {

			if (IsWallEmptyOrNull (neighbors [2]) && IsWallEmptyOrNull (neighbors [1]) && IsWallEmptyOrNull (neighbors [5])) {
				key += "Corner";
				return key;
			}

			if (!IsWallEmptyOrNull (neighbors [2]) && !IsWallEmptyOrNull (neighbors [1]) && IsWallEmptyOrNull (neighbors [5])) {
				key += "iCorner";
				return key;
			}

			if (IsWallEmptyOrNull (neighbors [2])) {
				key += "S";
				return key;
			}

			if (IsWallEmptyOrNull (neighbors [1])) {
				key += "E";
				return key;
			}
		}

		if (quadrant == 3) {

			if (IsWallEmptyOrNull (neighbors [2]) && IsWallEmptyOrNull (neighbors [3]) && IsWallEmptyOrNull (neighbors [6])) {
				key += "Corner";
				return key;
			}

			if (!IsWallEmptyOrNull (neighbors [2]) && !IsWallEmptyOrNull (neighbors [3]) && IsWallEmptyOrNull (neighbors [6])) {
				key += "iCorner";
				return key;
			}

			if (IsWallEmptyOrNull (neighbors [2])) {
				key += "S";
				return key;
			}

			if (IsWallEmptyOrNull (neighbors [3])) {
				key += "W";
				return key;
			}
		}

		if (quadrant == 4) {

			if (IsWallEmptyOrNull (neighbors [0]) && IsWallEmptyOrNull (neighbors [3]) && IsWallEmptyOrNull (neighbors [7])) {
				key += "Corner";
				return key;
			}

			if (!IsWallEmptyOrNull (neighbors [0]) && !IsWallEmptyOrNull (neighbors [3]) && IsWallEmptyOrNull (neighbors [7])) {
				key += "iCorner";
				return key;
			}

			if (IsWallEmptyOrNull (neighbors [0])) {
				key += "N";
				return key;
			}

			if (IsWallEmptyOrNull (neighbors [3])) {
				key += "W";
				return key;
			}
		}

		return key;
	}

	bool IsWallEmptyOrNull (Tile tile) {

		if (tile == null || tile.wall == Tile.Wall.Empty)
			return true;

		return false;
	}
}
