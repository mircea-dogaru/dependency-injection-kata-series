using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DependencyInjection.Console.Entities;

namespace DependencyInjection.Console
{
    interface IPatternGenerator
    {
        Pattern Generate(int width, int height);
    }
}
