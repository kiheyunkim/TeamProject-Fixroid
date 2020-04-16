using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Utils :MonoBehaviour
{ 
    static public List<Texture2D> Sprites2Textures2D(Sprite[] sprite)
    {
        List<Texture2D> texture2Ds = new List<Texture2D>();

        for (int i = 0; i < sprite.Length; i++)
        {
            Texture2D texture = new Texture2D((int)sprite[i].rect.width, (int)sprite[i].rect.height);
            Color[] pixels = sprite[i].texture.GetPixels((int)sprite[i].textureRect.x, (int)sprite[i].textureRect.y, (int)sprite[i].textureRect.width, (int)sprite[i].textureRect.height);

            if (pixels.Length != sprite[i].rect.width * sprite[i].rect.height)
                Debug.Log(i);//Check Sliced Sprite from Multiple Sprite

            texture.SetPixels(pixels);
            texture.Apply();
            texture2Ds.Add(texture);

        }

        return texture2Ds;
    }

    static public Texture2D Sprite2Texture2D(Sprite sprite)
    {
        Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        Color[] pixels = sprite.texture.GetPixels((int)sprite.textureRect.x, (int)sprite.textureRect.y, (int)sprite.textureRect.width, (int)sprite.textureRect.height);
        texture.SetPixels(pixels);
        texture.Apply();

        return texture;
    }
}

public static class JsonWrapper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Tiles;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>
        {
            Tiles = array
        };
        return UnityEngine.JsonUtility.ToJson(wrapper);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Tiles;
    }
}
