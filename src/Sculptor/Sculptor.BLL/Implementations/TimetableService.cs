using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sculptor.BLL.Contracts;
using Sculptor.Common.Models.Timetable;
using Sculptor.DAL.Data;

namespace Sculptor.BLL.Implementations;

internal class TimetableService : ITimetableService
{
    private readonly IMapper mapper;
    private readonly SculptorDbContext dbContext;

    // Add dependency injections
    public TimetableService(IMapper mapper, SculptorDbContext dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }

    // Add a new item to the schedule asyncronously
    public Task AddNewItemAsync()
    {
        throw new NotImplementedException();
    }

    // Update the schedule asyncronously
    public Task<TimetableVM> EditTimetableAsync()
    {
        throw new NotImplementedException();
    }

    // Return schedule information asyncronously
    public Task<TimetableVM> ViewDailyTimetableAsync()
    {
        throw new NotImplementedException();
    }
}
