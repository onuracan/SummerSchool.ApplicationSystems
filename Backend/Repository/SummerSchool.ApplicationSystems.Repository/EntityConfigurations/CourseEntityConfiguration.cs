using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SummerSchool.ApplicationSystems.Core.Entities;
using SummerSchool.ApplicationSystems.Core.Enums;

namespace SummerSchool.ApplicationSystems.Repository.EntityConfigurations;

public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("COURSE");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("ID").IsRequired();
        builder.Property(x => x.Code).HasColumnName("CODE").IsRequired().HasMaxLength(50);
        builder.Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(200);
        builder.Property(x => x.Department).HasColumnName("DEPARTMENT").IsRequired().HasMaxLength(200);
        builder.Property(x => x.Faculty).HasColumnName("FACULTY").IsRequired().HasMaxLength(200);
        builder.Property(x => x.Quota).HasColumnName("QUOTA").IsRequired();
        builder.Property(x => x.IsActive).HasColumnName("IS_ACTIVE").IsRequired();
        builder.Property(x => x.InsertedUser).HasColumnName("ADDED_USER").IsRequired().HasMaxLength(10);
        builder.Property(x => x.InsertedDate).HasColumnName("ADDED_DATE").IsRequired();
        builder.Property(x => x.UpdatedUser).HasColumnName("UPDATED_USER").HasMaxLength(10);
        builder.Property(x => x.UpdatedDate).HasColumnName("UPDATED_DATE");

        builder.HasQueryFilter(x => x.IsActive == (int)ActiveFlag.Active);
    }
}
