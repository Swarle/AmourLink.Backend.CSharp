using AmourLink.Recommendation.Data.Context;
using AmourLink.Recommendation.Data.Entities;
using Bogus;
using Bogus.DataSets;
using NetTopologySuite.Geometries;

namespace AmourLink.Recommendation.Extensions;

public static class HostExtension
{
    public static IHost SeedDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ApplicationDbContext>();

        const string password = "$2a$10$.qDt0HaxBEfDvOJQzVQozOLQUo8ANVotdxywO5HHdPyTd2edmJKkG";

        if (context.Users.Any())
            return host;
        
        var users = new Faker<User>()
            .RuleFor(u => u.Id, f => Guid.NewGuid())
            .RuleFor(u => u.Password, password)
            .RuleFor(u => u.Enabled, true)
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.AccountType, "LOCAL")
            .RuleFor(u => u.Rating, f => f.Random.Int(600, 3000))
            .RuleFor(u => u.CreatedAt, f => f.Date.Past())
            .RuleFor(u => u.UserDetails, (f, u) =>
                new UserDetails
                {
                    Id = u.Id,
                    FirstName = f.Name.FirstName(),
                    LastName = f.Name.LastName(),
                    Age = f.Random.UInt(18, 70),
                    Bio = f.Lorem.Paragraph(),
                    Height = f.Random.Int(120, 199),
                    Occupation = f.Person.Company.Name,
                    Nationality = f.Lorem.Word(),
                    Gender = ((Name.Gender)f.PickRandom(0, 1)).ToString(),
                    User = u,
                    LastLocation = new Point(f.Random.Double(50.30d,50.55d),f.Random.Double(30.25d, 30.75d)),
                    Tags = new List<Tag>(new Faker<Tag>()
                        .RuleFor(t => t.TagName, f => f.Lorem.Word())
                        .Generate(14)),
                    Hobbies = new List<Hobbie>(new Faker<Hobbie>()
                        .RuleFor(h => h.HobbieName, f => f.Lorem.Word())
                        .RuleFor(h => h.UserDetailsId, u.Id)
                        .Generate(2)),
                    Pictures = new List<Picture>(new Faker<Picture>()
                        .RuleFor(p => p.UserDetailsId, u.Id)
                        .RuleFor(p => p.PictureUrl, f => f.Image.PicsumUrl())
                        .RuleFor(p => p.Position, f.Random.Number(1,4))
                        .RuleFor(p => p.TimeAdded, f.Date.Past())
                        .Generate(4)),
                    Degree = new Faker<Degree>()
                        .RuleFor(d => d.DegreeType, "Бакалавр")
                        .RuleFor(d => d.SchoolName, f => f.Lorem.Word())
                        .RuleFor(d => d.StartYear, f.Date.Past())
                        .Generate(),
                }

            )
            .RuleFor(u => u.UserPreference, (f, u) => 
                new Preference
                {
                    MinAge = 18,
                    MaxAge = 44,
                    Gender = "Female",
                    DistanceRange = 25,
                })
            .Generate(100);
        
        users.Add(GenerateSpecificUser());
        
        context.Users.AddRange(users);
        context.SaveChanges();

        return host;
    }

    private static User GenerateSpecificUser()
    {
        return new Faker<User>()
            .RuleFor(u => u.Id, f => Guid.NewGuid())
            .RuleFor(u => u.Password, f => "$2a$10$.qDt0HaxBEfDvOJQzVQozOLQUo8ANVotdxywO5HHdPyTd2edmJKkG")
            .RuleFor(u => u.Email, f => "bogdanvalman@gmail.com")
            .RuleFor(u => u.Enabled, true)
            .RuleFor(u => u.AccountType, "GOOGLE")
            .RuleFor(u => u.Rating, f => 1500)
            .RuleFor(u => u.CreatedAt, f => f.Date.Past())
            .RuleFor(u => u.UserDetails, (f, u) =>
                new UserDetails
                {
                    Id = u.Id,
                    FirstName = f.Name.FirstName(),
                    LastName = f.Name.LastName(),
                    Age = f.Random.UInt(18, 70),
                    Bio = f.Lorem.Paragraph(),
                    Height = f.Random.Int(120, 199),
                    Occupation = f.Person.Company.Name,
                    Nationality = f.Lorem.Word(),
                    Gender = ((Name.Gender)f.PickRandom(0, 1)).ToString(),
                    User = u,
                    LastLocation = new Point(f.Random.Double(50.30d,50.55d),f.Random.Double(30.25d, 30.75d)),
                    Tags = new List<Tag>(new Faker<Tag>()
                        .RuleFor(t => t.TagName, f => f.Lorem.Word())
                        .Generate(14)),
                    Hobbies = new List<Hobbie>(new Faker<Hobbie>()
                        .RuleFor(h => h.HobbieName, f => f.Lorem.Word())
                        .RuleFor(h => h.UserDetailsId, u.Id)
                        .Generate(2)),
                    Pictures = new List<Picture>(new Faker<Picture>()
                        .RuleFor(p => p.UserDetailsId, u.Id)
                        .RuleFor(p => p.PictureUrl, f => f.Image.PicsumUrl())
                        .RuleFor(p => p.Position, f.Random.Number(1,4))
                        .RuleFor(p => p.TimeAdded, f.Date.Past())
                        .Generate(4)),
                    Degree = new Faker<Degree>()
                        .RuleFor(d => d.DegreeType, "Бакалавр")
                        .RuleFor(d => d.SchoolName, f => f.Lorem.Word())
                        .RuleFor(d => d.StartYear, f.Date.Past())
                        .Generate(),
                }

            )
            .RuleFor(u => u.UserPreference, (f, u) => 
                new Preference
                {
                    MinAge = 18,
                    MaxAge = 44,
                    Gender = "Female",
                    DistanceRange = 100,
                })
            .Generate();
    }
}