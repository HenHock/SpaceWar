using Project.Infrastructure.BootStateMachine;
using Project.Infrastructure.Services.Windows.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.UI.Gameplay.Windows.EndLevelWindows
{
    public class WinWindowDisplay : BaseWindow
    {
        private const string WinTitleText = "Congratulations!";
        
        [SerializeField] private Button exitButton;
        [SerializeField] private TextMeshProUGUI titleTMP;
        
        private IGameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IGameStateMachine stateMachine)
        {
            _gameStateMachine = stateMachine;
        }
        
        public override void Open()
        {
            titleTMP.SetText(WinTitleText);
            exitButton.onClick.AddListener(ToNextState);
        }
        
        private void ToNextState()
        {
            _gameStateMachine.CurrentState.Value.Next();
        }
    }
}