using web_app_demo.Entidades;
using web_app_demo.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using static System.Net.Mime.MediaTypeNames;

namespace web_app_demo.Service
{
    public class UserService : IUser
    {
        internal IUserContext _context;

        public UserService(IUserContext context)
        {
            _context = context;
        }
        public Guid Value { get; private set; } = Guid.NewGuid();

        public User CreateUser(User user)
        {

            _context.Test.Add(user);
            _context.SaveChanges();

            return user;
        }

        public async Task<Result> CreateUserAsync(User user)
        {
            try
            {
                using MySqlConnection conn = new MySqlConnection(_context.Database.GetConnectionString());
                conn.Open();
                using MySqlTransaction transaction = conn.BeginTransaction();

                using MySqlCommand cmd = new MySqlCommand("CREARUSUARIO", conn, transaction);
                cmd.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@correo", user.Correo);
                var nombreParam = new MySqlParameter("@nombre", user.Nombre);
                var typeParam = new MySqlParameter("@apellido", user.Apellido);
                var uniqueIdParam = new MySqlParameter("@direccion", user.Direccion);

                var loggingParam = new MySqlParameter("@logging", MySqlDbType.VarChar);

                cmd.Parameters.Add(idParam);
                cmd.Parameters.Add(nombreParam);
                cmd.Parameters.Add(typeParam);
                cmd.Parameters.Add(uniqueIdParam);
                cmd.Parameters.Add(loggingParam);
                cmd.Parameters["@logging"].Direction = ParameterDirection.Output;
                var result = cmd.ExecuteNonQuery();

                transaction.Commit();

                object logging = cmd.Parameters["@logging"].Value;

                return Result.Success(user,logging);
            }catch(Exception ex)
            {
                return Result.Failure(new Error("", ex.Message));
            }
        }

        
    }
}

