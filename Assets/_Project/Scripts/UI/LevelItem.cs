using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

namespace GamJam
{
    public class LevelItem : GameButton
    {
        [SerializeField]
        private TextMeshProUGUI _leveIndex;
        [SerializeField]
        private Image _lockIcon;

        private Vector2 _oScale;
        private bool _locked;

        public void Init(int id, LevelData data, UnityAction levelLoad)
        {
            _oScale = transform.localScale;
            _locked = data.isLocked;
            _leveIndex.gameObject.SetActive(!data.isLocked);
            _leveIndex.text = (id + 1).ToString();
            _lockIcon.gameObject.SetActive(data.isLocked);
            onClick.AddListener(levelLoad);
        }
        public void Unlock()
        {
            _locked = false;
            _leveIndex.gameObject.SetActive(!_locked);
            _lockIcon.gameObject.SetActive(_locked);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            transform.DOScale(_oScale * 0.9f, duration).SetEase(_ease);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            transform.DOScale(_oScale * 1.1f, duration).SetEase(_ease);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            transform.DOScale(_oScale, duration).SetEase(_ease);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (!_locked)
                onClick.Invoke();
            if (_onUi)
                transform.DOScale(_oScale * 1.1f, duration).SetEase(_ease);
        }
    }
}
