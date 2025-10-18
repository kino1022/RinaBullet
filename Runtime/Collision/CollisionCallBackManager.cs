using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace RinaBullet.Collision {
    public class CollisionCallBackManager : SerializedMonoBehaviour, ICollisionCallBackManager {

        [Title("コールバック")]
        
        [OdinSerialize]
        [LabelText("衝突時コールバック")]
        private List<ICollisionCallBackElement> m_onCollision = new();
        
        [OdinSerialize]
        [LabelText("滞在時コールバック")]
        private Dictionary<float, List<ICollisionCallBackElement>> m_onStay = new();
        
        [OdinSerialize]
        [LabelText("通過時コールバック")]
        private List<ICollisionCallBackElement> m_onExit = new();

        private bool m_isStayProgress = false;
        
        public IReadOnlyList<ICollisionCallBackElement> OnCollision => m_onCollision;

        public IReadOnlyDictionary<float,List<ICollisionCallBackElement>> OnStay => m_onStay;
        
        public IReadOnlyList<ICollisionCallBackElement> OnExit => m_onExit;
        
        public void AddOnCollision(ICollisionCallBackElement onCollision) => m_onCollision.Add(onCollision);
        
        public void RemoveOnCollision(ICollisionCallBackElement onCollision) => m_onCollision.Remove(onCollision);
        
        public void ClearOnCollision() => m_onCollision.Clear();
        
        public void AddOnExit(ICollisionCallBackElement onExit) => m_onExit.Add(onExit);
        
        public void RemoveOnExit(ICollisionCallBackElement onExit) => m_onExit.Remove(onExit);
        
        public void ClearOnExit() => m_onExit.Clear();

        public void AddOnStay(int interval, ICollisionCallBackElement element) {
            
            if (element == null) throw new ArgumentNullException(nameof(element));

            if (interval <= 0) throw new ArgumentOutOfRangeException(nameof(interval)); 
            
            if (m_onStay.TryGetValue(interval, out var list) is false) {
                m_onStay.Add(interval, new List<ICollisionCallBackElement> {element});
                return;
            }
            list.Add(element);
        }
        
        public void RemoveOnStay(ICollisionCallBackElement callBack) {
            
            m_onStay
                    .Values
                    .SelectMany(x => x)
                    .ToList()
                    .Remove(callBack);
            
        }
        
        public void ClearOnStay() => m_onStay.Clear();

        private void OnCollisionEnter(UnityEngine.Collision other) {
            ExecuteCallBack(other, m_onCollision);
        }

        private void OnCollisionStay(UnityEngine.Collision other) {

            if (m_isStayProgress) return;
            
            m_isStayProgress = true;
            
            var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            ProcessCallBack(other, token).Forget();
            cts.Cancel();
        }

        private void OnCollisionExit(UnityEngine.Collision other) {
            ExecuteCallBack(other, m_onExit);
        }

        private void ExecuteCallBack(UnityEngine.Collision collision, List<ICollisionCallBackElement> callBacks) {
            
            if (callBacks is null && callBacks.Count is 0) throw new ArgumentNullException(nameof(callBacks));

            callBacks
                .OrderBy(x => x.Priority)
                .ToList()
                .ForEach(x => x?.OnCollisionEnterCallBack(collision));
            
        }

        private async UniTask ProcessCallBack(UnityEngine.Collision other, CancellationToken token) {
            var progressSecond = 0.0f;
            
            while (!token.IsCancellationRequested) {
                try {
                    await UniTask.Delay(
                        TimeSpan.FromSeconds(0.1f), 
                        cancellationToken: token
                        );
                    progressSecond += 0.1f;

                    var keys = m_onStay.Keys;
                    
                    var hitKeys = keys.Where(x => progressSecond % x == 0).ToList();

                    m_onStay
                        .Where(x => hitKeys.Contains(x.Key))
                        .Select(x => x.Value)
                        .ToList()
                        .ForEach(x => ExecuteCallBack(other, x));

                }
                catch (OperationCanceledException) {
                    m_isStayProgress = false;
                }
            }
        }
    }
}