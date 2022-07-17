using FinalProject.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface ISuggestionService
{
    Task<int> Set(SuggestionDto dto);
    Task<IEnumerable<SuggestionDto>> GetAll();
    Task<SuggestionDto> GetById(int id);
    Task<int> Remove(int id);
    Task<int> Update(SuggestionDto dto);
}

