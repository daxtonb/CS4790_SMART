﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Smart.Data;

namespace Smart.data.migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Smart.Data.Models.ApplicantRating", b =>
                {
                    b.Property<int>("ApplicantRatingId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments");

                    b.Property<DateTime>("DateTime");

                    b.Property<int?>("RatingCirteriumId");

                    b.Property<int>("RatingCiteriumId");

                    b.Property<int>("ScoreAssigned");

                    b.Property<int>("StudentId");

                    b.Property<int>("TermId");

                    b.Property<int>("UserId");

                    b.HasKey("ApplicantRatingId");

                    b.HasIndex("RatingCirteriumId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TermId");

                    b.HasIndex("UserId");

                    b.ToTable("ApplicantRating");
                });

            modelBuilder.Entity("Smart.Data.Models.Assessment", b =>
                {
                    b.Property<int>("AssessmentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClassId");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("date");

                    b.Property<string>("Description");

                    b.Property<int>("PointsPossible");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("AssessmentId");

                    b.HasIndex("ClassId");

                    b.ToTable("Assessment");
                });

            modelBuilder.Entity("Smart.Data.Models.Attendance", b =>
                {
                    b.Property<int>("StudentId");

                    b.Property<int>("ClassId");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<int>("AttendanceStatusId");

                    b.Property<TimeSpan?>("TimeIn")
                        .HasColumnType("time(0)");

                    b.HasKey("StudentId", "ClassId", "Date");

                    b.HasIndex("AttendanceStatusId");

                    b.HasIndex("ClassId");

                    b.ToTable("Attendance");
                });

            modelBuilder.Entity("Smart.Data.Models.AttendanceStatus", b =>
                {
                    b.Property<int>("AttendanceStatusId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("AttendanceStatusId");

                    b.ToTable("AttendanceStatus");
                });

            modelBuilder.Entity("Smart.Data.Models.Class", b =>
                {
                    b.Property<int>("ClassId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Capacity");

                    b.Property<int>("CourseId");

                    b.Property<int>("InstructorUserId");

                    b.Property<int>("TermId");

                    b.HasKey("ClassId");

                    b.HasIndex("CourseId");

                    b.HasIndex("InstructorUserId");

                    b.HasIndex("TermId");

                    b.ToTable("Class");
                });

            modelBuilder.Entity("Smart.Data.Models.ClassSchedule", b =>
                {
                    b.Property<int>("ClassId");

                    b.Property<int>("ScheduleAvailabilityId");

                    b.HasKey("ClassId", "ScheduleAvailabilityId");

                    b.HasIndex("ScheduleAvailabilityId");

                    b.ToTable("ClassSchedule");
                });

            modelBuilder.Entity("Smart.Data.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsCoreRequirement");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("CourseId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("Smart.Data.Models.Error", b =>
                {
                    b.Property<int>("ErrorId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Message");

                    b.Property<string>("StackTrace");

                    b.Property<int?>("UserId");

                    b.HasKey("ErrorId");

                    b.HasIndex("UserId");

                    b.ToTable("Error");
                });

            modelBuilder.Entity("Smart.Data.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Description");

                    b.Property<int>("UserId");

                    b.HasKey("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("Smart.Data.Models.File", b =>
                {
                    b.Property<int>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("ByteData")
                        .IsRequired();

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int>("FileTypeId");

                    b.Property<int>("StudentId");

                    b.HasKey("FileId");

                    b.HasIndex("FileTypeId");

                    b.HasIndex("StudentId");

                    b.ToTable("File");
                });

            modelBuilder.Entity("Smart.Data.Models.FileType", b =>
                {
                    b.Property<int>("FileTypeId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("FileTypeId");

                    b.ToTable("FileType");
                });

            modelBuilder.Entity("Smart.Data.Models.Note", b =>
                {
                    b.Property<int>("NoteId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("NoteTypeId");

                    b.Property<int>("Studentid");

                    b.Property<string>("Text");

                    b.Property<int>("UserId");

                    b.HasKey("NoteId");

                    b.HasIndex("NoteTypeId");

                    b.HasIndex("Studentid");

                    b.HasIndex("UserId");

                    b.ToTable("Note");
                });

            modelBuilder.Entity("Smart.Data.Models.NoteType", b =>
                {
                    b.Property<int>("NoteTypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.HasKey("NoteTypeId");

                    b.ToTable("NoteType");
                });

            modelBuilder.Entity("Smart.Data.Models.PublicSchoolClassSchedule", b =>
                {
                    b.Property<int>("PublicSchoolClassScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ScheduleAvailabilityId");

                    b.Property<int>("StudentPublicSchoolClassId");

                    b.HasKey("PublicSchoolClassScheduleId");

                    b.HasIndex("ScheduleAvailabilityId");

                    b.HasIndex("StudentPublicSchoolClassId");

                    b.ToTable("PublicSchoolClassSchedule");
                });

            modelBuilder.Entity("Smart.Data.Models.RatingCirterium", b =>
                {
                    b.Property<int>("RatingCirteriumId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("MaxScore");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(512);

                    b.HasKey("RatingCirteriumId");

                    b.ToTable("RatingCirterium");
                });

            modelBuilder.Entity("Smart.Data.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Smart.Data.Models.ScheduleAvailability", b =>
                {
                    b.Property<int>("ScheduleAvailabilityId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("DayOfWeek");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time(0)");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time(0)");

                    b.HasKey("ScheduleAvailabilityId");

                    b.ToTable("ScheduleAvailability");
                });

            modelBuilder.Entity("Smart.Data.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(128);

                    b.Property<DateTime?>("DateOfBirth")
                        .IsRequired();

                    b.Property<byte>("EnglishLevel");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("GuardianName")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<byte>("ItLevel");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<double>("LocationLattitude");

                    b.Property<double>("LocationLongitude");

                    b.Property<string>("Phone")
                        .HasMaxLength(16);

                    b.Property<string>("Photo")
                        .HasMaxLength(256);

                    b.Property<byte>("PublicSchoolLevel");

                    b.Property<int>("StudentStatusId");

                    b.Property<string>("Village")
                        .HasMaxLength(64);

                    b.HasKey("StudentId");

                    b.HasIndex("StudentStatusId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Smart.Data.Models.StudentAssessment", b =>
                {
                    b.Property<int>("AssessmentId");

                    b.Property<int>("StudentId");

                    b.Property<string>("Comments");

                    b.Property<int?>("FileId");

                    b.Property<int>("PointsAwarded");

                    b.Property<DateTime>("SubmissionDateTime");

                    b.HasKey("AssessmentId", "StudentId");

                    b.HasIndex("FileId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentAssessment");
                });

            modelBuilder.Entity("Smart.Data.Models.StudentClass", b =>
                {
                    b.Property<int>("ClassId");

                    b.Property<int>("StudentId");

                    b.HasKey("ClassId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentClass");
                });

            modelBuilder.Entity("Smart.Data.Models.StudentPublicSchoolClass", b =>
                {
                    b.Property<int>("StudentPublicSchoolClassId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int>("StudentId");

                    b.Property<byte>("TimeOfYear");

                    b.HasKey("StudentPublicSchoolClassId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentPublicSchoolClass");
                });

            modelBuilder.Entity("Smart.Data.Models.StudentStatus", b =>
                {
                    b.Property<int>("StudentStatusId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("StudentStatusId");

                    b.ToTable("StudentStatus");
                });

            modelBuilder.Entity("Smart.Data.Models.Term", b =>
                {
                    b.Property<int>("TermId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.Property<byte>("TimeOfYear");

                    b.HasKey("TermId");

                    b.ToTable("Term");
                });

            modelBuilder.Entity("Smart.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Smart.Data.Models.UserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Smart.Data.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Smart.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Smart.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Smart.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Smart.Data.Models.ApplicantRating", b =>
                {
                    b.HasOne("Smart.Data.Models.RatingCirterium", "RatingCirterium")
                        .WithMany("ApplicantRatings")
                        .HasForeignKey("RatingCirteriumId");

                    b.HasOne("Smart.Data.Models.Student", "Student")
                        .WithMany("ApplicantRatings")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Smart.Data.Models.Term", "Term")
                        .WithMany()
                        .HasForeignKey("TermId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Smart.Data.Models.User", "User")
                        .WithMany("ApplicantRatings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Smart.Data.Models.Assessment", b =>
                {
                    b.HasOne("Smart.Data.Models.Class", "Class")
                        .WithMany("Assessments")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Smart.Data.Models.Attendance", b =>
                {
                    b.HasOne("Smart.Data.Models.AttendanceStatus", "AttendanceStatus")
                        .WithMany("Attendances")
                        .HasForeignKey("AttendanceStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Smart.Data.Models.Class", "Class")
                        .WithMany("Attendances")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Smart.Data.Models.Student", "Student")
                        .WithMany("Attendances")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Smart.Data.Models.Class", b =>
                {
                    b.HasOne("Smart.Data.Models.Course", "Course")
                        .WithMany("Classes")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Smart.Data.Models.User", "InstructorUser")
                        .WithMany("Classes")
                        .HasForeignKey("InstructorUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Smart.Data.Models.Term", "Term")
                        .WithMany("Classes")
                        .HasForeignKey("TermId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Smart.Data.Models.ClassSchedule", b =>
                {
                    b.HasOne("Smart.Data.Models.Class", "Class")
                        .WithMany("ClassSchedules")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Smart.Data.Models.ScheduleAvailability", "ScheduleAvailability")
                        .WithMany("ClassSchedules")
                        .HasForeignKey("ScheduleAvailabilityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Smart.Data.Models.Error", b =>
                {
                    b.HasOne("Smart.Data.Models.User", "User")
                        .WithMany("Errors")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Smart.Data.Models.Event", b =>
                {
                    b.HasOne("Smart.Data.Models.User", "User")
                        .WithMany("Events")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Smart.Data.Models.File", b =>
                {
                    b.HasOne("Smart.Data.Models.FileType", "FileType")
                        .WithMany("Files")
                        .HasForeignKey("FileTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Smart.Data.Models.Student", "Student")
                        .WithMany("Files")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Smart.Data.Models.Note", b =>
                {
                    b.HasOne("Smart.Data.Models.NoteType", "NoteType")
                        .WithMany("Notes")
                        .HasForeignKey("NoteTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Smart.Data.Models.Student", "Student")
                        .WithMany("Notes")
                        .HasForeignKey("Studentid")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Smart.Data.Models.User", "User")
                        .WithMany("Notes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Smart.Data.Models.PublicSchoolClassSchedule", b =>
                {
                    b.HasOne("Smart.Data.Models.ScheduleAvailability", "ScheduleAvailabilityd")
                        .WithMany("PublicSchoolClassSchedules")
                        .HasForeignKey("ScheduleAvailabilityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Smart.Data.Models.StudentPublicSchoolClass", "StudentPublicSchoolClass")
                        .WithMany("PublicSchoolClassSchedules")
                        .HasForeignKey("StudentPublicSchoolClassId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Smart.Data.Models.Student", b =>
                {
                    b.HasOne("Smart.Data.Models.StudentStatus", "StudentStatus")
                        .WithMany("Students")
                        .HasForeignKey("StudentStatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Smart.Data.Models.StudentAssessment", b =>
                {
                    b.HasOne("Smart.Data.Models.Assessment", "Assessment")
                        .WithMany("StudentAssessments")
                        .HasForeignKey("AssessmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Smart.Data.Models.File", "File")
                        .WithMany()
                        .HasForeignKey("FileId");

                    b.HasOne("Smart.Data.Models.Student", "Student")
                        .WithMany("StudentAssessments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Smart.Data.Models.StudentClass", b =>
                {
                    b.HasOne("Smart.Data.Models.Class", "Class")
                        .WithMany("StudentClasses")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Smart.Data.Models.Student", "Student")
                        .WithMany("StudentClasses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Smart.Data.Models.StudentPublicSchoolClass", b =>
                {
                    b.HasOne("Smart.Data.Models.Student", "Student")
                        .WithMany("StudentPublicSchoolClasss")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Smart.Data.Models.UserRole", b =>
                {
                    b.HasOne("Smart.Data.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Smart.Data.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
