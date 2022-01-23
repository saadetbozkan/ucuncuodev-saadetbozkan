using Data.Context;
using Data.DataModel;
using Data.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.ContainerRepo
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(CollectionSystemDbContext context, ILogger logger) : base(context, logger)
        {
        }
        public bool Delete(long id)
        {

            var listOfVehicle = dbSet.Include(v => v.Container).ToList();
            var vehicle = listOfVehicle.Where(v => v.Id == id).FirstOrDefault();

            if (vehicle == null)
                return false;

            //delete vehicle with its containers
            dbSet.Remove(vehicle);

            return true;
        }
    }
}
