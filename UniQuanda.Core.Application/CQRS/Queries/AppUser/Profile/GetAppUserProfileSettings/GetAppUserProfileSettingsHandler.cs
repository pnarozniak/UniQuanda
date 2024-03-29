﻿using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.AppUser.Profile.GetAppUserProfileSettings;

public class GetAppUserProfileSettingsHandler : IRequestHandler<GetAppUserProfileSettingsQuery, GetAppUserProfileSettingsResponseDTO>
{
    private readonly IAppUserRepository _appUserRepository;

    public GetAppUserProfileSettingsHandler(IAppUserRepository appUserRepository)
    {
        _appUserRepository = appUserRepository;
    }

    public async Task<GetAppUserProfileSettingsResponseDTO?> Handle(GetAppUserProfileSettingsQuery request, CancellationToken ct)
    {
        var appUser = await _appUserRepository.GetAppUserByIdForProfileSettingsAsync(request.IdAppUser, ct);
        if (appUser is null)
            return null;
        return new GetAppUserProfileSettingsResponseDTO
        {
            NickName = appUser.Nickname,
            FirstName = appUser.FirstName,
            LastName = appUser.LastName,
            Birthdate = appUser.Birthdate,
            City = appUser.City,
            Contact = appUser.Contact,
            AboutText = appUser.AboutText,
            SemanticScholarProfile = appUser.SemanticScholarProfile,
            Avatar = appUser.Avatar,
            Banner = appUser.Banner
        };
    }
}