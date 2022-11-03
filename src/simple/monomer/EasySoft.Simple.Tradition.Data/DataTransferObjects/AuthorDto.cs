﻿namespace EasySoft.Simple.Tradition.Data.DataTransferObjects;

public class AuthorDto
{
    public int AuthorId { get; set; }

    public string RealName { get; set; }

    public AuthorDto()
    {
        AuthorId = 0;
        RealName = "";
    }
}