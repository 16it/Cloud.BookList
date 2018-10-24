using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Cloud.BookList.Authorization.Roles;
using Cloud.BookList.Authorization.Users;
using Cloud.BookList.MultiTenancy;
using Cloud.BookList.CloudBookList.BookTagManagement;
using Cloud.BookList.CloudBookList.BookManagement;
using Cloud.BookList.CloudBookList.RelationshipManagement;

namespace Cloud.BookList.EntityFrameworkCore
{
    public class BookListDbContext : AbpZeroDbContext<Tenant, Role, User, BookListDbContext>
    {
        #region ���鵥

        public DbSet<BookTag> BookTags { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<CloudBookList.BookListManagement.BookList> BookLists { get; set; }

        public DbSet<BookAndBookTagRelationship> BookAndBookTagRelationships { get; set; }

        public DbSet<BookListAndBookRelationship> BookListAndBookRelationship { get; set; }

        #endregion

        public BookListDbContext(DbContextOptions<BookListDbContext> options)
            : base(options)
        {


        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //�޸�ABP��Ĭ�ϱ���Ϣ
            modelBuilder.ChangeAbpTablePrefix<Tenant, Role, User>("", YoYoAbpefCoreConsts.SchemaNames.ABP);
            base.OnModelCreating(modelBuilder);

            // ============  �����Ա���Ϣ���޸� =============



            // ��ϵ����

            // �鼮���鼮��ǩ
            modelBuilder.Entity<BookAndBookTagRelationship>()
                .HasOne(o => o.Book)
                .WithMany(o => o.BookAndBookTagRelationships)
                .HasForeignKey(e => e.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<BookAndBookTagRelationship>()
              .HasOne(o => o.BookTag)
              .WithMany(o => o.BookAndBookTagRelationships)
              .HasForeignKey(e => e.BookTagId)
              .OnDelete(DeleteBehavior.ClientSetNull);

            // �鵥���鼮
            modelBuilder.Entity<BookListAndBookRelationship>()
            .HasOne(o => o.BookList)
            .WithMany(o => o.BookListAndBookRelationships)
            .HasForeignKey(e => e.BookListId)
            .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<BookListAndBookRelationship>()
             .HasOne(o => o.Book)
             .WithMany(o => o.BookListAndBookRelationships)
             .HasForeignKey(e => e.BookId)
             .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
