using System;
using UnityEngine;

public class GpuInctancingEnabler : MonoBehaviour
{
    private void Awake()
    {
        GpuInctancingEnable();
    }

    public void GpuInctancingEnable()
    {
        MaterialPropertyBlock _materialPropertyBlock = new MaterialPropertyBlock();
        MeshRenderer _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.SetPropertyBlock(_materialPropertyBlock);
    }
}
