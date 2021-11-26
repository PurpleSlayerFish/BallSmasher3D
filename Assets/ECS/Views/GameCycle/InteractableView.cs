﻿using System;
using ECS.Game.Components;
using Ecs.Views.Linkable.Impl;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Views.GameCycle
{
    public class InteractableView : LinkableView
    {
        [SerializeField] protected int impact;
        private Action _onTriggerEnter;

        public override void Link(EcsEntity entity)
        {
            base.Link(entity);
            entity.Get<ImpactComponent>().Value = impact;
        }

        public void SetTriggerAction(Action onTriggerEnter) => _onTriggerEnter = onTriggerEnter;
        
        protected virtual void OnTriggerEnter(Collider other)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            _onTriggerEnter?.Invoke();
        }
    }
}