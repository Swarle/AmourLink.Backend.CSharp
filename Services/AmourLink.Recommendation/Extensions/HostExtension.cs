using AmourLink.Recommendation.Data.Context;
using AmourLink.Recommendation.Data.Entities;
using AmourLink.Recommendation.Data.Entities.Enums;
using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace AmourLink.Recommendation.Extensions;

public static class HostExtension
{
    public static IHost SeedDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ApplicationDbContext>();
        


        List<Tag> tags;
        
        if (context.Tags.Any())
            tags = context.Tags.ToList();
        else
        {
            tags =  new Faker<Tag>()
                .RuleFor(t => t.TagName, f => f.Lorem.Word())
                .Generate(8);
            
            context.Tags.AddRange(tags);
            context.SaveChanges();
        }

        List<Info> info;

        if (context.Info.Any())
            info = context.Info.Include(info => info.Answers).ToList();
        else
        {
            info = GenerateInfos();
            context.Info.AddRange(info);
            context.SaveChanges();
        }
        
        if (context.Users.Any())
            return host;

        const string password = "$2a$10$.qDt0HaxBEfDvOJQzVQozOLQUo8ANVotdxywO5HHdPyTd2edmJKkG";
        
        var users = new Faker<User>()
            .RuleFor(u => u.Id, f => Guid.NewGuid())
            .RuleFor(u => u.Password, password)
            .RuleFor(u => u.Enabled, true)
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.AccountType, AccountType.Local)
            .RuleFor(u => u.Rating, f => f.Random.Int(600, 3000))
            .RuleFor(u => u.CreatedAt, f => f.Date.Past())
            .RuleFor(u => u.UserDetails, (f, u) =>
                new UserDetails
                {
                    Id = u.Id,
                    FirstName = f.Name.FirstName(),
                    LastName = f.Name.LastName(),
                    Age = f.Random.Int(18, 70),
                    Bio = f.Lorem.Paragraph(),
                    Height = f.Random.Int(120, 199),
                    Occupation = f.Person.Company.Name,
                    Nationality = f.Lorem.Word(),
                    Gender = (Gender)f.PickRandom(0, 1),
                    User = u,
                    LastLocation = new Point(f.Random.Double(50.30d,50.55d),f.Random.Double(30.25d, 30.75d)),
                    Tags = tags,
                    Pictures = new List<Picture>(new Faker<Picture>()
                        .RuleFor(p => p.UserId, u.Id)
                        .RuleFor(p => p.PictureUrl, f => f.Image.PicsumUrl())
                        .RuleFor(p => p.Position, f.Random.Number(1,4))
                        .RuleFor(p => p.TimeAdded, f.Date.Past())
                        .Generate(4)),
                    Degree = new Faker<Degree>()
                        .RuleFor(d => d.DegreeName, "Бакалавр")
                        .RuleFor(d => d.SchoolName, f => f.Lorem.Word())
                        .RuleFor(d => d.DegreeType, f => f.Lorem.Word())
                        .Generate(),
                    InfoDetails = info.Select(i => new InfoDetails
                    {
                        InfoId = i.Id,
                        AnswerId = f.PickRandom(i.Answers.Select(a => a.Id)),
                        UserId = u.Id
                    }).ToList()
                }

            )
            .RuleFor(u => u.UserPreference, (f, u) => 
                new Preference
                {
                    MinAge = 18,
                    MaxAge = 44,
                    Gender = (GenderPreference)f.PickRandom(0, 1, 2),
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
            .RuleFor(u => u.AccountType, AccountType.Google)
            .RuleFor(u => u.Rating, f => 1500)
            .RuleFor(u => u.CreatedAt, f => f.Date.Past())
            .RuleFor(u => u.UserDetails, (f, u) =>
                new UserDetails
                {
                    Id = u.Id,
                    FirstName = f.Name.FirstName(),
                    LastName = f.Name.LastName(),
                    Age = f.Random.Int(18, 70),
                    Bio = f.Lorem.Paragraph(),
                    Height = f.Random.Int(120, 199),
                    Occupation = f.Person.Company.Name,
                    Nationality = f.Lorem.Word(),
                    Gender = (Gender)f.PickRandom(0, 1),
                    User = u,
                    LastLocation = new Point(f.Random.Double(50.30d,50.55d),f.Random.Double(30.25d, 30.75d)),
                    Pictures = new List<Picture>(new Faker<Picture>()
                        .RuleFor(p => p.UserId, u.Id)
                        .RuleFor(p => p.PictureUrl, f => f.Image.PicsumUrl())
                        .RuleFor(p => p.Position, f.Random.Number(1,4))
                        .RuleFor(p => p.TimeAdded, f.Date.Past())
                        .Generate(4)),
                    Degree = new Faker<Degree>()
                        .RuleFor(d => d.DegreeName, "Бакалавр")
                        .RuleFor(d => d.SchoolName, f => f.Lorem.Word())
                        .RuleFor(d => d.DegreeType, f => f.Lorem.Word())
                        .Generate(),
                }

            )
            .RuleFor(u => u.UserPreference, (f, u) => 
                new Preference
                {
                    MinAge = 18,
                    MaxAge = 44,
                    Gender = (GenderPreference)f.PickRandom(0, 1, 2),
                    DistanceRange = 100,
                })
            .Generate();
    }

    private static List<Info> GenerateInfos()
    {
        var info = new List<Info>
        {
            new()
            {
                Title = "Знак зодіаку",
                Answers = new List<InfoAnswer>
                {
                    new() { Answer = "Козеріг" },
                    new() { Answer = "Водолій" },
                    new() { Answer = "Риби" },
                    new() { Answer = "Овен" },
                    new() { Answer = "Телець" },
                    new() { Answer = "Близнюки" },
                    new() { Answer = "Рак" },
                    new() { Answer = "Лев" },
                    new() { Answer = "Діва" },
                    new() { Answer = "Терези" },
                    new() { Answer = "Скорпіон" },
                    new() { Answer = "Стрілець" },
                }
            },
            new()
            {
                Title = "Твій стиль спілкування",
                Answers = new List<InfoAnswer>
                {
                    new(){Answer = "Онлайн спілкування"},
                    new(){Answer = "Краще зустрітися"},
                    new(){Answer = "Рідко відповідаю"},
                }
            },
            new()
            {
                Title = "Шкідливі звички",
                Answers = new List<InfoAnswer>
                {
                    new(){Answer = "Курю та п'ю"},
                    new(){Answer = "Тільки курю"},
                    new(){Answer = "П'ю по святах"},
                    new(){Answer = "Немає",
                    }
                }
            },
            new()
            {
                Title = "Характер",
                Answers = new List<InfoAnswer>
                {
                    new(){Answer = "Інтроверт"},
                    new(){Answer = "Екстраверт"},
                    new(){Answer = "Усе і відразу"},
                }
            },
        };

        return info;
    }
}