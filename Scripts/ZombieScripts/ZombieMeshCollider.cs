using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMeshCollider : MonoBehaviour
{
    private MeshCollider meshCollider;
    private SkinnedMeshRenderer skinnedMeshRenderer;

    void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        meshCollider.convex = false;

        if (skinnedMeshRenderer == null)
            return;

        Mesh mesh = new Mesh();
        skinnedMeshRenderer.BakeMesh(mesh);

        if (meshCollider != null)
            meshCollider.sharedMesh = mesh;

        Vector3 parentScale = transform.parent.lossyScale;
        meshCollider.transform.localScale = new Vector3(1 / parentScale.x, 1 / parentScale.y, 1 / parentScale.z);
    }

    void Update()
    {
        UpdateMeshCollider();
    }

    void UpdateMeshCollider()
    {
        if (skinnedMeshRenderer == null)
            return;

        Mesh mesh = new Mesh();
        skinnedMeshRenderer.BakeMesh(mesh);

        if (meshCollider != null)
            meshCollider.sharedMesh = mesh;
    }
}