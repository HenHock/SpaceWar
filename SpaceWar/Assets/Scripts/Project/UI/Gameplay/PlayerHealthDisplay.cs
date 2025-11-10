using Project.Logic.Player.Model;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.UI.Gameplay
{
    public class PlayerHealthDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject[] hearts;

        [Inject]
        private void Construct(IPlayerModel playerModel)
        {
            playerModel.Health
                .Where(IsValidArrayIndex)
                .Subscribe(ChangeHealthDisplay)
                .AddTo(this);
        }

        private void Start()
        {
            foreach (var heart in hearts)
            {
                heart.SetActive(true);
            }
        }

        private void ChangeHealthDisplay(float health)
        {
            hearts[Mathf.RoundToInt(health)].SetActive(false);
        }

        private bool IsValidArrayIndex(float health) => health >= 0 && health < hearts.Length;
    }
}