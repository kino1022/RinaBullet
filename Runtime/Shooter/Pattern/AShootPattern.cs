using RinaBullet.Symbol;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace RinaBullet.Shooter.Pattern {
    
    public abstract class AShootPattern : SerializedScriptableObject, IShootPattern {

        [Title("散布界")]
        
        [SerializeField]
        [LabelText("水平方向の散布界")]
        [ProgressBar(0.0f,90.0f)]
        private float m_horizontalSpread = 0.0f;
        
        [SerializeField]
        [LabelText("縦方向の散布界")]
        [ProgressBar(0.0f,90.0f)]
        private float m_verticalSpread = 0.0f;
        
        public abstract void Shoot(Bullet prefab, IObjectResolver resolver, Vector3 pos, Quaternion rot);

        /// <summary>
        /// 弾丸のばらけ具合を計算するメソッド
        /// </summary>
        /// <returns></returns>
        protected Quaternion CalculateSpread() {
            float randomX = Random.Range(-m_horizontalSpread / 2.0f, m_horizontalSpread / 2.0f);
            float randomY = Random.Range(-m_verticalSpread / 2.0f, m_verticalSpread / 2.0f);

            return Quaternion.Euler(randomY, randomX, 0.0f);
        }

    }
}