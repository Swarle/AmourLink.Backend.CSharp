using System.Net;
using AmourLink.Infrastructure.Extensions;
using AmourLink.Infrastructure.ResponseHandling;
using AmourLink.Recommendation.Data.Entities;
using AmourLink.Recommendation.DTO;
using AmourLink.Recommendation.Repository;
using AmourLink.Recommendation.Services.Interfaces;
using AmourLink.Recommendation.Specifications;
using AutoMapper;

namespace AmourLink.Recommendation.Services;

public class PreferenceService : IPreferenceService
{
    private readonly IRepository<Preference> _repository;
    private readonly IMapper _mapper;
    private readonly HttpContext _context;

    public PreferenceService(IRepository<Preference> repository, IHttpContextAccessor accessor, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _context = accessor.HttpContext ??
                   throw new InvalidOperationException("HttpContextAccessor does`t have context");
    }

    public async Task<PreferenceDto> GetUserPreferenceAsync(CancellationToken cancellationToken = default)
    {
        var currentUserId = _context.User.GetUserId();

        if (currentUserId == Guid.Empty)
            throw new HttpException(HttpStatusCode.Unauthorized, "There are no id in token");

        var preferenceSpecification = new PreferenceByUserIdSpecification(currentUserId);

        var userPreference = await _repository.GetFirstOrDefaultAsync(preferenceSpecification, cancellationToken) ??
                             throw new HttpException(HttpStatusCode.NotFound, $"User with id: {currentUserId} does not have preferences");

        var preferenceDto = _mapper.Map<PreferenceDto>(userPreference);

        return preferenceDto;
    }

    public async Task UpdateUserPreferenceAsync(UpdatePreferenceDto preferenceDto, CancellationToken cancellationToken = default)
    {
        var currentUserId = _context.User.GetUserId();

        if (currentUserId == Guid.Empty)
            throw new HttpException(HttpStatusCode.Unauthorized, "There are no id in token");

        var preferenceSpecification = new PreferenceByUserIdSpecification(currentUserId);

        var userPreference = await _repository.GetFirstOrDefaultAsync(preferenceSpecification, cancellationToken) ??
                             throw new HttpException(HttpStatusCode.NotFound, $"User with id: {currentUserId} does not have preferences");

        var preference = _mapper.Map(preferenceDto, userPreference);

        await _repository.UpdateAsync(preference);
        await _repository.SaveChangesAsync();
    }
}