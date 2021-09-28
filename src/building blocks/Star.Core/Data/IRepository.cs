using Star.Core.DomainObjects;
using System;

namespace Star.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {

    }
}