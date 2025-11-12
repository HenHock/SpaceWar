using Project.Infrastructure.BootStateMachine;
using Project.Infrastructure.BootStateMachine.States.Gameplay;
using Project.Infrastructure.Services.Windows.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.UI.Gameplay.Windows.EndLevelWindows
{
    public class LoseWindowDisplay : BaseWindow
    {
        private const string LoseTitleText = "Game over!";
        
        [SerializeField] private Button exitButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private TextMeshProUGUI titleTMP;
        
        private IGameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IGameStateMachine stateMachine)
        {
            _gameStateMachine = stateMachine;
        }
        
        public override void Open()
        {
            titleTMP.SetText(LoseTitleText);
            restartButton.onClick.AddListener(Restart);
            exitButton.onClick.AddListener(ToNextState);
        }

        private void Restart()
        {
            _gameStateMachine.Enter<LoadGameplayState>();
        }
        
        private void ToNextState()
        {
            _gameStateMachine.CurrentState.Value.Next();
        }
    }
}