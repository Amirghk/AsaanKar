using AutoMapper;
using FinalProject.Application.Common.Dtos;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Application.Common.Services;

public class CommentService : ICommentService
{
    private readonly IMapper _mapper;
    private readonly ICommentRepository _repository;
    public CommentService(ICommentRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<CommentDto>> GetAll()
    {
        List<CommentDto> mappedModels = _mapper.Map<List<CommentDto>>(await _repository.GetAll()).ToList();
        return mappedModels;
    }

    public async Task<CommentDto> GetById(int id)
    {
        CommentDto mappedModel = _mapper.Map<CommentDto>(await _repository.GetById(id));
        return mappedModel;
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(CommentDto dto)
    {
        return await _repository.Add(_mapper.Map<Comment>(dto));
    }

    public async Task<int> Update(CommentDto dto)
    {
        return await _repository.Update(_mapper.Map<Comment>(dto));
    }
}