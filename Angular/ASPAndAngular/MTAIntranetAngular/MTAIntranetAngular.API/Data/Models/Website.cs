using System;
using System.Collections.Generic;

namespace MTAIntranetAngular.API.Data.Models;

public partial class Website
{
    public int Id { get; set; }

    public string? ServerName { get; set; }

    public string? WebsiteName { get; set; }

    public string? Recipients { get; set; }

    public string? PreviousState { get; set; }

    public string? CurrentState { get; set; }

    public DateTime? LastCheck { get; set; }
}
