using System;
using R3;
using RinaBullet.Range.Interface;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RinaBullet.Range {
    [Serializable]
    public class RengeRecorder :SerializedMonoBehaviour, IRangeRecorder {
        
        private Vector3 m_origin;
        
        private ReactiveProperty<float> m_range;
        
        private CompositeDisposable m_disposable = new CompositeDisposable();
        
        public ReadOnlyReactiveProperty<float> Range => m_range;

        private void Awake() {
            m_range = new ReactiveProperty<float>();
            //原点の保存処理
            m_origin = transform.position;
            //座標変化の監視処理
            RegisterChangeTransform();
        }

        private void OnDestroy() {
            m_disposable?.Dispose();
        }

        private void RegisterChangeTransform() {
            m_disposable = new CompositeDisposable();

            var stream = Observable
                .EveryValueChanged(gameObject.transform, x => x.position)
                .Subscribe(x => {
                    m_range.Value = Vector3.Distance(m_origin, x);
                })
                .AddTo(m_disposable);
        }
    }
}