using System;
using Pms.Domain;
using Pms.Domain.AggregateRoots;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Pms.Host
{
    public partial class OneForAll_PmsContext : DbContext
    {
        private Guid _tenantId = Guid.Empty;
        public OneForAll_PmsContext(DbContextOptions<OneForAll_PmsContext> options)
            : base(options)
        {

        }

        public OneForAll_PmsContext(
            DbContextOptions<OneForAll_PmsContext> options,
            ITenantProvider tenantProvider)
            : base(options)
        {
            _tenantId = tenantProvider.GetTenantId();
        }

        #region 项目管理

        public virtual PmsBug PmsBug { get; set; }
        public virtual PmsProject PmsProject { get; set; }
        public virtual PmsProjectMemberContact PmsProjectUser { get; set; }
        public virtual PmsRequirement PmsRequirement { get; set; }
        public virtual PmsRequirementRecord PmsRequirementRecord { get; set; }
        public virtual PmsTask PmsTask { get; set; }
        public virtual PmsTaskMemberContact PmsTaskUser { get; set; }
        public virtual PmsTaskFile PmsTaskFile { get; set; }
        public virtual PmsMember PmsMember { get; set; }
        public virtual PmsTaskRecord PmsTaskRecord { get; set; }
        public virtual PmsMilestone PmsMilestone { get; set; }
        public virtual PmsRisk PmsRisk { get; set; }

        #endregion

        #region 代码生成
        public virtual DbSet<PmsEntityTable> PmsEntityTable { get; set; }
        public virtual DbSet<PmsEntityTableContact> PmsEntityTableContact { get; set; }
        public virtual DbSet<PmsCodeStructure> PmsCodeStructure { get; set; }
        public virtual DbSet<PmsCodeDenerationRecord> PmsCodeDenerationRecord { get; set; }
        public virtual DbSet<PmsDbConnectString> PmsDbConnectString { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 项目管理
            modelBuilder.Entity<PmsProject>(form =>
            {
                form.ToTable("Pms_Project");
                form.Property(e => e.Id).ValueGeneratedOnAdd();

                form.HasQueryFilter(e => e.SysTenantId == _tenantId);
            });

            modelBuilder.Entity<PmsMember>(form =>
            {
                form.ToTable("Pms_Member");
                form.Property(e => e.Id).ValueGeneratedOnAdd();

                form.HasQueryFilter(e => e.SysTenantId == _tenantId);
            });

            modelBuilder.Entity<PmsBug>(form =>
            {
                form.ToTable("Pms_Bug");
                form.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PmsProjectMemberContact>(form =>
            {
                form.ToTable("Pms_ProjectMemberContact");
                form.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PmsRequirement>(form =>
            {
                form.ToTable("Pms_Requirement");
                form.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PmsRequirementRecord>(form =>
            {
                form.ToTable("Pms_RequirementRecord");
                form.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PmsTask>(form =>
            {
                form.ToTable("Pms_Task");
                form.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PmsTaskMemberContact>(form =>
            {
                form.ToTable("Pms_TaskMemberContact");
                form.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PmsTaskFile>(form =>
            {
                form.ToTable("Pms_TaskFile");
                form.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PmsTaskRecord>(form =>
            {
                form.ToTable("Pms_TaskRecord");
                form.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PmsMilestone>(form =>
            {
                form.ToTable("Pms_Milestone");
                form.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PmsRisk>(form =>
            {
                form.ToTable("Pms_Risk");
                form.Property(e => e.Id).ValueGeneratedOnAdd();
            });



            #endregion

            #region 代码生成

            modelBuilder.Entity<PmsEntityTable>(entity =>
            {
                entity.ToTable("Pms_EntityTable");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PmsEntityTableContact>(entity =>
            {
                entity.ToTable("Pms_EntityTableContact");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PmsCodeStructure>(entity =>
            {
                entity.ToTable("Pms_CodeStructure");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PmsCodeDenerationRecord>(entity =>
            {
                entity.ToTable("Pms_CodeDenerationRecord");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PmsDbConnectString>(entity =>
            {
                entity.ToTable("Pms_DbConnectString");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            #endregion
        }
    }
}
