using AmourLink.Recommendation.DTO;

namespace AmourLink.Recommendation.Services.Interfaces;

public interface IPreferenceService
{
    public Task<PreferenceDto> GetUserPreferenceAsync(CancellationToken cancellationToken = default);

    public Task UpdateUserPreferenceAsync(UpdatePreferenceDto preferenceDto,
        CancellationToken cancellationToken = default);

}