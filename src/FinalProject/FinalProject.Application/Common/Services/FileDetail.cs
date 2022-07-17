using AutoMapper;
using FinalProject.Application.Common.Dtos;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Application.Common.Services;

public class FileDetailService : IFileDetailService
{
    private readonly IMapper _mapper;
    private readonly IFileDetailRepository _repository;
    public FileDetailService(IFileDetailRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<FileDetailDto>> GetAll()
    {
        List<FileDetailDto> mappedModels = _mapper.Map<List<FileDetailDto>>(await _repository.GetAll()).ToList();
        return mappedModels;
    }

    public async Task<FileDetailDto> GetById(int id)
    {
        FileDetailDto mappedModel = _mapper.Map<FileDetailDto>(await _repository.GetById(id));
        return mappedModel;
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(FileDetailDto dto)
    {
        return await _repository.Add(_mapper.Map<Upload>(dto));
    }

    public async Task<int> Update(FileDetailDto dto)
    {
        return await _repository.Update(_mapper.Map<Upload>(dto));
    }
}