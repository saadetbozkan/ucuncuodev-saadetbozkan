using Data.Context;
using Data.DataModel;
using Data.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ContainerRepo
{
    public class ContainerRepository : GenericRepository<Container>, IContainerRepository
    {
        private IVehicleRepository vehicleRepo;
        public ContainerRepository(CollectionSystemDbContext context, ILogger logger) : base(context, logger)
        {
            vehicleRepo = new VehicleRepository(context, logger);
        }
        public bool Update(Container entity)
        {
            var id = entity.Id;
           
            var container = GetById(id);
            container.Longitude = entity.Longitude == 0 ? container.Longitude : entity.Longitude;
            container.Latitude = entity.Latitude == 0 ? container.Latitude: entity.Latitude;
            container.ContainerName = entity.ContainerName == null ? container.ContainerName : entity.ContainerName;

            dbSet.Update(container);
            return true;
        }
        public IEnumerable<Container> GetByVehicleId(long id)
        {
            var listOfContainer = dbSet.Where(s => s.VehicleId == id);
            return listOfContainer;
        }
        public bool Add(Container entity)
        {
            var vehicle = vehicleRepo.GetById(entity.VehicleId);
            if (vehicle == null)
                return false;
            dbSet.Add(entity);
            return true;
        }
        public bool DeleteRange(IEnumerable<Container> container)
        {
            if (container == null)
                return false;

            dbSet.RemoveRange(container);
            return true;
        }
    }
}
