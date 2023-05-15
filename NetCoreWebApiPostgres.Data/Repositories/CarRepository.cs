using Dapper;
using NetCoreWebApiPostgres.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApiPostgres.Data.Repositories
{
    public class CarRepository : ICarRepository
    {
        private PostgresSQLConfiguration _connectionString;
        public CarRepository(PostgresSQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<IEnumerable<Car>> GetAllCars()
        {
            var db = dbConnection();
            // Usar Dapper Micro ORM
            // -> agregar NUGGET - permite escribir la consulta y que luego se ejecute de forma asíncrona
            var sql = @"SELECT id, make, model, color, year, doors
                        FROM passenger_flow.cars";
            return await db.QueryAsync<Car>(sql, new { });

            //throw new NotImplementedException();
        }

        public async Task<Car> GetCarDetails(int id)
        {
            var db = dbConnection();
            // Usar Dapper Micro ORM -> agregar NUGET - permite escribir la consulta y que luego se ejecute de forma asíncrona
            var sql = @"SELECT id, make, model, color, year, doors
                        FROM passenger_flow.cars
                        WHERE id = @Id";
            return await db.QueryFirstOrDefaultAsync<Car>(sql, new { Id = id });
        }

        public async Task<bool> InsertCar(Car car)
        {
            var db = dbConnection();
            // Usar Dapper Micro ORM -> agregar NUGET - permite escribir la consulta y que luego se ejecute de forma asíncrona
            var sql = @"INSERT into passenger_flow.cars (make, model, color, year, doors )
                        VALUES ( @Make, @Model, @Color, @Year , @Doors )";
            var result = await db.ExecuteAsync(sql, new { car.Make , car.Model, car.Color, car.Year, car.Doors  });
            return result > 0;  // Devuelve True or False si hay al menos una fila está afectada.
        }

        public async Task<bool> UpdateCar(Car car)
        {
            var db = dbConnection();
            // Usar Dapper Micro ORM -> agregar NUGET - permite escribir la consulta y que luego se ejecute de forma asíncrona
            var sql = @"UPDATE passenger_flow.cars 
                        SET make  = @Make, 
                            model = @Model, 
                            color = @Color, 
                            year  = @Year ,
                            doors = @Doors 
                        WHERE id = @Id";

            var result = await db.ExecuteAsync(sql, new { car.Make, car.Model, car.Color, car.Year, car.Doors , car.Id });
            return result > 0;  // Devuelve True or False si hay al menos una fila está afectada.
        }
        public async Task<bool> DeleteCar(int id)
        {
            var db = dbConnection();
            // Usar Dapper Micro ORM -> agregar NUGET - permite escribir la consulta y que luego se ejecute de forma asíncrona
            var sql = @"DELETE FROM passenger_flow.cars 
                        WHERE id = @Id"
            ;

            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;  // Devuelve True or False si hay al menos una fila está afectada.
        }
    }
}
