using CodingTracker.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Data.Repositories;
public class GoalRepository : RepositoryGenerics
{
    public GoalRepository(ISqliteConnectionFactory connectionFactory) : base(connectionFactory) { }



}
