using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SummerSchool.ApplicationSystems.Core.Entities;
using SummerSchool.ApplicationSystems.Core.Enums;

namespace SummerSchool.ApplicationSystems.Repository.EntityConfigurations;

public class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("STUDENT");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("ID").IsRequired();
        builder.Property(x => x.FirstName).HasColumnName("FIRST_NAME").IsRequired().HasMaxLength(100);
        builder.Property(x => x.LastName).HasColumnName("LAST_NAME").IsRequired().HasMaxLength(100);
        builder.Property(x => x.IdentityNumber).HasColumnName("IDENTITY_NUMBER").IsRequired().HasMaxLength(11);
        builder.Property(x => x.SchoolNumber).HasColumnName("SCHOOL_NUMBER").HasMaxLength(50);
        builder.Property(x => x.Department).HasColumnName("DEPARTMENT").IsRequired().HasMaxLength(200);
        builder.Property(x => x.Faculty).HasColumnName("FACULTY").IsRequired().HasMaxLength(200);
        builder.Property(x => x.PhoneNumber).HasColumnName("PHONE_NUMBER").IsRequired().HasMaxLength(20);
        builder.Property(x => x.EMail).HasColumnName("EMAIL").IsRequired().HasMaxLength(200);
        builder.Property(x => x.CountryId).HasColumnName("COUNTRY_ID").IsRequired();
        builder.Property(x => x.IsActive).HasColumnName("IS_ACTIVE").IsRequired();
        
        builder.HasMany(x => x.CourseApplications).WithOne(x => x.Student).HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => x.IsActive == (int)ActiveFlag.Active);
    }
}
