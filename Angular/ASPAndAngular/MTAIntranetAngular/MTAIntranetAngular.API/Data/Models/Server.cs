using System;
using System.Collections.Generic;

namespace MTAIntranetAngular.API.Data.Models;

public partial class Server
{
    public int Id { get; set; }

    public string ServerName { get; set; } = null!;

    public string Recipients { get; set; } = null!;

    public string PreviousState { get; set; } = null!;

    public string CurrentState { get; set; } = null!;

    public DateTime LastCheck { get; set; }

    public DateTime LastEmailsent { get; set; }

    public int TimeInterval { get; set; }
}
