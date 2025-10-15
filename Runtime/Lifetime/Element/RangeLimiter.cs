using System;
using RinaBullet.Range.Interface;
using VContainer;
using UnityEngine;
using Sirenix.OdinInspector;
using R3;
using Sirenix.Serialization;

namespace RinaBullet.Lifetime.Element
{
    [Serializable]
    public class RangeLimiter : ABulletLifetimeElement
    {
        [SerializeField]
        [LabelText("最大距離")]
        private float m_maxRange = 1000.0f;

        [TitleGroup("参照")]
        [OdinSerialize]
        [ReadOnly]
        private IRangeRecorder m_rangeRecorder;

        public override void Start()
        {
            base.Start();

            RegisterStream();
        }

        protected override void ResolveDependency()
        {
            base.ResolveDependency();

            m_rangeRecorder = m_resolver.Resolve<IRangeRecorder>()
                    ?? throw new NullReferenceException();
        }

        private void RegisterStream() {
            m_rangeRecorder
                .Range
                .Where(x => x >= m_maxRange)
                .Subscribe(x => IsDead());
        }
    }
}