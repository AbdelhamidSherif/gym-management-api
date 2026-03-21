
namespace Gym.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }

    protected void SetUpdated() => UpdatedAt = DateTime.UtcNow;

}
