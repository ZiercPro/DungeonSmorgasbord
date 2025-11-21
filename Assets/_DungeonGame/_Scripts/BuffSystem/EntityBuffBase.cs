using ZiercCode._DungeonGame._Scripts.EntityClasses;

namespace ZiercCode._DungeonGame._Scripts.BuffSystem
{
    public abstract class EntityBuffBase : BuffBase
    {
        protected Entity MyEntity => (Entity)Holder;
    }
}