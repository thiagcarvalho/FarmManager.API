using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManager.Application.Exceptions;

public class DuplicateResourceException : Exception
{
    public DuplicateResourceException(string message) : base(message) { }
    public DuplicateResourceException(string entityName, object key)
        : base($"{entityName} with identifier '{key}' already exists.") { }
}
