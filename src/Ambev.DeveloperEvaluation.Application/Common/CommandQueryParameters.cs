﻿namespace Ambev.DeveloperEvaluation.Application.Common;

public class CommandQueryParameters
{
    public int _page { get; set; } = 1;

    public int _size { get; set; } = 10;

    public string _order { get; set; } = string.Empty;
}
