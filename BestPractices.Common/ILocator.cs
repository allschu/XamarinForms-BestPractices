using System;
using System.Collections.Generic;
using System.Text;

namespace BestPractices.Common
{
    public interface ILocator
    {
        T GetInstance<T>() where T : class;
        void Register<T>() where T : class;
    }
}
