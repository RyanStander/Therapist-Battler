using System;
using UnityEngine;
using UnityEngine.UI;

//----------------------------------------------------
//Code obtained from https://gist.github.com/tklee1975 at https://gist.github.com/tklee1975/fa106950d5ad47cd60b814992e609337
//----------------------------------------------------
namespace UI
{
    public class ResizeImageToTextureSize : MonoBehaviour
    {
        [SerializeField] private RawImage rawImage;
        private float aspectRatio = 1.0f;
        private float rectAspectRatio = 1.0f;

        private void FixedUpdate()
        {
            AdjustAspect();
        }

        private void SetupImage()
        {
            CalculateImageAspectRatio();
            CalculateTextureAspectRatio();
        }

        private void CalculateImageAspectRatio()
        {
            var rt = transform as RectTransform;
            if (rt == null) 
                return;
            
            var sizeDelta = rt.sizeDelta;
            rectAspectRatio = sizeDelta.x / sizeDelta.y;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void CalculateTextureAspectRatio()
        {
            if(rawImage == null)
            {
                Debug.LogWarning("CalculateAspectRatio: rawImage is null");
                return;
            }

            var texture = (Texture2D) rawImage.texture;
            if(texture == null)
            {
                Debug.LogWarning("CalculateAspectRatio: texture is null");
                return;
            }

		
            aspectRatio = (float)texture.width / texture.height;
        }

        private void AdjustAspect()
        {
            SetupImage();

            var fitY = aspectRatio < rectAspectRatio;

            SetAspectFitToImage(rawImage, fitY);
        }


        protected virtual void SetAspectFitToImage(RawImage image,
            bool yOverflow)
        {
            if (image == null)
            {
                return;
            }
		
            var rect = new Rect(0, 0, 1, 1);   // default
            if (yOverflow)
            {

                rect.height = aspectRatio / rectAspectRatio;
                rect.y = (1 - rect.height) * 0.5f;
            }
            else
            {
                rect.width = rectAspectRatio / aspectRatio;
                rect.x = (1 - rect.width) * 0.5f; 
			
            }
            image.uvRect = rect;
        }
    }
}