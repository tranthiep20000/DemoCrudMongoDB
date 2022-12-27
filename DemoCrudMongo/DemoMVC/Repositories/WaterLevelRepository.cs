using DemoMVC.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace DemoMVC.Repositories
{
    public class WaterLevelRepository : IWaterLevelRepository
    {
        #region Feild

        private readonly DBContext _context;

        #endregion Feild

        #region Contructor

        public WaterLevelRepository(IOptions<Settings> settings)
        {
            _context = new DBContext(settings);
        }

        #endregion Contructor

        public bool Create(WaterLevel waterLevel)
        {
            try
            {
                waterLevel.CreatedDate = DateTime.UtcNow;
                waterLevel.LastUpdatedDate = DateTime.UtcNow;

                _context.WaterLevel.InsertOneAsync(waterLevel);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                DeleteResult actionResult = _context.WaterLevel.DeleteMany(waterLevel => waterLevel.Id.Equals(id));

                return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public IEnumerable<WaterLevel> GetAll()
        {
            try
            {
                IEnumerable<WaterLevel> waterLevels = _context.WaterLevel.Find(_ => true).ToList();

                foreach (var waterLevel in waterLevels)
                {
                    waterLevel.MeasurementDate = waterLevel.MeasurementDate.AddHours(7);
                }

                return waterLevels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public WaterLevel GetById(Guid id)
        {
            try
            {
                WaterLevel waterLevel = _context.WaterLevel.Find(waterLevel => waterLevel.Id == id).FirstOrDefault();

                waterLevel.MeasurementDate = waterLevel.MeasurementDate.AddHours(7);

                return waterLevel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public IEnumerable<WaterLevel> GetBySearchValue(string valueSearch)
        {
            try
            {
                var waterlevels = _context.WaterLevel.Find(_ => true).ToList();

                var waterLevelsByValueSearch = waterlevels.Where(waterLevel =>
                             waterLevel.HeightWater.ToString().ToLower().Trim().Contains(valueSearch.ToLower().Trim().ToString()))
                             .ToList();

                foreach (var waterLevel in waterLevelsByValueSearch)
                {
                    waterLevel.MeasurementDate = waterLevel.MeasurementDate.AddHours(7);
                }

                return waterLevelsByValueSearch;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public bool Update(Guid id, WaterLevel waterLevel)
        {
            try
            {
                IMongoCollection<WaterLevel> waterLevels = _context.WaterLevel;

                Expression<Func<WaterLevel, bool>> filter = x => x.Id.Equals(id);

                var waterLevelUpdate = waterLevels.Find(filter).FirstOrDefault();

                if (waterLevelUpdate != null)
                {
                    waterLevelUpdate.MeasurementDate = waterLevel.MeasurementDate;
                    waterLevelUpdate.HeightWater = waterLevel.HeightWater;
                    waterLevelUpdate.LastUpdatedDate = DateTime.UtcNow;
                    ReplaceOneResult result = waterLevels.ReplaceOne(filter, waterLevelUpdate);

                    return result.IsAcknowledged && result.ModifiedCount > 0;
                }
                else return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}