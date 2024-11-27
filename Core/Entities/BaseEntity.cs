using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public abstract class BaseEntity
{
	[Column("Id")]
	public long Id { get; set; }
}
