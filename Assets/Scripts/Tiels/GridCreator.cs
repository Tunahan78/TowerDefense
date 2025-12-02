using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    [SerializeField] private GameObject mainTile;
    [SerializeField] private int IzgaraUzunluğu;
    [SerializeField] private int IzgaraGenişliği;

    [SerializeField] private List<GameObject> createdTiles ;

  
    private void CreatTile(float xPosition, float zPosition)
    {
        Vector3  tilePosition = new Vector3(xPosition,0,zPosition);
        GameObject newTile = Instantiate(mainTile, tilePosition, Quaternion.identity, transform);
        createdTiles.Add(newTile);
    }

    [ContextMenu("Clear Tiles")]
    private void ClearTiles()
    {
        foreach(GameObject tile in createdTiles)
        {
            DestroyImmediate(tile);
        }
        createdTiles.Clear();
    }
    
    [ContextMenu("Build Grid")]
    private void BuildGrid()
    {
        ClearTiles();
        createdTiles = new List<GameObject>();
       for (int x = 0; x < IzgaraUzunluğu; x++)
       {
           for(int z = 0; z < IzgaraGenişliği; z++)
           {
            CreatTile(x,z);
           }
        } 
    }

    

}
