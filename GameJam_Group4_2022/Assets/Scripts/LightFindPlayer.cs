using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFindPlayer : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject target;
    private Mesh mesh;
    private Vector3 origin;
    private float startingAngle;
    public float fov = 150f;
    public int rayCount = 80;
    
    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        if (target == null) target = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {

        float angle = 0f;
        float angleIncrease = fov / rayCount;
        float viewDistance = 60f;

        origin = Vector3.zero;

        Vector3[] verticies = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[verticies.Length];
        int[] triangles = new int[rayCount * 3];

        verticies[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 1; i <= rayCount; i++)
        {
            Vector3 vertex;

            RaycastHit rayHit;

            if (Physics.Raycast(origin, GetVectorFromAngle(angle), out rayHit, layerMask))
            {
                if (rayHit.collider == null)
                {
                    vertex = origin + GetVectorFromAngle(angle) * viewDistance;
                }
                else if (rayHit.collider.gameObject.tag == "Player")
                {
                    vertex = rayHit.point;
                    GameObject go = rayHit.collider.gameObject;
                    try
                    {
                        go.GetComponent<MoveToPoints>().playerState = playerState.Failed;
                        go.GetComponent<MoveToPoints>().stopAgent();
                    }
                    catch(System.NullReferenceException)
                    {
                        Debug.Log("GO Only has player tag!");
                    }
                    Debug.Log("<color=red>Player object found!</color>");
                }
                else vertex = rayHit.point;
            }
            else
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            verticies[vertexIndex] = vertex;
            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }
            vertexIndex++;

            angle -= angleIncrease;
        }

        mesh.vertices = verticies;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    /*
     * public void setOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void setDirection(Vector3 dir)
    {
        startingAngle = GetAngleFromVectorFloat(dir) - fov/2;
    }

    float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n <= 0) n += 360;
        return n;
    }*/
}
