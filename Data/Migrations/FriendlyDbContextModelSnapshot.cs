using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Migrations;

[DbContext(typeof(FriendlyDbContext))]
internal class FriendlyDbContextModelSnapshot : ModelSnapshot
{
	protected override void BuildModel(ModelBuilder modelBuilder)
	{
		modelBuilder.HasAnnotation("ProductVersion", "10.0.3");
		modelBuilder.Entity("Data.Entities.Customer", delegate(EntityTypeBuilder b)
		{
			b.Property<long>("Id").ValueGeneratedOnAdd().HasColumnType("INTEGER");
			b.Property<string>("CreatedAt").IsRequired().HasColumnType("TEXT");
			b.Property<bool?>("EligibleForHat").HasColumnType("INTEGER");
			b.Property<string>("Name").IsRequired().HasColumnType("TEXT");
			b.Property<string>("UpdatedAt").IsRequired().HasColumnType("TEXT");
			b.HasKey("Id");
			b.ToTable("Customer");
			b.HasData(new
			{
				Id = 1L,
				CreatedAt = "1970-01-01 00:00:00",
				Name = "Acme Corp",
				UpdatedAt = "1970-01-01 00:00:00"
			}, new
			{
				Id = 2L,
				CreatedAt = "1970-01-01 00:00:00",
				Name = "Globex Inc",
				UpdatedAt = "1970-01-01 00:00:00"
			}, new
			{
				Id = 3L,
				CreatedAt = "1970-01-01 00:00:00",
				Name = "Initech",
				UpdatedAt = "1970-01-01 00:00:00"
			}, new
			{
				Id = 4L,
				CreatedAt = "1970-01-01 00:00:00",
				Name = "Umbrella Corp",
				UpdatedAt = "1970-01-01 00:00:00"
			}, new
			{
				Id = 5L,
				CreatedAt = "1970-01-01 00:00:00",
				Name = "Stark Industries",
				UpdatedAt = "1970-01-01 00:00:00"
			});
		});
		modelBuilder.Entity("Data.Entities.Load", delegate(EntityTypeBuilder b)
		{
			b.Property<long>("Id").ValueGeneratedOnAdd().HasColumnType("INTEGER");
			b.Property<long>("CustomerId").HasColumnType("INTEGER");
			b.HasKey("Id");
			b.HasIndex("CustomerId");
			b.ToTable("Load");
		});
		modelBuilder.Entity("Data.Entities.Stop", delegate(EntityTypeBuilder b)
		{
			b.Property<long>("Id").ValueGeneratedOnAdd().HasColumnType("INTEGER");
			b.Property<long>("LoadId").HasColumnType("INTEGER");
			b.Property<int>("SequenceNumber").HasColumnType("INTEGER");
			b.ComplexProperty(typeof(Dictionary<string, object>), "AppointmentWindow", "Data.Entities.Stop.AppointmentWindow#LocalTimeWindow", delegate(ComplexPropertyBuilder complexPropertyBuilder)
			{
				complexPropertyBuilder.IsRequired();
				complexPropertyBuilder.Property<string>("End").IsRequired().HasColumnType("TEXT")
					.HasColumnName("AppointmentLocalEndTime");
				complexPropertyBuilder.Property<string>("Start").IsRequired().HasColumnType("TEXT")
					.HasColumnName("AppointmentLocalStartTime");
				complexPropertyBuilder.Property<string>("ZoneId").IsRequired().HasColumnType("TEXT")
					.HasColumnName("AppointmentTimeZoneId");
			});
			b.HasKey("Id");
			b.HasIndex("LoadId");
			b.ToTable("Stop");
		});
		modelBuilder.Entity("Data.Entities.Load", delegate(EntityTypeBuilder b)
		{
			b.HasOne("Data.Entities.Customer", "Customer").WithMany("Loads").HasForeignKey("CustomerId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.Navigation("Customer");
		});
		modelBuilder.Entity("Data.Entities.Stop", delegate(EntityTypeBuilder b)
		{
			b.HasOne("Data.Entities.Load", "Load").WithMany("Stops").HasForeignKey("LoadId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.Navigation("Load");
		});
		modelBuilder.Entity("Data.Entities.Customer", delegate(EntityTypeBuilder b)
		{
			b.Navigation("Loads");
		});
		modelBuilder.Entity("Data.Entities.Load", delegate(EntityTypeBuilder b)
		{
			b.Navigation("Stops");
		});
	}
}
