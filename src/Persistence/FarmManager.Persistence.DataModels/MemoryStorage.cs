using FarmManager.Persistence.DataModels.Store;

namespace FarmManager.Persistence.DataModels;

public static class MemoryStorage
{
    public static Dictionary<int, AnimalDataModel> Animals = new Dictionary<int, AnimalDataModel>
    {
        [1] = new CowDataModel
        {
            Id = Guid.NewGuid(),
            RegisterNumber = 6,
            Weight = 680.0m,
            Birthday = new DateTime(2022, 6, 25),
            Type = "Cow",
            IsPregnant = false,
            Age = 3,
            HasCalf = true,
            Name = "Estrela",
            IsMilking = false,
            CreatedBy = "System",
            CreatedAt = DateTime.UtcNow
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
