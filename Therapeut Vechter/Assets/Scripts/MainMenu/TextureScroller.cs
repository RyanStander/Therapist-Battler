using UnityEngine;
using Unity.Collections;

namespace MainMenu
{
    public class TextureScroller : MonoBehaviour
    {
       // [SerializeField] private float scrollX;
        //[SerializeField] private float scrollY;
        public int materialIndex = 0;
        public Vector2 uvAnimationRate = new Vector2(1.0f, 0.0f);
        public string textureName = "_MainTex";

        Vector2 uvOffset = Vector2.zero;
//https://answers.unity.com/questions/19848/making-textures-scroll-animate-textures.html
        void LateUpdate()
        {
            uvOffset += (uvAnimationRate * Time.deltaTime);
            if (GetComponent<Renderer>().enabled)
            {
                GetComponent<Renderer>().materials[materialIndex].SetTextureOffset(textureName, uvOffset);
            }
        }
    }

/*
    // Update is called once per frame
    void Update()
    {
        float offsetX = Time.time * scrollX;
        float offsetY = Time.time * scrollY;
        GetComponent<Image>().material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }*/
}