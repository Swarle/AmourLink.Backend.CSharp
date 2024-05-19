﻿using AmourLink.Recommendation.DTO;
using AmourLink.Recommendation.Pagination;

namespace AmourLink.Recommendation.Services.Interfaces;

public interface IRecommendationService
{
    public Task<MemberDto> GetPagedFeedAsync(PaginationParams paginationParams, CancellationToken cancellationToken = default);
}