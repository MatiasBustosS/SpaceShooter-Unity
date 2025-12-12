using System;
using UnityEngine;

public class ScrollBackGround : MonoBehaviour
{
    [SerializeField] private float speed = 0.2f;
    
    private Renderer rend;

    private void Start()
    {
        rend =  GetComponent<Renderer>();
    }

    private void Update()
    {
        Vector2 offset = new Vector2(Time.time * speed, 0);
        rend.material.mainTextureOffset = offset;
    }
}
