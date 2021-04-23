using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Shared
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; protected set; }
        public DateTime CreatedTime { get; protected set; }

        protected BaseEntity(TId id)
        {
            if (object.Equals(id, default(TId)))
            {
                throw new ArgumentException("The ID cannot be the type's default value.", "id");
            }
            Id = id;
            
        }
        protected BaseEntity() {
            CreatedTime = DateTime.Now;
        }
    }
}
