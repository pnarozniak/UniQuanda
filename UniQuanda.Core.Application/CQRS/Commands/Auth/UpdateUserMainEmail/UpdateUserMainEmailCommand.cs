﻿using MediatR;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.UpdateUserMainEmail;

public class UpdateUserMainEmailCommand : IRequest<UpdateSecurityResultEnum>
{
    public UpdateUserMainEmailCommand(UpdateUserMainEmailRequestDTO request, int idUser)
    {
        NewMainEmail = IdExtraEmail == null ? request.NewMainEmail : null;
        IdExtraEmail = request.IdExtraEmail;
        PlainPassword = request.Password;
        IdUser = idUser;
    }

    public string? NewMainEmail { get; set; }
    public int? IdExtraEmail { get; set; }
    public string PlainPassword { get; set; }
    public int IdUser { get; set; }
}