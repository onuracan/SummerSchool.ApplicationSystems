using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SummerSchool.ApplicationSystems.Core.Entities;
using SummerSchool.ApplicationSystems.Core.Enums;

namespace SummerSchool.ApplicationSystems.Repository.EntityConfigurations;

public class OtpVerificationEntityConfiguration : IEntityTypeConfiguration<OtpVerification>
{
    public void Configure(EntityTypeBuilder<OtpVerification> builder)
    {
        builder.ToTable("OTP_VERIFICATION");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("ID").IsRequired();
        builder.Property(x => x.PhoneNumber).HasColumnName("PHONE_NUMBER").IsRequired().HasMaxLength(20);
        builder.Property(x => x.Code).HasColumnName("CODE").IsRequired().HasMaxLength(6);
        builder.Property(x => x.InsertedDate).HasColumnName("ADDED_DATE").IsRequired();
        builder.Property(x => x.IsActive).HasColumnName("IS_ACTIVE").IsRequired();

        builder.HasIndex(x => x.PhoneNumber);

        builder.HasQueryFilter(x => x.IsActive == (int)ActiveFlag.Active);
    }
}
