﻿using EasySoft.Domain.Abstractions.Events;

namespace EasySoft.Domain.Abstractions.Entities;

public abstract class Entity : IEntity
{
    public abstract object GetKey();

    public override string ToString()
    {
        return $"[Entity: {GetType().Name}] Keys = {string.Join(",", GetKey())}";
    }

    #region 领域事件的处理

    private List<IDomainEvent>? _domainEvents;
    public IReadOnlyCollection<IDomainEvent>? DomainEvents => _domainEvents?.AsReadOnly();

    public void AddDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents = _domainEvents ?? new List<IDomainEvent>();
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    #endregion
}

public abstract class Entity<TKey> : Entity, IEntity<TKey> where TKey : struct
{
    private int? _requestedHashCode;

    protected Entity()
    {
        Id = new TKey();
    }

    public virtual TKey Id { get; protected set; }

    public override object GetKey()
    {
        return new object[] { Id };
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || !(obj is Entity<TKey>))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (GetType() != obj.GetType())
            return false;

        var item = (Entity<TKey>)obj;

        if (item.IsTransient() || IsTransient())
            return false;
        else
            return item.Id.Equals(Id);
    }

    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode = Id.GetHashCode() ^ 31;

            return _requestedHashCode.Value;
        }
        else
        {
            return base.GetHashCode();
        }
    }

    //表示对象是否为全新创建的，未持久化的
    public bool IsTransient()
    {
        return EqualityComparer<TKey>.Default.Equals(Id, default);
    }

    public override string ToString()
    {
        return $"[Entity: {GetType().Name}] Id = {Id}";
    }

    public static bool operator ==(Entity<TKey>? left, Entity<TKey> right)
    {
        return left?.Equals(right) ?? Equals(right, null);
    }

    public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
    {
        return !(left == right);
    }
}