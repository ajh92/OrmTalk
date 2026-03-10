using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;

namespace Data.Migrations;

[DbContext(typeof(FriendlyDbContext))]
[Migration("20260309035550_InitialCreate")]
public class InitialCreate : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable("Customer", (ColumnsBuilder table) => new
		{
			Id = table.Column<long>("INTEGER").Annotation("Sqlite:Autoincrement", true),
			Name = table.Column<string>("TEXT"),
			EligibleForHat = table.Column<bool>("INTEGER", null, null, rowVersion: false, null, nullable: true),
			CreatedAt = table.Column<string>("TEXT"),
			UpdatedAt = table.Column<string>("TEXT")
		}, null, table =>
		{
			table.PrimaryKey("PK_Customer", x => x.Id);
		});
		migrationBuilder.CreateTable("Load", (ColumnsBuilder table) => new
		{
			Id = table.Column<long>("INTEGER").Annotation("Sqlite:Autoincrement", true),
			CustomerId = table.Column<long>("INTEGER")
		}, null, table =>
		{
			table.PrimaryKey("PK_Load", x => x.Id);
			table.ForeignKey("FK_Load_Customer_CustomerId", x => x.CustomerId, "Customer", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
		});
		migrationBuilder.CreateTable("Stop", (ColumnsBuilder table) => new
		{
			Id = table.Column<long>("INTEGER").Annotation("Sqlite:Autoincrement", true),
			LoadId = table.Column<long>("INTEGER"),
			SequenceNumber = table.Column<int>("INTEGER"),
			AppointmentLocalEndTime = table.Column<string>("TEXT"),
			AppointmentLocalStartTime = table.Column<string>("TEXT"),
			AppointmentTimeZoneId = table.Column<string>("TEXT")
		}, null, table =>
		{
			table.PrimaryKey("PK_Stop", x => x.Id);
			table.ForeignKey("FK_Stop_Load_LoadId", x => x.LoadId, "Load", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
		});
		migrationBuilder.InsertData("Customer", new string[5] { "Id", "CreatedAt", "EligibleForHat", "Name", "UpdatedAt" }, new object[5, 5]
		{
			{ 1L, "1970-01-01 00:00:00", null, "Acme Corp", "1970-01-01 00:00:00" },
			{ 2L, "1970-01-01 00:00:00", null, "Globex Inc", "1970-01-01 00:00:00" },
			{ 3L, "1970-01-01 00:00:00", null, "Initech", "1970-01-01 00:00:00" },
			{ 4L, "1970-01-01 00:00:00", null, "Umbrella Corp", "1970-01-01 00:00:00" },
			{ 5L, "1970-01-01 00:00:00", null, "Stark Industries", "1970-01-01 00:00:00" }
		});
		migrationBuilder.CreateIndex("IX_Load_CustomerId", "Load", "CustomerId");
		migrationBuilder.CreateIndex("IX_Stop_LoadId", "Stop", "LoadId");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable("Stop");
		migrationBuilder.DropTable("Load");
		migrationBuilder.DropTable("Customer");
	}

	protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
