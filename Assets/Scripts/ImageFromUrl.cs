using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageFromUrl : MonoBehaviour
{
    [SerializeField] private string _imageUrl;
    private Image _imageComponent;

    public string ImageUrl { get => _imageUrl; set => _imageUrl = value; }

    private void Awake()
    {
        _imageComponent = GetComponent<Image>();
    }

    private void OnEnable()
    {
        ImageLoaderUrl.OnImageLoaded += OnImageReceived;
    }

    private void OnDisable()
    {
        ImageLoaderUrl.OnImageLoaded -= OnImageReceived;
    }
    
    private void OnImageReceived(string url, Sprite sprite)
    {
        if (url == _imageUrl)
        {
            _imageComponent.sprite = sprite;
        }
    }
}
