using Project.Infrastructure.Services.AssetManagement.Data;
using Project.Logic.Player.Data;
using UnityEngine;
using Zenject;

namespace Project.Logic.Bullet
{
    public class BulletMovement : MonoBehaviour
    {
        private BulletConfig _bulletConfig;

        [Inject]
        private void Construct(IReadAssetContainer assetContainer)
        {
            _bulletConfig = assetContainer.GetConfig<PlayerConfig>().BulletConfig;
        }
        
        private void Update()
        {
            transform.Translate(_bulletConfig.MoveSpeed * Time.deltaTime * Vector3.up);
        }
    }
}