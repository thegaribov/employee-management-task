using EmployeeManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.DataAccess.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder
            .Property(e => e.Name)
            .IsRequired();

        builder
            .Property(e => e.Surname)
            .IsRequired();

        builder
            .Property(e => e.BirthDate)
            .IsRequired();

        builder
           .HasOne<Department>(employee => employee.Department)
           .WithMany(department => department.Employees)
           .HasForeignKey(employee => employee.DepartmentId)
           .OnDelete(DeleteBehavior.NoAction);

        builder
           .Property(p => p.MonthlyPayment)
           .HasColumnType("decimal(8,2)")
           .IsRequired(true);

        builder
            .ToTable("Employees");

        builder
            .HasData(
                new Employee
                {
                    Id = 1,
                    DepartmentId = 1,
                    Name = "Mahmood",
                    Surname = "Garibov",
                    BirthDate = new DateTime(2001, 1, 6),
                    Age = 22,
                    MonthlyPayment = 10000,
                    CreatedOn = new DateTime(2023, 12, 19),
                    LastModifiedOn = new DateTime(2023, 12, 19),
                    LastModifiedBy = "Seeding",
                    CreatedBy = "Seeding",
                },
                new Employee
                {
                    Id = 2,
                    DepartmentId = 1,
                    Name = "Elchin",
                    Surname = "Garibov",
                    BirthDate = new DateTime(1996, 2, 18),
                    Age = 26,
                    MonthlyPayment = 3000,
                    CreatedOn = new DateTime(2023, 12, 19),
                    LastModifiedOn = new DateTime(2023, 12, 19),
                    LastModifiedBy = "Seeding",
                    CreatedBy = "Seeding",

                },
                new Employee
                {
                    Id = 3,
                    DepartmentId = 1,
                    Name = "Eldar",
                    Surname = "Rasulov",
                    BirthDate = new DateTime(1998, 2, 25),
                    Age = 32,
                    MonthlyPayment = 5000,
                    CreatedOn = new DateTime(2023, 12, 19),
                    LastModifiedOn = new DateTime(2023, 12, 19),
                    LastModifiedBy = "Seeding",
                    CreatedBy = "Seeding",
                });
    }
}
