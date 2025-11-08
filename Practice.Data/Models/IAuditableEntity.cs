using System;

namespace Practice.Data.Models
{
    public interface IAuditableEntity
    {
        int Id { get; set; }

        DateTime RowCreatedOn { get; set; }
        string RowCreatedBy { get; set; }
        DateTime? RowModifiedOn { get; set; }
        string? RowModifiedBy { get; set; }

        bool Deleted { get; set; }
    }
}