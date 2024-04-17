using System;
using web_app_demo.Entidades;

namespace web_app_demo.Interfaces
{
	public interface IID
	{
		Guid Value { get; }
	}

	public interface IUser: IID
	{
		User CreateUser(User user);
        Task<Result> CreateUserAsync(User user);
    }
}

