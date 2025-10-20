using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;

namespace RinaBullet.Lifetime.Element
{
    /// <summary>
    /// 一定時間で消滅する弾丸に対して適用する
    /// </summary>
    [Serializable]
    public class TimeLimiter : ABulletLifetimeElement
    {

        [SerializeField]
        [LabelText("生存できる期間(秒)")]
        [ProgressBar(0.0f, 100.0f)]
        private float duration = 10.0f;

        public override void Start() {
            base.Start();
            AsyncCount().Forget();
        }

        private async UniTask AsyncCount() {

            var token = m_bullet.GetCancellationTokenOnDestroy();

            try {
                await UniTask.Delay(
                    TimeSpan.FromSeconds(duration),
                    cancellationToken: token
                    );
            }
            catch (OperationCanceledException) {
                
            }
            finally {
                IsDead?.Invoke();
            }
        }
    }
}