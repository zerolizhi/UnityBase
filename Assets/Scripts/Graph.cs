using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Transform pointPrefab;
    public GraphFunctionName function;
    Transform[] points;
    [Range(10, 100)]
    public int resolution = 10;
    const float pi = Mathf.PI;
    void Awake()
    {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        //Vector3 position;
        //position.z = 0f;
        points = new Transform[resolution * resolution];
        //for(int i=0, z=0; z < resolution; z++) {
        //    position.z = (z + 0.5f) * step - 1f;
        //    for (int x=0; x < resolution; x++,i++)
        //    {
        //        Transform point = Instantiate(pointPrefab);
        //        position.x = ((x + 0.5f) * step - 1f);
        //        position.y = position.x * position.x * position.x;
                
        //        point.localPosition = position;
        //        point.localScale = scale;
        //        point.SetParent(transform, false);
        //        points[i] = point;
        //    }
        //}
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = Instantiate(pointPrefab);
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }

    static Vector3 Cylinder(float u, float v, float t)
    {
        float r = 0.8f + Mathf.Sin(6f * u + 2f * v + t);
        Vector3 p;
        p.x = r * Mathf.Sin(pi * u);
        p.y = v;
        p.z = r * Mathf.Cos(pi * u);
        return p;
    }

    static Vector3 SineFunction(float x, float z, float t)
    {
        Vector3 p;
        p.z = z;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        return p;
    }

    static Vector3 MultiSineFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(2f * pi * (x + 2f * t)) / 2f;
        p.y *= 2f / 3f;
        p.z = z;
        return p;
    }

    static Vector3 Sine2DFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(pi * (z + t))*0.5f;
        p.z = z;
        return p;
    }

    static Vector3 MultiSine2DFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = 4f * Mathf.Sin(pi * (x + z + t * 0.5f));
        p.y += Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(2f * pi * (z + 2f * t)) * 0.5f;
        p.y *= 1f / 5.5f;
        p.z = z;
        return p;
    }

    static Vector3 Ripple(float x, float z, float t)
    {
        Vector3 p;
        float d = Mathf.Sqrt(x * x + z * z);
        p.y = Mathf.Sin(pi*(4f*d-t));
        p.y /= 1f + 10f * d;
        p.x = x;
        p.z = z;
        return p;
    }

    void Update()
    {
        float t = Time.time;
        GraphFunction[] functions = {
            SineFunction, Sine2DFunction, MultiSineFunction, MultiSine2DFunction,Ripple,Cylinder
        };

        //for (int i = 0; i < resolution; i++)
        //{
        //    for(int j = 0; j < resolution; j++)
        //    {
        //        Transform point = points[i * resolution + j];
        //        Vector3 position = point.localPosition;

        //        GraphFunction f = functions[(int)function];
        //        position.y = f(position.x, position.z, t);
        //        point.localPosition = position;
        //    }

        //}

        float step = 2f / resolution;
        for(int i = 0, z = 0; z < resolution; z++)
        {
            float v = (z + 0.5f) * step - 1f;
            for(int x = 0; x < resolution; x++, i++)
            {
                GraphFunction f = functions[(int)function];
                float u = (x + 0.5f) * step - 1f;
                points[i].localPosition = f(u, v, t);
            }
        }
    }
}
