﻿using MediatR;

namespace Test.Application.Commands.Test.StopTest
{
    public class StopTestCommand : IRequest<long>
    {
        public Guid AttemptId { get; set; }
    }
}
