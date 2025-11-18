using System;
using System.Collections.Generic;
using R3;
using RinaBullet.Direction.State.Condition;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VContainer;

namespace RinaBullet.Direction.State {
    /// <summary>
    /// 弾丸の進行方向をStateパターンで扱うためのインターフェース
    /// </summary>
    public interface IDirectionState {
        
        /// <summary>
        /// このステートが現在示している進行方向
        /// </summary>
        Vector3 Direction { get; }
        
        /// <summary>
        /// 次のステートに進行する際に流すストリーム
        /// </summary>
        Observable<Unit> OnNextStateRequest { get; }
        
        void Initialize (GameObject bullet, IObjectResolver resolver);

        void Enter();
        
        void Update (float deltaTime);
        
        void Exit();
        
    }
    
    [Serializable]
    public abstract class ADirectionState : IDirectionState {

        protected GameObject _bullet;

        protected IObjectResolver _resolver;
        
        [Title("遷移条件")]

        [OdinSerialize]
        protected List<IStateTransitionCondition> _conditions = new();
        
        protected Subject<Unit> _onNextStateRequest = new Subject<Unit>();

        protected Vector3 _direction = Vector3.zero;
        
        public Vector3 Direction => _direction;
        
        public Observable<Unit> OnNextStateRequest => _onNextStateRequest;

        public void Initialize(GameObject bullet, IObjectResolver resolver) {
            
            _resolver = resolver;
            
            _bullet = bullet;
            
            OnInitialize();
            
            foreach (var condition in _conditions) {
                condition?.Initialize(bullet, resolver);
            }
            
        }
        
        protected virtual void OnInitialize() { }

        public abstract void Enter();

        public void Update(float deltaTime) {
            
            OnPreUpdate(deltaTime);

            if (CheckTransitionConditions() is false) {
                _onNextStateRequest.OnNext(Unit.Default);
                _onNextStateRequest.OnCompleted();
            }
            
            OnPostUpdate(deltaTime);
            
        }
        
        protected virtual void OnPreUpdate (float deltaTime) { }
        
        protected virtual void OnPostUpdate (float deltaTime) { }
        
        private bool CheckTransitionConditions() {
            
            if (_conditions.Count is 0) {
                return true;
            }
            
            foreach (var condition in _conditions) {
                if (condition == null) continue;
                
                if (!condition.Condition) return false;
            }

            return true;
            
        }

        public abstract void Exit();
    }
}