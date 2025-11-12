using System;
using Project.Services.LevelServices.LevelProgression.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Menu
{
    public class LevelElementDisplay : MonoBehaviour
    {
        [SerializeField] private Image stateImage;
        [SerializeField] private TextMeshProUGUI levelNumberTMP;
        [SerializeField] private GameObject generatedLevelIndicator;
        
        [Space]
        [SerializeField] private Color lockedColor = Color.gray;
        [SerializeField] private Color unlockedColor = Color.white;
        [SerializeField] private Color completedColor = Color.green;

        public void Initialize(int level, LevelState state, bool isGenerated = false)
        {
            SetState(state);
            levelNumberTMP.SetText(level.ToString());
            generatedLevelIndicator.SetActive(isGenerated);
        }

        private void SetState(LevelState state)
        {
            switch (state)
            {
                case LevelState.Locked:
                    stateImage.color = lockedColor;
                    break;
                case LevelState.Unlocked:
                    stateImage.color = unlockedColor;
                    break;
                case LevelState.Completed:
                    stateImage.color = completedColor;
                    break;
            }
        }
    }
}