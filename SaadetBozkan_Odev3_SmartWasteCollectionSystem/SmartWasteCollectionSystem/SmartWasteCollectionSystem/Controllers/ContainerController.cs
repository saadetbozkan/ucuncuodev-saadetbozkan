using Data.DataModel;
using Data.Uow;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartWasteCollectionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly ILogger<ContainerController> logger;
        private readonly IMapper mapper;

        public ContainerController(ILogger<ContainerController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper; 
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var listOfContainer = unitOfWork.Container.GetAll();
            unitOfWork.Complate();

            var entities = mapper.Map<List<Container>, List<ContainerEntity>>(listOfContainer.ToList());
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var container = unitOfWork.Container.GetById(id);
            if (container is null)
            {
                return NotFound();
            }
            unitOfWork.Complate();

            var entity = mapper.Map<Container, ContainerEntity>(container);
            return Ok(entity);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ContainerEntity entity)
        {
            var container = mapper.Map<ContainerEntity, Container>(entity);

            var response = unitOfWork.Container.Add(container);

            unitOfWork.Complate();

            return CreatedAtAction("Post", response);
        }

        // Update withoud vehicleId
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] ContainerEntity entity, long id)
        {
            var container = mapper.Map<ContainerEntity, Container>(entity);

            container.Id = id;

            var response = unitOfWork.Container.Update(container);

            unitOfWork.Complate();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var response = unitOfWork.Container.Delete(id);

            unitOfWork.Complate();

            return Ok();
        }


        [HttpGet("/VehicleId")]
        public IActionResult GetByVehicleId([FromQuery] long vehicleId)
        {
            var containers = unitOfWork.Container.GetByVehicleId(vehicleId);

            unitOfWork.Complate();

            var entities = mapper.Map<List<Container>, List<ContainerEntity>>(containers.ToList());
            return Ok(entities);
        }

        [HttpGet("/GetGroupOfContainer")]
        public IActionResult GetGroupOfContainer([FromQuery] long vehicleId, [FromQuery] int n)
        {
            var containerOfList = unitOfWork.Container.GetByVehicleId(vehicleId);
            List<List<Container>> resultContainerList = new List<List<Container>>();
            int index = 0;
            for (int i = 0; i< n; i++)
            {
                
                List<Container> contains = new List<Container>();
                for(int j = 0; j< containerOfList.Count()/n; j++)
                {
                    contains.Add(containerOfList.ElementAt<Container>(index));
                    index++;
                }
                resultContainerList.Add(contains);
               
            }
            if (containerOfList.Count() % n != 0)
            {
                for (int i = 0; i < containerOfList.Count() % n; i++)
                {
                    resultContainerList.ElementAt<List<Container>>(i).Add(containerOfList.ElementAt<Container>(index));
                }
            }

            unitOfWork.Complate();
            var entities = mapper.Map< List<List<Container>>, List<List <ContainerEntity>>>(resultContainerList);
            return Ok(entities);
        }
    }
}
