﻿using System.ComponentModel.DataAnnotations;

namespace EasySoft.Simple.Single.Application.Models;

/// <summary>
/// Hello
/// </summary>
public class Hello
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Date
    /// </summary>
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    /// <summary>
    /// Hello
    /// </summary>
    public Hello()
    {
        Name = "";
    }
}