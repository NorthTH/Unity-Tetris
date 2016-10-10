using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

	public static int gridWidth = 10;
	public static int gridHeight = 20;

	public static Transform[,] grid = new Transform[gridWidth, gridHeight];

	// Use this for initialization
	void Start () {
		SpawnNextTetromino ();
	}

	public bool CheckIsAboveGrid(Tetromino tetromino)
	{
		for (int x = 0; x < gridWidth; x++) {
			foreach (Transform mino in tetromino.transform) {
				Vector2 pos = Round (mino.position);
				if (pos.y > gridHeight - 1) {
					return true;
				}
			}
		}

		return false;
	}

	public bool isFullRowAt(int y)
	{
		for (int x = 0; x < gridWidth; x++) {
			if (grid [x, y] == null)
				return false;
		}
		return true;
	}

	public void DeleteMinoAt(int y)
	{
		for (int x = 0; x < gridWidth; x++) {
			Destroy (grid [x, y].gameObject);

			grid [x, y] = null;
		}
	}

	public void MoveRowDown(int y)
	{
		for (int x = 0; x < gridWidth; x++) {
			if (grid [x, y] != null) {
				grid [x, y - 1] = grid [x, y];
				grid [x, y] = null;
				grid [x, y - 1].position += new Vector3 (0, -1, 0);
			}
		}
	}

	public void MoveAllRowsDown(int y)
	{
		for (int i = y; i < gridWidth; i++) {
			MoveRowDown (i);
		}
	}

	public void DeleteRow()
	{
		for (int y = 0; y < gridHeight; y++) {
			if (isFullRowAt (y)) {
				DeleteMinoAt (y);

				MoveAllRowsDown (y+1);

				y--;
			}
		}
	}

	public void UpdateGrid(Tetromino tetromino)
	{
		for(int y=0;y<gridHeight;y++)
		{
			for(int x=0;x<gridWidth;x++)
			{
				if (grid [x, y] != null) 
				{
					if (grid [x, y].parent == tetromino.transform)
					{
						grid [x, y] = null;
					}
				}
			}
		}

		foreach (Transform mino in tetromino.transform) 
		{
			Vector2 pos = Round (mino.position);

			if (pos.y < gridHeight) 
			{
				grid [(int)pos.x, (int)pos.y] = mino;
			}
		}
	}

	public Transform GetTransformAtGridPosition(Vector2 pos)
	{
		if (pos.y > gridHeight - 1) {
			return null;
		} else {

			return grid [(int)pos.x, (int)pos.y];
		}
	}

	public void SpawnNextTetromino()
	{
		GameObject nextTetromino = (GameObject)Instantiate (Resources.Load (GetRandomTetromino (), typeof(GameObject)), new Vector2 (5.0f, 20.0f), Quaternion.identity);
	}

	public bool checkInsideGrid(Vector2 pos)
	{
		return ((int)pos.x >= 0 && (int)pos.x < gridWidth && (int)pos.y >= 0);
	}

	public Vector2 Round (Vector2 pos)
	{
		return new Vector2 (Mathf.Round (pos.x), Mathf.Round (pos.y));
	}

	string GetRandomTetromino()
	{
		int randomTetromino = Random.Range (1, 8);

		string randomTetrominoName = "Prefabs/Tetrismino_T";

		switch(randomTetromino)
		{
		case 1:
			randomTetrominoName = "Prefabs/Tetrismino_T";
			break;
		case 2:
			randomTetrominoName = "Prefabs/Tetrismino_Long";
			break;
		case 3:
			randomTetrominoName = "Prefabs/Tetrismino_Square";
			break;
		case 4:
			randomTetrominoName = "Prefabs/Tetrismino_J";
			break;
		case 5:
			randomTetrominoName = "Prefabs/Tetrismino_L";
			break;
		case 6:
			randomTetrominoName = "Prefabs/Tetrismino_S";
			break;
		case 7:
			randomTetrominoName = "Prefabs/Tetrismino_Z";
			break;
		}

		return randomTetrominoName;
	}

	public void GameOver()
	{
		SceneManager.LoadScene ("GameOver");
	}
}
