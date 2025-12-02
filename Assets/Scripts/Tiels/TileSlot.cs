using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TileSlot : MonoBehaviour
{
    private Collider myCollider  => GetComponent<Collider>();
    private MeshRenderer meshRenderer => GetComponent<MeshRenderer>();
    private MeshFilter meshFilter => GetComponent<MeshFilter>();

    private NavMeshSurface myNavMesh => GetComponentInParent<NavMeshSurface>();

    public void SwitchTİle(GameObject referenceTile)
    {
        gameObject.name = referenceTile.name;
        TileSlot newTile = referenceTile.GetComponent<TileSlot>();
        meshFilter.mesh = newTile.GetMesh();
        meshRenderer.material = newTile.GetMaterial();

        UpdateCollider(newTile.GetCollider());
        foreach(GameObject obj in GetAllChilderen())
        {
            DestroyImmediate(obj);
        }

        foreach(GameObject obj in newTile.GetAllChilderen())
        {
            Instantiate(obj, transform);
        }

        UpdateLayer(referenceTile);
        UpdateNavMesh();
    }
    
    public Material GetMaterial() => meshRenderer.sharedMaterial;
    public Mesh GetMesh() => meshFilter.sharedMesh;

    public Collider GetCollider()  => myCollider;

    public List<GameObject> GetAllChilderen()
    {
        List<GameObject> children = new List<GameObject>();
        foreach(Transform child in transform)
        {
            children.Add(child.gameObject);
        }
        return children;
    }

    public void UpdateCollider(Collider newCollider)


    {
        DestroyImmediate(myCollider);
        if(newCollider is BoxCollider)
        {
            BoxCollider original = newCollider.GetComponent<BoxCollider>();
            BoxCollider myNewCollider = transform.AddComponent<BoxCollider>();

            myNewCollider.center = original.center;
            myNewCollider.size = original.size; 
        }

         if(newCollider is MeshCollider)
        {
            MeshCollider original = newCollider.GetComponent<MeshCollider>();
            MeshCollider myNewCollider = transform.AddComponent<MeshCollider>();

            myNewCollider.sharedMesh = original.sharedMesh;
            myNewCollider.convex = original.convex; 
        }
    }

public void UpdateLayer(GameObject referancedobj) => gameObject.layer = referancedobj.layer;
public void UpdateNavMesh() => myNavMesh.BuildNavMesh();
public void TileRotate(int dir)
    {
         transform.Rotate(0,90 * dir,0);
         UpdateNavMesh();
    }
public void ADJuse(int verticalDir)
    {
        transform.position += new Vector3(0,.1f * verticalDir,0);
        UpdateNavMesh();
    } 

}
