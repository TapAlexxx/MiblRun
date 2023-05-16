using System;
using DG.Tweening;
using UnityEngine;

namespace Scripts.Logic.CellControll
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private Renderer renderer;
        [SerializeField] private Vector3 targetScale;
        [SerializeField] private Color defaultColor;

        [field:SerializeField] public Vector3 InitialScale { get; private set; }
        
        private Vector3 _startPosition;
        private Vector3 _selectedScale;

        public Vector2Int Position { get; private set; }
        public bool IsSelected { get; private set; }
        public bool IsFilled { get; private set; }
        public NumberCell NumberCell { get; private set; }
        public bool IsNumberCell { get; private set; }

        public event Action InteractedFilled;


        private void OnValidate()
        {
            if (!renderer) TryGetComponent(out renderer);
        }

        public void Initialize(Vector2Int position)
        {
            Position = position;
            _startPosition = transform.position;
            _selectedScale = targetScale;
            if (TryGetComponent(out NumberCell numberCell))
            {
                IsNumberCell = true;
                NumberCell = numberCell;
            }
        }

        public void ResetCellSelection()
        {
            ResetCell();
            transform.DOScale(InitialScale, 0.2f);
        }

        public void ResetCell()
        {
            renderer.material.color = defaultColor;
            transform.DOMove(_startPosition, 0.2f);
            IsSelected = false;
            IsFilled = false;
        }

        public void MakeSelected()
        {
            IsSelected = true;
            transform.DOMove(_startPosition + Vector3.up * 0.5f, 0.2f);
            transform.DOScale(_selectedScale, 0.2f);
        }

        public void InteractFilled() => 
            InteractedFilled?.Invoke();

        public void SetColor(Color color)
        {
            renderer.material.color = color;
        }

        public void MakeNonSelected()
        {
            renderer.material.color = defaultColor;
            transform.DOMove(_startPosition, 0.2f);
            transform.DOScale(InitialScale, 0.2f);
            IsSelected = false;
        }

        public void Fill()
        {
            transform.DOMove(_startPosition, 0.2f);
            transform.DOScale(_selectedScale, 0.2f);
            IsFilled = true;
        }
    }
}