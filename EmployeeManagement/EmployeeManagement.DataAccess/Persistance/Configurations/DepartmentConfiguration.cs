using EmployeeManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.DataAccess.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder
            .Property(e => e.Name)
            .IsRequired();


        builder
            .ToTable("Departments");

        builder
            .HasData(
                new Department
                {
                    Id = 1,
                    Name = "IT",
                    CreatedOn = new DateTime(2023, 12, 19),
                    LastModifiedOn = new DateTime(2023, 12, 19),
                    LastModifiedBy = "Seeding",
                    CreatedBy = "Seeding",
                },
                new Department
                {
                    Id = 2,
                    Name = "HR",
                    CreatedOn = new DateTime(2023, 12, 19),
                    LastModifiedOn = new DateTime(2023, 12, 19),
                    LastModifiedBy = "Seeding",
                    CreatedBy = "Seeding",
                },
                new Department
                {
                    Id = 3,
                    Name = "Marketing",
                    CreatedOn = new DateTime(2023, 12, 19),
                    LastModifiedOn = new DateTime(2023, 12, 19),
                    LastModifiedBy = "Seeding",
                    CreatedBy = "Seeding",
                });
    }
}

