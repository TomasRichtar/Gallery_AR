using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageLoaderUrl : SingletonMonoBehaviour<ImageLoaderUrl>
{
    public static event Action<string, Sprite> OnImageLoaded;

    // Predefined Images
    [SerializeField] private List<string> _imageUrls = new List<string>() 
    {
        "https://drive.google.com/uc?export=view&id=1jslyrQY9uy-5ZeOI_dVccDbP31mmffvn",
        "https://drive.google.com/uc?export=view&id=1OXGKupQwWwdF2mfqLmyTUVWJdKRrSjns",
        "https://drive.google.com/uc?export=view&id=1_wPUnO5-Nqu-bYgx82ofHpXZgBghRAlF",
        "https://drive.google.com/uc?export=view&id=1I58AOlzI9yYM2ngWwQYfoze0kzKDYFJi",
    };

    private Dictionary<string, Sprite> _imageCache = new Dictionary<string, Sprite>();

    public List<string> ImageUrls => _imageUrls;

    void Start()
    {
        StartCoroutine(DownloadAllImages());
    }

    private IEnumerator DownloadAllImages()
    {
        // Right now there are not many images to download so I'm downloading all at the start of the application 
        // When the gallery increases in size, it would be better to load them only when needed
        foreach (var url in _imageUrls)
        {
            if (!_imageCache.ContainsKey(url))
            {
                yield return StartCoroutine(DownloadImage(url));
            }
        }
    }

    private IEnumerator DownloadImage(string url)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                _imageCache[url] = sprite;

                OnImageLoaded?.Invoke(url, sprite);
            }
            else
            {
                Debug.LogError("Error loading image: " + url + ": " + request.error);
            }
        }
    }
}
