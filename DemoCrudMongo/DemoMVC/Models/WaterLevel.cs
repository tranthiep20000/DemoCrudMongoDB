using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DemoMVC.Models
{
    /// <summary>
    /// Information of WaterLevel
    /// CreatedBy: ThiepTT(28/11/2022)
    /// </summary>
    public class WaterLevel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// MeasurementDate
        /// </summary>
        [NotNull]
        public DateTime MeasurementDate { get; set; }

        /// <summary>
        /// HeightWater
        /// </summary>
        [NotNull]
        public double HeightWater { get; set; }

        /// <summary>
        /// CreatedDate
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// LastUpdatedDate
        /// </summary>
        public DateTime LastUpdatedDate { get; set; }
    }
}