﻿namespace Test.Application.Events.Teacher
{
    public class FailCreateTeacher
    {
        //public Guid CorrelationId { get; set; }
        public string Email { get; set; } = string.Empty;

        public string Reason { get; set; } = string.Empty;
    }
}
