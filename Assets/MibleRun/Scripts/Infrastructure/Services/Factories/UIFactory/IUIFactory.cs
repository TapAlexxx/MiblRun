using Scripts.Window;
using UnityEngine;

namespace Scripts.Infrastructure.Services.Factories.UIFactory
{
  public interface IUIFactory
  {
    void CreateUiRoot();
    RectTransform CrateWindow(WindowTypeId windowTypeId);
  }
}