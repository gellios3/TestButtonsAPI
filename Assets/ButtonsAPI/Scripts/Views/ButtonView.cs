using ButtonsAPI.Interfaces;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ButtonsAPI.Views
{
    public class ButtonView : MonoBehaviour, IButtonView
    {
        [SerializeField] private Image m_image;
        [SerializeField] private TMP_Text m_text;

        private const float maxScale = 1.5f;
        private const float minScale = 1f;
        private const float animDuration = 1.5f;

        public int id { get; private set; }

        public void Setup(IButton button)
        {
            id = button.buttonId;

            if (button.buttonColor != null)
            {
                m_image.color = (Color)button.buttonColor;
            }

            m_text.text = button.buttonText;

            if (button.isAnimation)
            {
                PlayAnimation();
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void PlayAnimation()
        {
            TweenerCore<Vector3, Vector3, VectorOptions> anim = transform.DOScale(maxScale, animDuration);
            anim.onComplete += GoBack;
        }

        private void GoBack()
        {
            transform.DOScale(minScale, animDuration);
        }
    }
}