namespace VerticalSlice.Api.Features.Person
{
	public class PersonRepository : IPersonRepository
	{
		private static Dictionary<Guid, PersonEntity> PersonContext = new();

		public PersonRepository() { }

		public PersonEntity? Find(Guid id)
		{
			if (PersonContext.ContainsKey(id))
			{
				return PersonContext[id];
			}

			return null;
		}

		public void Update(PersonEntity person)
		{
			if (PersonContext.ContainsKey(person.Id))
			{
				// simulate complex update
				var entity = PersonContext[person.Id]!;

				entity.FirstName = person.FirstName;
				entity.LastName = person.LastName;

				PersonContext[person.Id] = person;
			}

			PersonContext[person.Id] = person;
		}

		public bool Delete(Guid id)
		{
			if (PersonContext.ContainsKey(id))
			{
				PersonContext.Remove(id);

				return true;
			}

			return false;
		}
	}
}
