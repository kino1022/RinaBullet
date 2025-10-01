using System;
using R3;
using RinaBullet.Range.Interface;
using UnityEngine;

namespace RinaBullet.Range {
    [Serializable]
    public class RengeRecorder : IRangeRecorder, IDisposable{

        private GameObject m_obj;
        
        private Vector3 m_originPosition = Vector3.zero;

        private ReactiveProperty<float> m_range = new ReactiveProperty<float>();
        
        private CompositeDisposable m_disposable = new CompositeDisposable();
        
        public ReadOnlyReactiveProperty<float> Range => m_range;

        public RengeRecorder(GameObject obj) {
            m_obj = obj ?? throw new ArgumentNullException();
            
            //原点を保存
            m_originPosition = m_obj.transform.position;

            m_disposable = new CompositeDisposable();
            
            m_range = CreateProperty();
        }

        public void Dispose() {
            m_disposable?.Dispose();
        }

        private ReactiveProperty<float> CreateProperty() {
            
            var property = new ReactiveProperty<float>();

            var stream = Observable
                .EveryValueChanged(m_obj.transform, x => x.position)
                .Subscribe(x => {
                    property.Value = CalculateRange(x);
                })
                .AddTo(m_disposable);

            return property;
        }
        
        private float CalculateRange(Vector3 position) {
            return Vector3.Distance(m_originPosition, position);
        }
    }
}