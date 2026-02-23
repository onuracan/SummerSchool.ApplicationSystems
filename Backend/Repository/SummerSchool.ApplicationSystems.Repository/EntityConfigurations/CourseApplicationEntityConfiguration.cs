using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SummerSchool.ApplicationSystems.Core.Entities;
using SummerSchool.ApplicationSystems.Core.Enums;

namespace SummerSchool.ApplicationSystems.Repository.EntityConfigurations;

public class CourseApplicationEntityConfiguration : IEntityTypeConfiguration<CourseApplication>
{
    public void Configure(EntityTypeBuilder<CourseApplication> builder)
    {
        builder.ToTable("COURSE_APPLICATION");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("ID").IsRequired();
        builder.Property(x => x.StudentId).HasColumnName("STUDENT_ID").IsRequired();
        builder.Property(x => x.CourseId).HasColumnName("COURSE_ID").IsRequired();
        builder.Property(x => x.ApplicationStatus).HasColumnName("APPLICATION_STATUS").IsRequired();
        builder.Property(x => x.IsActive).HasColumnName("IS_ACTIVE").IsRequired();
        builder.Property(x => x.UpdatedUser).HasColumnName("UPDATED_USER").HasMaxLength(10);
        builder.Property(x => x.UpdatedDate).HasColumnName("UPDATED_DATE");

        builder.HasOne(x => x.Student).WithMany(x => x.CourseApplications).HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Course).WithMany(x => x.CourseApplications).HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.StudentId, x.CourseId }).IsUnique();

        builder.HasQueryFilter(x => x.IsActive == (int)ActiveFlag.Active);
    }
}
