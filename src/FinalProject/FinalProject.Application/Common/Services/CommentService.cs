using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Dtos;
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
        return await _repository.GetAll();
    }

    public async Task<CommentDto> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(CommentDto dto)
    {
        return await _repository.Add(dto);
    }

    public async Task<int> Update(CommentDto dto)
    {
        return await _repository.Update(dto);
    }
}