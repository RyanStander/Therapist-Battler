using UnityEngine;
using UnityEngine.UI;


namespace MainMenu
{
    public class TextureScroller : MonoBehaviour
    {
        [SerializeField] private RawImage scrollImage;
        [SerializeField] private float scrollX;
        [SerializeField] private float scrollY;
        [SerializeField] private Image maskImage;
        [SerializeField] private Image parentImage;
        
        void Update()
        {
            scrollImage.uvRect = new Rect(scrollImage.uvRect.position + new Vector2(scrollX, scrollY) * Time.deltaTime,
                scrollImage.uvRect.size);
            maskImage.fillAmount = parentImage.fillAmount;
            
        }
    }
}