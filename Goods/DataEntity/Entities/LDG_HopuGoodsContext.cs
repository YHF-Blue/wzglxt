using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataEntity.Entities
{
    public partial class LDG_HopuGoodsContext : DbContext
    {
        public LDG_HopuGoodsContext()
        {
        }

        public LDG_HopuGoodsContext(DbContextOptions<LDG_HopuGoodsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DepartmentInfo> DepartmentInfos { get; set; }
        public virtual DbSet<GoodsCategory> GoodsCategories { get; set; }
        public virtual DbSet<GoodsConsumableInfo> GoodsConsumableInfos { get; set; }
        public virtual DbSet<GoodsConsumableInput> GoodsConsumableInputs { get; set; }
        public virtual DbSet<GoodsEquipmentInfo> GoodsEquipmentInfos { get; set; }
        public virtual DbSet<GoodsEquipmentInput> GoodsEquipmentInputs { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<PowerInfo> PowerInfos { get; set; }
        public virtual DbSet<RRoleInfoPowerInfo> RRoleInfoPowerInfos { get; set; }
        public virtual DbSet<RUserInfoRoleInfo> RUserInfoRoleInfos { get; set; }
        public virtual DbSet<RoleInfo> RoleInfos { get; set; }
        public virtual DbSet<UserInfo> UserInfos { get; set; }
        public virtual DbSet<WorkFlowInstance> WorkFlowInstances { get; set; }
        public virtual DbSet<WorkFlowInstanceStep> WorkFlowInstanceSteps { get; set; }
        public virtual DbSet<WorkFlowModel> WorkFlowModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=LDG_HopuGoods;uid=sa;pwd=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_PRC_CI_AS");

            modelBuilder.Entity<DepartmentInfo>(entity =>
            {
                entity.ToTable("DepartmentInfo");

                entity.HasIndex(e => e.DepartmentId, "IX_DepartmentInfo")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddTime).HasColumnType("datetime");

                entity.Property(e => e.DepartmentId)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.LeaderId)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.ParentId)
                    .IsRequired()
                    .HasMaxLength(16);
            });

            modelBuilder.Entity<GoodsCategory>(entity =>
            {
                entity.ToTable("Goods_Category");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.Description).HasMaxLength(32);
            });

            modelBuilder.Entity<GoodsConsumableInfo>(entity =>
            {
                entity.ToTable("Goods_ConsumableInfo");

                entity.Property(e => e.AddTime).HasColumnType("datetime");

                entity.Property(e => e.GoodsId)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.Money).HasColumnType("money");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.Specification).HasMaxLength(32);

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasMaxLength(8);
            });

            modelBuilder.Entity<GoodsConsumableInput>(entity =>
            {
                entity.ToTable("Goods_ConsumableInput");

                entity.Property(e => e.AddTime).HasColumnType("datetime");

                entity.Property(e => e.AddUserId)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.GoodsId)
                    .IsRequired()
                    .HasMaxLength(16);
            });

            modelBuilder.Entity<GoodsEquipmentInfo>(entity =>
            {
                entity.ToTable("Goods_EquipmentInfo");

                entity.Property(e => e.AddTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(16);

                entity.Property(e => e.GoodsId)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.Money).HasColumnType("money");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.Specification).HasMaxLength(32);

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasMaxLength(8);
            });

            modelBuilder.Entity<GoodsEquipmentInput>(entity =>
            {
                entity.ToTable("Goods_EquipmentInput");

                entity.Property(e => e.AddTime).HasColumnType("datetime");

                entity.Property(e => e.AddUserId)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.GoodsId)
                    .IsRequired()
                    .HasMaxLength(16);
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("Log");

                entity.Property(e => e.AddTime).HasColumnType("datetime");

                entity.Property(e => e.Decription).HasMaxLength(128);
            });

            modelBuilder.Entity<PowerInfo>(entity =>
            {
                entity.ToTable("PowerInfo");

                entity.HasIndex(e => e.PowerId, "IX_PowerInfo")
                    .IsUnique();

                entity.Property(e => e.ActionUrl)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Description).HasMaxLength(32);

                entity.Property(e => e.HttpMethod).HasMaxLength(4);

                entity.Property(e => e.MenuIconUrl).HasMaxLength(32);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.ParentId).HasMaxLength(16);

                entity.Property(e => e.PowerId)
                    .IsRequired()
                    .HasMaxLength(16);
            });

            modelBuilder.Entity<RRoleInfoPowerInfo>(entity =>
            {
                entity.ToTable("R_RoleInfo_PowerInfo");

                entity.Property(e => e.PowerId)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(16);
            });

            modelBuilder.Entity<RUserInfoRoleInfo>(entity =>
            {
                entity.ToTable("R_UserInfo_RoleInfo");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(16);
            });

            modelBuilder.Entity<RoleInfo>(entity =>
            {
                entity.ToTable("RoleInfo");

                entity.HasIndex(e => e.RoleId, "IX_RoleInfo")
                    .IsUnique();

                entity.Property(e => e.AddTime).HasColumnType("datetime");

                entity.Property(e => e.DelTime).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(16);
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.ToTable("UserInfo");

                entity.HasIndex(e => e.UserId, "IX_UserInfo")
                    .IsUnique();

                entity.Property(e => e.AddTime).HasColumnType("datetime");

                entity.Property(e => e.DelTime).HasColumnType("datetime");

                entity.Property(e => e.DepartmentId)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.Email).HasMaxLength(32);

                entity.Property(e => e.PassWord)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.PhoneNum).HasMaxLength(16);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(16);
            });

            modelBuilder.Entity<WorkFlowInstance>(entity =>
            {
                entity.ToTable("WorkFlow_Instance");

                entity.Property(e => e.AddTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(64);

                entity.Property(e => e.InstanceId)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.NextReviewer).HasMaxLength(16);

                entity.Property(e => e.OutGoodsId)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.Reason).HasMaxLength(64);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(16);
            });

            modelBuilder.Entity<WorkFlowInstanceStep>(entity =>
            {
                entity.ToTable("WorkFlow_InstanceStep");

                entity.Property(e => e.InstanceId)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.NextReviewerId)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.ReviewReason).HasMaxLength(64);

                entity.Property(e => e.ReviewTime).HasColumnType("datetime");

                entity.Property(e => e.ReviewerId)
                    .IsRequired()
                    .HasMaxLength(16);
            });

            modelBuilder.Entity<WorkFlowModel>(entity =>
            {
                entity.ToTable("WorkFlow_Model");

                entity.Property(e => e.AddTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(64);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
