﻿namespace Scheduler.JWT;

public class JWTTokenDescriptor
{
    public string Token { get; set; }

    public DateTime ExpirationDate { get; set; }
}