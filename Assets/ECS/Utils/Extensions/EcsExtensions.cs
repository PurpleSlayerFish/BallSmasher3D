﻿using DataBase.Game;
using ECS.Core.Utils.ReactiveSystem.Components;
using ECS.Game.Components;
using ECS.Game.Components.Events;
using ECS.Game.Components.Input;
using ECS.Views;
using Leopotam.Ecs;
using PdUtils;

namespace ECS.Utils.Extensions
{
    public static class EcsExtensions
    {
        public static EcsEntity GetEntity<T>(this EcsWorld world) where T : struct
        {
            var filter = world.GetFilter(typeof(EcsFilter<T>));
            foreach (var i in filter)
                return filter.GetEntity(i);
            return default;
        }

        public static void SetStage(this EcsWorld world, EGameStage value) => world.GetGameStage().Get<ChangeStageComponent>().Value = value;

        public static EcsEntity GetGameStage(this EcsWorld world)
        {
            var filter = world.GetFilter(typeof(EcsFilter<GameStageComponent>));
            return filter.GetEntity(0);
        }
        
        public static ref T GetAndFire<T>(this ref EcsEntity entity) where T : struct
        {
            entity.Get<T>();
            entity.Get<EventAddComponent<T>>();
            return ref entity.Get<T>();
        }
        
        public static void DelAndFire<T>(this ref EcsEntity entity) where T : struct
        {
            entity.Del<T>();
            entity.Get<EventRemoveComponent<T>>();
        }
        
        public static ref T ReloadAndFire<T>(this ref EcsEntity entity) where T : struct
        {
            entity.Del<T>();
            entity.Get<T>();
            entity.Get<EventAddComponent<T>>();
            return ref entity.Get<T>();
        }
        
        public static void LinkView(this ref EcsEntity entity, ILinkable ILinkable)
        {
            ILinkable.Link(entity);
        }
        
        public static T GetView<T>(this ref EcsEntity entity) where T : ILinkable
        {
            return (T) entity.Get<LinkComponent>().View;
        }
        
        public static EcsEntity GetEntityWithUid(this EcsWorld world, Uid uid)
        {
            var value = new EcsEntity();
            var filter = world.GetFilter(typeof(EcsFilter<UIdComponent>));
            foreach (var i in filter)
            {
                ref var entity = ref filter.GetEntity(i);
                if (uid.Equals(entity.Get<UIdComponent>().Value))
                    return entity;
            }
            return value;
        }

        public static void DeclareOneFrameEvents(this EcsSystems systems)
        {
            systems.OneFrame<TimerTickEventComponent>();
            systems.OneFrame<PointerUpComponent>();
            systems.OneFrame<PointerDragComponent>();
            systems.OneFrame<PointerDownComponent>();
        }
    }
}