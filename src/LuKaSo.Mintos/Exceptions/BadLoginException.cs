﻿using LuKaSo.Mintos.Models.Login;
using System;
using System.Net.Http;

namespace LuKaSo.Mintos.Exceptions
{
    public class BadLoginException : Exception
    {
        public BadLoginException(HttpResponseMessage message, User user) : base($"Bad login information for user {user.Name}. \r\n Server return \r\n {message.ToString()}")
        { }

        public BadLoginException(User user) : base($"Bad login information for user {user.Name}.")
        { }
    }
}
