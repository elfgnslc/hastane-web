﻿// <auto-generated />
using System;
using MedicalTrackingSystem.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MedicalTrackingSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("MedicalTrackingSystem.Models.Allergy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Allergen")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PatientId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Reaction")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Allergies");
                });

            modelBuilder.Entity("MedicalTrackingSystem.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("DoctorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PatientId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AppointmentId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("MedicalTrackingSystem.Models.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Fee")
                        .HasColumnType("REAL");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Hospital")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Specialty")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("DoctorId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("MedicalTrackingSystem.Models.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PatientId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("MedicalTrackingSystem.Models.TestResults", b =>
                {
                    b.Property<int>("TestResultsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PatientId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TestName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("TestResultsId");

                    b.HasIndex("PatientId");

                    b.ToTable("TestResults");
                });

            modelBuilder.Entity("MedicalTrackingSystem.Models.Allergy", b =>
                {
                    b.HasOne("MedicalTrackingSystem.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("MedicalTrackingSystem.Models.Appointment", b =>
                {
                    b.HasOne("MedicalTrackingSystem.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicalTrackingSystem.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("MedicalTrackingSystem.Models.TestResults", b =>
                {
                    b.HasOne("MedicalTrackingSystem.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });
#pragma warning restore 612, 618
        }
    }
}
