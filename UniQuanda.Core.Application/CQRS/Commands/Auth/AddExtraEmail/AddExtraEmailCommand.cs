﻿using MediatR;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.AddExtraEmail;

public class AddExtraEmailCommand : IRequest<UpdateSecurityResultEnum>
{
    public AddExtraEmailCommand(AddExtraEmailRequestDTO request, int idUser)
    {
        NewExtraEmail = request.NewExtraEmail;
        PlainPassword = request.Password;
        IdUser = idUser;
    }

    public string NewExtraEmail { get; set; }
    public string PlainPassword { get; set; }
    public int IdUser { get; set; }
}