using System.Collections.Generic;
using UnityEngine;

public class TileSlot : MonoBehaviour
{
    
    private MeshRenderer meshRenderer => GetComponent<MeshRenderer>();
    private MeshFilter meshFilter => GetComponent<MeshFilter>();

    public void SwitchTİle(GameObject referenceTile)
    {
        TileSlot newTile = referenceTile.GetComponent<TileSlot>();

        meshFilter.mesh = newTile.GetMesh();
        meshRenderer.material = newTile.GetMaterial();
        foreach(GameObject obj in GetAllChilderen())
        {
            DestroyImmediate(obj);
        }

        foreach(GameObject obj in newTile.GetAllChilderen())
        {
            Instantiate(obj, transform);
        }
    }
    
    public Material GetMaterial() => meshRenderer.sharedMaterial;
    public Mesh GetMesh() => meshFilter.sharedMesh;

    public List<GameObject> GetAllChilderen()
    {
        List<GameObject> children = new List<GameObject>();
        foreach(Transform child in transform)
        {
            children.Add(child.gameObject);
        }
        return children;
    }
}
