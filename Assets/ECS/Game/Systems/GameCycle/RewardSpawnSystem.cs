﻿using System.Diagnostics.CodeAnalysis;
using ECS.Core.Utils.ReactiveSystem;
using ECS.Game.Components;
using ECS.Game.Components.Events;
using ECS.Game.Components.Flags;
using ECS.Game.Components.GameCycle;
using ECS.Utils.Extensions;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Game.Systems.GameCycle
{
    [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
    public class RewardSpawnSystem : ReactiveSystem<RewardSpawnEventComponent>
    {
#pragma warning disable 649
        private EcsWorld _world;
        private EcsFilter<PlayerComponent, PositionComponent> _player;
#pragma warning restore 649

        private Vector3 rewardOffset = new Vector3(0, 3f, 0);

        protected override EcsFilter<RewardSpawnEventComponent> ReactiveFilter { get; }

        protected override void Execute(EcsEntity entity)
        {
            foreach (var i in _player)
            {
                var reward = _world.CreateReward();
                var delta = entity.Get<PositionComponent>().Value - _player.Get2(i).Value;
                reward.Get<PositionComponent>().Value = entity.Get<PositionComponent>().Value + rewardOffset + GetRandomOffset(ref delta);
                reward.Get<TargetPositionComponent>().Value = _player.Get2(i).Value + rewardOffset;
            }
        }

        private Vector3 GetRandomOffset(ref Vector3 delta)
        {
            return new Vector3((delta.x > 0 ? -1 : 1) * Random.Range(-5f, 5f), 0, (delta.z > 0 ? -1 : 1) * Random.Range(-5f, 5f));
        }
    }
}