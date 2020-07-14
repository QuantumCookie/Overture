using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Lightning : MonoBehaviour
{
    [SerializeField] private Transform origin, target;
    [SerializeField] private int pointsPerMeter = 50;
    [SerializeField] private float amplitude = 1;
    [SerializeField] private Texture2D noiseTexture;
    [SerializeField] private Vector2 noiseScrollSpeed;
    [SerializeField] private float primaryNoiseScale = 1;
    [SerializeField] private float secondaryNoiseScale = 1;

    private LineRenderer line;

    private float xOffset, yOffset;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        
        Generate();
    }

    private void Generate()
    {
        xOffset += noiseScrollSpeed.x * Time.deltaTime;
        yOffset += noiseScrollSpeed.y * Time.deltaTime;

        //line.positionCount = 2;
        //line.SetPosition(0, transform.InverseTransformPoint(origin.position));
        //line.SetPosition(1, transform.InverseTransformPoint(target.position));

        Vector3 startPos = Vector3.zero;
        Vector3 endPos = target.position - origin.position;

        //Debug.Log(startPos);
        //Debug.Log(endPos);

        float lineLength = Vector3.Distance(startPos, endPos);

        line.positionCount = Mathf.Min(2000, (int)(pointsPerMeter * lineLength));
        line.SetPosition(0, startPos);
        line.SetPosition(line.positionCount - 1, endPos);
        
        for (int i = 1; i < line.positionCount - 1; i++)
        {
            float u = xOffset + primaryNoiseScale * i;
            float v = yOffset + primaryNoiseScale * i;

            //Debug.Log(u + ", " + v);

            float raw = Mathf.PerlinNoise(u, v) + Mathf.PerlinNoise(xOffset + secondaryNoiseScale * i, yOffset + secondaryNoiseScale * i);
            raw *= 0.5f;
            raw = raw * 2 - 1;
            Vector3 noise = (Quaternion.AngleAxis(raw * 360, Vector3.forward) * Vector3.up) * raw * amplitude;
            line.SetPosition(i, Vector3.forward * i * (lineLength / line.positionCount) + noise);
        }
    }

    private void OnDrawGizmos()
    {
        if (line == null) return;

        for(int i = 0; i < line.positionCount; i++)
        {
            //Gizmos.DrawSphere(transform.TransformPoint(line.GetPosition(i)), 0.05f);
        }
    }

    private void Update()
    {
        Generate();
        //line.enabled = Input.GetButton("Fire1");
    }
}
