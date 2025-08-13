using FarmManager.Persistence.DataModels.Store;

namespace FarmManager.Persistence.DataModels;

public static class MemoryStorage
{
    public static Dictionary<int, AnimalDataModel> Animals = new Dictionary<int, AnimalDataModel>
    {
        [1] = new AnimalDataModel
        {
            Id = Guid.NewGuid(),
            RegisterNumber = 1,
            Age = 2,
            Weight = 150.5m,
            Type = "Cow",
            Birthday = new DateTime(2021, 5, 20),
            CreatedBy = "System",
            CreatedAt = DateTime.UtcNow,
        },
        [2] = new AnimalDataModel
        {
            Id = Guid.NewGuid(),
            RegisterNumber = 2,
            Age = 3,
            Weight = 200.0m,
            Type = "Bull",
            Birthday = new DateTime(2020, 3, 15),
            CreatedBy = "System",
            CreatedAt = DateTime.UtcNow,
        },
        [3] = new AnimalDataModel
        {
            Id = Guid.NewGuid(),
            RegisterNumber = 3,
            Age = 1,
            Weight = 100.0m,
            Type = "Calf",
            Birthday = new DateTime(2022, 8, 10),
            CreatedBy = "System",
            CreatedAt = DateTime.UtcNow,
        }
    };
}
