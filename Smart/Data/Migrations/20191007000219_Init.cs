using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Smart.data.migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 32, nullable: false),
                    LastName = table.Column<string>(maxLength: 32, nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "FileType",
                columns: table => new
                {
                    FileTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileType", x => x.FileTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RatingCirterium",
                columns: table => new
                {
                    RatingCirteriumId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 512, nullable: false),
                    MaxScore = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingCirterium", x => x.RatingCirteriumId);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DayOfWeek = table.Column<byte>(nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(0)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.ScheduleId);
                });

            migrationBuilder.CreateTable(
                name: "Term",
                columns: table => new
                {
                    TermId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 128, nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    TimeOfYear = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Term", x => x.TermId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Error",
                columns: table => new
                {
                    ErrorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(nullable: true),
                    StackTrace = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Error", x => x.ErrorId);
                    table.ForeignKey(
                        name: "FK_Error_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Event_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseTerm",
                columns: table => new
                {
                    CourseTermId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseId = table.Column<int>(nullable: false),
                    TermId = table.Column<int>(nullable: false),
                    Capacity = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTerm", x => x.CourseTermId);
                    table.ForeignKey(
                        name: "FK_CourseTerm_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTerm_Term_TermId",
                        column: x => x.TermId,
                        principalTable: "Term",
                        principalColumn: "TermId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assessment",
                columns: table => new
                {
                    AssessmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseTermId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PointsPossible = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessment", x => x.AssessmentId);
                    table.ForeignKey(
                        name: "FK_Assessment_CourseTerm_CourseTermId",
                        column: x => x.CourseTermId,
                        principalTable: "CourseTerm",
                        principalColumn: "CourseTermId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseTermInstructor",
                columns: table => new
                {
                    CourseTermId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTermInstructor", x => new { x.CourseTermId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CourseTermInstructor_CourseTerm_CourseTermId",
                        column: x => x.CourseTermId,
                        principalTable: "CourseTerm",
                        principalColumn: "CourseTermId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTermInstructor_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseTermSchedule",
                columns: table => new
                {
                    CourseTermId = table.Column<int>(nullable: false),
                    ScheduleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTermSchedule", x => new { x.CourseTermId, x.ScheduleId });
                    table.ForeignKey(
                        name: "FK_CourseTermSchedule_CourseTerm_CourseTermId",
                        column: x => x.CourseTermId,
                        principalTable: "CourseTerm",
                        principalColumn: "CourseTermId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTermSchedule_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 32, nullable: false),
                    LastName = table.Column<string>(maxLength: 32, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    Address = table.Column<string>(maxLength: 128, nullable: true),
                    Village = table.Column<string>(maxLength: 64, nullable: true),
                    LocationLattitude = table.Column<decimal>(nullable: false),
                    LocationLongitude = table.Column<decimal>(nullable: false),
                    PublicSchoolLevel = table.Column<byte>(nullable: false),
                    GuardianName = table.Column<string>(maxLength: 64, nullable: true),
                    Phone = table.Column<string>(maxLength: 16, nullable: true),
                    Photo = table.Column<string>(maxLength: 256, nullable: true),
                    IsEnrolled = table.Column<bool>(nullable: false),
                    IsWaitlisted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CourseTermId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Student_CourseTerm_CourseTermId",
                        column: x => x.CourseTermId,
                        principalTable: "CourseTerm",
                        principalColumn: "CourseTermId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantRating",
                columns: table => new
                {
                    ApplicantRatingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    RatingCiteriumId = table.Column<int>(nullable: false),
                    TermId = table.Column<int>(nullable: false),
                    ScoreAssigned = table.Column<int>(nullable: false),
                    RatingCirteriumId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantRating", x => x.ApplicantRatingId);
                    table.ForeignKey(
                        name: "FK_ApplicantRating_RatingCirterium_RatingCirteriumId",
                        column: x => x.RatingCirteriumId,
                        principalTable: "RatingCirterium",
                        principalColumn: "RatingCirteriumId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicantRating_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicantRating_Term_TermId",
                        column: x => x.TermId,
                        principalTable: "Term",
                        principalColumn: "TermId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicantRating_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    CourseTermId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => new { x.StudentId, x.CourseTermId, x.Date });
                    table.ForeignKey(
                        name: "FK_Attendance_CourseTerm_CourseTermId",
                        column: x => x.CourseTermId,
                        principalTable: "CourseTerm",
                        principalColumn: "CourseTermId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendance_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendance_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    FileId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(nullable: false),
                    FileTypeId = table.Column<int>(nullable: false),
                    Path = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_File_FileType_FileTypeId",
                        column: x => x.FileTypeId,
                        principalTable: "FileType",
                        principalColumn: "FileTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_File_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    NoteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Studentid = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_Note_Student_Studentid",
                        column: x => x.Studentid,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Note_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAssessment",
                columns: table => new
                {
                    AssessmentId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    PointsAwarded = table.Column<int>(nullable: false),
                    Comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAssessment", x => new { x.AssessmentId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_StudentAssessment_Assessment_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessment",
                        principalColumn: "AssessmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAssessment_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourseTerm",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    CourseTermId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourseTerm", x => new { x.CourseTermId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_StudentCourseTerm_CourseTerm_CourseTermId",
                        column: x => x.CourseTermId,
                        principalTable: "CourseTerm",
                        principalColumn: "CourseTermId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourseTerm_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentPublicSchoolCourse",
                columns: table => new
                {
                    StudentPublicSchoolCourseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(nullable: false),
                    CourseName = table.Column<string>(maxLength: 128, nullable: false),
                    TimeOfYear = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentPublicSchoolCourse", x => x.StudentPublicSchoolCourseId);
                    table.ForeignKey(
                        name: "FK_StudentPublicSchoolCourse_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicSchoolCourseSchedule",
                columns: table => new
                {
                    PublicSchoolCourseScheduleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentPublicSchoolCourseId = table.Column<int>(nullable: false),
                    ScheduleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicSchoolCourseSchedule", x => x.PublicSchoolCourseScheduleId);
                    table.ForeignKey(
                        name: "FK_PublicSchoolCourseSchedule_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicSchoolCourseSchedule_StudentPublicSchoolCourse_StudentPublicSchoolCourseId",
                        column: x => x.StudentPublicSchoolCourseId,
                        principalTable: "StudentPublicSchoolCourse",
                        principalColumn: "StudentPublicSchoolCourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantRating_RatingCirteriumId",
                table: "ApplicantRating",
                column: "RatingCirteriumId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantRating_StudentId",
                table: "ApplicantRating",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantRating_TermId",
                table: "ApplicantRating",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantRating_UserId",
                table: "ApplicantRating",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_CourseTermId",
                table: "Assessment",
                column: "CourseTermId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_CourseTermId",
                table: "Attendance",
                column: "CourseTermId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_UserId",
                table: "Attendance",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTerm_CourseId",
                table: "CourseTerm",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTerm_TermId",
                table: "CourseTerm",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTermInstructor_UserId",
                table: "CourseTermInstructor",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTermSchedule_ScheduleId",
                table: "CourseTermSchedule",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Error_UserId",
                table: "Error",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_UserId",
                table: "Event",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_File_FileTypeId",
                table: "File",
                column: "FileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_File_StudentId",
                table: "File",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_Studentid",
                table: "Note",
                column: "Studentid");

            migrationBuilder.CreateIndex(
                name: "IX_Note_UserId",
                table: "Note",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicSchoolCourseSchedule_ScheduleId",
                table: "PublicSchoolCourseSchedule",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicSchoolCourseSchedule_StudentPublicSchoolCourseId",
                table: "PublicSchoolCourseSchedule",
                column: "StudentPublicSchoolCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_CourseTermId",
                table: "Student",
                column: "CourseTermId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssessment_StudentId",
                table: "StudentAssessment",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseTerm_StudentId",
                table: "StudentCourseTerm",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPublicSchoolCourse_StudentId",
                table: "StudentPublicSchoolCourse",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicantRating");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "CourseTermInstructor");

            migrationBuilder.DropTable(
                name: "CourseTermSchedule");

            migrationBuilder.DropTable(
                name: "Error");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "PublicSchoolCourseSchedule");

            migrationBuilder.DropTable(
                name: "StudentAssessment");

            migrationBuilder.DropTable(
                name: "StudentCourseTerm");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "RatingCirterium");

            migrationBuilder.DropTable(
                name: "FileType");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "StudentPublicSchoolCourse");

            migrationBuilder.DropTable(
                name: "Assessment");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "CourseTerm");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Term");
        }
    }
}
