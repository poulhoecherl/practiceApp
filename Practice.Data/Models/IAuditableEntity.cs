using System;

namespace Practice.Data.Models
{
    public interface IAuditableEntity
    {
        DateTime RowCreatedOn { get; set; }
        string RowCreatedBy { get; set; }
        DateTime? RowModifiedOn { get; set; }
        string? RowModifiedBy { get; set; }
    }
}