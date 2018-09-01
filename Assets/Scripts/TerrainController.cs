using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
	[Header("Terrain Bounds")]
	public Vector2 MinPosition;
	public Vector2 MaxPosition;

	[Header("Tiles Settings")]
	public GameObject TilePrefab;
	public List<TileOption> TileOptions = new List<TileOption>();
	public Transform TilesParent;
	
	[Header("Coins Settings")]
	public GameObject CoinPrefab;
	public Transform CoinParent;
	public int CoinAmount;

	public void CreateTerrain()
	{
		ClearParent(TilesParent);

		for (var i = MinPosition.x; i <= MaxPosition.x; i++)
		{
			for (var j = MinPosition.y; j <= MaxPosition.y; j++)
			{
				var instance = Instantiate(TilePrefab, new Vector3(i, j, 0), Quaternion.identity, TilesParent);
				instance.GetComponent<SpriteRenderer>().sprite = GetTile();
			}
		}
	}

	public void CreateCoins()
	{
		ClearParent(CoinParent);

		for (var i = 0; i < CoinAmount; i++)
		{
			Instantiate(CoinPrefab,
				new Vector3(Random.Range(MinPosition.x, MaxPosition.x), Random.Range(MinPosition.y, MaxPosition.y), 0),
				Quaternion.identity,
				CoinParent
			);
		}
	}
	
	public void ClearParent(Transform parent)
	{
		for (var i = parent.childCount - 1; i >= 0; i--)
		{
			DestroyImmediate(parent.GetChild(i).gameObject);
		}
	}

	private Sprite GetTile()
	{
		var res = new List<Sprite>();
		foreach (var option in TileOptions)
		{
			for(var i=0;i<option.Weight;i++)
				res.Add(option.Sprite);
		}
		return res[Random.Range(0, res.Count)];
	}
}

[System.Serializable]
public class TileOption
{
	public Sprite Sprite;
	public int Weight;
}
