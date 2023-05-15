using NetCoreWebApiPostgres.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace NetCoreWebApiPostgres.Data.Repositories
{
    public interface ICarRepository
    {
        // Métodos o contrato : la que consume y la que implementa
        Task<IEnumerable<Car>> GetAllCars();  // Asincrono
        Task<Car> GetCarDetails(int id);
        Task<bool> InsertCar(Car car);
        Task<bool> UpdateCar(Car car);
        Task<bool> DeleteCar(int id);
        //Task<bool> DeleteCarDetails(Car car);

    }
}
