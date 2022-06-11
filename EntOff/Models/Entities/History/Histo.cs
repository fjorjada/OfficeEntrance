﻿using EntOff.Api.Models.Entities.Users;

namespace EntOff.Models.Entities.History
{
    public class Histo
    {
        public Guid Id { get; set; }

        public Boolean InOut { get; set; }
        public DateTimeOffset Date { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
