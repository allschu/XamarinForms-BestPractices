using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BestPractices.Common.Models;
using Bestpractices.Service.Contract;

namespace Bestpractices.Service.Interfaces
{
    public interface ICastService
    {
        Task<IEnumerable<Cast>> GetMovieCredits(int id);
    }
}
