
namespace VerticalSlice.Api.Features.Person
{
	public interface IPersonRepository
	{
		bool Delete(Guid id);
		PersonEntity? Find(Guid id);
		void Update(PersonEntity person);
	}
}