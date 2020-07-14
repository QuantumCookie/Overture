using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource_Material : MonoBehaviour
{
    private PowerSource powerSource;
    private MaterialPropertyBlock propertyBlock;
    private Renderer renderer;
    [ColorUsage(false, true)] public Color emissionColor;

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        powerSource = GetComponent<PowerSource>();
        propertyBlock = new MaterialPropertyBlock();
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        propertyBlock.SetColor("_Color", emissionColor * Mathf.Pow(2, 7 + 2f * powerSource.power / powerSource.capacity));
        renderer.SetPropertyBlock(propertyBlock);
    }

    private void OnDisable()
    {
        
    }
}
