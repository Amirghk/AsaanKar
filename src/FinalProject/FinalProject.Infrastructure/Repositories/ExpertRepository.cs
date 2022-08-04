using AutoMapper;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Entities;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FinalProject.Application.Common.Interfaces.Repositories;
using FinalProject.Infrastructure.Extensions;

namespace FinalProject.Infrastructure.Repositories
{
    public class ExpertRepository : IExpertRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExpertRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> Add(ExpertDto model)
        {
            var record = _mapper.Map<Expert>(model);
            await _context.Experts.AddAsync(record);
            await _context.SaveChangesAsync();
            return record.Id!;
        }

        public async Task<IEnumerable<ExpertDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _mapper.ProjectTo<ExpertDto>(_context.Experts).ToListAsync(cancellationToken);
        }

        public async Task<ExpertDto> GetById(string id, CancellationToken cancellationToken)
        {
            var record = await _mapper.ProjectTo<ExpertDto>(_context.Experts).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (record == null)
            {
                throw new NotFoundException(nameof(Expert), id);
            }
            return record;
        }

        public async Task<string> Remove(string id)
        {
            var record = await _context.Experts.FindAsync(id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Expert), id);
            }
            _context.Remove(record);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<string> RemoveService(string id, int serviceId)
        {
            var record = await _context.Experts.Include(x => x.ServiceExperts).SingleOrDefaultAsync(x => x.Id == id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Expert), id);
            }
            var se = await _context.ServiceExperts.Where(x => x.ExpertId == id && x.ServiceId == serviceId).SingleOrDefaultAsync();
            if (se == null)
            {
                throw new NotFoundException(nameof(ServiceExpert), id);
            }
            _context.ServiceExperts.Remove(se);

            await _context.SaveChangesAsync();
            return record.Id!;
        }

        public async Task<string> SoftDelete(string id)
        {
            var record = await _context.Experts.SingleOrDefaultAsync(x => x.Id == id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Expert), id);
            }
            record.IsDeleted = true;
            record.IsActive = false;
            await _context.SaveChangesAsync();
            return record.Id!;
        }

        public async Task<string> Update(ExpertDto model)
        {
            var record = await _context.Experts.Include(x => x.ServiceExperts).SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Expert), model.Id);
            }

            //var services = _mapper.Map<List<ServiceDto>>(record.Services);


            if (model.Services.Count > record.ServiceExperts.Count)
            {
                _context.TryUpdateManyToMany(record.ServiceExperts, model.Services
               .Select(x => new ServiceExpert
               {
                   ServiceId = x.Id,
                   ExpertId = record.Id!
               }), x => x.ServiceId);

                await _context.SaveChangesAsync();
                return record.Id!;
            }
            //return record.Id!;

            //if (services.Count > model.Services.Count)
            //{
            //    var serviceExperts = new List<ServiceExpert>();
            //    foreach (var service in model.Services)
            //    {
            //        ServiceExpert? serviceExpert = await _context.ServiceExperts.Where(x => x.ExpertId == record.Id && x.ServiceId == service.Id).FirstOrDefaultAsync();
            //        if (serviceExpert == null)
            //        {
            //            serviceExpert = new ServiceExpert
            //            {

            //                ExpertId = record.Id,
            //                ServiceId = service.Id,
            //            };
            //        }
            //        serviceExperts.Add(serviceExpert);
            //    }
            //    record.ServiceExperts = serviceExperts;
            //    await _context.SaveChangesAsync();
            //}

            var mapped = _mapper.Map(model, record);
            await _context.SaveChangesAsync();

            return record.Id!;




        }
    }
}
