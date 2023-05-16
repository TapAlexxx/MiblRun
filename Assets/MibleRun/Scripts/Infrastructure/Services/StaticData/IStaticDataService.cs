using Scripts.StaticData;
using Scripts.StaticData.Level;
using Scripts.StaticData.Window;
using Scripts.Window;

namespace Scripts.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        void LoadData();
        GameStaticData GameConfig();
        WindowConfig ForWindow(WindowTypeId windowTypeId);
        LevelStaticData GetLevelStaticData();
    }
}