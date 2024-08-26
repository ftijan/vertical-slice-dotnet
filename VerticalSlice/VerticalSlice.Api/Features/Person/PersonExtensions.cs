using Microsoft.AspNetCore.Mvc;

namespace VerticalSlice.Api.Features.Person
{
	public static class PersonExtensions
	{
		public static IServiceCollection AddPersonFeature(this IServiceCollection services)
		{
			services.AddTransient<IPersonRepository, PersonRepository>();

			return services;
		}

		public static IApplicationBuilder UsePersonFeature(this IApplicationBuilder app)
		{
			// Add if necessary

			return app;
		}

		public static IEndpointRouteBuilder MapPersonEndpoints(this IEndpointRouteBuilder app)
		{
			// find/get
			app.MapGet("/api/person/{id}", (
				[FromServices] IPersonRepository personRepository, 
				[FromRoute] Guid id) =>
			{
				var entity = personRepository.Find(id);

				if (entity != null) 
				{
					return Results.Ok(entity);
				}

				return Results.NotFound();
			})
			.WithName("GetPerson")
			.WithOpenApi()
			.Produces<PersonEntity>(200)
			.ProducesProblem(404);

			// create
			app.MapPost("/api/person", (
				[FromServices] IPersonRepository personRepository,
				[FromBody] PersonEntity personEntity
				) => 
			{
				try
				{
					personRepository.Update(personEntity);

					return Results.Created($"/api/person/{personEntity.Id}", personEntity);
				}
				catch (Exception)
				{
					return Results.StatusCode(500);
				}				
			})
			.WithName("UpdatePerson")
			.WithOpenApi()
			.Produces<PersonEntity>(201)
			.ProducesProblem(500);

			// delete
			app.MapDelete("/api/person/{id}", (
				[FromServices] IPersonRepository personRepository,
				[FromRoute] Guid id
				) =>
				{
					if (personRepository.Delete(id))
					{
						return Results.NoContent();
					}

					return Results.NotFound();
				})
			.WithName("DeletePerson")
			.WithOpenApi()
			.Produces(204)
			.ProducesProblem(404);

			return app;
		}
	}
}
