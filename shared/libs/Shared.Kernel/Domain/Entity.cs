using System;

namespace Shared.Kernel.Domain
{
    /// <summary>
    /// Base entity with a Guid identifier and value/equality helpers.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Unique identifier for the entity.
        /// </summary>
        public Guid Id { get; protected set; }

        /// <summary>
        /// Protected constructor initializes a new Guid by default.
        /// Derived types can set the Id explicitly when needed (rehydration).
        /// </summary>
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (obj is not Entity other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Id == other.Id;
        }

        /// <inheritdoc />
        public override int GetHashCode() => Id.GetHashCode();

        /// <summary>
        /// Equality operator for entities.
        /// </summary>
        public static bool operator ==(Entity? a, Entity? b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (a is null || b is null)
                return false;

            return a.Id == b.Id;
        }

        /// <summary>
        /// Inequality operator for entities.
        /// </summary>
        public static bool operator !=(Entity? a, Entity? b) => !(a == b);
    }
}

