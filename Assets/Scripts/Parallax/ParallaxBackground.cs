using System;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera mainCamera;
    private float lastCameraPositionX;
    private float cameraHalfWidth;
    
    
    [SerializeField] private ParallaxLayer[] backgroundLayers;

    private void Awake()
    {
        mainCamera = Camera.main;
        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect; // multiplicamos a metade da altura pelo aspecto da tela para descobrir a metade da largura visível pela câmera.
        
        InitializeLayers();
    }

    private void Start()
    {
        if(Camera.main != null)
            lastCameraPositionX = Camera.main.transform.position.x;
    }

    private void Update()
    {
        float currentCameraPositionX = mainCamera.transform.position.x;
        
        float distanceToMove = currentCameraPositionX - lastCameraPositionX;
        lastCameraPositionX = currentCameraPositionX;
        
        float cameraLeftEdge = currentCameraPositionX - cameraHalfWidth;
        float cameraRightEdge = currentCameraPositionX + cameraHalfWidth;

        foreach (ParallaxLayer layer in backgroundLayers)
        {
            layer.Move(distanceToMove);
            layer.LoopBackground(cameraLeftEdge, cameraRightEdge);
        }
    }

    private void InitializeLayers()
    {
        foreach (ParallaxLayer layer in backgroundLayers)
        {
            layer.CalculateImageWidth();
        }
    }
}
