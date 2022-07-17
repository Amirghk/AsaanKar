using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Interfaces;
public interface IBidRepository
{
    Task<int> Add(Bid model);
    Task<int> Update(Bid model);
    Task<int> Remove(int id);
    Task<Bid> GetById(int id);
    Task<IEnumerable<Bid>> GetAll();
}

