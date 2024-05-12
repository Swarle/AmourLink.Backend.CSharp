using System;
using System.Collections.Generic;
using AmourLink.Backend.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace AmourLink.Backend.Infrastructure.Context;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Conversation> Conversations { get; set; }

    public virtual DbSet<Degree> Degrees { get; set; }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<Hobbie> Hobbies { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Music> Musics { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    public virtual DbSet<Preference> Preferences { get; set; }

    public virtual DbSet<Response> Responses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=rootPassword;database=amourlink", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.HasKey(e => e.ConversationId).HasName("PRIMARY");

            entity.ToTable("conversation");

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.ConversationId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("conversation_id");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp")
                .HasColumnName("creation_date");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Degree>(entity =>
        {
            entity.HasKey(e => e.DegreeId).HasName("PRIMARY");

            entity.ToTable("degree");

            entity.HasIndex(e => e.UserDetailsId, "fk_degree_user_details1_idx");

            entity.Property(e => e.DegreeId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("degree_id");
            entity.Property(e => e.DegreeType)
                .HasMaxLength(45)
                .HasColumnName("degree_type");
            entity.Property(e => e.SchoolName)
                .HasMaxLength(100)
                .HasColumnName("school_name");
            entity.Property(e => e.StartYear)
                .HasColumnType("datetime")
                .HasColumnName("start_year");
            entity.Property(e => e.UserDetailsId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("user_details_id");

            entity.HasOne(d => d.UserDetails).WithMany(p => p.Degrees)
                .HasForeignKey(d => d.UserDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_degree_user_details1");
        });

        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity
                .ToTable("__efmigrationshistory")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Hobbie>(entity =>
        {
            entity.HasKey(e => e.HobbieId).HasName("PRIMARY");

            entity.ToTable("hobbie");

            entity.HasIndex(e => e.UserDetailsId, "fk_user_account_hobbie_user_details1_idx");

            entity.Property(e => e.HobbieId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("hobbie_id");
            entity.Property(e => e.HobbieName)
                .HasMaxLength(45)
                .HasColumnName("hobbie_name");
            entity.Property(e => e.UserDetailsId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("user_details_id");

            entity.HasOne(d => d.UserDetails).WithMany(p => p.Hobbies)
                .HasForeignKey(d => d.UserDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_account_hobbie_user_details1");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.LanguageId).HasName("PRIMARY");

            entity.ToTable("language");

            entity.Property(e => e.LanguageId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("language_id");
            entity.Property(e => e.LanguageName)
                .HasMaxLength(45)
                .HasColumnName("language_name");

            entity.HasMany(d => d.UserDetailsUserDetails).WithMany(p => p.LanguageLanguages)
                .UsingEntity<Dictionary<string, object>>(
                    "LanguageUserDetail",
                    r => r.HasOne<UserDetail>().WithMany()
                        .HasForeignKey("UserDetailsUserDetailsId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_language_has_user_details_user_details1"),
                    l => l.HasOne<Language>().WithMany()
                        .HasForeignKey("LanguageLanguageId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_language_has_user_details_language1"),
                    j =>
                    {
                        j.HasKey("LanguageLanguageId", "UserDetailsUserDetailsId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("language_user_details");
                        j.HasIndex(new[] { "LanguageLanguageId" }, "fk_language_has_user_details_language1_idx");
                        j.HasIndex(new[] { "UserDetailsUserDetailsId" }, "fk_language_has_user_details_user_details1_idx");
                        j.IndexerProperty<byte[]>("LanguageLanguageId")
                            .HasMaxLength(16)
                            .IsFixedLength()
                            .HasColumnName("language_language_id");
                        j.IndexerProperty<byte[]>("UserDetailsUserDetailsId")
                            .HasMaxLength(16)
                            .IsFixedLength()
                            .HasColumnName("user_details_user_details_id");
                    });
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.MatchId).HasName("PRIMARY");

            entity.ToTable("match");

            entity.HasIndex(e => e.UserGivenId, "fk_Match_User1_idx");

            entity.HasIndex(e => e.UserAccountReceivedId, "fk_Match_User2_idx");

            entity.HasIndex(e => e.ResponseId, "fk_match_response1_idx");

            entity.Property(e => e.MatchId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("match_id");
            entity.Property(e => e.CompatibilityScore)
                .HasMaxLength(45)
                .HasColumnName("compatibility_score");
            entity.Property(e => e.Match1)
                .HasMaxLength(45)
                .HasColumnName("match");
            entity.Property(e => e.ResponseId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("response_id");
            entity.Property(e => e.UserAccountReceivedId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("user_account_received_id");
            entity.Property(e => e.UserGivenId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("user_given_id");

            entity.HasOne(d => d.Response).WithMany(p => p.Matches)
                .HasForeignKey(d => d.ResponseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_match_response1");

            entity.HasOne(d => d.UserAccountReceived).WithMany(p => p.MatchUserAccountReceiveds)
                .HasForeignKey(d => d.UserAccountReceivedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Match_User2");

            entity.HasOne(d => d.UserGiven).WithMany(p => p.MatchUserGivens)
                .HasForeignKey(d => d.UserGivenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Match_User1");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PRIMARY");

            entity.ToTable("message");

            entity.HasIndex(e => e.ConversationId, "fk_message_conversation1_idx");

            entity.HasIndex(e => e.UserId, "fk_message_user_account1_idx");

            entity.Property(e => e.MessageId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("message_id");
            entity.Property(e => e.ConversationId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("conversation_id");
            entity.Property(e => e.CreationDatre)
                .HasColumnType("timestamp")
                .HasColumnName("creation_datre");
            entity.Property(e => e.MessageBody)
                .HasColumnType("text")
                .HasColumnName("message_body");
            entity.Property(e => e.Read).HasColumnName("read");
            entity.Property(e => e.UserId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("user_id");

            entity.HasOne(d => d.Conversation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ConversationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_message_conversation1");

            entity.HasOne(d => d.User).WithMany(p => p.Messages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_message_user_account1");
        });

        modelBuilder.Entity<Music>(entity =>
        {
            entity.HasKey(e => e.MusicId).HasName("PRIMARY");

            entity.ToTable("music");

            entity.HasIndex(e => e.SpotifyId, "music_name_UNIQUE").IsUnique();

            entity.Property(e => e.MusicId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("music_id");
            entity.Property(e => e.ArtistName)
                .HasMaxLength(200)
                .HasColumnName("artist_name");
            entity.Property(e => e.SpotifyId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("spotify_id");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Picture>(entity =>
        {
            entity.HasKey(e => e.PictureId).HasName("PRIMARY");

            entity.ToTable("picture");

            entity.HasIndex(e => e.UserDetailsId, "fk_picture_user_details1_idx");

            entity.Property(e => e.PictureId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("picture_id");
            entity.Property(e => e.AddedTime)
                .HasColumnType("timestamp")
                .HasColumnName("added_time");
            entity.Property(e => e.PictureUrl)
                .HasMaxLength(255)
                .HasColumnName("picture_url");
            entity.Property(e => e.UserDetailsId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("user_details_id");

            entity.HasOne(d => d.UserDetails).WithMany(p => p.Pictures)
                .HasForeignKey(d => d.UserDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_picture_user_details1");
        });

        modelBuilder.Entity<Preference>(entity =>
        {
            entity.HasKey(e => e.PreferenceId).HasName("PRIMARY");

            entity.ToTable("preference");

            entity.HasIndex(e => e.UsertId, "fk_preference_user_account1_idx");

            entity.Property(e => e.PreferenceId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("preference_id");
            entity.Property(e => e.AgeRange).HasColumnName("age_range");
            entity.Property(e => e.CompatibilityPreference).HasColumnName("compatibility_preference");
            entity.Property(e => e.DistanceRange)
                .HasPrecision(4, 2)
                .HasColumnName("distance_range");
            entity.Property(e => e.Gender)
                .HasMaxLength(45)
                .HasColumnName("gender");
            entity.Property(e => e.UsertId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("usert_id");

            entity.HasOne(d => d.Usert).WithMany(p => p.Preferences)
                .HasForeignKey(d => d.UsertId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_preference_user_account1");
        });

        modelBuilder.Entity<Response>(entity =>
        {
            entity.HasKey(e => e.ResponseId).HasName("PRIMARY");

            entity.ToTable("response");

            entity.Property(e => e.ResponseId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("response_id");
            entity.Property(e => e.Match)
                .HasMaxLength(45)
                .HasColumnName("match");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.RoleId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(45)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "email_UNIQUE").IsUnique();

            entity.Property(e => e.UserId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("user_id");
            entity.Property(e => e.CreatedTime)
                .HasColumnType("timestamp")
                .HasColumnName("created_time");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasMany(d => d.Conversations).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "ConversationParticipan",
                    r => r.HasOne<Conversation>().WithMany()
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_user_account_has_conversation_conversation1"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_user_account_has_conversation_user_account1"),
                    j =>
                    {
                        j.HasKey("UserId", "ConversationId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("conversation_participans");
                        j.HasIndex(new[] { "ConversationId" }, "fk_user_account_has_conversation_conversation1_idx");
                        j.HasIndex(new[] { "UserId" }, "fk_user_account_has_conversation_user_account1_idx");
                        j.IndexerProperty<byte[]>("UserId")
                            .HasMaxLength(16)
                            .IsFixedLength()
                            .HasColumnName("user_id");
                        j.IndexerProperty<byte[]>("ConversationId")
                            .HasMaxLength(16)
                            .IsFixedLength()
                            .HasColumnName("conversation_id");
                    });

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserHasRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_user_has_role_role1"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_user_has_role_user1"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("user_has_role");
                        j.HasIndex(new[] { "RoleId" }, "fk_user_has_role_role1_idx");
                        j.HasIndex(new[] { "UserId" }, "fk_user_has_role_user1_idx");
                        j.IndexerProperty<byte[]>("UserId")
                            .HasMaxLength(16)
                            .IsFixedLength()
                            .HasColumnName("user_id");
                        j.IndexerProperty<byte[]>("RoleId")
                            .HasMaxLength(16)
                            .IsFixedLength()
                            .HasColumnName("role_id");
                    });
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.UserDetailsId).HasName("PRIMARY");

            entity.ToTable("user_details");

            entity.HasIndex(e => e.MusicId, "fk_user_details_music1_idx");

            entity.HasIndex(e => e.UserId, "fk_user_details_user_account1_idx").IsUnique();

            entity.Property(e => e.UserDetailsId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("user_details_id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Bio)
                .HasColumnType("text")
                .HasColumnName("bio");
            entity.Property(e => e.FirstName)
                .HasMaxLength(45)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(45)
                .HasColumnName("gender");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.LastLocationLatitude).HasColumnName("last_location_latitude");
            entity.Property(e => e.LastLocationLongitude).HasColumnName("last_location_longitude");
            entity.Property(e => e.LastName)
                .HasMaxLength(45)
                .HasColumnName("last_name");
            entity.Property(e => e.MusicId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("music_id");
            entity.Property(e => e.Nationality)
                .HasMaxLength(45)
                .HasColumnName("nationality");
            entity.Property(e => e.Occupation)
                .HasMaxLength(45)
                .HasColumnName("occupation");
            entity.Property(e => e.UserId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("user_id");

            entity.HasOne(d => d.Music).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.MusicId)
                .HasConstraintName("fk_user_details_music1");

            entity.HasOne(d => d.User).WithOne(p => p.UserDetail)
                .HasForeignKey<UserDetail>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_details_user_account1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
