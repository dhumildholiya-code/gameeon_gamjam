using UnityEngine.EventSystems;
using DG.Tweening;

namespace GamJam
{
    public class MenuButton : GameButton
    {
        private float _oScale;

        private void Start()
        {
            _oScale = transform.localScale.x;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            transform.DOScaleX(_oScale * 0.9f, duration).SetEase(_ease);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            transform.DOScaleX(_oScale * 1.1f, duration).SetEase(_ease);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            transform.DOScaleX(_oScale, duration).SetEase(_ease);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            onClick.Invoke();
            if (_onUi)
            {
                transform.DOScaleX(_oScale * 1.1f, duration).SetEase(_ease);
            }
        }
    }
}
