using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Repositories;

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

    public async Task<int> Approve(int id)
    {
        var record = await _repository.GetById(id);
        record.IsApproved = true;
        await _repository.Update(record);
        return record.Id;

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