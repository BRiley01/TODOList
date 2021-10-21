﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAPI.Exceptions
{
    public class ResourceNotFoundException: Exception
    {
        public ResourceNotFoundException()
        {

        }

        public ResourceNotFoundException(string message): base(message)
        {

        }
    }
}
