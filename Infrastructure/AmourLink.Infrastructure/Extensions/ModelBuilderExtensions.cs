using System.Reflection;
using AmourLink.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace AmourLink.Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder SetValueConverterForGuids(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(Guid))
                {
                    property.SetValueConverter(new GuidToBytesConverter());
                }
            }
        }

        return modelBuilder;
    }

    public static ModelBuilder SetColumnTypeForGuids(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(Guid))
                {
                    property.SetColumnType("binary(16)");
                }
            }
        }

        return modelBuilder;
    }

    public static ModelBuilder SetDefaultModelBuilder<TDbContext>(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(TDbContext))!);

        modelBuilder.SetValueConverterForGuids();
        modelBuilder.SetColumnTypeForGuids();
        
        return modelBuilder;
    }
}