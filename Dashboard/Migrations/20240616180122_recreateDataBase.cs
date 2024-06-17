using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dashboard.Migrations
{
    /// <inheritdoc />
    public partial class recreateDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Clinic_ClinicId",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorWorkingDay_Doctor_DoctorId",
                table: "DoctorWorkingDay");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorWorkingDay_WorkingDay_WorkingDayId",
                table: "DoctorWorkingDay");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientAppointment_Doctor_DoctorId",
                table: "PatientAppointment");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientAppointment_Patient_PatientId",
                table: "PatientAppointment");

            migrationBuilder.AddColumn<int>(
                name: "WorkingDayId",
                table: "WorkingDay",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartDate",
                table: "PatientAppointment",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndDate",
                table: "PatientAppointment",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "PatientAppointment",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "WorkingDayId",
                table: "PatientAppointment",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "WorkingDay",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "WorkingDayId" },
                values: new object[] { "Sunday", null });

            migrationBuilder.UpdateData(
                table: "WorkingDay",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "WorkingDayId" },
                values: new object[] { "Monday", null });

            migrationBuilder.UpdateData(
                table: "WorkingDay",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "WorkingDayId" },
                values: new object[] { "Tuesday", null });

            migrationBuilder.UpdateData(
                table: "WorkingDay",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Name", "WorkingDayId" },
                values: new object[] { "Wednesday", null });

            migrationBuilder.UpdateData(
                table: "WorkingDay",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "WorkingDayId" },
                values: new object[] { "Thursday", null });

            migrationBuilder.UpdateData(
                table: "WorkingDay",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Name", "WorkingDayId" },
                values: new object[] { "Friday", null });

            migrationBuilder.UpdateData(
                table: "WorkingDay",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Name", "WorkingDayId" },
                values: new object[] { "Saturday", null });

            migrationBuilder.CreateIndex(
                name: "IX_WorkingDay_WorkingDayId",
                table: "WorkingDay",
                column: "WorkingDayId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAppointment_WorkingDayId",
                table: "PatientAppointment",
                column: "WorkingDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Clinic_ClinicId",
                table: "Doctor",
                column: "ClinicId",
                principalTable: "Clinic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorWorkingDay_Doctor_DoctorId",
                table: "DoctorWorkingDay",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorWorkingDay_WorkingDay_WorkingDayId",
                table: "DoctorWorkingDay",
                column: "WorkingDayId",
                principalTable: "WorkingDay",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointment_Doctor_DoctorId",
                table: "PatientAppointment",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointment_Patient_PatientId",
                table: "PatientAppointment",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointment_WorkingDay_WorkingDayId",
                table: "PatientAppointment",
                column: "WorkingDayId",
                principalTable: "WorkingDay",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingDay_WorkingDay_WorkingDayId",
                table: "WorkingDay",
                column: "WorkingDayId",
                principalTable: "WorkingDay",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Clinic_ClinicId",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorWorkingDay_Doctor_DoctorId",
                table: "DoctorWorkingDay");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorWorkingDay_WorkingDay_WorkingDayId",
                table: "DoctorWorkingDay");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientAppointment_Doctor_DoctorId",
                table: "PatientAppointment");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientAppointment_Patient_PatientId",
                table: "PatientAppointment");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientAppointment_WorkingDay_WorkingDayId",
                table: "PatientAppointment");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingDay_WorkingDay_WorkingDayId",
                table: "WorkingDay");

            migrationBuilder.DropIndex(
                name: "IX_WorkingDay_WorkingDayId",
                table: "WorkingDay");

            migrationBuilder.DropIndex(
                name: "IX_PatientAppointment_WorkingDayId",
                table: "PatientAppointment");

            migrationBuilder.DropColumn(
                name: "WorkingDayId",
                table: "WorkingDay");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "PatientAppointment");

            migrationBuilder.DropColumn(
                name: "WorkingDayId",
                table: "PatientAppointment");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "PatientAppointment",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "PatientAppointment",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.UpdateData(
                table: "WorkingDay",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Saturday");

            migrationBuilder.UpdateData(
                table: "WorkingDay",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Sunday");

            migrationBuilder.UpdateData(
                table: "WorkingDay",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Monday");

            migrationBuilder.UpdateData(
                table: "WorkingDay",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Tuesday");

            migrationBuilder.UpdateData(
                table: "WorkingDay",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Wednesday");

            migrationBuilder.UpdateData(
                table: "WorkingDay",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Thursday");

            migrationBuilder.UpdateData(
                table: "WorkingDay",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Friday");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Clinic_ClinicId",
                table: "Doctor",
                column: "ClinicId",
                principalTable: "Clinic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorWorkingDay_Doctor_DoctorId",
                table: "DoctorWorkingDay",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorWorkingDay_WorkingDay_WorkingDayId",
                table: "DoctorWorkingDay",
                column: "WorkingDayId",
                principalTable: "WorkingDay",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointment_Doctor_DoctorId",
                table: "PatientAppointment",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointment_Patient_PatientId",
                table: "PatientAppointment",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
