using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Fractal : MonoBehaviour
{
    public Mesh mesh;
    public Material material;
    public float childScale = 0.5f;
    public int maxDepth = 4;
    private int depth;
    void Start()
    {
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
        if (depth < maxDepth)
        {
            StartCoroutine(CreateChildren());
        }
    }

    private static Vector3[] childDirections =
    {
        Vector3.up,
        Vector3.right,
        Vector3.left,
    };

    private static Quaternion[] childOrientations ={
        Quaternion.identity,
        Quaternion.Euler(0f, 0f, -90f),
        Quaternion.Euler(0f, 0f, 90f)
    };

    private IEnumerator CreateChildren()
    {
        for(int i = 0; i<childDirections.Length; i++)
        {
            yield return new WaitForSeconds(0.5f);
            new GameObject("Fractal Child").
                    AddComponent<Fractal>().Initialize(this, childDirections[i], childOrientations[i]);
        }
    }

    private void Initialize(Fractal parent, Vector3 direction, Quaternion orientation)
    {
        mesh = parent.mesh;
        material = parent.material;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        childScale = parent.childScale;
        transform.parent = parent.transform;
        transform.localScale = Vector3.one * childScale;
        transform.localPosition = direction * (0.5f + 0.5f * childScale);
        transform.localRotation = orientation;
    }

    void Update()
    {
        
    }
}
