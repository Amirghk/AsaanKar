using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Interfaces;
public interface ISuggestionRepository
{
    Task<int> Add(Suggestion model);
    Task<int> Update(Suggestion model);
    Task<int> Remove(int id);
    Task<Suggestion> GetById(int id);
    Task<IEnumerable<Suggestion>> GetAll();
}

