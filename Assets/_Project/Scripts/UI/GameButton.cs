using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using GamJam.EventChannel;
using UnityEngine.Events;

namespace GamJam
{
    public abstract class GameButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler

    {
        [SerializeField]
        protected float duration;
        [SerializeField]
        protected Ease _ease;
        [SerializeField]
        protected AudioData _hoverSound;
        [SerializeField]
        protected AudioData _clickSound;

        public UnityEvent onClick;
        protected bool _onUi;

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            _onUi = true;
            _hoverSound.Play();
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            _onUi = false;
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            _clickSound.Play();
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
        }
    }
}
