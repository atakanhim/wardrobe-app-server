﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wardrobe.Application.Exceptions
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException() : base("kullanici veya adi sifre hatali")
        {
        }

        public NotFoundUserException(string? message) : base(message)
        {
        }

        public NotFoundUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
