using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryWallController : MonoBehaviour
{
    [SerializeField] private GameObject _imageFramePrefab;
    [SerializeField] private Transform _gridParent;

    [SerializeField] private int _rows = 2;
    [SerializeField] private float _spacing;

    private void Start()
    {
        CreateFrames();
    }

    private void CreateFrames()
    {
        int totalImages = ImageLoaderUrl.Instance.ImageUrls.Count;
        int columns = Mathf.CeilToInt((float)totalImages / _rows);
        int x = 0;

        foreach (var item in ImageLoaderUrl.Instance.ImageUrls)
        {
            int row = x / columns;
            int column = x % columns;

            Vector3 position = new Vector3(column * _spacing, 0, -row * _spacing);
            GameObject newImageFrame = Instantiate(_imageFramePrefab, position, _gridParent.rotation, _gridParent);
            newImageFrame.GetComponentInChildren<ImageFromUrl>().ImageUrl = item;
            x++;
        }
    }
}
