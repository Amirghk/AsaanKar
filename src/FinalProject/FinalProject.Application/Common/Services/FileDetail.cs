using AutoMapper;
using FinalProject.Application.Common.Dtos;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Application.Common.Services;

public class FileDetailService : IUploadService
{
    private readonly IMapper _mapper;
    private readonly IFileDetailRepository _repository;
    public FileDetailService(IFileDetailRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<UploadDto>> GetAll()
    {
        List<UploadDto> mappedModels = _mapper.Map<List<UploadDto>>(await _repository.GetAll()).ToList();
        return mappedModels;
    }

    public async Task<UploadDto> GetById(int id)
    {
        UploadDto mappedModel = _mapper.Map<UploadDto>(await _repository.GetById(id));
        return mappedModel;
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(UploadDto dto)
    {
        return await _repository.Add(_mapper.Map<Upload>(dto));
    }

    public async Task<int> Update(UploadDto dto)
    {
        return await _repository.Update(_mapper.Map<Upload>(dto));
    }
}