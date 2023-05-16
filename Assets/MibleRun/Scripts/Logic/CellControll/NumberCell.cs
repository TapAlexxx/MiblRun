using System.Collections.Generic;
using Scripts.StaticData;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Logic.CellControll
{
    public class NumberCell : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        
        private List<Cell> _savedSelection;
        private ColorPaletteStaticData colorPaletteStaticData;

        [field:SerializeField] public int CountToFill { get; private set; }
        [field:SerializeField] public Color Color { get; private set; }

        private void OnValidate()
        {
            text = GetComponentInChildren<TMP_Text>();
            UpdateText();
        }

        private void Awake()
        {
            InitializeDefault();
        }

        private void InitializeDefault()
        {
            _savedSelection = new List<Cell>();
        }

        public void Initialize(int countToFill)
        {
            CountToFill = countToFill;
            colorPaletteStaticData = Resources.Load<ColorPaletteStaticData>("StaticData/ColorPallete/Pallete");
            SetRandomColorFromSource();
            UpdateText();
        }

        private void UpdateText()
        {
            text.text = CountToFill.ToString();
        }

        public void SaveSelection(List<Cell> currentSelection)
        {
            _savedSelection = currentSelection;
            foreach (Cell cell in _savedSelection)
            {
                cell.InteractedFilled += ResetSelection;
            }
        }

        private void ResetSelection()
        {
            foreach (Cell cell in _savedSelection)
            {
                cell.InteractedFilled -= ResetSelection;
                cell.ResetCellSelection();
            }

            _savedSelection = new List<Cell>();
        }
        
        private void ResetSelectionOnFieldReset()
        {
            foreach (Cell cell in _savedSelection)
            {
                cell.InteractedFilled -= ResetSelection;
                cell.ResetCell();
            }

            _savedSelection = new List<Cell>();
        }
        
        public void SetRandomColorFromSource()
        {
            List<Color> colors = colorPaletteStaticData.Colors;
            Color = colors[Random.Range(0, colors.Count)];
        }

        public void TryResetCellOnFieldReset()
        {
            if (_savedSelection.Count > 0) 
                ResetSelectionOnFieldReset();
        }
    }
}