using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DailyProduction.Models;
using Azure.Data.Tables;
using Azure;
using System.Text.Json;

namespace IbasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DailyProductionController : ControllerBase
    {

        string connectionString = "DefaultEndpointsProtocol=https;AccountName=rskriver19;AccountKey=9W9AnopE9yHNL7wVZIJE1OKzCcTP7xywIHlWXlECweqTTp+xUUV9T3Jvbtc7sbilGcW7YFxOxtuU+AStw6L3tw==;BlobEndpoint=https://rskriver19.blob.core.windows.net/;QueueEndpoint=https://rskriver19.queue.core.windows.net/;TableEndpoint=https://rskriver19.table.core.windows.net/;FileEndpoint=https://rskriver19.file.core.windows.net/;";
        string tableName = "IBASProduktion2020";
        TableClient tableClient;



        private List<DailyProductionDTO> _productionRepo;
        private readonly ILogger<DailyProductionController> _logger;

        public DailyProductionController(ILogger<DailyProductionController> logger)
        {
            tableClient = new TableClient(connectionString, tableName);
            _logger = logger;
            //_productionRepo = new List<DailyProductionDTO>
            //{
            //    new DailyProductionDTO {Date = new DateTime(2020, 1, 31), Model = BikeModel.IBv1, ItemsProduced = 12},
            //    new DailyProductionDTO {Date = new DateTime(2020, 2, 28), Model = BikeModel.IBv1, ItemsProduced = 32},
            //    new DailyProductionDTO {Date = new DateTime(2020, 3, 31), Model = BikeModel.IBv1, ItemsProduced = 32},
            //    new DailyProductionDTO {Date = new DateTime(2020, 4, 30), Model = BikeModel.IBv1, ItemsProduced = 141},
            //    new DailyProductionDTO {Date = new DateTime(2020, 5, 31), Model = BikeModel.IBv1, ItemsProduced = 146},
            //    new DailyProductionDTO {Date = new DateTime(2020, 6, 30), Model = BikeModel.IBv1, ItemsProduced = 162},
            //    new DailyProductionDTO {Date = new DateTime(2020, 7, 31), Model = BikeModel.IBv1, ItemsProduced = 102},
            //    new DailyProductionDTO {Date = new DateTime(2020, 8, 31), Model = BikeModel.IBv1, ItemsProduced = 210},
            //    new DailyProductionDTO {Date = new DateTime(2020, 9, 30), Model = BikeModel.IBv1, ItemsProduced = 144},
            //    new DailyProductionDTO {Date = new DateTime(2020, 10, 31), Model = BikeModel.IBv1, ItemsProduced = 151},
            //    new DailyProductionDTO {Date = new DateTime(2020, 11, 30), Model = BikeModel.IBv1, ItemsProduced = 61},
            //    new DailyProductionDTO {Date = new DateTime(2020, 12, 31), Model = BikeModel.IBv1, ItemsProduced = 86},

            //    new DailyProductionDTO {Date = new DateTime(2020, 1, 31), Model = BikeModel.evIB100, ItemsProduced = 1},
            //    new DailyProductionDTO {Date = new DateTime(2020, 2, 28), Model = BikeModel.evIB100, ItemsProduced = 2},
            //    new DailyProductionDTO {Date = new DateTime(2020, 3, 31), Model = BikeModel.evIB100, ItemsProduced = 3},
            //    new DailyProductionDTO {Date = new DateTime(2020, 4, 30), Model = BikeModel.evIB100, ItemsProduced = 4},
            //    new DailyProductionDTO {Date = new DateTime(2020, 5, 31), Model = BikeModel.evIB100, ItemsProduced = 4},
            //    new DailyProductionDTO {Date = new DateTime(2020, 6, 30), Model = BikeModel.evIB100, ItemsProduced = 6},
            //    new DailyProductionDTO {Date = new DateTime(2020, 7, 31), Model = BikeModel.evIB100, ItemsProduced = 10},
            //    new DailyProductionDTO {Date = new DateTime(2020, 8, 31), Model = BikeModel.evIB100, ItemsProduced = 21},
            //    new DailyProductionDTO {Date = new DateTime(2020, 9, 30), Model = BikeModel.evIB100, ItemsProduced = 17},
            //    new DailyProductionDTO {Date = new DateTime(2020, 10, 31), Model = BikeModel.evIB100, ItemsProduced = 15},
            //    new DailyProductionDTO {Date = new DateTime(2020, 11, 30), Model = BikeModel.evIB100, ItemsProduced = 25},
            //    new DailyProductionDTO {Date = new DateTime(2020, 12, 31), Model = BikeModel.evIB100, ItemsProduced = 30},

            //    new DailyProductionDTO {Date = new DateTime(2020, 1, 31), Model = BikeModel.evIB200, ItemsProduced = 10},
            //    new DailyProductionDTO {Date = new DateTime(2020, 2, 28), Model = BikeModel.evIB200, ItemsProduced = 2},
            //    new DailyProductionDTO {Date = new DateTime(2020, 3, 31), Model = BikeModel.evIB200, ItemsProduced = 32},
            //    new DailyProductionDTO {Date = new DateTime(2020, 4, 30), Model = BikeModel.evIB200, ItemsProduced = 41},
            //    new DailyProductionDTO {Date = new DateTime(2020, 5, 31), Model = BikeModel.evIB200, ItemsProduced = 46},
            //    new DailyProductionDTO {Date = new DateTime(2020, 6, 30), Model = BikeModel.evIB200, ItemsProduced = 62},
            //    new DailyProductionDTO {Date = new DateTime(2020, 7, 31), Model = BikeModel.evIB200, ItemsProduced = 102},
            //    new DailyProductionDTO {Date = new DateTime(2020, 8, 31), Model = BikeModel.evIB200, ItemsProduced = 21},
            //    new DailyProductionDTO {Date = new DateTime(2020, 9, 30), Model = BikeModel.evIB200, ItemsProduced = 44},
            //    new DailyProductionDTO {Date = new DateTime(2020, 10, 31), Model = BikeModel.evIB200, ItemsProduced = 51},
            //    new DailyProductionDTO {Date = new DateTime(2020, 11, 30), Model = BikeModel.evIB200, ItemsProduced = 61},
            //    new DailyProductionDTO {Date = new DateTime(2020, 12, 31), Model = BikeModel.evIB200, ItemsProduced = 88}

            // };

        }

        [HttpGet]
        public async Task<IEnumerable<DailyProductionDTO>> Get()
        {
            _productionRepo = new();
            // Replace "markers" and "id-001" with your actual PartitionKey and RowKey values
            string MyPK = "markers";
            string MyRK = "id-001";

            string filter = "RowKey ne 'id-001'";
            Pageable<TableEntity> entities = tableClient.Query<TableEntity>(filter: filter);

            // Convert Azure Table entities to DailyProductionDTO objects
            foreach (TableEntity entity in entities)
            {
                
                    DailyProductionDTO production = new DailyProductionDTO
                    {

                        Date = Convert.ToDateTime(entity["RowKey"]),
                        Model = (BikeModel)Enum.Parse(typeof(BikeModel), entity.GetString("PartitionKey")),
                        ItemsProduced = (int)entity["itemsProduced"]

                    };
                    _productionRepo.Add(production);

                

            }
            return _productionRepo;
        }
    }
}
