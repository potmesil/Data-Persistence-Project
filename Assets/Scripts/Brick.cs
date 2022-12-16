using System;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public Action<int> OnDestroyed { get; set; }
    public int PointValue { get; set; }

    private void Start()
    {
        var renderer = GetComponentInChildren<Renderer>();
        var block = new MaterialPropertyBlock();

        switch (PointValue)
        {
            case 1 :
                block.SetColor("_BaseColor", Color.green);
                break;
            case 2:
                block.SetColor("_BaseColor", Color.yellow);
                break;
            case 5:
                block.SetColor("_BaseColor", Color.blue);
                break;
            default:
                block.SetColor("_BaseColor", Color.red);
                break;
        }

        renderer.SetPropertyBlock(block);
    }

    private void OnCollisionEnter(Collision other)
    {
        OnDestroyed?.Invoke(PointValue);
        
        //slight delay to be sure the ball have time to bounce
        Destroy(gameObject, 0.2f);
    }
}