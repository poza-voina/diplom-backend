namespace Core.Interfaces.Services;

public interface ICrudService<TInput, TResult> where TInput : class where TResult : class
{
	Task<TResult> CreateAsync(TInput entity);
	Task<TResult> UpdateAsync(TInput entity);
	Task DeleteAsync(TInput entity);
}

public interface ICrudServiceById<TResult> where TResult : class
{
	Task DeleteAsync(long id);
	Task<TResult> GetAsync(long id);
}